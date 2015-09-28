namespace Interpreter
{
    using System;
    using System.Collections.Generic;

    using GnomUi;
    using GnomUi.Contracts;

    using Interpreter.Contracts;

    public class Core : IGnomInterpreter
    {
        public Core()
        {
        }
        
        public INodeElement ParseToGnomTree(string gnomDsl)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }
    }
}