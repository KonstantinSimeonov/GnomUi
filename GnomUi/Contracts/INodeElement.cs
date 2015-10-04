namespace GnomUi.Contracts
{
    using System.Collections.Generic;

    public interface INodeElement : IElement
    {
        IList<INodeElement> Children { get; }
        INodeElement AddChild(INodeElement element);
        INodeElement RemoveElement(INodeElement element);
    }
}