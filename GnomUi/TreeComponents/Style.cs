namespace GnomUi.TreeComponents
{
    using System;

    using GnomUi.Contracts;
    using System.Collections.Generic;
    using System.Text;

    public class Style : IStyle
    {
        private readonly IDictionary<string, string> styleMap = new Dictionary<string, string>()
        {
            { "left", "1" },
            { "top", "1" },
            { "height", "5" },
            { "width", "5" },
            { "color", "gray" },
        };

        private static readonly IDictionary<string, ConsoleColor> colors = new Dictionary<string, ConsoleColor>()
        {
            { "gray", ConsoleColor.Gray },
            { "green", ConsoleColor.Green },
            { "blue", ConsoleColor.Blue },
            { "red", ConsoleColor.Red },
            { "white", ConsoleColor.White },
            { "yellow", ConsoleColor.Yellow },
            { "magenta", ConsoleColor.Magenta },
            { "black", ConsoleColor.Black}
        };

        public int AbsPaddingLeft { get; set; }

        public int AbsPaddingTop { get; set; }

        public string this[string propertyName]
        {
            get
            {
                return this.styleMap[propertyName];
            }

            set
            {
                this.styleMap[propertyName] = value;
            }
        }

        public int PaddingLeft
        {
            get
            {
                return int.Parse(this["left"]);
            }

            set
            {
                this["left"] = value.ToString();
            }
        }

        public int PaddingTop
        {
            get
            {
                return int.Parse(this["top"]);
            }

            set
            {
                this["top"] = value.ToString();
            }
        }

        public int Width
        {
            get
            {
                return int.Parse(this["width"]);
            }

            set
            {
                this["width"] = value.ToString();
            }
        }

        public int Height
        {
            get
            {
                return int.Parse(this["height"]);
            }

            set
            {
                this["height"] = value.ToString();
            }
        }

        public ConsoleColor Color
        {
            get
            {
                var c = this["color"];
                return colors[c];
            }

            set
            {
                this["color"] = value.ToString().ToLower();
            }
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            foreach (var pair in this.styleMap)
            {
                result.AppendLine(pair.ToString());
            }

            return result.ToString();
        }
    }
}
