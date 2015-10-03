namespace Interpreter.Contracts
{
    using GnomUi.Contracts;
    using System.Collections.Generic;

    internal interface IGnomStyleParser
    {
        IDictionary<string, IStyle> ParseStyles(string stylesheet);
    }
}