namespace Interpreter
{
    using Interpreter.Contracts;
    using Interpreter.Core;

    public class ParserProvider
    {
        private static readonly IGnomConstructor constructor = new GnomConstructor(new GnomInterpreter(), new GnomStyleParser(), new SelectionGraphParser());

        public static IGnomConstructor GetGnomConstructor()
        {
            return constructor;
        }
    }
}