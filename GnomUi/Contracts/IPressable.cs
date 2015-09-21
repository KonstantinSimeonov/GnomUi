namespace GnomUi.Contracts
{
    using System;

    public interface IPressable : ISelectable
    {
        Action<IElement> OnClick { get; set; }

        void FireEvent();
    }
}