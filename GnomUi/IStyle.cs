using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnomUi
{
    public interface IStyle
    {
        int AbsPaddingLeft { get; set; }
        int AbsPaddingTop { get; set; }
        int PaddingLeft { get; set; }
        int PaddingTop { get; set; }
        int Width { get; set; }
        int Height { get; set; }

        ConsoleColor Color { get; set; }
    }
}
