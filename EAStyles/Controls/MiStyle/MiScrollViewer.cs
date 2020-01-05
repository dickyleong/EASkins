using EAStyles.Utilitys;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EAStyles.Controls.MiStyle
{
    public class MiScrollViewer : ScrollViewer
    {
        public static readonly DependencyProperty FloatProperty = ElementBase.Property<MiScrollViewer, bool>("FloatProperty");
        public static readonly DependencyProperty AutoLimitMouseProperty = ElementBase.Property<MiScrollViewer, bool>("AutoLimitMouseProperty");
        public static readonly DependencyProperty VerticalMarginProperty = ElementBase.Property<MiScrollViewer, Thickness>("VerticalMarginProperty");
        public static readonly DependencyProperty HorizontalMarginProperty = ElementBase.Property<MiScrollViewer, Thickness>("HorizontalMarginProperty");

        public bool Float { get { return (bool)GetValue(FloatProperty); } set { SetValue(FloatProperty, value); } }
        public bool AutoLimitMouse { get { return (bool)GetValue(AutoLimitMouseProperty); } set { SetValue(AutoLimitMouseProperty, value); } }
        public Thickness VerticalMargin { get { return (Thickness)GetValue(VerticalMarginProperty); } set { SetValue(VerticalMarginProperty, value); } }
        public Thickness HorizontalMargin { get { return (Thickness)GetValue(HorizontalMarginProperty); } set { SetValue(HorizontalMarginProperty, value); } }

        public MiScrollViewer()
        {
            Controls.ControlUtility.Refresh(this);
        }

        static MiScrollViewer()
        {
            ElementBase.DefaultStyle<MiScrollViewer>(DefaultStyleKeyProperty);
        }
    }
}