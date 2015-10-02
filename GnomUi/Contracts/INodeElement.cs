namespace GnomUi.Contracts
{
    using System.Collections.Generic;

    public interface INodeElement : IElement
    {
        IList<IElement> Children { get; }
        INodeElement AddChild(IElement element);
        INodeElement RemoveElement(IElement element);
    }
}