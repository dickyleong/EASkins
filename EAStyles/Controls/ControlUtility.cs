using EAStyles.Controls.MiStyle;
using System.ComponentModel;
using System.Windows;

namespace EAStyles.Controls
{
    public class ControlUtility
    {
        /// <summary>
        /// 刷新样式
        /// </summary>
        /// <param name="control"></param>
        public static void Refresh(FrameworkElement control)
        {
            if (control == null)
            {
                return;
            }
            //正在运行的状态
            if (!DesignerProperties.GetIsInDesignMode(control))
            {
                if (control.IsLoaded)
                {
                    SetColor(control);
                }
                else
                {
                    control.Loaded += delegate { SetColor(control); };
                }
            }
        }

        static void SetColor(FrameworkElement control)
        {
            Window mw = Window.GetWindow(control) is MiWindow ? Window.GetWindow(control) as MiWindow : null;
            if (mw != null)
            {
                if(control is MiWindow)
                {
                    MiWindow window = control as MiWindow;
                    if(window.Owner!=null && window.Owner is MiWindow)
                    {
                        window.BorderBrush = window.Owner.BorderBrush.Clone();
                    }
                }
                if (control is MiTabControl)
                {
                    (control as MiTabControl).BorderBrush = mw.BorderBrush.Clone();
                }
                if (control is MiTabItem)
                {
                    (control as MiTabItem).Background = mw.BorderBrush.Clone();
                }
                if (control is MiButton)
                {
                    (control as MiButton).BorderBrush = mw.BorderBrush.Clone();
                }
                if (control is MiRichButton)
                {
                    (control as MiRichButton).BorderBrush = mw.BorderBrush.Clone();                    
                    (control as MiRichButton).Background = mw.BorderBrush.Clone();
                    (control as MiRichButton).Background.Opacity = 0.3;
                }
                if (control is MiTitleButton)
                {
                    (control as MiTitleButton).Background = mw.BorderBrush.Clone();
                }
                if (control is MiToggleButton)
                {
                    (control as MiToggleButton).Background = mw.BorderBrush.Clone();
                }
                if (control is MiRichTextBox)
                {
                    (control as MiRichTextBox).MouseMoveThemeBorderBrush = mw.BorderBrush.Clone();
                }
                if (control is MiTextBox)
                {
                    (control as MiTextBox).BorderBrush = mw.BorderBrush.Clone();
                }
                if (control is MiComboBox)
                {
                    (control as MiComboBox).BorderBrush = mw.BorderBrush.Clone();
                }
                if (control is MiDateTimePicker)
                {
                    (control as MiDateTimePicker).BorderBrush = mw.BorderBrush.Clone();
                }
                if(control is MiProgressBar)
                {
                    (control as MiProgressBar).Background = mw.BorderBrush.Clone();
                }
                if (control is MiDataGrid)
                {
                    (control as MiDataGrid).BorderBrush = mw.BorderBrush.Clone();
                }
                if(control is MiEfficientDataGrid)
                {
                    (control as MiEfficientDataGrid).BorderBrush = mw.BorderBrush.Clone();
                }
                if (control is MiCheckBox)
                {
                    (control as MiCheckBox).BorderBrush = mw.BorderBrush.Clone();
                }
                if (control is MiRadioButton)
                {
                    (control as MiRadioButton).BorderBrush = mw.BorderBrush.Clone();
                }
                if (control is MiTextLink)
                {
                    (control as MiTextLink).BorderBrush = mw.BorderBrush.Clone();
                }
                //if (control is MiTitleMenu)
                //{
                //    (control as MiTitleMenu).Background = mw.BorderBrush;
                //}
                //if (control is MiTitleMenuItem)
                //{
                //    (control as MiTitleMenuItem).Background = mw.BorderBrush;
                //}
                //if (control is MiMenuItem)
                //{
                //    (control as MiMenuItem).Background = mw.BorderBrush;
                //}
                //if (control is MiContextMenu)
                //{
                //    (control as MiContextMenu).Background = mw.BorderBrush;
                //}                
                //if (control is MiMenuTabControl)
                //{
                //    (control as MiMenuTabControl).BorderBrush = mw.BorderBrush;
                //}

                //if (control is MiCanvasGrid)
                //{
                //    if ((control as MiCanvasGrid).IsApplyTheme)
                //        (control as MiCanvasGrid).Background = new RgbaColor(mw.BorderBrush).OpaqueSolidColorBrush;
                //}
                //if (control is MiColorPicker)
                //{
                //    (control as MiColorPicker).BorderBrush = mw.BorderBrush;
                //}
            }
        }
    }
}