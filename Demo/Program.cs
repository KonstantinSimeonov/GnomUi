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
        static void Main()
        {
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
            div.Style = new Style()
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
