using EAStyles.Utilitys;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EAStyles.Controls.MiStyle
{
    public class MiDataGrid : DataGrid
    {
        public MiDataGrid()
        {
            ControlUtility.Refresh(this);
        }
        static MiDataGrid()
        {
            ElementBase.DefaultStyle<MiDataGrid>(DefaultStyleKeyProperty);
        }
    }
}
