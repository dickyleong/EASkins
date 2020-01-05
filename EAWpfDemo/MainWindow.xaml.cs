using EAStyles.Controls.MiStyle;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace EAWpfDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MiWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            InitDataGrid();
        }

        private void InitDataGrid()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]{ new DataColumn("test1"),new DataColumn("test2"),new DataColumn("test3")});
            dt.Rows.Add(new object[3]{"1","3","2" });
            dt.Rows.Add(new object[3] { "4", "3", "2" });
            dt.Rows.Add(new object[3] { "7", "3", "6" });
            dt.Rows.Add(new object[3] { "1", "9", "2" });
            miDataGrid.DataContext = dt.DefaultView;
            miEDataGrid.DataContext = dt.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void menuclick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("test");
        }

        private void btnStyle_Click(object sender, RoutedEventArgs e)
        {
            Style style = new Style();
            style.ShowInTaskbar = false;
            style.Owner = this;
            style.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            style.ShowDialog();
        }

        private void MiTextLink_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("点击了MiTextLink");
        }
    }
}
