namespace GnomUi
{
    using System;
    using System.Collections.Generic;

    using GnomUi.Contracts;

    public class Node : Element, INodeElement
    {
        // i don't like null ref exceptions
        protected static readonly Action<IElement> Empty = (element) => { };

        // the bool parameter is for development purposes and will probably be removed for release
        public Node(bool selected = false)
        {
            this.OnClick = Empty;
            this.Children = new List<IElement>();
            this.IsSelected = selected;
        }

        // INodeElement properties

        public IList<IElement> Children { get; set; }

        // IPressable properties

        public Action<IElement> OnClick { get; set; }

        public bool IsSelected { get; private set; }

        // INodeElement methods

        public INodeElement AddChild(IElement element)
        {
            this.Children.Add(element);
            return this;
        }

        public INodeElement RemoveElement(IElement element)
        {
            this.Children.Remove(element);
            return this;
        }

        // IPressable methods

        public void FireEvent()
        {
            this.OnClick(this);
        }

        // inherited and private methods

        public override void Display(int x, int y)
        {
            this.Style.AbsPaddingLeft = this.Style.PaddingLeft + x;
            this.Style.AbsPaddingTop = this.Style.PaddingTop + y;
            this.ApplyStyleToConsole(this.Style);
            var counter = this.Style.AbsPaddingTop;

            if(this.IsSelected)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            foreach (var line in this.Render())
            {
                Console.SetCursorPosition(this.Style.AbsPaddingLeft, counter++);
                Console.WriteLine(line);
            }

            foreach (var child in this.Children)
            {
                child.Display(this.Style.AbsPaddingLeft + 1, this.Style.AbsPaddingTop + 1);
            }
        }

        private string[] Render()
        {

            var topBottomBorder = ' ' + new string('_', this.Style.Width - 2);
            var result = new string[this.Style.Height];
            result[0] = (topBottomBorder);

            for (int i = 1; i < this.Style.Height - 1; i++)
            {
                result[i] = ('|' + new string(' ', this.Style.Width - 2) + '|');
            }

            result[result.Length - 1] = ('|' + new string('_', this.Style.Width - 2) + '|');

            return result;
        }
    }
}
