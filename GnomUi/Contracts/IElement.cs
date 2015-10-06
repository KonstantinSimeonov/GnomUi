namespace GnomUi.Contracts
{
    public interface IElement : IPressable, ISelectable
    {
        string Id { get; set; }

        string Class { get; set; }

        IStyle Style { get; set; }

        INodeElement Parent { get; set; }

        string[] ToStringArray();
    }
}