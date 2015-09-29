namespace Interpreter.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using GnomUi.Contracts;
    using GnomUi;

    using Interpreter.Gadgets;

    public class IndentParser
    {
        private IDictionary<string, IStyle> styleMap;
        private IDictionary<string, IElement> idMap;
        private IDictionary<string, IList<IElement>> classMap;


        public IndentParser()
        {
            this.idMap = new Dictionary<string, IElement>();
            this.classMap = new Dictionary<string, IList<IElement>>();
            this.styleMap = new Dictionary<string, IStyle>();
        }

        public IGnomTree Parse(string[] args, string stylesheet)
        {
            this.ClearMaps();

            var root = ParseRecursive(args[0], args, 1, args.Length);
            var styles = ParseStylesToMap(stylesheet);
            var tree = new GnomTree(root, this.idMap, this.classMap, styles);

            ApplyStyleMapToTree(tree, styles);

            return tree;
        }

        private void ClearMaps()
        {
            this.idMap.Clear();
            this.classMap.Clear();
        }

        private static IDictionary<string, IStyle> ParseStylesToMap(string stylesheet)
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

        private INodeElement ParseRecursive(string root, string[] sub, int start, int end)
        {
            var depth = root.Depth() + 1;

            var nextRoot = ParseToNode(root);

            UpdateIdMap(nextRoot, start, this.idMap);
            UpdateClassMap(nextRoot, this.classMap);

            for (int i = start + 1; i < end; i++)
            {
                ThrowIfInvalidIndenting(sub[i], sub[i - 1], i);

                bool currentElementIsChildOfRoot = i == sub.Length - 1 || sub[i].Depth() <= depth;

                if (currentElementIsChildOfRoot)
                {
                    var trimmed = sub[i - 1].Trim();

                    if (i < sub.Length - 1 && trimmed[0] == ':')
                    {
                        nextRoot.AddChild(new TextElement(trimmed.Remove(0, 1)));
                    }
                    else
                    {
                        nextRoot.AddChild(ParseRecursive(sub[start], sub, start + 1, i + 1));
                    }

                    start = i;
                }
            }

            return nextRoot;
        }

        private static void UpdateClassMap(INodeElement nextRoot, IDictionary<string, IList<IElement>> classMap)
        {
            if (!classMap.ContainsKey(nextRoot.Class))
            {
                classMap[nextRoot.Class] = new List<IElement>();
            }

            classMap[nextRoot.Class].Add(nextRoot);
        }

        private static void UpdateIdMap(INodeElement nextRoot, int row, IDictionary<string, IElement> idMap)
        {
            if (idMap.ContainsKey(nextRoot.Id))
            {
                throw new InvalidOperationException("Duplicate Ids in gnom resource at row " + (row) + ". Id name: " + nextRoot.Id);
            }

            idMap.Add(nextRoot.Id, nextRoot);
        }

        private static void ThrowIfInvalidIndenting(string current, string previous, int row)
        {
            bool currentElementHasInvalidIndent = current.Depth() - previous.Depth() > 1;

            if (currentElementHasInvalidIndent)
            {
                throw new ArgumentException("Invalid gnome composition at row " + (row + 1) + ". Node " + current.Trim() + " has invalid tree depth.");
            }
        }

        private static INodeElement ParseToNode(string node)
        {
            var split = node.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                            .Where(x => x[0] == '#' || x[0] == '.') // remove all other entries
                            .GroupBy(x => x[0]) // divide id and classes attaching
                            .OrderBy(x => x.Key == '#' ? -1 : 1) // ids come first
                            .Select(x => x.ToArray()) // remove dupes and cast to array
                            .ToArray();

            var parsedNode = new Node();

            var handler = new Switch<string[][]>(split, true);

            handler
                .Case(split.Length == 0, () =>
                    {
                        handler.FallThrough = false;
                    })
                .Case(split[0].Length > 1, () =>
                    {
                        throw new InvalidOperationException("Cannot specify two Ids for element " + node);
                    })
                .Case(split[0].Length == 1, () =>
                    {
                        parsedNode.Id = split[0][0].Remove(0, 1);
                    })
                .Case(split.Length > 1, () =>
                    {
                        parsedNode.Class = split[1][0].Remove(0, 1);
                    });

            return parsedNode;
        }

        private static void ApplyStyleMapToTree(IGnomTree tree, IDictionary<string, IStyle> map)
        {
            foreach (var node in tree)
            {
                if (map.ContainsKey(node.Class))
                {
                    node.Style = map[node.Class];
                }
            }
        }

    }
}