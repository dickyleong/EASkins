using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MetroUiToolkitDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<DataGridItem> dataGridItems;
        public MainWindow()
        {
            InitializeComponent();
            dataGridItems = new ObservableCollection<DataGridItem>();
            for(int i=0;i<20;++i)
            {
                DataGridItem item = new DataGridItem();
                item.ItemGuid = i.ToString();
                item.ItemStatus = false;
                item.ItemName = "text" + i;
                dataGridItems.Add(item);
            }
            this.dgView.DataContext = dataGridItems;
        }
        
        public class DataGridItem
        {
            public string ItemName { get; set; }
            public bool ItemStatus { get; set; }
            public string ItemGuid { get; set; }
        }
    }
}
