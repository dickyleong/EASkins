using EAStyles.Utilitys;
using System.Windows.Controls;

namespace EAStyles.Controls.MiStyle
{
    public class MiCheckBox:CheckBox
    {
        public MiCheckBox()
        {
            ControlUtility.Refresh(this);
        }
        static MiCheckBox()
        {
            ElementBase.DefaultStyle<MiCheckBox>(DefaultStyleKeyProperty);
        }
    }
}
