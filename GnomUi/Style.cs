using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnomUi
{
    public class Style : IStyle
    {
        private int paddingLeft = 1;
        private int paddingTop = 1;
        private int width = Console.BufferWidth - 2;
        private int height = Console.BufferWidth - 2;
        private ConsoleColor color = ConsoleColor.Gray;
        
        public int AbsPaddingLeft { get; set; }

        public int AbsPaddingTop { get; set; }

        public int PaddingLeft
        {
            get
            {
                return paddingLeft;
            }

            set
            {
                this.paddingLeft = value;
            }
        }

        public int PaddingTop
        {
            get
            {
                return paddingTop;
            }

            set
            {
                this.paddingTop = value;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                this.width = value;
            }
        }

        public int Height
        {
            get
            {
                return height;
            }

            set
            {
                this.height = value;
            }
        }

        public ConsoleColor Color
        {
            get
            {
                return color;
            }

            set
            {
                this.color = value;
            }
        }
    }
}
