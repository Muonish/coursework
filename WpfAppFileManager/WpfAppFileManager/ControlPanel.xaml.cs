using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;


namespace WpfAppFileManager
{
    /// <summary>
    /// Interaction logic for ControlPanel.xaml
    /// </summary>
    public partial class ControlPanel : UserControl
    {
        public delegate void OperateInFolder(DirectoryInfo directory, string name);

        public delegate void OperateOnDirectory(DirectoryInfo directory);

        public delegate void OperateOnGui();

        public delegate void OperateOnThisPanel(ControlPanel sender);

        public const string InitialDirectory = @"C:\";
        private DirectoryInfo _curDir;
        private string _curPath;
        private List<ManagerItem> directoryContent;
        public List<ManagerItem> DirectoryContent
        {
            get { return directoryContent; }
            set
            {
                directoryContent = value;
                contentGrid.ItemsSource = directoryContent;
            }
        }

        public object SelectedItem
        {
            get { return contentGrid.SelectedItem; }
        }

        public string CurrentDirectory
        {
            get { return _curDir.FullName; }
            set
            {
                if (_curDir != null &&  !Directory.Exists(value)) //if no Dir 
                    return;

                _curDir = new DirectoryInfo(value);

                contentGrid.ItemsSource = null;

                DirectoryContent = new List<ManagerItem>();

                foreach (string path in Directory.GetDirectories(value))
                // Cut the string to only show names without path for Dirs
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(path);
                    DirectoryContent.Add(new ManagerItem(true, dirInfo.Name, 0, dirInfo.CreationTime));
                }

                foreach (string path in Directory.GetFiles(value))
                // Cut the string to only show names without path for Files
                {
                    FileInfo fileInfo = new FileInfo(path);
                    DirectoryContent.Add(new ManagerItem(false, fileInfo.Name, fileInfo.Length, fileInfo.CreationTime));
                }

                //contentGrid.ItemsSource = DirectoryContent;

                textBoxPath.Text = _curDir.ToString();
                _curPath = value;

                string curDrive = Path.GetPathRoot(_curPath);

                foreach (object drive in comboBoxDrives.Items)
                {
                    if (drive.ToString().ToUpper() == curDrive.ToUpper())
                        comboBoxDrives.SelectedItem = drive;
                }
            }
        }

        public static event OperateInFolder OnDeleteFromFolderClicked;
        public static event OperateOnGui OnRefreshListClicked;
        public static event OperateOnThisPanel OnCopyFileClicked;
        public static event OperateOnThisPanel OnMoveFileClicked;
        public static event OperateOnDirectory OnDirectoryCreateClicked;
        public static event OperateOnThisPanel OnItemDoubleClicked;
        public static event OperateOnThisPanel OnSearchButtonClicked;
        public static event OperateOnGui OnCompareClicked;

        public ControlPanel()
        {
            InitializeComponent();
            CurrentDirectory = InitialDirectory;
            comboBoxDrives.ItemsSource = DriveInfo.GetDrives();
            comboBoxDrives.SelectedItem = comboBoxDrives.Items[0];
        }

        public void RefreshList()
        {
            string dir = CurrentDirectory;
            CurrentDirectory = InitialDirectory;
            CurrentDirectory = dir;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CurrentDirectory = InitialDirectory;
        }

        private void buttonUp_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.GetParent(CurrentDirectory) != null)
                CurrentDirectory = Directory.GetParent(CurrentDirectory).ToString();
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            if (OnSearchButtonClicked != null)
                OnSearchButtonClicked(this);
        }

        private void contentGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (OnItemDoubleClicked != null)
                OnItemDoubleClicked(this);
        }

        private void buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
            if (OnRefreshListClicked != null)
                OnRefreshListClicked();
        }

        private void buttonRoot_Click(object sender, RoutedEventArgs e)
        {
            CurrentDirectory = Path.GetPathRoot(CurrentDirectory);
        }

        private void buttonCopy_Click(object sender, RoutedEventArgs e)
        {
            if (OnCopyFileClicked != null)
                OnCopyFileClicked(this);
        }

        private void buttonMove_Click(object sender, RoutedEventArgs e)
        {
            if (OnMoveFileClicked != null)
                OnMoveFileClicked(this);
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (OnDeleteFromFolderClicked != null)
                OnDeleteFromFolderClicked(new DirectoryInfo(CurrentDirectory), SelectedItem.ToString());
        }

        private void buttonNewFolder_Click(object sender, RoutedEventArgs e)
        {
            OnDirectoryCreateClicked(new DirectoryInfo(CurrentDirectory));
        }

        private void comboBoxDrives_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedDrive = (DriveInfo)comboBoxDrives.SelectedItem;
            if (selectedDrive.IsReady)
            {
                CurrentDirectory = comboBoxDrives.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("This drive isn't ready!", "Drive not ready", MessageBoxButton.OK, MessageBoxImage.Error);
                string curDrive = Path.GetPathRoot(CurrentDirectory);
                foreach (object currentDrive in comboBoxDrives.Items)
                {
                    if (currentDrive.ToString().ToUpper() == curDrive.ToUpper())
                        comboBoxDrives.SelectedItem = currentDrive;
                }
            }
        }

        private void buttonCompare_Click(object sender, RoutedEventArgs e)
        {
            if (OnCompareClicked != null)
                OnCompareClicked();
        }     
    }
}
