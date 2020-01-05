using EAStyles.Themes;
using EAStyles.Utilitys;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace EAStyles.Controls.MiStyle
{
    public class MiWindow : Window
    {
        public static readonly DependencyProperty IsSubWindowShowProperty = ElementBase.Property<MiWindow, bool>("IsSubWindowShowProperty", false);
        public static readonly DependencyProperty UserComponentProperty = ElementBase.Property<MiWindow, object>("UserComponentProperty", null);
        //public new static readonly DependencyProperty BorderBrushProperty = ElementBase.Property<MiWindow, Brush>("BorderBrushProperty");
        public static readonly DependencyProperty TitleForegroundProperty = ElementBase.Property<MiWindow, Brush>("TitleForegroundProperty");
        public static readonly DependencyProperty TitleLocationProperty = ElementBase.Property<MiWindow, HorizontalAlignment>("TitleLocationProperty", HorizontalAlignment.Center);

        public bool IsSubWindowShow { get { return (bool)GetValue(IsSubWindowShowProperty); } set { SetValue(IsSubWindowShowProperty, value); GoToState(); } }
        public object UserComponent { get { return GetValue(UserComponentProperty); } set { SetValue(UserComponentProperty, value); } }
        public new Brush BorderBrush { get { return (Brush)GetValue(BorderBrushProperty); } set { SetValue(BorderBrushProperty, value); BorderBrushChange(value); } }
        public Brush TitleForeground { get { return (Brush)GetValue(TitleForegroundProperty); } set { SetValue(TitleForegroundProperty, value); } }
        public HorizontalAlignment TitleLocation { get { return (HorizontalAlignment)GetValue(TitleLocationProperty); } set { SetValue(TitleLocationProperty, value); } }

        void BorderBrushChange(Brush brush)
        {
            if (IsLoaded)
            {
                Theme.Switch(this);
            }
        }

        void GoToState()
        {
            ElementBase.GoToState(this, IsSubWindowShow ? "Enabled" : "Disable");
        }

        public object ReturnValue { get; set; } //= null;
        public bool EscClose { get; set; } //= false;

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            AllowsTransparency = false;
            if (WindowStyle == WindowStyle.None)
            {
                WindowStyle = WindowStyle.SingleBorderWindow;
            }
        }

        public MiWindow()
        {
            // 修复WindowChrome导致的窗口大小错误
            var sizeToContent = SizeToContent.Manual;
            Loaded += (ss, ee) =>
            {
                sizeToContent = SizeToContent;
            };
            ContentRendered += (ss, ee) =>
            {
                SizeToContent = SizeToContent.Manual;
                Width = ActualWidth;
                Height = ActualHeight;
                SizeToContent = sizeToContent;
            };

            KeyUp += delegate (object sender, KeyEventArgs e)
            {
                if (e.Key == Key.Escape && EscClose)
                {
                    Close();
                }
            };
            StateChanged += delegate
              {
                  if (ResizeMode == ResizeMode.CanMinimize || ResizeMode == ResizeMode.NoResize)
                  {
                      if (WindowState == WindowState.Maximized)
                      {
                          WindowState = WindowState.Normal;
                      }
                  }
              };
            ControlUtility.Refresh(this);
        }

        static MiWindow()
        {
            ElementBase.DefaultStyle<MiWindow>(DefaultStyleKeyProperty);
        }
    }
    
}