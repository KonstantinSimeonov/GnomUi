namespace Interpreter
{
    internal static class ImAnnoyed
    {
        public static string Format(this string format, params object[] stuff)
        {
            return string.Format(format, stuff);
        }

        public static int Depth(this string node)
        {
            var i = 0;

            while (i < node.Length && node[i++] == ' ')
            {
            }

            return i / 4;
        }
    }
}