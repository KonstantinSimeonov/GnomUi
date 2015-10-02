namespace GnomUi
{
    using System;

    using System.Collections.Generic;
    using GnomUi.Contracts;

    public abstract class Element : IElement, IPressable
    {
         private static readonly IDictionary<ConsoleKey, ConsoleKey> reverseKeys = new Dictionary<ConsoleKey, ConsoleKey>()
            {
                { ConsoleKey.UpArrow, ConsoleKey.DownArrow },
                { ConsoleKey.LeftArrow, ConsoleKey.DownArrow },
                { ConsoleKey.DownArrow, ConsoleKey.UpArrow },
                { ConsoleKey.RightArrow, ConsoleKey.LeftArrow}
            };
        
        // i don't like null ref exceptions
        protected static readonly Action<IElement> Empty = (element) => { };

        private INodeElement parent;

        public string Id { get; set; }

        public string Class { get; set; }

        public IStyle Style { get; set; }

        protected Element(bool selected = false)
        {
            this.Style = new Style();
            this.Id = string.Empty;
            this.Class = string.Empty;
            this.Neighbors = new Dictionary<ConsoleKey, ISelectable>();
            this.IsSelected = selected;
        }

        // INodeElement properties

        

        // IPressable properties
        // ISelectable methods

        public void LinkTo(ConsoleKey key, ISelectable element, bool doubly = true)
        {
            this.Neighbors.Add(key, element);

            if (!element.Neighbors.ContainsKey(reverseKeys[key]) && doubly)
            {
                element.Neighbors.Add(reverseKeys[key], this);
            }
        }

        // IPressable methods

        public void FireEvent()
        {
            this.OnClick(this);
        }

        public Action<IElement> OnClick { get; set; }

        public bool IsSelected { get; set; }

        public IDictionary<ConsoleKey, ISelectable> Neighbors { get; private set; }

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
