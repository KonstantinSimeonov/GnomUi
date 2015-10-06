namespace Interpreter.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using GnomUi.Contracts;
    using GnomUi;

    using Interpreter.Contracts;
    using Interpreter.Gadgets;
    using GnomUi.TreeComponents;

    public class GnomInterpreter : IGnomInterpreter
    {
        private const char Id = '#';
        private const char Class = '.';
        private const char Text = ':';

        private const StringSplitOptions RemoveEmpty = StringSplitOptions.RemoveEmptyEntries;

        private static readonly char[] attributeCharacters = new char[] { Id, Class, Text };
        
        private IDictionary<string, INodeElement> idMap;

        private IDictionary<string, IList<INodeElement>> classMap;

        internal GnomInterpreter()
        {
            this.idMap = new Dictionary<string, INodeElement>();
            this.classMap = new Dictionary<string, IList<INodeElement>>();
        }

        public IGnomTree Parse(string treeDescription)
        {
            this.ClearMaps();

            var args = treeDescription.SplitBy(Environment.NewLine).Concat(new string[] { "" }).ToArray();

            var root = ParseRecursive(args[0], args, 1, args.Length);

            var tree = new GnomTree(root, this.idMap, this.classMap);

            return tree;
        }

        private void ClearMaps()
        {
            this.idMap.Clear();
            this.classMap.Clear();
        }

        // TODO: Implement iterative parsing and scrap this one

        private INodeElement ParseRecursive(string root, string[] fragments, int start, int end)
        {
            var depth = root.Depth() + 1;

            var nextRoot = ParseToNode(root);

            UpdateIdMap(nextRoot, start, this.idMap);
            UpdateClassMap(nextRoot, this.classMap);

            for (int i = start + 1; i < end; i++)
            {
                ThrowIfInvalidIndenting(fragments[i], fragments[i - 1], i);

                bool currentElementIsChildOfRoot = i == fragments.Length - 1 || fragments[i].Depth() <= depth;

                if (currentElementIsChildOfRoot)
                {
                    nextRoot.AddChild(ParseRecursive(fragments[start], fragments, start + 1, i + 1));
                    start = i;
                }
            }

            return nextRoot;
        }

        private static void UpdateClassMap(INodeElement nextRoot, IDictionary<string, IList<INodeElement>> classMap)
        {
            if (!classMap.ContainsKey(nextRoot.Class))
            {
                classMap[nextRoot.Class] = new List<INodeElement>();
            }

            classMap[nextRoot.Class].Add(nextRoot);
        }

        private static void UpdateIdMap(INodeElement nextRoot, int row, IDictionary<string, INodeElement> idMap)
        {
            if (idMap.ContainsKey(nextRoot.Id))
            {
                throw new InvalidOperationException("Duplicate Ids in gnom resource at row {0}. Id name: {1}".Format(row + 1,  nextRoot.Id));
            }

            idMap.Add(nextRoot.Id, nextRoot);
        }

        private static void ThrowIfInvalidIndenting(string current, string previous, int row)
        {
            bool currentElementHasInvalidIndent = current.Depth() - previous.Depth() > 1;

            if (currentElementHasInvalidIndent)
            {
                throw new ArgumentException("Invalid gnome composition at row {0}. Node {1} has invalid tree depth.".Format(row + 1, current.Trim()));
            }
        }

        private static INodeElement ParseToNode(string node)
        {
            var attributeGroups = node.SplitBy(" ")
                            .Where(x => attributeCharacters.Contains(x[0])) // remove all other entries
                            .GroupBy(x => x[0]) // divide id and classes attaching
                            .ToDictionary(group => group.Key, group => group.Select(x => x.RemoveFirst()).ToArray());

            var parsedNode = new Element();

            // falthrough switch base on dictionary
            new Switch<IDictionary<char, string[]>>(attributeGroups, true)
                    .Case(attributeGroups.ContainsKey(Id), () =>
                        {
                            if (attributeGroups[Id].Length > 1)
                            {
                                throw new InvalidOperationException("Cannot specify two Ids for element " + node);
                            }

                            parsedNode.Id = attributeGroups[Id][0];
                        })
                    .Case(attributeGroups.ContainsKey(Class), () => parsedNode.Class = attributeGroups[Class][0])
                    .Case(attributeGroups.ContainsKey(Text), () => parsedNode.AddChild(new TextElement(attributeGroups[Text][0])));

            return parsedNode;
        }
    }
}