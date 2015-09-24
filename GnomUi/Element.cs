namespace GnomUi
{
    using System;

    using GnomUi.Contracts;

    public abstract class Element : IElement
    {
        private INodeElement parent;

        public string Id { get; set; }

        public string Class { get; set; }

        public IStyle Style { get; set; }

        public INodeElement Parent
        {
            get
            {
                return this.parent;
            }

            private set
            {
                // a validation : O
                if(value != null && this.parent != null)
                {
                    throw new InvalidOperationException("gnom doesnt allow changing the parent of an element that already has a parent");
                }
                this.parent = value;
            }
        }        

        public void Display(int x, int y)
        {
            this.InitializeAbsolutePadding(this.Style, x, y);
            this.ApplyConsoleStyle(this.Style);
            var renderedElement = this.Render();
            this.Draw(renderedElement, x, y);
        }

        protected virtual void InitializeAbsolutePadding(IStyle style, int x, int y)
        {
            style.AbsPaddingLeft = style.PaddingLeft + x;
            style.AbsPaddingTop = style.PaddingTop + y;
        }

        protected virtual void ApplyConsoleStyle(IStyle style)
        {
            ApplyStyleToConsole(style);
        }

        protected abstract string[] Render();

        protected virtual void Draw(string[] renderedElement, int x, int y)
        {
            var counter = this.Style.AbsPaddingTop;

            foreach (var line in renderedElement)
            {
                Console.SetCursorPosition(this.Style.AbsPaddingLeft, counter++);
                Console.WriteLine(line);
            }
        }

        protected static void ApplyStyleToConsole(IStyle style)
        {
            Console.ForegroundColor = style.Color;
            Console.SetCursorPosition(style.AbsPaddingLeft, style.AbsPaddingTop);
        }
    }
}
