namespace GnomUi.Contracts
{
    public interface IElement
    {
        IStyle Style { get; set; }
        void Display(int x, int y);
    }
}
