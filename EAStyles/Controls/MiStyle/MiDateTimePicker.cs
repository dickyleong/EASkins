using EAStyles.Utilitys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace EAStyles.Controls.MiStyle
{
    public enum DateTimePickerFormat { Long, Short, Time, Custom }

    [System.ComponentModel.DefaultBindingProperty("Value")]

    public class MiDateTimePicker : Control
    {
        private CheckBox _checkBox;
        internal TextBox _textBox;
        private TextBlock _textBlock;
        private Popup _popUp;
        private Calendar _calendar;
        private Border _borderFocus;
        private Border _borderNormal;
        private BlockManager _blockManager;
        private string _defaultFormat = "yyyy/MM/dd HH:mm:ss";
        [Category("MiDateTimePicker")]
        public bool ShowCheckBox
        {
            get { return this._checkBox.Visibility == System.Windows.Visibility.Visible ? true : false; }
            set
            {
                if (value)
                    this._checkBox.Visibility = System.Windows.Visibility.Visible;
                else
                {
                    this._checkBox.Visibility = System.Windows.Visibility.Collapsed;
                    this.Checked = true;
                }
            }
        }
        [Category("MiDateTimePicker")]
        public bool ShowDropDown
        {
            get { return this._textBlock.Visibility == System.Windows.Visibility.Visible ? true : false; }
            set
            {
                if (value)
                    this._textBlock.Visibility = System.Windows.Visibility.Visible;
                else
                    this._textBlock.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        [Category("MiDateTimePicker")]
        public bool Checked
        {
            get { return this._checkBox.IsChecked.HasValue ? this._checkBox.IsChecked.Value : false; }
            set { this._checkBox.IsChecked = value; }
        }
        [Category("MiDateTimePicker")]
        private string FormatString
        {
            get
            {
                switch (this.Format)
                {
                    case DateTimePickerFormat.Long:
                        return "yyyy/MM/dd HH:mm:ss tt";
                    case DateTimePickerFormat.Short:
                        return "yyyy/MM/dd";
                    case DateTimePickerFormat.Time:
                        return "h:mm:ss tt";
                    case DateTimePickerFormat.Custom:
                        if (string.IsNullOrEmpty(this.CustomFormat))
                            return this._defaultFormat;
                        else
                            return this.CustomFormat;
                    default:
                        return this._defaultFormat;
                }
            }
        }
        private string _customFormat;
        [Category("MiDateTimePicker")]
        public string CustomFormat
        {
            get { return this._customFormat; }
            set
            {
                this._customFormat = value;
                this._blockManager = new BlockManager(this, this.FormatString);
            }
        }
        private DateTimePickerFormat _format;
        [Category("MiDateTimePicker")]
        public DateTimePickerFormat Format
        {
            get { return this._format; }
            set
            {
                this._format = value;
                this._blockManager = new BlockManager(this, this.FormatString);
            }
        }
        [Category("MiDateTimePicker")]
        public DateTime? Value
        {
            get
            {
                if (!this.Checked) return null;
                return (DateTime?)GetValue(ValueProperty);
            }
            set { SetValue(ValueProperty, value); }
        }
        // Using a DependencyProperty as the backing store for TheDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(DateTime?), typeof(MiDateTimePicker), 
                new FrameworkPropertyMetadata(DateTime.Now, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, 
                    new PropertyChangedCallback(MiDateTimePicker.OnValueChanged), 
                    new CoerceValueCallback(MiDateTimePicker.CoerceValue), true, System.Windows.Data.UpdateSourceTrigger.PropertyChanged));

        static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if((d as MiDateTimePicker)._blockManager!=null)
                (d as MiDateTimePicker)._blockManager.Render();
        }

        static object CoerceValue(DependencyObject d, object value)
        {
            return value;
        }

        internal DateTime InternalValue
        {
            get
            {
                DateTime? value = this.Value;
                if (value.HasValue) return value.Value;
                return DateTime.MinValue;
            }
        }

        public MiDateTimePicker()
        {
            this.Initializ();
            this._blockManager = new BlockManager(this, this.FormatString);
            ControlUtility.Refresh(this);
        }

        private void Initializ()
        {
            this.Template = this.GetTemplate();
            this.ApplyTemplate();            
            this._checkBox = (CheckBox)this.Template.FindName("checkBox", this);
            this._textBox = (TextBox)this.Template.FindName("textBox", this);
            this._textBlock = (TextBlock)this.Template.FindName("textBlock", this);
            this._calendar = new Calendar();
            this._borderNormal = (Border)this.Template.FindName("borderNormal", this);
            this._borderFocus = (Border)this.Template.FindName("borderFocus", this);
            this._popUp = new Popup();
            this._popUp.PlacementTarget = this._textBox;
            this._popUp.Placement = PlacementMode.Bottom;
            this._popUp.Child = this._calendar;
            this._popUp.AllowsTransparency = true;
            this._popUp.PopupAnimation = PopupAnimation.Slide;
            this._checkBox.Checked += new RoutedEventHandler(_checkBox_Checked);
            this._checkBox.Unchecked += new RoutedEventHandler(_checkBox_Checked);
            this.MouseWheel += new System.Windows.Input.MouseWheelEventHandler(MiDateTimePicker_MouseWheel);
            this.MouseEnter += new System.Windows.Input.MouseEventHandler(MiDateTimePicker_MouseEnter);
            this.MouseLeave += new System.Windows.Input.MouseEventHandler(MiDateTimePicker_MouseLeave);
            this.Focusable = false;
            this.ShowCheckBox = false;
            this._textBox.Cursor = System.Windows.Input.Cursors.Arrow;
            this._textBox.AllowDrop = false;
            this._textBox.GotFocus += new System.Windows.RoutedEventHandler(_textBox_GotFocus);
            this._textBox.PreviewMouseUp += new System.Windows.Input.MouseButtonEventHandler(_textBox_PreviewMouseUp);
            this._textBox.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(_textBox_PreviewKeyDown);
            this._textBox.ContextMenu = null;
            this._textBox.IsEnabled = this.Checked;
            this._textBox.IsReadOnly = true;
            this._textBox.IsReadOnlyCaretVisible = false;
            this._textBlock.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(_textBlock_MouseLeftButtonDown);
            this._calendar.SelectedDatesChanged += new EventHandler<SelectionChangedEventArgs>(calendar_SelectedDatesChanged);            
        }

        private void MiDateTimePicker_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this._borderFocus == null || this._borderNormal == null)
                return;
            this._borderFocus.Opacity = 0;
            this._borderNormal.Opacity = 0.15;
        }

        private void MiDateTimePicker_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (this._borderFocus == null || this._borderNormal == null)
                return;
            this._borderFocus.Opacity = 1;
            this._borderNormal.Opacity = 0;
        }

        void _textBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {            
            this._popUp.IsOpen = !(this._popUp.IsOpen);
        }

        void _checkBox_Checked(object sender, RoutedEventArgs e)
        {
            this._textBox.IsEnabled = this.Checked;
        }

        void MiDateTimePicker_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {            
            this._blockManager.Change(((e.Delta < 0) ? -1 : 1), true);
        }

        void _textBox_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            this._blockManager.ReSelect();
        }

        void _textBox_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {            
            this._blockManager.ReSelect();
        }

        void _textBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {           
            byte b = (byte)e.Key;

            if (e.Key == System.Windows.Input.Key.Left)
                this._blockManager.Left();
            else if (e.Key == System.Windows.Input.Key.Right)
                this._blockManager.Right();
            else if (e.Key == System.Windows.Input.Key.Up)
                this._blockManager.Change(1, true);
            else if (e.Key == System.Windows.Input.Key.Down)
                this._blockManager.Change(-1, true);
            if (b >= 34 && b <= 43)
                this._blockManager.ChangeValue(b - 34);
            if (!(e.Key == System.Windows.Input.Key.Tab))
                e.Handled = true;
        }

        void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Checked = true;
            ((sender as Calendar).Parent as Popup).IsOpen = false;
            DateTime selectedDate = (DateTime)e.AddedItems[0];
            this.Value = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, this.InternalValue.Hour, this.InternalValue.Minute, this.InternalValue.Second);
            this._blockManager.Render();
        }

        private ControlTemplate GetTemplate()
        {
            //return (ControlTemplate)XamlReader.Parse(@"
            //    <ControlTemplate  xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
            //                      xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">
                   
            //            <Border x:Name = ""bordermove"" BorderThickness = ""1"" CornerRadius = ""0"" BorderBrush = ""{TemplateBinding BorderBrush}"" Opacity = ""0"">
            //                <Grid >
            //                    <CheckBox Name=""checkBox"" VerticalAlignment=""Center"" />
            //                    <TextBox Name=""textBox"" BorderThickness=""0"" VerticalAlignment=""Center"" VerticalContentAlignment=""Center""/>
            //                    <TextBlock Name=""textBlock"" Text=""...""  =""Center"" HorizontalAlignment=""Right"" />
            //                </Grid>
            //            </Border>                    
            //    </ControlTemplate>");
            return (ControlTemplate)XamlReader.Parse(@"
                <ControlTemplate  xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                                  xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""> 
                    <Border Height=""20"">
                        <Grid>
                            <Border x:Name = ""borderNormal"" BorderThickness = ""1"" CornerRadius = ""0"" BorderBrush = ""#FF000000""  Opacity=""0.15""/>
                            <Border x:Name = ""borderFocus"" BorderThickness = ""1"" CornerRadius = ""0"" BorderBrush = ""{TemplateBinding BorderBrush}"" Opacity=""0""/>
                            <CheckBox Name=""checkBox"" VerticalAlignment=""Center"" Margin=""1""/>
                            <TextBox Name=""textBox"" BorderThickness=""0"" VerticalAlignment=""Center"" VerticalContentAlignment=""Center"" Margin=""1""/>
                            <TextBlock Name=""textBlock"" Text=""..."" VerticalAlignment =""Center"" HorizontalAlignment=""Right"" Margin=""1""/>
                        </Grid>
                    </Border>
                </ControlTemplate>");
                     //< ControlTemplate.Triggers >
                     //   < Trigger Property = ""IsMouseOver"" Value = ""true"" > 
                     //        < Setter Property = ""Opacity"" TargetName = ""borderNormal"" Value = ""0"" /> 
                     //        < Setter Property = ""Opacity"" TargetName = ""borderFocus"" Value = ""1"" /> 
                     //    </ Trigger > 
                     //</ ControlTemplate.Triggers >
            // <Border x:Name=""border"" BorderThickness=""1"" CornerRadius=""0"" BorderBrush=""#FF000000"" Opacity=""0.15"">
            //</Border>
        }

        public override string ToString()
        {
            return this.InternalValue.ToString();
        }
    }

    internal class BlockManager
    {
        internal MiDateTimePicker _dtPicker;
        private List<Block> _blocks;
        private string _format;
        private Block _selectedBlock;
        private int _selectedIndex;
        public event EventHandler NeglectProposed;
        private string[] _supportedFormats = new string[] {
                "yyyy", "MMMM", "dddd",
                "yyy", "MMM", "ddd",
                "yy", "MM", "dd",
                "y", "M", "d",
                "HH", "H", "hh", "h",
                "mm", "m",
                "ss", "s",
                "tt", "t",
                "fff", "ff", "f",
                "K", "g"};

        public BlockManager(MiDateTimePicker dtPicker, string format)
        {            
            this._dtPicker = dtPicker;
            this._format = format;
            this._dtPicker.LostFocus += new RoutedEventHandler(_MiDateTimePicker_LostFocus);
            this._blocks = new List<Block>();
            this.InitBlocks();            
        }

        private void InitBlocks()
        {            
            foreach (string f in this._supportedFormats)
                this._blocks.AddRange(this.GetBlocks(f));
            this._blocks = this._blocks.OrderBy((a) => a.Index).ToList();
            this._selectedBlock = this._blocks[0];
            this.Render();
        }

        internal void Render()
        {
            int accum = 0;
            StringBuilder sb = new StringBuilder(this._format);
            foreach (Block b in this._blocks)
                b.Render(ref accum, sb);
            this._dtPicker._textBox.Text = this._format = sb.ToString();
            this.Select(this._selectedBlock);
        }

        private List<Block> GetBlocks(string pattern)
        {            
            List<Block> bCol = new List<Block>();

            int index = -1;
            while ((index = this._format.IndexOf(pattern, ++index)) > -1)
                bCol.Add(new Block(this, pattern, index));
            this._format = this._format.Replace(pattern, (0).ToString().PadRight(pattern.Length, '0'));
            return bCol;
        }

        internal void ChangeValue(int p)
        {
            this._selectedBlock.Proposed = p;
            this.Change(this._selectedBlock.Proposed, false);
        }

        internal void Change(int value, bool upDown)
        {
            this._dtPicker.Value = this._selectedBlock.Change(this._dtPicker.InternalValue, value, upDown);
            if (upDown)
                this.OnNeglectProposed();
            this.Render();
        }

        internal void Right()
        {            
            if (this._selectedIndex + 1 < this._blocks.Count)
                this.Select(this._selectedIndex + 1);
        }

        internal void Left()
        {            
            if (this._selectedIndex > 0)
                this.Select(this._selectedIndex - 1);
        }

        private void _MiDateTimePicker_LostFocus(object sender, RoutedEventArgs e)
        {
            this.OnNeglectProposed();
        }

        protected virtual void OnNeglectProposed()
        {
            EventHandler temp = this.NeglectProposed;
            if (temp != null)
            {
                temp(this, EventArgs.Empty);
            }
        }

        internal void ReSelect()
        {            
            foreach (Block b in this._blocks)
                if ((b.Index <= this._dtPicker._textBox.SelectionStart) && ((b.Index + b.Length) >= this._dtPicker._textBox.SelectionStart))
                { this.Select(b); return; }
            Block bb = this._blocks.Where((a) => a.Index < this._dtPicker._textBox.SelectionStart).LastOrDefault();
            if (bb == null) this.Select(0);
            else this.Select(bb);
        }

        private void Select(int blockIndex)
        {
            if (this._blocks.Count > blockIndex)
                this.Select(this._blocks[blockIndex]);
        }

        private void Select(Block block)
        {
            if (!(this._selectedBlock == block))
                this.OnNeglectProposed();
            this._selectedIndex = this._blocks.IndexOf(block);
            this._selectedBlock = block;
            this._dtPicker._textBox.Select(block.Index, block.Length);
        }
    }

    internal class Block
    {
        private BlockManager _blockManager;
        internal string Pattern { get; set; }
        internal int Index { get; set; }
        private int _length;
        internal int Length
        {
            get
            {
                return this._length;
            }
            set
            {
                this._length = value;
            }
        }
        private int _maxLength;
        private string _proposed;
        internal int Proposed
        {
            get
            {                
                string p = this._proposed;
                return int.Parse(p.PadLeft(this.Length, '0'));
            }
            set
            {                
                if (!(this._proposed == null) && this._proposed.Length >= this._maxLength)
                    this._proposed = value.ToString();
                else
                    this._proposed = string.Format("{0}{1}", this._proposed, value);
            }
        }

        public Block(BlockManager blockManager, string pattern, int index)
        {           
            this._blockManager = blockManager;
            this._blockManager.NeglectProposed += new EventHandler(_blockManager_NeglectProposed);
            this.Pattern = pattern;
            this.Index = index;
            this.Length = this.Pattern.Length;
            this._maxLength = this.GetMaxLength(this.Pattern);
        }

        private int GetMaxLength(string p)
        {
            switch (p)
            {
                case "y":
                case "M":
                case "d":
                case "h":
                case "m":
                case "s":
                case "H":
                    return 2;
                case "yyy":
                    return 4;
                default:
                    return p.Length;
            }
        }

        private void _blockManager_NeglectProposed(object sender, EventArgs e)
        {            
            this._proposed = null;
        }

        internal DateTime Change(DateTime dateTime, int value, bool upDown)
        {            
            if (!upDown && !this.CanChange()) return dateTime;
            int y, m, d, h, n, s;
            y = dateTime.Year;
            m = dateTime.Month;
            d = dateTime.Day;
            h = dateTime.Hour;
            n = dateTime.Minute;
            s = dateTime.Second;

            if (this.Pattern.Contains("y"))
                y = ((upDown) ? dateTime.Year + value : value);
            else if (this.Pattern.Contains("M"))
                m = ((upDown) ? dateTime.Month + value : value);
            else if (this.Pattern.Contains("d"))
                d = ((upDown) ? dateTime.Day + value : value);
            else if (this.Pattern.Contains("h") || this.Pattern.Contains("H"))
                h = ((upDown) ? dateTime.Hour + value : value);
            else if (this.Pattern.Contains("m"))
                n = ((upDown) ? dateTime.Minute + value : value);
            else if (this.Pattern.Contains("s"))
                s = ((upDown) ? dateTime.Second + value : value);
            else if (this.Pattern.Contains("t"))
                h = ((h < 12) ? (h + 12) : (h - 12));

            if (y > 9999) y = 1;
            if (y < 1) y = 9999;
            if (m > 12) m = 1;
            if (m < 1) m = 12;
            if (d > DateTime.DaysInMonth(y, m)) d = 1;
            if (d < 1) d = DateTime.DaysInMonth(y, m);
            if (h > 23) h = 0;
            if (h < 0) h = 23;
            if (n > 59) n = 0;
            if (n < 0) n = 59;
            if (s > 59) s = 0;
            if (s < 0) s = 59;

            return new DateTime(y, m, d, h, n, s);
        }

        private bool CanChange()
        {
            switch (this.Pattern)
            {
                case "MMMM":
                case "dddd":
                case "MMM":
                case "ddd":
                case "g":
                    return false;
                default:
                    return true;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", this.Pattern, this.Index);
        }

        internal void Render(ref int accum, StringBuilder sb)
        {
            this.Index += accum;

            string f = this._blockManager._dtPicker.InternalValue.ToString(this.Pattern + ",").TrimEnd(',');
            sb.Remove(this.Index, this.Length);
            sb.Insert(this.Index, f);
            accum += f.Length - this.Length;

            this.Length = f.Length;
        }
    }
}
