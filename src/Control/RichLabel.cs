using RichLabel.Maui.Enums;
using RichLabel.Maui.Handlers;
using RichLabel.Maui.Helpers;
using RichLabel.Maui.Models;

namespace RichLabel.Maui.Control
{
    public class RichLabel : ContentView
    {
        public static readonly BindableProperty DocumentProperty =
            BindableProperty.Create(nameof(Document), typeof(RichTextDocument), typeof(RichLabel),
                propertyChanged: (b, o, n) => ((RichLabel)b).Render());

        public RichTextDocument? Document
        {
            get => (RichTextDocument?)GetValue(DocumentProperty);
            set => SetValue(DocumentProperty, value);
        }

        public static readonly BindableProperty DefaultFontFamilyProperty =
            BindableProperty.Create(nameof(DefaultFontFamily), typeof(string), typeof(RichLabel), default(string));

        public string? DefaultFontFamily
        {
            get => (string?)GetValue(DefaultFontFamilyProperty);
            set => SetValue(DefaultFontFamilyProperty, value);
        }

        public static readonly BindableProperty IsResponsiveFontSizeProperty =
          BindableProperty.Create(
              nameof(IsResponsiveFontSize),
              typeof(bool),
              typeof(RichLabel),
              defaultValue: false);

        public bool IsResponsiveFontSize
        {
            get => (bool)GetValue(IsResponsiveFontSizeProperty);
            set => SetValue(IsResponsiveFontSizeProperty, value);
        }

        readonly VerticalStackLayout _stack = new() { Spacing = 4 };

        public RichLabel()
        {
            Content = _stack;
        }

        void Render()
        {
            _stack.Clear();
            if (Document is null) return;

            int numberedCounter = 1;

            foreach (var line in Document.Lines)
            {
                if (line.Type != LineType.Numbered) numberedCounter = 1;
                var row = BuildLineLayout(line, ref numberedCounter);
                _stack.Add(row);
            }
        }

        View BuildLineLayout(RichTextLine line, ref int numberedCounter)
        {
            var label = new ActionableLabel
            {
                FormattedText = BuildFormattedString(line),
                LineBreakMode = LineBreakMode.WordWrap,
                VerticalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = ToMauiAlignment(line.Alignment),
                HorizontalOptions = LayoutOptions.Fill // importante para que el centrado funcione
            };

            if (line.Type == LineType.Normal)
                return label;

            string marker = line.Type == LineType.Bullet
                ? (line.BulletChar ?? "•")
                : $"{numberedCounter++}.";

            var markerLabel = new ActionableLabel
            {
                Text = marker,
                FontSize = (IsResponsiveFontSize) ? ResponsiveSize.Get(14) : 14,
                VerticalOptions = LayoutOptions.Start,
                WidthRequest = (IsResponsiveFontSize) ? ResponsiveSize.Get(24) : 24
            };

            var grid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition(GridLength.Auto),
                    new ColumnDefinition(GridLength.Star)
                },
                Margin = new Thickness(line.IndentLevel * 20, 0, 0, 0),
                ColumnSpacing = 4
            };

            grid.Add(markerLabel, 0, 0);
            grid.Add(label, 1, 0);

            return grid;
        }

        TextAlignment ToMauiAlignment(LineAlignment align) => align switch
        {
            LineAlignment.Center => TextAlignment.Center,
            LineAlignment.End => TextAlignment.End,
            LineAlignment.Justify => TextAlignment.Justify,
            _ => TextAlignment.Start
        };

        FormattedString BuildFormattedString(RichTextLine line)
        {
            var fs = new FormattedString();

            foreach (var s in line.Spans)
            {
                if (!string.IsNullOrWhiteSpace(s.IconGlyph))
                {
                    var iconSpan = new Span
                    {
                        Text = s.IconGlyph,
                        FontFamily = s.IconFontFamily ?? "FA-Solid",
                        FontSize = s.FontSize ?? 14
                    };

                    iconSpan.FontSize = (IsResponsiveFontSize) ? ResponsiveSize.Get((int)iconSpan.FontSize) : iconSpan.FontSize;

                    if (!string.IsNullOrWhiteSpace(s.Color))
                        iconSpan.TextColor = Color.FromArgb(s.Color);

                    AttachAction(iconSpan, s);
                    fs.Spans.Add(iconSpan);
                    continue;
                }

                var span = new Span
                {
                    Text = s.Text,
                    FontAttributes = (s.Bold ? FontAttributes.Bold : FontAttributes.None) |
                                     (s.Italic ? FontAttributes.Italic : FontAttributes.None),
                    TextDecorations = s.Underline ? TextDecorations.Underline : TextDecorations.None,
                    FontSize = s.FontSize ?? 14,
                    FontFamily = s.FontFamily ?? DefaultFontFamily
                };

                span.FontSize = (IsResponsiveFontSize) ? ResponsiveSize.Get((int)span.FontSize) : span.FontSize;

                if (!string.IsNullOrWhiteSpace(s.Color))
                    span.TextColor = Color.FromArgb(s.Color);

                AttachAction(span, s);
                fs.Spans.Add(span);
            }

            return fs;
        }

        void AttachAction(Span span, RichTextSpan s)
        {
            if (string.IsNullOrWhiteSpace(s.ActionType)) return;

            var tap = new TapGestureRecognizer();
            tap.Tapped += async (_, _) => await RichActionHandler.Handle(s.ActionType!, s.ActionValue);
            span.GestureRecognizers.Add(tap);

            if (string.IsNullOrWhiteSpace(s.Color))
                span.TextColor = Colors.Blue;
        }
    }
}
