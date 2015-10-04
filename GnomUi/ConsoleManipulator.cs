using GnomUi.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnomUi
{
    public class ConsoleManipulator
    {
        public void DrawGnomTree(IGnomTree tree)
        {
            DrawTreeRec(tree.Root, 0, 0);
        }

        private static void DrawTreeRec(INodeElement node, int x, int y)
        {
            InitializeAbsolutePadding(node, x, y);
            ApplyNodeStyleToConsole(node);

            var renderedElement = node.ToStringArray();

            DrawStringArray(renderedElement, node.Style.AbsPaddingTop, node.Style.AbsPaddingLeft);

            foreach (var childNode in node.Children)
            {
                DrawTreeRec(childNode, node.Style.AbsPaddingLeft + 1, node.Style.AbsPaddingTop + 1);
            }
        }

        private static void DrawStringArray(string[] renderedElement, int top, int left)
        {
            var row = top;
            foreach (var line in renderedElement)
            {
                Console.SetCursorPosition(left, row++);
                Console.WriteLine(line);
            }
        }

        public static void InitializeAbsolutePadding(INodeElement node, int x, int y)
        {
            node.Style.AbsPaddingLeft = node.Style.PaddingLeft + x;
            node.Style.AbsPaddingTop = node.Style.PaddingTop + y;
        }

        public static void ApplyNodeStyleToConsole(INodeElement node)
        {
            if (node.IsSelected)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = node.Style.Color;
            }

            Console.SetCursorPosition(node.Style.AbsPaddingLeft, node.Style.AbsPaddingTop);
        }
    }
}
