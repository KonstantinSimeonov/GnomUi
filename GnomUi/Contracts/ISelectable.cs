namespace GnomUi.Contracts
{
    public interface ISelectable : IGnomeGraphElement
    {
        bool IsSelected { get; set; }
    }
}