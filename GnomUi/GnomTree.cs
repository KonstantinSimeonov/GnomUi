namespace GnomUi
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using GnomUi.Contracts;

    public class GnomTree : IGnomTree, IEnumerable<IElement>
    {
        private readonly IDictionary<string, IElement> idMap;
        private readonly IDictionary<string, IList<IElement>> classMap;

        public INodeElement Root { get; private set; }

        public GnomTree(INodeElement root)
        {
            this.Root = root;
        }

        public GnomTree(INodeElement root, IDictionary<string, IElement> idMap, IDictionary<string, IList<IElement>> classMap)
            : this(root)
        {
            this.classMap = classMap;
            this.idMap = idMap;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {


            return this.GetEnumerator();
        }

        public IElement this[string id]
        {
            get
            {
                return this.idMap[id];
            }
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
