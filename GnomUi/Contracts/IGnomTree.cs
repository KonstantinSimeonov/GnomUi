namespace GnomUi.Contracts
{
    using System.Collections.Generic;

    public interface IGnomTree
    {
        INodeElement Root { get; }
        IDictionary<string, IStyle> Styles { get; }
    }
}