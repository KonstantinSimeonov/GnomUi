using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GnomUi;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //var topBottomBorder = ' '+new string('_', 18);
            //var sideBottomBorder = string.Join(string.Empty, Enumerable.Repeat("=\n", 10));
            ////Console.SetCursorPosition(0, 1);
            ////Console.Write(sideBottomBorder);
            ////Console.SetCursorPosition(0, 0);
            ////Console.Write(topBottomBorder);
            ////Console.SetCursorPosition(20, 1);
            ////Console.Write(sideBottomBorder);
            ////Console.SetCursorPosition(0, 11);
            ////Console.Write(topBottomBorder);

            //var result = new StringBuilder();
            //result.AppendLine(topBottomBorder);
            //for (int i = 0; i < 7; i++)
            //{
            //    result.AppendLine('|' + new string(' ', 18) + '|');
            //}
            //result.Append('|' + new string('_', 18) +'|');

            //Console.WriteLine(result);

            var box = new Node();
            box.style = new Style() 
            {
                PaddingLeft = 1,
                PaddingTop = 1,
                Color = ConsoleColor.Red,
                Width = 30,
                Height = 15
            };

            var btn = new Node();
            btn.style = new Style()
            {
                PaddingLeft = 1,
                PaddingTop = 1,
                Color = ConsoleColor.Green,
                Width = 8,
                Height = 3
            };

            var txt = new TextElement("Click");
            txt.style = new Style()
            {
                PaddingLeft = 1,
                PaddingTop = 1,
                Color = ConsoleColor.White,
                //Width = 5,
                //Height = 1
            };

            var div = new Node();
            div.style = new Style()
            {
                PaddingLeft = 15,
                PaddingTop = 1,
                Color = ConsoleColor.Blue,
                Width = 10,
                Height = 5
            };

            var kur = new TextElement("kur");
            kur.style = new Style()
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

            box.Display(0, 0);

            Console.SetCursorPosition(0, 15);
        }
    }
}
