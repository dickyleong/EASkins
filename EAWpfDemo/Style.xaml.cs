using EAStyles.Controls.MiStyle;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
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
using System.Windows.Shapes;
using System.Xml;

namespace EAWpfDemo
{
    /// <summary>
    /// Setup.xaml 的交互逻辑
    /// </summary>
    public partial class Style : MiWindow
    {
        public Style()
        {
            InitializeComponent();            
        }

        #region 点击事件
        /*******************************************
        [LastModifiedTime]:2016.1.21
        [ModifiedContents]:点击的时候即保存配置
        /******************************************/
        private void Rect_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MiWindow win = this.Owner as MiWindow;
                if (win != null)
                {
                    win.BorderBrush = (sender as Rectangle).Fill;
                    this.BorderBrush = win.BorderBrush;
                }
            }catch(Exception ex)
            {
                Debug.Write(ex.StackTrace);
            }
        }
        #endregion
    }
}