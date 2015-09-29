namespace Interpreter.Core
{
    using System;
    using System.Text;

    using GnomUi.Contracts;
    using GnomUi;

    public class IndentParser
    {
        private static string[] input1 = new string[] 
        {
            "box",
            "    gnom",
            "        durvo",
            "        buhal",
            "            godji",
            "            godji",
            "            godji",
            "    machka",
            "        kon",
            "            godji",
            "            kon2",
            "        sopol",
            "            smurdish",
            
        };

        private static string[] input2 = new string[] 
        {
            "doc",
            "    zob",
            "        UIlo Kendov",
            "    domcho",
            "    domcho",
            "            ognqn",
            ""
        };

        private static int index = 0;

        private static StringBuilder result = new StringBuilder();

        private static IStyle GetStyleFromRow(string row)
        {
            var split = row.Split(' ');

            var result = new Style()
            {
                

            };

            return null;
        }
        private INodeElement ParseRevursive(string parent, string root, string[] sub, int start, int end)
        {
            var depth = root.Depth() + 1;

            var split = root.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // TODO: fix dat shyt

            var self = new Node()
            {
                Id = split[1],
                Class = split[2],
                // sample ugly styles, remove ASAP
                Style = new Style()
                {
                    PaddingLeft = 10,
                    PaddingTop = 6,
                    Height = 3,
                    Width = 6,
                    Color = ConsoleColor.DarkGreen
                }
            };
            for (int i = start + 1; i < end; i++)
            {
                if (sub[i].Depth() - sub[i - 1].Depth() > 1)
                {
                    throw new ArgumentException("Invalid gnome composition at row " + (i+1) + ". Node " + sub[i].Trim() + " has invalid tree depth.");
                }

                if (i == sub.Length - 1 || sub[i].Depth() <= depth)
                {
                    self.AddChild(ParseRevursive(root, sub[start], sub, start + 1, i + 1));
                    start = i;
                }
            }

            return self;
        }

        public INodeElement Parse(string[] args)
        {
            var result = ParseRevursive("document", args[0], args, 1, args.Length);
            return result;
        }

    }
}