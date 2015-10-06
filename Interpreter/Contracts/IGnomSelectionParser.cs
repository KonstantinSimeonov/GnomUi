namespace GnomInterpreter.Contracts
{
    using System.Collections.Generic;

    internal interface IGnomSelectionParser
    {
        IDictionary<string, IList<string>> ParseSelections(string selectionDescription);
    }
}