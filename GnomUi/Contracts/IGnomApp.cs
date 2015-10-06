namespace GnomUi.Contracts
{
    using System;

    using GnomUi.EventModel;

    public interface IGnomApp
    {
       IPressable Selected { get; set; }

       IConsoleManipulator Manipulator { get; set; }

       IGnomTree View { get; set; }

       Action<GnomEventArgs> KeyRoutingMethod { get; set; }

        void Start();
    }
}