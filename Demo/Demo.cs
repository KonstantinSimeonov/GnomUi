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

            result["restart"].OnClick = (x => Console.BackgroundColor = ConsoleColor.Red);
            result["undo"].OnClick = x => Console.BackgroundColor = ConsoleColor.Blue;
            result["exit"].OnClick = x => Environment.Exit(0);

            app.Start();
            //drawer.DrawGnomTree(result);
        }

        public static IPressable AddMatrixToGnom(INodeElement field, string[,] matrix)
        {
            var result = new TextElement[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0;i < matrix.GetLength(0);i++)
            {
                for (int j = 0;j < matrix.GetLength(1);j++)
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
    }
}