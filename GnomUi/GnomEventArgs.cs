using GnomUi.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnomUi
{
    public class GnomEventArgs
    {
        public ConsoleKeyInfo PressedKeyInfo { get; private set; }

        public IGnomTree View { get; private set; }

        public IPressable Target { get; private set; }

        public GnomEventArgs(IGnomTree view, IPressable target, ConsoleKeyInfo keyInfo)
        {
            this.View = view;
            this.Target = target;
            this.PressedKeyInfo = keyInfo;
        }
    }
}
