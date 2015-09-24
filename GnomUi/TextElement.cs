namespace GnomUi
{
    using System;

    public class TextElement : Element
    {
        private string content;

        public TextElement(string content)
        {
            this.content = content;
        }

        public override void Display(int x, int y)
        {
            this.Style.AbsPaddingLeft = this.Style.PaddingLeft + x;
            this.Style.AbsPaddingTop = this.Style.PaddingTop + y - 1;
            ApplyStyleToConsole(this.Style);

            Console.Write(this.content);
        }
    }
}
