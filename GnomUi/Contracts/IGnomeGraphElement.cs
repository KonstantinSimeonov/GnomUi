namespace GnomUi.Contracts
{
    using System;

    using System.Collections.Generic;

    public interface IGnomeGraphElement
    {
        IDictionary<ConsoleKey, ISelectable> Neighbors { get; }
    }
}