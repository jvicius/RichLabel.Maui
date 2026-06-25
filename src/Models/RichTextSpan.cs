namespace RichLabel.Maui.Models
{
    public class RichTextSpan
    {
        public string Text { get; set; } = "";
        public bool Bold { get; set; }
        public bool Italic { get; set; }
        public bool Underline { get; set; }
        public string? Color { get; set; }
        public double? FontSize { get; set; }
        public string? FontFamily { get; set; } 
        public string? IconGlyph { get; set; }    
        public string? IconFontFamily { get; set; } 
        public string? ActionType { get; set; }
        public string? ActionValue { get; set; }
    }
}
