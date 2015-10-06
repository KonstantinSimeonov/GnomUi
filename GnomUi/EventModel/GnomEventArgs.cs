namespace GnomUi.EventModel
{
    using System;

    using GnomUi.Contracts;
    
    public class GnomEventArgs
    {
        public ConsoleKeyInfo PressedKeyInfo { get; internal set; }

        public IGnomTree View { get; internal set; }

        public IPressable Target { get; internal set; }

        public GnomEventArgs(IGnomTree view, IPressable target, ConsoleKeyInfo keyInfo)
        {
            this.View = view;
            this.Target = target;
            this.PressedKeyInfo = keyInfo;
        }
    }
}
