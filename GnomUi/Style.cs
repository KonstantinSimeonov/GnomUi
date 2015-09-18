using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnomUi
{
    public class Style : IStyle
    {
        public int AbsPaddingLeft { get; set; }

        public int AbsPaddingTop { get; set; }

        public int PaddingLeft { get; set; }

        public int PaddingTop { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public ConsoleColor Color { get; set; }
    }
}
