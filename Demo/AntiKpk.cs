namespace Demo
{
    using System;
    using System.Collections.Generic;

    using GnomUi;
    using GnomUi.Contracts;

    using Interpreter.Core;

    class AntiKpk
    {
        static void Main()
        {
            GnomCompositeUiDemo();
            //var test = new IndentParser();
            //var input = new string[] 
            //{
            //    "root #r .doc",
            //    "    child1 #header .nav-bar",
            //    "    child2 #container .full-size",
            //    ""
            //};

            //var result = test.Parse(input);

            //result.Display(0, 0);

            //var dict = new Dictionary<string, int>().Init<string, int>(x => 3, y => 8, z => 10);

            //foreach (var item in dict)
            //{
            //    Console.WriteLine(item);
            //}

        }

        public static void GnomCompositeUiDemo()
        {
            // convenience
            var keys = new Dictionary<string, ConsoleKey>()
           {
                {"u", ConsoleKey.UpArrow},
                {"d", ConsoleKey.DownArrow},
                {"l", ConsoleKey.LeftArrow},
                {"r", ConsoleKey.RightArrow}

           };

            // sample elements

            var box = new Node();
            box.Style = new Style()
            {
                PaddingLeft = 1,
                PaddingTop = 1,
                Color = ConsoleColor.Red,
                Width = 30,
                Height = 15
            };

            var btn = new Node();
            btn.Style = new Style()
            {
                PaddingLeft = 1,
                PaddingTop = 1,
                Color = ConsoleColor.Green,
                Width = 8,
                Height = 3
            };



            var txt = new TextElement("btn");
            txt.Style = new Style()
            {
                PaddingLeft = 1,
                PaddingTop = 1,
                Color = ConsoleColor.White,
                //Width = 5,
                //Height = 1
            };

            var div = new Node();
            div.Style = new Style()
            {
                PaddingLeft = 15,
                PaddingTop = 1,
                Color = ConsoleColor.Blue,
                Width = 10,
                Height = 5
            };

            var kur = new TextElement("div");
            kur.Style = new Style()
            {
                PaddingLeft = 1,
                PaddingTop = 1,
                Color = ConsoleColor.Yellow,
                Width = 3,
                Height = 1
            };

            div.AddChild(kur);
            btn.AddChild(txt);

            box.AddChild(btn);
            box.AddChild(div);

            var btn2 = new Node(true);
            btn2.Style = new Style()
            {
                PaddingLeft = 10,
                PaddingTop = 9,
                Color = ConsoleColor.Magenta,
                Width = 12,
                Height = 3
            };
            btn2.OnClick = (target) => { Console.BackgroundColor = ConsoleColor.DarkGreen; };
            var txt3 = new TextElement("btn2");
            txt3.Style = new Style();

            // link the elements in a graph

            btn.LinkTo(keys["r"], div);
            btn.LinkTo(keys["d"], btn2);
            div.LinkTo(keys["d"], btn2);

            btn2.AddChild(txt3);

            box.AddChild(btn2);

            box.Display(0, 0);

            Console.SetCursorPosition(0, 20);

            //var keyInfo = Console.ReadKey();

            //if(keyInfo.Key == ConsoleKey.Enter)
            //{
            //    btn2.FireEvent();
            //}


            // attach event functions
            btn.OnClick = target =>
            {
                Console.BackgroundColor = ConsoleColor.White;
            };

            ISelectable selected = btn2;

            Console.CursorVisible = false;

            // traverse the UI graph with the keyboard arrows
            while (true)
            {
                var keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.Enter && selected as IPressable != null)
                {
                    (selected as IPressable).FireEvent();
                }
                else if (selected.Neighbors.ContainsKey(keyInfo.Key))
                {
                    selected.IsSelected = false;
                    selected = selected.Neighbors[keyInfo.Key];
                    selected.IsSelected = true;
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    break;
                }

                Console.Clear();
                box.Display(0, 0);
            }
        }
    }
}