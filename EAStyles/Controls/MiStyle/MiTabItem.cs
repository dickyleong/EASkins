using EAStyles.Utilitys;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EAStyles.Controls.MiStyle
{
    public class MiTabItem : TabItem
    {
        public static readonly DependencyProperty IdxHeaderProperty = ElementBase.Property<MiTabItem, string>("IdxHeaderProperty", null);
        public static readonly DependencyProperty TextHorizontalAlignmentProperty = ElementBase.Property<MiTabItem, HorizontalAlignment>("TextHorizontalAlignmentProperty", HorizontalAlignment.Right);

        public string IdxHeader
        {
            get
            {
                return (string)GetValue(IdxHeaderProperty);
            }
            set
            {
                SetValue(IdxHeaderProperty, value);
            }
        }
       
        public HorizontalAlignment TextHorizontalAlignment { get { return (HorizontalAlignment)GetValue(TextHorizontalAlignmentProperty); } set { SetValue(TextHorizontalAlignmentProperty, value); } }

        static MiTabItem()
        {
            ElementBase.DefaultStyle<MiTabItem>(DefaultStyleKeyProperty);
        }
    }
}