using RichLabel.Maui.Enums;

namespace RichLabel.Maui.Models
{
    public class RichTextLine
    {
        public LineType Type { get; set; } = LineType.Normal;
        public LineAlignment Alignment { get; set; } = LineAlignment.Start;
        public int IndentLevel { get; set; } = 0;    
        public string? BulletChar { get; set; }      
        public List<RichTextSpan> Spans { get; set; } = new();
    }
}
