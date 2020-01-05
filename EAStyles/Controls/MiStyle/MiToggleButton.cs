using EAStyles.Utilitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace EAStyles.Controls.MiStyle
{
    public class MiToggleButton : ToggleButton
    {
        public static readonly DependencyProperty TextHorizontalAlignmentProperty = ElementBase.Property<MiToggleButton, HorizontalAlignment>("TextHorizontalAlignmentProperty", HorizontalAlignment.Left);
        public static readonly DependencyProperty CornerRadiusProperty = ElementBase.Property<MiToggleButton, CornerRadius>("CornerRadiusProperty", new CornerRadius(10));
        public static readonly DependencyProperty BorderCornerRadiusProperty = ElementBase.Property<MiToggleButton, CornerRadius>("BorderCornerRadiusProperty", new CornerRadius(12));

        public HorizontalAlignment TextHorizontalAlignment { get { return (HorizontalAlignment)GetValue(TextHorizontalAlignmentProperty); } set { SetValue(TextHorizontalAlignmentProperty, value); } }
        public CornerRadius CornerRadius { get { return (CornerRadius)GetValue(CornerRadiusProperty); } set { SetValue(CornerRadiusProperty, value); } }
        public CornerRadius BorderCornerRadius { get { return (CornerRadius)GetValue(BorderCornerRadiusProperty); } set { SetValue(BorderCornerRadiusProperty, value); } }

        public MiToggleButton()
        {
            EAStyles.Controls.ControlUtility.Refresh(this);
            Loaded += delegate { ElementBase.GoToState(this, (bool)IsChecked ? "OpenLoaded" : "CloseLoaded"); };
        }

        protected override void OnChecked(RoutedEventArgs e)
        {
            base.OnChecked(e);
            ElementBase.GoToState(this, "Open");
        }

        protected override void OnUnchecked(RoutedEventArgs e)
        {
            base.OnChecked(e);
            ElementBase.GoToState(this, "Close");
        }

        static MiToggleButton()
        {
            ElementBase.DefaultStyle<MiToggleButton>(DefaultStyleKeyProperty);
        }
    }
}
