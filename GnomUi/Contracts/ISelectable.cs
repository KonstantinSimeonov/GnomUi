namespace GnomUi.Contracts
{
    using System;

    public interface ISelectable : IGnomeGraphElement
    {
        bool IsSelected { get; set; }

        void LinkTo(ConsoleKey key, ISelectable element);
    }
}