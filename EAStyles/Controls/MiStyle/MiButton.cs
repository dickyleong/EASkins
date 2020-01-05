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
    

    public class MiButton : Button
    {
        public static readonly DependencyProperty MiButtonStyleProperty = ElementBase.Property<MiButton, ButtonStyle>("MiButtonStyleProperty", ButtonStyle.None);

        public ButtonStyle MiButtonStyle { get { return (ButtonStyle)GetValue(MiButtonStyleProperty); } set { SetValue(MiButtonStyleProperty, value); } }        

        public MiButton()
        {
            EAStyles.Controls.ControlUtility.Refresh(this);
        }

        static MiButton()
        {
            ElementBase.DefaultStyle<MiButton>(DefaultStyleKeyProperty);
        }

        public enum ButtonStyle
        {
            None,
            StyleOne,
            StyleTwo
        }
    }
}
