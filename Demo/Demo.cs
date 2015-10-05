namespace Demo
{
    using System;
    using System.Collections.Generic;

    using GnomUi;
    using GnomUi.Contracts;

    using Interpreter;
    using GnomUi.TreeComponents;
    using GnomUi.Drawing;

    class Demo
    {
        static void Main()
        {
            Console.CursorVisible = false;
            // GnomCompositeUiDemo();
            var uiDescription = @"root #root .root-element
    box #container .div
        field #field .field
        button #restart .btn1 :restart
        button #undo .btn2 :undo
        button #exit .btn3 :exit
        button #top .btn4 :top";
            var styles = @".div
width 50
height 20
color blue

.root-element
color black
width 2
height 2
left 2
top 2

.field
width 28
height 11
color red

.root
width 50
height 30
color green

.btn1
left 4
top 12
height 4
width 10
color blue

.btn2
left 16
top 12
height 4
width 8
color blue

.btn3
left 26
top 12
height 4
width 8
color blue

.btn4
left 36
top 12
height 4
width 8
color blue";
            var graph = @"restart # undo # #
undo restart exit # #
exit undo top # #
top exit # # #";

            var gnomBuilder = ParserProvider.GetGnomConstructor();
            var result = gnomBuilder.Construct(uiDescription, graph, styles);
            var drawer = new ConsoleManipulator();
            var matrix = new string[,]{
                {"1", "2", "3"},
                {"4", "5", "6"}
            };
            var startingSelection = AddMatrixToGnom(result["field"], matrix);
            startingSelection.LinkTo(ConsoleKey.DownArrow, result["restart"], true);
            var app = new GnomApp(result, startingSelection, drawer, x => { });
            app.Start();
            //drawer.DrawGnomTree(result);
        }

        public static IPressable AddMatrixToGnom(INodeElement field, string[,] matrix)
        {
            var result = new TextElement[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    result[i, j] = new TextElement(matrix[i, j])
                    {
                        Style = new Style()
                        {
                            PaddingLeft = j * 2,
                            PaddingTop = i * 2,
                            Color = ConsoleColor.Green
                        }
                    };

                    if (i > 0)
                    {
                        result[i, j].LinkTo(ConsoleKey.UpArrow, result[i - 1, j]);
                    }

                    if (j > 0)
                    {
                        result[i, j].LinkTo(ConsoleKey.LeftArrow, result[i, j - 1]);
                    }

                }
            }

            result[result.GetLength(0) - 1, 0].IsSelected = true;

            foreach (var node in result)
            {
                field.AddChild(node);
            }

            return result[result.GetLength(0) - 1, 0];
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

            var box = new Element(false);
            box.Style = new Style()
            {
                PaddingLeft = 1,
                PaddingTop = 1,
                Color = ConsoleColor.Red,
                Width = 30,
                Height = 15
            };

            var btn = new Element(false);
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

            var div = new Element(false);
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

            var btn2 = new Element(false);
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

            //box.Display(0, 0);

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
                //box.Display(0, 0);
            }
        }
    }
}