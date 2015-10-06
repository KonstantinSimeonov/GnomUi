namespace GnomUi.Contracts
{
    using System;

    public interface IStyle
    {
        int AbsPaddingLeft { get; set; }

        int AbsPaddingTop { get; set; }

        int PaddingLeft { get; set; }

        int PaddingTop { get; set; }

        int Width { get; set; }

        int Height { get; set; }

        ConsoleColor Color { get; set; }

        string this[string propertyName] { get; set; }
    }
}
