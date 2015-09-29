namespace GnomUi
{
    using System.Collections.Generic;

    using GnomUi.Contracts;;

    public class GnomTree : IGnomTree
    {
        private readonly IDictionary<string, IStyle> styles;
        private readonly IDictionary<string, INodeElement> idMap;

        public INodeElement Root{get; private set;}

        public GnomTree(INodeElement root)
        {
            this.Root = root;
            this.styles = new Dictionary<string, IStyle>();
        }

        public GnomTree(INodeElement root, IDictionary<string, INodeElement> idMap)
            :this(root)
        {
            this.idMap = idMap;
        }

        public IDictionary<string, IStyle> Styles
        {
            get
            {
                return this.styles;
            }
        }
    }
}
