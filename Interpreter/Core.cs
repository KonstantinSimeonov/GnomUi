namespace Interpreter.Core
{
    using System;
    using System.Collections.Generic;

    using GnomUi;
    using GnomUi.Contracts;

    using Interpreter.Contracts;

    public class InterpreterCore : IGnomInterpreter
    {
        public InterpreterCore()
        {
        }
        
        public INodeElement ParseToGnomTree(string gnomDsl)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }
    }
}