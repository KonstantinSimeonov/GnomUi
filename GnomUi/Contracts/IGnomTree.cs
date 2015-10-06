namespace GnomUi.Contracts
{
    using System.Collections.Generic;

    public interface IGnomTree : IEnumerable<IElement>
    {
        INodeElement Root { get; }

        INodeElement this[string id] { get; set; }

        void AddChildToParent(INodeElement parent, INodeElement child);
    }
}