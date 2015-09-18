namespace GnomUi.Contracts
{
    using System;

    public interface IPressable
    {
        Action<IElement> OnClick { get; set; }
    }
}