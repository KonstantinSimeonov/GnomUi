namespace GnomUi.Contracts
{
    using System.Collections.Generic;

    public interface IGnomTree : IEnumerable<IElement>
    {
        INodeElement Root { get; }

        IElement this[string id] { get; }
    }
}