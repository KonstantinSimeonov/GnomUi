namespace GnomInterpreter.Contracts
{
    using System.Collections.Generic;

    using GnomUi.Contracts;

    internal interface IGnomStyleParser
    {
        IDictionary<string, IStyle> ParseStyles(string stylesheet);
    }
}