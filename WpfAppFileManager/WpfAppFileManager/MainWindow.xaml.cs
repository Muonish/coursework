using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppFileManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ControlPanel ControlPanelLeft { set; get; }

        public ControlPanel ControlPanelRight { set; get; }

        public MainWindow()
        {
            InitializeComponent();
            ControlPanelLeft = new ControlPanel();
            ControlPanelRight = new ControlPanel();

            mainGrid.Children.Add(ControlPanelLeft);
            mainGrid.Children.Add(ControlPanelRight);
            ControlPanelLeft.SetValue(Grid.ColumnProperty, 0);
            ControlPanelLeft.SetValue(Grid.RowProperty, 1);
            ControlPanelRight.SetValue(Grid.ColumnProperty, 1);
            ControlPanelRight.SetValue(Grid.RowProperty, 1);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ControlPanelLeft.contentGrid.Height = this.Height - 120;
            ControlPanelRight.contentGrid.Height = this.Height - 120;
        }
    }
}
