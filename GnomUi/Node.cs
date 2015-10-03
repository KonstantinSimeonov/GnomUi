namespace GnomUi
{
    using System;
    using System.Collections.Generic;

    using GnomUi.Contracts;

    public class Node : Element, INodeElement
    {
        public IList<IElement> Children { get; set; }
       
        public Node(bool selected = false)
            :base(selected)
        {
            this.OnClick = x => { };
            this.Children = new List<IElement>();
        }

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

        protected override void Draw(string[] renderedElement, int x, int y)
        {
            base.Draw(renderedElement, x, y);
            
            foreach (var child in this.Children)
            {
                child.Display(this.Style.AbsPaddingLeft + 1, this.Style.AbsPaddingTop + 1);
            }
        }

        protected override string[] Render()
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

        protected override void ApplyConsoleStyle(IStyle style)
        {
            base.ApplyConsoleStyle(style);
        }
    }
}
