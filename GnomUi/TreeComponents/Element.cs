namespace GnomUi.TreeComponents
{
    using System;
    using System.Collections.Generic;

    using EventModel;
    using GnomUi.Contracts;

    public class Element : INodeElement, IPressable
    {
        private static readonly IDictionary<ConsoleKey, ConsoleKey> reverseKeys = new Dictionary<ConsoleKey, ConsoleKey>()
        {
            { ConsoleKey.UpArrow, ConsoleKey.DownArrow },
            { ConsoleKey.LeftArrow, ConsoleKey.RightArrow },
            { ConsoleKey.DownArrow, ConsoleKey.UpArrow },
            { ConsoleKey.RightArrow, ConsoleKey.LeftArrow }
        };

        private INodeElement parent;

        public Element(bool selected = false, string id = "", string className = "")
        {
            this.Style = new Style();
            this.Id = id;
            this.Class = className;
            this.IsSelected = selected;
            this.Neighbors = new Dictionary<ConsoleKey, IPressable>();
            this.Children = new List<INodeElement>();
        }

        public string Id { get; set; }

        public string Class { get; set; }

        public IStyle Style { get; set; }

        public bool IsSelected { get; set; }

        public IDictionary<ConsoleKey, IPressable> Neighbors { get; private set; }

        public IList<INodeElement> Children { get; private set; }

        public Action<GnomEventArgs> OnClick { get; set; }

        // TODO: implement setting in parser
        public INodeElement Parent
        {
            get
            {
                return this.parent;
            }

            set
            {
                // a validation : O
                if (value != null && this.parent != null)
                {
                    throw new InvalidOperationException("gnom doesnt allow changing the parent of an element that already has a parent");
                }

                this.parent = value;
            }
        }

        public void LinkTo(ConsoleKey key, IPressable element, bool doubly = true)
        {
            if(!this.Neighbors.ContainsKey(key))
            {
                this.Neighbors.Add(key, element);
            }
            else
            {
                this.Neighbors[key] = element;
            }

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

        public void FireEvent(GnomEventArgs args)
        {
            if (this.OnClick == null)
            {
                return;
            }

            this.OnClick(args);
        }

        public INodeElement AddChild(INodeElement element)
        {
            element.Parent = this;

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
            char colBorderChar = '_';
            char rowBorderChar = '|';

            // building the top/bottom border and adding it
            string topBottomBorder = ' ' + new string(colBorderChar, this.Style.Width - 2);
            var result = new string[this.Style.Height];
            result[0] = topBottomBorder;

            // building the side borders
            for (int i = 1; i < this.Style.Height - 1; i++)
            {
                result[i] = rowBorderChar + new string(' ', this.Style.Width - 2) + rowBorderChar;
            }

            // adding the bottom border.
            result[result.Length - 1] = rowBorderChar + new string(colBorderChar, this.Style.Width - 2) + rowBorderChar;

            return result;
        }
    }
}
