namespace GnomUi.Drawing
{
    using System;

    using GnomUi.Contracts;

    public class ConsoleManipulator : IConsoleManipulator
    {
        public void DrawGnomTree(IGnomTree tree, int topStart = 0, int leftStart = 0)
        {
            DrawTreeRec(tree.Root, topStart, leftStart);
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
            foreach (var line in renderedElement)
            {
                Console.SetCursorPosition(left, top++);
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
