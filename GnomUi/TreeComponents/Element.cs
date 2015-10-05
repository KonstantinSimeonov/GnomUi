namespace GnomUi.TreeComponents
{
    using System;

    using System.Collections.Generic;
    using GnomUi.Contracts;

    public class Element : INodeElement, IPressable
    {
        private static readonly IDictionary<ConsoleKey, ConsoleKey> reverseKeys = new Dictionary<ConsoleKey, ConsoleKey>()
        {
            { ConsoleKey.UpArrow, ConsoleKey.DownArrow },
            { ConsoleKey.LeftArrow, ConsoleKey.RightArrow },
            { ConsoleKey.DownArrow, ConsoleKey.UpArrow },
            { ConsoleKey.RightArrow, ConsoleKey.LeftArrow}
        };

        private INodeElement parent;

        public string Id { get; set; }

        public string Class { get; set; }

        public IStyle Style { get; set; }

        public Element(bool selected = false, string id = "", string className = "")
        {
            this.Style = new Style();
            this.Id = id;
            this.Class = className;
            this.IsSelected = selected;
            this.Neighbors = new Dictionary<ConsoleKey, IPressable>();
            this.Children = new List<INodeElement>();
        }

        public void LinkTo(ConsoleKey key, IPressable element, bool doubly = true)
        {
            this.Neighbors.Add(key, element);

            if (!element.Neighbors.ContainsKey(reverseKeys[key]) || doubly)
            {
                if (!element.Neighbors.ContainsKey(reverseKeys[key]))
                {
                    element.Neighbors.Add(reverseKeys[key], this);
                }
                else
                {
                    element.Neighbors[reverseKeys[key]] = this;
                }
            }
        }

        // TODO: should create new GnomEventArgs and pass then to OnClick
        public void FireEvent()
        {
            this.OnClick(this);
        }

        // TODO: OnClick event should be Action<GnomEventArgs>
        public Action<IElement> OnClick { get; set; }

        public bool IsSelected { get; set; }

        public IDictionary<ConsoleKey, IPressable> Neighbors { get; private set; }

        public IList<INodeElement> Children { get; private set; }

        // TODO: implement setting in parser
        public INodeElement Parent
        {
            get
            {
                return this.parent;
            }

            private set
            {
                // a validation : O
                if (value != null && this.parent != null)
                {
                    throw new InvalidOperationException("gnom doesnt allow changing the parent of an element that already has a parent");
                }

                this.parent = value;
            }
        }

        // ISSUE: AddChild doesn't add the element's id to the GnomTree id map
        public INodeElement AddChild(INodeElement element)
        {
            this.Children.Add(element);
            return this;
        }

        // ISSUE: RemoveElement doesn't remove the element's id from the GnomTree id map
        public INodeElement RemoveElement(INodeElement element)
        {
            if (this.Children.Contains(element))
            {
                this.Children.Remove(element);
            }

            return this;
        }

        // TODO: terri-ugly code, needs some shining
        public virtual string[] ToStringArray()
        {
            var topBottomBorder = ' ' + new string('_', this.Style.Width - 2);
            var result = new string[this.Style.Height];
            result[0] = (topBottomBorder);

            for (int i = 1; i < this.Style.Height - 1; i++)
            {
                result[i] = ('|' + new string(' ', this.Style.Width - 2) + '|');
            }

            result[result.Length - 1] = ('|' + new string('_', this.Style.Width - 2) + '|');

            return result;
        }
    }
}
