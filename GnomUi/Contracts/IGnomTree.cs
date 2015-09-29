namespace GnomUi.Contracts
{
    using System.Collections.Generic;

    public interface IGnomTree : IEnumerable<IElement>
    {
        INodeElement Root { get; }
        IDictionary<string, IStyle> Styles { get; set; }
    }
}