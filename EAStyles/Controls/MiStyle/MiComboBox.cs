using EAStyles.Utilitys;
using System.Windows.Controls;

namespace EAStyles.Controls.MiStyle
{
    public class MiComboBox : ComboBox
    {
        public MiComboBox()
        {
            ControlUtility.Refresh(this);
        }

        static MiComboBox()
        {
            ElementBase.DefaultStyle<MiComboBox>(DefaultStyleKeyProperty);
        }
    }
}
