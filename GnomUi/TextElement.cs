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

        public IStyle Style { get; set; }

        public TextElement(string content)
        {
            this.content = content;
        }

        public override void Display(int x, int y)
        {
            this.Style.AbsPaddingLeft = this.Style.PaddingLeft + x;
            this.Style.AbsPaddingTop = this.Style.AbsPaddingTop + y;
            this.ApplyStyleToConsole(this.Style);

            Console.Write(this.content);
        }
    }
}
