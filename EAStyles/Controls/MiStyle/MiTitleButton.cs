using EAStyles.Utilitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace EAStyles.Controls.MiStyle
{    

    public class MiTitleButton : ButtonBase
    {
        public static readonly DependencyProperty MiTitleButtonStyleProperty = ElementBase.Property<MiTitleButton, TitleButtonStyle>("MiTitleButtonStyleProperty", TitleButtonStyle.None);

        public TitleButtonStyle MiTitleButtonStyle { get { return (TitleButtonStyle)GetValue(MiTitleButtonStyleProperty); } set { SetValue(MiTitleButtonStyleProperty, value); } }

        public MiTitleButton()
        {
            ControlUtility.Refresh(this);
        }

        static MiTitleButton()
        {
            ElementBase.DefaultStyle<MiTitleButton>(DefaultStyleKeyProperty);
        }

        public enum TitleButtonStyle
        {
            None,
            StyleOne,
            StyleTwo
        }
    }
}
