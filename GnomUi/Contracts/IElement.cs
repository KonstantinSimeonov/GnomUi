namespace GnomUi.Contracts
{
    using System.Collections.Generic;

    public interface IElement
    {
        string Id { get; set; }
        string Class { get; set; }
        IStyle Style { get; set; }
        INodeElement Parent { get; }

        void Display(int x, int y);
    }
}
