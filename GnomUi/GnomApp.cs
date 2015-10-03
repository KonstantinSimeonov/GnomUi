namespace GnomUi
{
    using GnomUi.Contracts;
    using System;
    using System.Linq;

    public class GnomApp : IGnomApp
    {
        public GnomApp(IGnomTree view, IPressable startingElement, Action<GnomEventArgs> appMethod)
        {
            IPressable selected = startingElement;

            view.Root.Display(0, 0);

            while (true)
            {
                var keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    selected.FireEvent();
                }
                else if(selected.Neighbors.ContainsKey(keyInfo.Key))
                {
                    selected.IsSelected = false;
                    selected = selected.Neighbors[keyInfo.Key];
                    selected.IsSelected = true;
                }
                else
                {
                    var args = new GnomEventArgs(view, selected, keyInfo);
                    appMethod(args);
                }

                view.Root.Display(0, 0);
            }
        }

        public void Start()
        {
            
        }
    }
}