namespace GnomUi.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface ISelectable
    {
        IDictionary<ConsoleKey, IPressable> Neighbors { get; }

        bool IsSelected { get; set; }

        void LinkTo(ConsoleKey key, IPressable element, bool doubly = true);
    }
}