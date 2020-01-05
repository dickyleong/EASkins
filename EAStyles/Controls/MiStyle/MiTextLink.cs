using EAStyles.Utilitys;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace EAStyles.Controls.MiStyle
{
    public class MiTextLink : UserControl
    {        
        public MiTextLink()
        {
            ResourceDictionary styleRes = new ResourceDictionary();
            styleRes.Source = new Uri("/EAStyles;component/Themes/MiStyle/MiTextLink.xaml",
                    UriKind.RelativeOrAbsolute);
            Style textLinkStyle = styleRes["miTextLink"] as Style;
            this.SetValue(MiTextLink.StyleProperty, textLinkStyle);
            ControlUtility.Refresh(this);
        }
    }
}
