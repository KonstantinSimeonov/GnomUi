namespace Interpreter.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using GnomUi;
    using GnomUi.Contracts;

    internal class GnomStyleParser
    {
        private const StringSplitOptions NoOptions = StringSplitOptions.None;
        private const StringSplitOptions RemoveEmpty = StringSplitOptions.RemoveEmptyEntries;

        internal GnomStyleParser()
        {
        }

        public IDictionary<string, IStyle> ParseStyles(string stylesheet)
        {
            return ParseStylesToMap(stylesheet);
        }

        private static IDictionary<string, IStyle> ParseStylesToMap(string stylesheet)
        {
            var fragments = stylesheet.Split(new char[] { '.', '#' }, RemoveEmpty);

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
            var rows = style.Split(new string[] { Environment.NewLine }, NoOptions).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            var result = new Style();

            for (var i = 1; i < rows.Length; i++)
            {
                var splitRow = rows[i].Split(new char[] { ' ' }, RemoveEmpty);

                result[splitRow[0]] = splitRow[1];
            }

            return new KeyValuePair<string, IStyle>(rows[0].Trim(), result);
        }
    }
}
