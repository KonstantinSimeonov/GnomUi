namespace Interpreter.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using GnomUi.Contracts;
    using GnomUi;

    using Interpreter.Gadgets;

    public class GnomInterpreter
    {
        //private const int NodeLinksCount = 4;
        //private const StringSplitOptions NoOptions = StringSplitOptions.None;
        private const StringSplitOptions RemoveEmpty = StringSplitOptions.RemoveEmptyEntries;

        private static readonly ConsoleKey[] directionKeysMap = new ConsoleKey[] { ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.UpArrow, ConsoleKey.DownArrow };
        //private IDictionary<string, IStyle> styleMap;
        private IDictionary<string, IElement> idMap;
        private IDictionary<string, IList<IElement>> classMap;
        //private IDictionary<string, IList<string>> selectionGraph;

        public GnomInterpreter()
        {
            this.idMap = new Dictionary<string, IElement>();
            this.classMap = new Dictionary<string, IList<IElement>>();
            //this.styleMap = new Dictionary<string, IStyle>();
            //this.selectionGraph = new Dictionary<string, IList<string>>();
        }

        public IGnomTree Parse(string[] args, string stylesheet, string selectionMap)
        {
            this.ClearMaps();

            var root = ParseRecursive(args[0], args, 1, args.Length);
            //var styles = ParseStylesToMap(stylesheet);
            //this.selectionGraph = ParseGnomSelectionMap(selectionMap);

            var tree = new GnomTree(root, this.idMap, this.classMap);
            //ApplySelectionMapToTree(tree, this.selectionGraph);
            //ApplyStyleMapToTree(tree, styles);

            return tree;
        }

        private void ClearMaps()
        {
            this.idMap.Clear();
            this.classMap.Clear();
            //this.styleMap.Clear();
            //this.selectionGraph.Clear();
        }

        //private static IDictionary<string, IList<string>> ParseGnomSelectionMap(string selectionMap)
        //{
        //    var selectionMapRows = selectionMap.Split(new string[] { Environment.NewLine }, NoOptions);
        //    var result = new Dictionary<string, IList<string>>();

        //    foreach (var nodeMapping in selectionMapRows)
        //    {
        //        var nodesAsStringArray = nodeMapping.Split(new char[] { ' ' }, RemoveEmpty)
        //                                            .Select(x => x.ToLower())
        //                                            .ToArray();

        //        var nodeId = nodesAsStringArray[0];

        //        if (!result.ContainsKey(nodeId))
        //        {
        //            result.Add(nodeId, new List<string>(NodeLinksCount));
        //        }

        //        for (int i = 1; i <= NodeLinksCount; i++)
        //        {
        //            var nodeToAdd = nodesAsStringArray[i] == "#" ? nodeId : nodesAsStringArray[i];
        //            result[nodeId].Add(nodeToAdd);
        //        }
        //    }

        //    return result;
        //}

        //private static IDictionary<string, IStyle> ParseStylesToMap(string stylesheet)
        //{
        //    var fragments = stylesheet.Split(new char[] { '.', '#' }, RemoveEmpty);

        //    var result = new Dictionary<string, IStyle>();

        //    foreach (var style in fragments)
        //    {
        //        var trimmedStyle = style.Trim();

        //        if (!string.IsNullOrEmpty(trimmedStyle))
        //        {
        //            var parsedStyle = ParseStyle(style);
        //            result.Add(parsedStyle.Key, parsedStyle.Value);
        //        }
        //    }

        //    return result;
        //}

        //private static KeyValuePair<string, IStyle> ParseStyle(string style)
        //{
        //    var rows = style.Split(new string[] { Environment.NewLine }, NoOptions).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

        //    var result = new Style();

        //    for (var i = 1; i < rows.Length; i++)
        //    {
        //        var splitRow = rows[i].Split(new char[] { ' ' }, RemoveEmpty);

        //        result[splitRow[0]] = splitRow[1];
        //    }

        //    return new KeyValuePair<string, IStyle>(rows[0].Trim(), result);
        //}

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
                    nextRoot.AddChild(ParseRecursive(sub[start], sub, start + 1, i + 1));
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
            var split = node.Split(new char[] { ' ' }, RemoveEmpty)
                            .Where(x => x[0] == '#' || x[0] == '.' || x[0] == ':') // remove all other entries
                            .GroupBy(x => x[0]) // divide id and classes attaching
                            .OrderBy(x => x.Key) // ids come first
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
                    })
                .Case(split.Length > 2, () =>
                {
                    parsedNode.AddChild(new TextElement(split[2][0].Remove(0, 1)));
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

        private static void ApplySelectionMapToTree(IGnomTree tree, IDictionary<string, IList<string>> seletionMap)
        {
            var directionIndex = 0;

            foreach (var nodeLinkInfo in seletionMap)
            {                
                for (int i = 0; i < 4; i++)
                {
                    var key = directionKeysMap[i];
                    var neighborForKey = tree[nodeLinkInfo.Value[i]];
                    tree[nodeLinkInfo.Key].Neighbors.Add(key, neighborForKey);
                    
                }
            }

            directionIndex++;
        }

    }
}