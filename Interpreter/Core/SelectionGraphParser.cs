namespace Interpreter.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Interpreter.Contracts;

    internal class SelectionGraphParser : IGnomSelectionParser
    {
        private const int NodeLinksCount = 4;
        private const StringSplitOptions NoOptions = StringSplitOptions.None;
        private const StringSplitOptions RemoveEmpty = StringSplitOptions.RemoveEmptyEntries;

        internal SelectionGraphParser()
        {
        }

        public IDictionary<string, IList<string>> ParseSelections(string selectionDescription)
        {
            return ParseGnomSelectionMap(selectionDescription);
        }

        private static IDictionary<string, IList<string>> ParseGnomSelectionMap(string selectionMap)
        {
            var selectionMapRows = selectionMap.Split(new string[] { Environment.NewLine }, NoOptions);
            var result = new Dictionary<string, IList<string>>();

            foreach (var nodeMapping in selectionMapRows)
            {
                var nodesAsStringArray = nodeMapping.Split(new char[] { ' ' }, RemoveEmpty)
                                                    .Select(x => x.ToLower())
                                                    .ToArray();

                var nodeId = nodesAsStringArray[0];

                if (!result.ContainsKey(nodeId))
                {
                    result.Add(nodeId, new List<string>(NodeLinksCount));
                }

                for (int i = 1; i <= NodeLinksCount; i++)
                {
                    var nodeToAdd = nodesAsStringArray[i] == "#" ? nodeId : nodesAsStringArray[i];
                    result[nodeId].Add(nodeToAdd);
                }
            }

            return result;
        }
    }
}