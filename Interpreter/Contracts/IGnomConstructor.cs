namespace GnomInterpreter.Contracts
{
    using GnomUi.Contracts;

    public interface IGnomConstructor
    {
        IGnomTree Construct(string treeDescription, string selectionGraph = "", string stylesheet = "");
    }
}