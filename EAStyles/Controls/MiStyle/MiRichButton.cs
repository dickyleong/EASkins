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
    

    public class MiRichButton : Button
    {
        public static readonly DependencyProperty IsButtonBusyProperty = ElementBase.Property<MiRichButton, bool>("IsButtonBusyProperty", false);
        public bool IsButtonBusy { get { return (bool)GetValue(IsButtonBusyProperty); } set { SetValue(IsButtonBusyProperty, value); } }

        public MiRichButton()
        {
            EAStyles.Controls.ControlUtility.Refresh(this);
        }

        static MiRichButton()
        {
            ElementBase.DefaultStyle<MiRichButton>(DefaultStyleKeyProperty);
        }
    }
}
