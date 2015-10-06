namespace GnomUi
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using GnomUi.Contracts;

    public class GnomTree : IGnomTree, IEnumerable<IElement>
    {
        private readonly IDictionary<string, INodeElement> idMap;
        private readonly IDictionary<string, IList<INodeElement>> classMap;

        public INodeElement Root { get; private set; }

        public GnomTree(INodeElement root)
        {
            this.Root = root;
        }

        public GnomTree(INodeElement root, IDictionary<string, INodeElement> idMap, IDictionary<string, IList<INodeElement>> classMap)
            : this(root)
        {
            this.classMap = classMap;
            this.idMap = idMap;
        }

        public void AddChildToParent(INodeElement parent, INodeElement child)
        {
            parent.Children.Add(child);
            this.idMap.Add(child.Id, child);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public INodeElement this[string id]
        {
            get
            {
                return this.idMap[id];
            }

            set
            {
                if(!this.idMap.ContainsKey(value.Id))
                {
                    this.idMap.Add(value.Id, value);
                }
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
