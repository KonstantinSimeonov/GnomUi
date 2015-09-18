namespace GnomUi.Contracts
{
    using System.Collections.Generic;

    public interface INodeElement : IElement, IPressable
    {
        IList<IElement> Children { get; }
        INodeElement AddChild(IElement element);
        INodeElement RemoveElement(IElement element);
    }
}