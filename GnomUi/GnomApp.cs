namespace GnomUi
{
    using System;
    using System.Linq;

    using GnomUi.Contracts;
    using GnomUi.EventModel;

    public class GnomApp : IGnomApp
    {
        public GnomApp(IGnomTree view, IPressable startingElementId, IConsoleManipulator manipulator, Action<GnomEventArgs> keyRoutingMethod)
        {
            this.View = view;
            this.KeyRoutingMethod = keyRoutingMethod;
            this.Selected = startingElementId;
            this.Manipulator = manipulator;
        }

        public IPressable Selected { get; set; }

        public IConsoleManipulator Manipulator { get; set; }

        public IGnomTree View { get; set; }

        public Action<GnomEventArgs> KeyRoutingMethod { get; set; }

        public void Start()
        {
            this.Manipulator.DrawGnomTree(this.View);

            while (true)
            {
                var keyInfo = Console.ReadKey();
                var args = new GnomEventArgs(this.View, this.Selected, keyInfo);

                if (this.Selected.Neighbors.ContainsKey(keyInfo.Key))
                {
                    this.Selected.IsSelected = false;
                    this.Selected = this.Selected.Neighbors[keyInfo.Key];
                    this.Selected.IsSelected = true;
                }
                else
                {
                    this.KeyRoutingMethod(args);
                }

                this.Manipulator.DrawGnomTree(this.View);
            }
        }
    }
}