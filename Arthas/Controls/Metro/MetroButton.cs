﻿using Arthas.Utility.Element;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Arthas.Controls.Metro
{
    public enum ButtonState
    {
        None,
        Red,
        Green
    }

    public class MetroButton : ButtonBase
    {
        public static readonly DependencyProperty MetroButtonStateProperty = ElementBase.Property<MetroButton, ButtonState>("MetroButtonStateProperty", ButtonState.None);

        public ButtonState MetroButtonState { get { return (ButtonState)GetValue(MetroButtonStateProperty); } set { SetValue(MetroButtonStateProperty, value); } }

        public MetroButton()
        {
            Utility.Refresh(this);
        }

        static MetroButton()
        {
            ElementBase.DefaultStyle<MetroButton>(DefaultStyleKeyProperty);
        }
    }
}