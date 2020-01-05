using EAStyles.Utilitys;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EAStyles.Controls.MiStyle
{
    public class MiTabControl : TabControl
    {
        public static readonly DependencyProperty TabPanelVerticalAlignmentProperty = ElementBase.Property<MiTabControl, VerticalAlignment>("TabPanelVerticalAlignmentProperty", VerticalAlignment.Top);
        public static readonly DependencyProperty OffsetProperty = ElementBase.Property<MiTabControl, Thickness>("OffsetProperty", new Thickness(0));
        public static readonly DependencyProperty IconModeProperty = ElementBase.Property<MiTabControl, bool>("IconModeProperty", false);

        public static RoutedUICommand IconModeClickCommand = ElementBase.Command<MiTabControl>("IconModeClickCommand");

        public VerticalAlignment TabPanelVerticalAlignment { get { return (VerticalAlignment)GetValue(TabPanelVerticalAlignmentProperty); } set { SetValue(TabPanelVerticalAlignmentProperty, value); } }
        public Thickness Offset { get { return (Thickness)GetValue(OffsetProperty); } set { SetValue(OffsetProperty, value); } }
        public bool IconMode { get { return (bool)GetValue(IconModeProperty); } set { SetValue(IconModeProperty, value); GoToState(); } }

        void GoToState()
        {
            ElementBase.GoToState(this, IconMode ? "EnterIconMode" : "ExitIconMode");
        }

        void SelectionState()
        {
            if (IconMode)
            {
                ElementBase.GoToState(this, "SelectionStartIconMode");
                ElementBase.GoToState(this, "SelectionEndIconMode");
            }
            else
            {
                ElementBase.GoToState(this, "SelectionStart");
                ElementBase.GoToState(this, "SelectionEnd");
            }
        }

        public MiTabControl()
        {
            Loaded += delegate { GoToState(); ElementBase.GoToState(this, IconMode ? "SelectionLoadedIconMode" : "SelectionLoaded"); };
            SelectionChanged += delegate (object sender, SelectionChangedEventArgs e) { if (e.Source is MiTabControl) { SelectionState(); } };
            CommandBindings.Add(new CommandBinding(IconModeClickCommand, delegate { IconMode = !IconMode; GoToState();}));

            ControlUtility.Refresh(this);
        }

        static MiTabControl()
        {
            ElementBase.DefaultStyle<MiTabControl>(DefaultStyleKeyProperty);
        }
    }
}