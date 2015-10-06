namespace GnomInterpreter
{
    using GnomInterpreter.Contracts;
    using GnomInterpreter.Core;

    public static class ParserProvider
    {
        private static readonly IGnomConstructor constructor = new GnomConstructor(new GnomMarkupParser(), new GnomStyleParser(), new SelectionGraphParser());

        public static IGnomConstructor GetGnomConstructor()
        {
            return constructor;
        }
    }
}