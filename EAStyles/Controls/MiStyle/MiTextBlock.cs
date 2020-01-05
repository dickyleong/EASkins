using EAStyles.Utilitys;
using System.Windows.Controls;

namespace EAStyles.Controls.MiStyle
{
    public class MiTextBlock: TextBlock
    {
        static MiTextBlock()
        {
            ElementBase.DefaultStyle<MiTextBlock>(DefaultStyleKeyProperty);
        }
    }
}
