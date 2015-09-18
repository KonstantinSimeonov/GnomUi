using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnomUi
{
    public abstract class Element : IElement
    {

        public IStyle Style { get; set; }

        public abstract void Display(int x, int y);

        protected virtual void ApplyStyleToConsole(IStyle style)
        {
            Console.ForegroundColor = style.Color;
            Console.SetCursorPosition(style.AbsPaddingLeft, style.AbsPaddingTop);
        }
    }
}
