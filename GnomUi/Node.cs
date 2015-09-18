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

        public IStyle style { get; set; }

        public override void Display(int x, int y)
        {
            this.style.AbsPaddingLeft = this.style.PaddingLeft + x;
            this.style.AbsPaddingTop = this.style.AbsPaddingTop + y;
            this.ApplyStyleToConsole(this.style);
            var counter = this.style.AbsPaddingTop;
            foreach (var line in this.Render())
            {
                Console.SetCursorPosition(this.style.AbsPaddingLeft, counter++);
                Console.WriteLine(line);
            }

            foreach (var child in this.Children)
            {
                child.Display(this.style.AbsPaddingLeft + 1, this.style.AbsPaddingTop + 1);
            }
        }

        private string[] Render()
        {

            var topBottomBorder = ' '+new string('_', this.style.Width - 2);
            var result = new string[this.style.Height];
            result[0] = (topBottomBorder);
            for (int i = 1; i < this.style.Height - 1; i++)
            {
                result[i] = ('|' + new string(' ', this.style.Width - 2) + '|');
            }
            result[result.Length - 1] = ('|' + new string('_', this.style.Width-2) +'|');

            return result;
        }
    }
}
