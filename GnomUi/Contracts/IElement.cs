namespace GnomUi.Contracts
{
    using System.Collections.Generic;

    public interface IElement : IPressable
    {
        string Id { get; set; }
        string Class { get; set; }
        IStyle Style { get; set; }
        INodeElement Parent { get; }

        string[] ToStringArray();
    }
}
