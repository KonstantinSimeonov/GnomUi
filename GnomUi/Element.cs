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

        public abstract void Display(int x, int y);

        protected virtual void ApplyStyleToConsole(IStyle style)
        {
            Console.ForegroundColor = style.Color;
            Console.SetCursorPosition(style.AbsPaddingLeft, style.AbsPaddingTop);
        }
    }
}
