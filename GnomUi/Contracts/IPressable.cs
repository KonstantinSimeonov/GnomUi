namespace GnomUi.Contracts
{
    using System;

    using GnomUi.EventModel;

    public interface IPressable : ISelectable
    {
        Action<GnomEventArgs> OnClick { get; set; }

        void FireEvent(GnomEventArgs args);
    }
}