using System.Windows;
using System.Windows.Media;

namespace EAStyles.Themes
{
    public class Theme
    {
        public static void Switch(Visual pVisual)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(pVisual); i++)
            {
                Visual childVisual = (Visual)VisualTreeHelper.GetChild(pVisual, i);
                if (childVisual != null)
                {
                    Controls.ControlUtility.Refresh(childVisual as FrameworkElement);
                    Switch(childVisual);
                }
            }
        }
    }
}
