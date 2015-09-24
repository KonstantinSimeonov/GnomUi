namespace GnomUi.Contracts
{
    using System;

    public interface ISelectable : IGnomeGraphElement
    {
        bool IsSelected { get; set; }

        void AddNeighbor(ConsoleKey key, ISelectable element);
    }
}