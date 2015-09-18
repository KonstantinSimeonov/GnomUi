using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnomUi
{
    public class Node : Element, INodeElement
    {
        public IList<IElement> Children { get; set; }

        public Node()
        {
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

        public IStyle Style { get; set; }

        public override void Display(int x, int y)
        {
            this.Style.AbsPaddingLeft = this.Style.PaddingLeft + x;
            this.Style.AbsPaddingTop = this.Style.AbsPaddingTop + y;
            this.ApplyStyleToConsole(this.Style);
            var counter = this.Style.AbsPaddingTop;

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

            var topBottomBorder = ' '+new string('_', this.Style.Width - 2);
            var result = new string[this.Style.Height];
            result[0] = (topBottomBorder);

            for (int i = 1; i < this.Style.Height - 1; i++)
            {
                result[i] = ('|' + new string(' ', this.Style.Width - 2) + '|');
            }

            result[result.Length - 1] = ('|' + new string('_', this.Style.Width-2) +'|');

            return result;
        }
    }
}
