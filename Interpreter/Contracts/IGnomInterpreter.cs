namespace GnomInterpreter.Contracts
{
    using GnomUi.Contracts;

    internal interface IGnomInterpreter
    {
        IGnomTree Parse(string gnomDsl);
    }
}