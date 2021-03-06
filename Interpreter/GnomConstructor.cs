﻿namespace GnomInterpreter
{
    using System;
    using System.Collections.Generic;

    using Contracts;
    using Core;

    using GnomUi.Contracts;

    internal class GnomConstructor : IGnomConstructor
    {
        private const int NeighborCount = 4;

        private static readonly ConsoleKey[] directionKeysMap = new ConsoleKey[]
        { 
            ConsoleKey.LeftArrow, 
            ConsoleKey.RightArrow,
            ConsoleKey.UpArrow, 
            ConsoleKey.DownArrow 
        };

        private readonly GnomMarkupParser structureParser;

        private readonly GnomStyleParser styleParser;

        private readonly SelectionGraphParser selectionParser;

        internal GnomConstructor(GnomMarkupParser structureParser, GnomStyleParser styleParser, SelectionGraphParser selectionParser)
        {
            this.selectionParser = selectionParser;
            this.styleParser = styleParser;
            this.structureParser = structureParser;
        }

        public IGnomTree Construct(string treeDescription, string selectionGraph, string stylesheet)
        {
            var result = this.structureParser.Parse(treeDescription);

            var selectionsMap = this.selectionParser.ParseSelections(selectionGraph);
            ApplySelectionMapToTree(result, selectionsMap);

            var styles = this.styleParser.ParseStyles(stylesheet);
            ApplyStyleMapToTree(result, styles);

            return result;
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
            foreach (var nodeLinkInfo in seletionMap)
            {
                for (int i = 0; i < NeighborCount; i++)
                {
                    var key = directionKeysMap[i];
                    var neighborForKey = tree[nodeLinkInfo.Value[i]];
                    tree[nodeLinkInfo.Key].Neighbors.Add(key, neighborForKey);
                }
            }
        }
    }
}
