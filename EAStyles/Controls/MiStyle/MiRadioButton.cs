using System;
using System.Windows;
using System.Windows.Controls;

namespace EAStyles.Controls.MiStyle
{
    public class MiRadioButton : RadioButton
    {
        public MiRadioButton()
        {
            ResourceDictionary styleRes = new ResourceDictionary();
            styleRes.Source = new Uri("/EAStyles;component/Themes/MiStyle/MiRadioButton.xaml",
                    UriKind.RelativeOrAbsolute);
            Style radioButtonStyle = styleRes["miRadioButton"] as Style;
            this.SetValue(MiRadioButton.StyleProperty, radioButtonStyle);
            ControlUtility.Refresh(this);
        }
    }
}
