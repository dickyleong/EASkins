using EAStyles.Utilitys;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace EAStyles.Controls.MiStyle
{
    public class MiRichTextBox : RichTextBox
    {
        public static readonly DependencyProperty CornerRadiusProperty = ElementBase.Property<MiRichTextBox, CornerRadius>("CornerRadiusProperty");
        public static readonly new DependencyProperty BorderBrushProperty = ElementBase.Property<MiRichTextBox, Brush>("ForegroundProperty");
        public static readonly DependencyProperty MouseMoveBorderBrushProperty = ElementBase.Property<MiRichTextBox, Brush>("MouseMoveBorderBrushProperty");
        public static readonly DependencyProperty MouseMoveThemeBorderBrushProperty = ElementBase.Property<MiRichTextBox, Brush>("MouseMoveThemeBorderBrushProperty");
        public static readonly DependencyProperty AutoLimitMouseProperty = ElementBase.Property<MiRichTextBox, bool>("AutoLimitMouseProperty", false);

        public CornerRadius CornerRadius { get { return (CornerRadius)GetValue(CornerRadiusProperty); } set { SetValue(CornerRadiusProperty, value); } }
        public new Brush BorderBrush { get { return (Brush)GetValue(BorderBrushProperty); } set { SetValue(BorderBrushProperty, value); } }
        public Brush MouseMoveBorderBrush { get { return (Brush)GetValue(MouseMoveBorderBrushProperty); } set { SetValue(MouseMoveBorderBrushProperty, value); } }
        public Brush MouseMoveThemeBorderBrush { get { return (Brush)GetValue(MouseMoveThemeBorderBrushProperty); } set { SetValue(MouseMoveThemeBorderBrushProperty, value); } }
        public bool AutoLimitMouse { get { return (bool)GetValue(AutoLimitMouseProperty); } set { SetValue(AutoLimitMouseProperty, value); } }

        void SetColor()
        {
            CaretBrush = Foreground;
            SelectionBrush = Foreground;
        }

        void SetSize()
        {
            var w = ActualWidth - BorderThickness.Left - BorderThickness.Right - Padding.Left - Padding.Right - 2;
            Document.MaxPageWidth = w > 0 ? w : Document.MaxPageWidth;
        }

        static MiRichTextBox()
        {
            ElementBase.DefaultStyle<MiRichTextBox>(DefaultStyleKeyProperty);
        }

        public MiRichTextBox()
        {
            ContextMenu = null;
            ControlUtility.Refresh(this);
            Loaded += delegate
            {
                SetColor();
                SetSize();
            };
            SizeChanged += delegate
            {
                SetSize();
            };
        }

        public void Clear()
        {
            Document.Blocks.Clear();
        }

        public string Text
        {
            get
            {
                var sb = new StringBuilder();
                var isFirst = true;
                foreach (var block in Document.Blocks)
                {
                    if (isFirst)
                        isFirst = false;
                    else
                        sb.AppendLine();

                    if (block is Paragraph)
                        foreach (var inline in ((Paragraph)block).Inlines)
                        {
                            if (inline is Run)
                                sb.Append(((Run)inline).Text);
                            else if (inline is LineBreak)
                                sb.AppendLine();
                        }
                }
                return sb.ToString();
            }
            set
            {
                Document.Blocks.Clear();
                Paragraph paragraph = new Paragraph();
                Run run = new Run(value);
                paragraph.Inlines.Add(run);
                Document.Blocks.Add(paragraph);
            }
        }

        public void AddLine(string content, RgbaColor rgba, Action action)
        {
            Run run = new Run(content);
            if (action == null)
            {
                if (rgba != null) { run.Foreground = rgba.SolidColorBrush; }
                Document.Blocks.Add(new Paragraph(run));
            }
            else
            {
                Hyperlink hl = new Hyperlink(run);
                if (rgba != null) { hl.Foreground = rgba.SolidColorBrush; }
                hl.Click += delegate { action(); };
                hl.MouseLeftButtonDown += delegate { action(); };
                Document.Blocks.Add(new Paragraph(hl));
            }
            ScrollToEnd();
        }
        //public void AddLine(ImageSource image, Action action)
        //{
        //    if (action == null)
        //    {
        //        Document.Blocks.Add(new Paragraph(new InlineUIContainer(new Image(image))));
        //    }
        //    else
        //    {
        //        Hyperlink hl = new Hyperlink(new InlineUIContainer(new Image(image)));
        //        hl.Foreground = null;
        //        hl.Click += delegate { action(); };
        //        hl.MouseLeftButtonDown += delegate { action(); };
        //        Document.Blocks.Add(new Paragraph(hl));
        //    }
        //    ScrollToEnd();
        //}

        public void AddLine() { AddLine("", (Action)null); }
        public void AddLine(string content) { AddLine(content, (Action)null); }
        public void AddLine(string content, RgbaColor rgba) { AddLine(content, rgba, null); }
        //public void AddLine(ImageSource image) { AddLine(image, (Action)null); }
        public void AddLine(string content, Action action) { AddLine(content, null, action); }

        public void AddLine(string title, string url) { AddLine(title, () => System.Diagnostics.Process.Start(url)); }
        public void Add(string title, string url) { Add(title, () => System.Diagnostics.Process.Start(url)); }


        public void Add(string content, RgbaColor rgba, Action action)
        {
            if (Document.Blocks.Count <= 0)
            {
                Document.Blocks.Add(new Paragraph());
            }
            Run run = new Run(content);
            if (action == null)
            {
                if (rgba != null) { run.Foreground = rgba.SolidColorBrush; }
                (Document.Blocks.LastBlock as Paragraph).Inlines.Add(run);
            }
            else
            {
                Hyperlink hl = new Hyperlink(run);
                if (rgba != null) { hl.Foreground = rgba.SolidColorBrush; }
                hl.Click += delegate { action(); };
                hl.MouseLeftButtonDown += delegate { action(); };
                (Document.Blocks.LastBlock as Paragraph).Inlines.Add(hl);
            }
            ScrollToEnd();
        }
        //public void Add(ImageSource image, Action action)
        //{
        //    if (Document.Blocks.Count <= 0)
        //    {
        //        Document.Blocks.Add(new Paragraph());
        //    }
        //    if (action == null)
        //    {
        //        (Document.Blocks.LastBlock as Paragraph).Inlines.Add(new InlineUIContainer(new Image(image)));
        //    }
        //    else
        //    {
        //        Hyperlink hl = new Hyperlink(new InlineUIContainer(new Image(image)));
        //        hl.Foreground = null;
        //        hl.Click += delegate { action(); };
        //        hl.MouseLeftButtonDown += delegate { action(); };
        //        (Document.Blocks.LastBlock as Paragraph).Inlines.Add(hl);
        //    }
        //    ScrollToEnd();
        //}

        public void Add(string content) { Add(content, null, (Action)null); }
        public void Add(string content, RgbaColor rgba) { Add(content, rgba, null); }
        //public void Add(ImageSource image) { Add(image, (Action)null); }
        public void Add(string content, Action action) { Add(content, null, action); }
    }
}
