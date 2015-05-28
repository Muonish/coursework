using System;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace WpfAppFileManager
{
    internal class Controller
    {
        private readonly Operation model;
        private readonly MainWindow view;

        public Controller()
        {
            view = new MainWindow();
            model = new Operation();
            Subscribe();
            view.Show();
        }


        private void Subscribe()
        {
            ControlPanel.OnDeleteFromFolderClicked += OnDeleteItemClickedListner;
            ControlPanel.OnRefreshListClicked += OnRefreshListsClickedListner;
            ControlPanel.OnCopyFileClicked += OnCopyFileClickedListner;
            ControlPanel.OnMoveFileClicked += OnMoveFileClickedListner;
            ControlPanel.OnDirectoryCreateClicked += OnDirectoryCreateListner;
            ControlPanel.OnItemDoubleClicked += OnItemDoubleClickedListner;
            ControlPanel.OnSearchButtonClicked += OnSearchButtonClickedListner;
            ControlPanel.OnCompareClicked += OnCompareClickedListner;
        }


        private void OnItemDoubleClickedListner(ControlPanel source)
        {
            string selectedItemPath = Path.Combine(source.CurrentDirectory, source.SelectedItem.ToString());

            if (Directory.Exists(selectedItemPath) && model.IsDirectoryAccessable(selectedItemPath))
            {
                source.DirectoryContent.Clear();
                source.CurrentDirectory = selectedItemPath;
            }
            else if (File.Exists(selectedItemPath))
            {
                try
                {
                    model.Execute(new FileInfo(selectedItemPath));
                }
                catch (Win32Exception exception)
                {
                    MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else if (model.IsDirectoryAccessable(selectedItemPath) == false)
            {
                MessageBox.Show("Sorry, you don't have permission to access this folder.", "Error", MessageBoxButton.OK,
                                MessageBoxImage.Warning);
            }
        }

        private void OnSearchButtonClickedListner(ControlPanel sourcePanel)
        {
            string searchValue = PromptDialog.ShowDialog("Enter search term:", "Search");

            if (!String.IsNullOrEmpty(searchValue))
            {
                sourcePanel.DirectoryContent.Clear();
                sourcePanel.DirectoryContent = model.Search(searchValue, new DirectoryInfo(sourcePanel.CurrentDirectory));
            }
        }

        private void OnMoveFileClickedListner(ControlPanel source)
        {
            ControlPanel target = (source == view.ControlPanelLeft) ? view.ControlPanelRight : view.ControlPanelLeft;

            var sourceFile = new FileInfo(Path.Combine(source.CurrentDirectory, source.SelectedItem.ToString()));

            var destinationFolder = new DirectoryInfo(target.CurrentDirectory);

            if (File.Exists(sourceFile.FullName))
            {
                if (!File.Exists(Path.Combine(destinationFolder.FullName, sourceFile.Name)))
                {
                    model.MoveFileToDirectory(sourceFile, destinationFolder);
                    OnRefreshListsClickedListner();
                }
                else
                {
                    MessageBox.Show("This file already exists in the destination folder!", "Cannot Move",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void OnDirectoryCreateListner(DirectoryInfo CurrentDirectory)
        {
            string newDirValue = PromptDialog.ShowDialog("Enter name:", "New Folder");

            if (newDirValue == "")
                return;

            if (isLegalName(newDirValue))
            {
                if (!(Directory.Exists(Path.Combine(CurrentDirectory.ToString(), newDirValue))))
                {
                    model.CreateDirectory(Path.Combine(CurrentDirectory.ToString(), newDirValue));
                    OnRefreshListsClickedListner();
                }
                else
                {
                    MessageBox.Show("This folder already exists!");
                }
            }
            else
                MessageBox.Show("Illegal folder name!");
        }

        private bool isLegalName(string name)
        {
            char[] invalid = Path.GetInvalidFileNameChars();

            for (int i = 0; i < name.Length; i++)
            {
                if (name.Contains(invalid[i].ToString()))
                {
                    return false;
                }
            }
            return true;
        }

        private void OnCopyFileClickedListner(ControlPanel source)
        {
            ControlPanel target = (source == view.ControlPanelLeft) ? view.ControlPanelRight : view.ControlPanelLeft;
            if (source.SelectedItem != null)
            {
                var sourceFile = new FileInfo(Path.Combine(source.CurrentDirectory, source.SelectedItem.ToString()));

                var destinationFolder = new DirectoryInfo(target.CurrentDirectory);

                if (File.Exists(sourceFile.FullName))
                {
                    if (!File.Exists(Path.Combine(destinationFolder.FullName, sourceFile.Name)))
                    {
                        model.CopyFileToDirectory(sourceFile, destinationFolder);
                        OnRefreshListsClickedListner();
                    }
                    else
                    {
                        MessageBox.Show("This file already exists in the destination folder!", "Cannot Copy",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
                MessageBox.Show("No file selected", "Cannot Copy",
                    MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void OnRefreshListsClickedListner()
        {
            view.ControlPanelLeft.RefreshList();
            view.ControlPanelRight.RefreshList();
        }

        private void OnDeleteItemClickedListner(DirectoryInfo containingDirectory, string itemToRemove)
        {
            if (File.Exists(Path.Combine(containingDirectory.ToString(), itemToRemove)))
            {
                if (MessageBox.Show("Are You sure you want to delete the file " + itemToRemove + "?", "Confirm delete",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    model.DeleteFile(containingDirectory, itemToRemove);
                    OnRefreshListsClickedListner();
                }
            }
            else if (Directory.Exists(Path.Combine(containingDirectory.ToString(), itemToRemove)))
            {
                if (MessageBox.Show(
                        "Are You sure you want to delete the folder " + itemToRemove + " \nand all of its contents?",
                        "Confirm delete", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    model.DeleteDirectory(containingDirectory, itemToRemove);
                    OnRefreshListsClickedListner();
                }
            }
        }

        public void OnCompareClickedListner()
        {
            if (view.ControlPanelLeft.SelectedItem != null && view.ControlPanelRight.SelectedItem != null)
            {
                string path1 = Path.Combine(view.ControlPanelLeft.CurrentDirectory,
                    view.ControlPanelLeft.SelectedItem.ToString());
                string path2 = Path.Combine(view.ControlPanelRight.CurrentDirectory,
                    view.ControlPanelRight.SelectedItem.ToString());


                if (File.Exists(path1) && File.Exists(path2))
                {
                    if (model.IsFileContentEqual(new FileInfo(path1), new FileInfo(path2)))
                    {
                        MessageBox.Show("The files content is equal.", "Content Comparison", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("The files content is different.", "Content Comparison", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    }
                }
                else
                {
                    CompareDirectory();
                }
            }
        }

        public void CompareDirectory(){
            string dir1 = Path.Combine(view.ControlPanelLeft.CurrentDirectory, view.ControlPanelLeft.SelectedItem.ToString());
            string dir2 = Path.Combine(view.ControlPanelRight.CurrentDirectory, view.ControlPanelRight.SelectedItem.ToString());


            if (view.ControlPanelLeft.SelectedItem != null && view.ControlPanelRight.SelectedItem != null
                && Directory.Exists(dir1) && Directory.Exists(dir2))

                if (model.AreDirectoriesEqual(new DirectoryInfo(dir1), new DirectoryInfo(dir2)))
                {
                    MessageBox.Show("Folders are equal!", "Result", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else
                {
                    MessageBox.Show("Folders are NOT equal!", "Result", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            else
            {
                MessageBox.Show("Selections aren't the same! Choose folder and folder or file and file.", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
