namespace GnomUi.Contracts
{
    public interface IConsoleManipulator
    {
        bool RefreshConsole { get; set; }

        void DrawGnomTree(IGnomTree tree, int topStart = 0, int leftStart = 0);
    }
}