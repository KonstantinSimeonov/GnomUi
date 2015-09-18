﻿namespace GnomUi.Contracts
{
    public interface IElement
    {
        string Id { get; set; }
        string Class { get; set; }
        IStyle Style { get; set; }
        void Display(int x, int y);
    }
}
