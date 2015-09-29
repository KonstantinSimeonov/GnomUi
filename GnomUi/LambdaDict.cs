namespace GnomUi
{
    using System;
    using System.Collections.Generic;

    public static class LambdaDict
    {
        public static IDictionary<string, R> Init<T, R>(this IDictionary<string, R> dictionary, params Func<T, R>[] pairs)
        {
            var def = default(T);
            foreach (var pair in pairs)
            {
                dictionary.Add(pair.Method.GetParameters()[0].ToString(), pair(def));
            }

            return dictionary;
        }
    }
}
