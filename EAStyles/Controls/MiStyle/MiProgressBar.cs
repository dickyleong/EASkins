using EAStyles.Utilitys;
using System.Windows;
using System.Windows.Controls;

namespace EAStyles.Controls.MiStyle
{
    public enum ProgressBarState
    {
        None,
        Error,
        Wait
    }

    public class MiProgressBar : ProgressBar
    {
        public static readonly DependencyProperty ProgressBarStateProperty = ElementBase.Property<MiProgressBar, ProgressBarState>("ProgressBarStateProperty");
        public static readonly DependencyProperty CornerRadiusProperty = ElementBase.Property<MiProgressBar, CornerRadius>("CornerRadiusProperty");
        public static readonly DependencyProperty TitleProperty = ElementBase.Property<MiProgressBar, string>("TitleProperty");
        public static readonly DependencyProperty HintProperty = ElementBase.Property<MiProgressBar, string>("HintProperty");
        public static readonly DependencyProperty ProgressBarHeightProperty = ElementBase.Property<MiProgressBar, double>("ProgressBarHeightProperty");
        public static readonly DependencyProperty TextHorizontalAlignmentProperty = ElementBase.Property<MiProgressBar, HorizontalAlignment>("TextHorizontalAlignmentProperty");

        public ProgressBarState ProgressBarState { get { return (ProgressBarState)GetValue(ProgressBarStateProperty); } set { SetValue(ProgressBarStateProperty, value); } }
        public CornerRadius CornerRadius { get { return (CornerRadius)GetValue(CornerRadiusProperty); } set { SetValue(CornerRadiusProperty, value); } }
        public string Title { get { return (string)GetValue(TitleProperty); } set { SetValue(TitleProperty, value); } }
        public string Hint { get { return (string)GetValue(HintProperty); } set { SetValue(HintProperty, value); } }
        public double ProgressBarHeight { get { return (double)GetValue(ProgressBarHeightProperty); } set { SetValue(ProgressBarHeightProperty, value); } }
        public HorizontalAlignment TextHorizontalAlignment { get { return (HorizontalAlignment)GetValue(TextHorizontalAlignmentProperty); } set { SetValue(TextHorizontalAlignmentProperty, value); } }

        public MiProgressBar()
        {
            ControlUtility.Refresh(this);
            ValueChanged += delegate
            {
                if (Hint == null||Hint.EndsWith(" %"))
                {
                    Hint = ((int)(Value / Maximum * 100)).ToString() + " %";
                }
            };
        }

        static MiProgressBar()
        {
            ElementBase.DefaultStyle<MiProgressBar>(DefaultStyleKeyProperty);
        }
    }
}