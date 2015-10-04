namespace GnomUi.TreeComponents
{
    using System;

    using GnomUi.Contracts;

    public class TextElement : Element
    {
        private string content;

        public TextElement(string content)
            :base(false)
        {
            this.content = content;
        }

        protected override string[] Render()
        {
            return new string[] { this.content };
        }

        protected override void InitializeAbsolutePadding(IStyle style, int x, int y)
        {
            base.InitializeAbsolutePadding(style, x, y);
            style.AbsPaddingTop--;
        }
    }
}
