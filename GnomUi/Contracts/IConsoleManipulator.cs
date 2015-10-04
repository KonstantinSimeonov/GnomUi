namespace GnomUi.Contracts
{
    public interface IConsoleManipulator
    {
        void DrawGnomTree(IGnomTree tree, int topStart = 0, int leftStart = 0);
    }
}