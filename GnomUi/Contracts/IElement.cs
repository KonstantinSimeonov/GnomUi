namespace GnomUi.Contracts
{
    public interface IElement : IPressable
    {
        string Id { get; set; }
        string Class { get; set; }
        IStyle Style { get; set; }
        INodeElement Parent { get; }

        string[] ToStringArray();
    }
}
