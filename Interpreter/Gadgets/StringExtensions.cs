namespace Interpreter.Gadgets
{
    using System;

    internal static class StringExtensions
    {
        /// <summary>
        /// Does the same as string.Format.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="stuff"></param>
        /// <returns></returns>
        public static string Format(this string format, params object[] stuff)
        {
            return string.Format(format, stuff);
        }

        /// <summary>
        /// Removes the first character of the string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveFirst(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            return str.Remove(0, 1);
        }

        /// <summary>
        /// Calculates the depth based on indentation. 4 spaces = 1 indentation.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static int Depth(this string node)
        {
            var i = 0;

            while (i < node.Length && node[i++] == ' ')
            {
            }

            return i / 4;
        }

        public static string[] SplitBy(this string str, params string[] splitSymbols)
        {
            return str.Split(splitSymbols, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}