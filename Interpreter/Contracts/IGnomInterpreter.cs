namespace Interpreter.Contracts
{
    using GnomUi.Contracts;

    public interface IGnomInterpreter
    {
        INodeElement ParseToGnomTree(string gnomDsl);
    }
}