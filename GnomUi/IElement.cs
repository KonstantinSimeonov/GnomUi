using System;
using System.Linq;

namespace GnomUi
{
    public interface IElement
    {
        IStyle Style { get; set; }
        void Display(int x, int y);
    }
}
