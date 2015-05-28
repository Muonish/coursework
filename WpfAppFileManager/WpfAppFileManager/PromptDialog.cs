using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppFileManager
{
    class PromptDialog
    {
        public static string ShowDialog(string text, string caption)
        {
            var prompt = new Window { Width = 300, Height = 120, Title = caption };
            var stackPanel = new StackPanel { Orientation = Orientation.Vertical, 
                                              Background = (Brush)new BrushConverter().ConvertFrom("#FFF0F0F0") };
            stackPanel.Children.Add(new Label { Content = text, HorizontalAlignment = HorizontalAlignment.Center });
            var textBox = new TextBox { Width = 200 };
            stackPanel.Children.Add(textBox);
            var stPan = new StackPanel { Orientation = Orientation.Horizontal, 
                                         HorizontalAlignment = HorizontalAlignment.Center };
            var confirmation = new Button { Content = "Ok", Width = 70, Margin = new Thickness(10, 10, 10, 10) };
            var cancel = new Button { Content = "Cancel", Width = 70, Margin = new Thickness(10, 10, 10, 10) };
            stPan.Children.Add(confirmation);
            stPan.Children.Add(cancel);
            stackPanel.Children.Add(stPan);
            prompt.Content = stackPanel;
            bool cancelClick = false;
            
            confirmation.Click += (sender, e) => prompt.Close();
            cancel.Click += (sender, e) => { cancelClick = true; prompt.Close(); };
            prompt.ShowDialog();

            if (cancelClick)
                return "";

            return textBox.Text;
        }
    }
}
