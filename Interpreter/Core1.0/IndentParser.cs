namespace Interpreter.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using GnomUi.Contracts;
    using GnomUi;

    public class IndentParser
    {
        public IDictionary<string, IStyle> ParseStylesToMap(string stylesheet)
        {
            var fragments = stylesheet.Split(new char[] { '.', '#' }, StringSplitOptions.RemoveEmptyEntries);

            var result = new Dictionary<string, IStyle>();

            foreach (var style in fragments)
            {
                var trimmedStyle = style.Trim();
                if (!string.IsNullOrEmpty(trimmedStyle))
                {
                    var parsedStyle = ParseStyle(style);
                    result.Add(parsedStyle.Key, parsedStyle.Value);
                }
            }

            return result;
        }

        private static KeyValuePair<string, IStyle> ParseStyle(string style)
        {
            var rows = style.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            var result = new Style();

            for (var i = 1; i < rows.Length; i++)
            {
                var splitRow = rows[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                result[splitRow[0]] = splitRow[1];
            }

            return new KeyValuePair<string, IStyle>(rows[0].Trim(), result);
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
                    throw new ArgumentException("Invalid gnome composition at row " + (i + 1) + ". Node " + sub[i].Trim() + " has invalid tree depth.");
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