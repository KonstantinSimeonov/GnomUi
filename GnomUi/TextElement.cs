using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnomUi
{
    public class TextElement : Element
    {
        private string content;

        public IStyle style { get; set; }

        public TextElement(string content)
        {
            this.content = content;
        }

        public override void Display(int x, int y)
        {
            this.style.AbsPaddingLeft = this.style.PaddingLeft + x;
            this.style.AbsPaddingTop = this.style.AbsPaddingTop + y;
            this.ApplyStyleToConsole(this.style);
            Console.Write(this.content);
        }
    }
}
