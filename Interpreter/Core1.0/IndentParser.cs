namespace TreeParsing
{
    using System;
    using System.Text;

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

        private static StringBuilder result = new StringBuilder();

        private static void ParseRevursive(string parent, string root, string[] sub, int start, int end)
        {
            result.AppendFormat("{0} child of {1}{2}", root.Trim(), parent.Trim(), Environment.NewLine);

            var depth = root.Depth() + 1;

            for (int i = start + 1; i < end; i++)
            {
                if (sub[i].Depth() - sub[i - 1].Depth() > 1)
                {
                    throw new ArgumentException("Invalid gnome composition at row " + (i+1) + ". Node " + sub[i].Trim() + " has invalid tree depth.");
                }

                if (i == sub.Length - 1 || sub[i].Depth() <= depth)
                {
                    ParseRevursive(root, sub[start], sub, start + 1, i + 1);
                    start = i;
                }
            }

        }

        public static void Parse(string[] args)
        {
            ParseRevursive("document", input1[0], input1, 1, input1.Length);
            Console.WriteLine(result.ToString());
        }

    }
}