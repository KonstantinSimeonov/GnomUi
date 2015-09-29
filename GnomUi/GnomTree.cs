namespace GnomUi
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using GnomUi.Contracts;

    public class GnomTree : IGnomTree, IEnumerable<IElement>
    {
        private IDictionary<string, IStyle> styles;
        private readonly IDictionary<string, IElement> idMap;
        private readonly IDictionary<string, IList<IElement>> classMap;

        public INodeElement Root { get; private set; }

        public GnomTree(INodeElement root)
        {
            this.Root = root;
            this.styles = new Dictionary<string, IStyle>();


        }

        public GnomTree(INodeElement root, IDictionary<string, IElement> idMap, IDictionary<string, IList<IElement>> classMap, IDictionary<string, IStyle> styleMap)
            : this(root)
        {
            this.styles = styleMap;
            this.classMap = classMap;
            this.idMap = idMap;
        }

        public IDictionary<string, IStyle> Styles
        {
            get
            {
                return this.styles;
            }

            set
            {
                this.styles = value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {


            return this.GetEnumerator();
        }

        public IEnumerator<IElement> GetEnumerator()
        {
            var nodes = new Stack<IElement>();

            nodes.Push(this.Root);

            while (nodes.Count > 0)
            {
                var current = nodes.Pop();

                yield return current;

                if (current as INodeElement != null)
                {
                    foreach (var node in (current as INodeElement).Children)
                    {
                        nodes.Push(node);
                    }
                }
            }
        }
    }
}
