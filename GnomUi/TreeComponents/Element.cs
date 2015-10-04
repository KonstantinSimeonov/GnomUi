﻿namespace GnomUi.TreeComponents
{
    using System;

    using System.Collections.Generic;
    using GnomUi.Contracts;

    public class Element : INodeElement, IPressable
    {
        private static readonly IDictionary<ConsoleKey, ConsoleKey> reverseKeys = new Dictionary<ConsoleKey, ConsoleKey>()
            {
                { ConsoleKey.UpArrow, ConsoleKey.DownArrow },
                { ConsoleKey.LeftArrow, ConsoleKey.DownArrow },
                { ConsoleKey.DownArrow, ConsoleKey.UpArrow },
                { ConsoleKey.RightArrow, ConsoleKey.LeftArrow}
            };

        private INodeElement parent;

        public string Id { get; set; }

        public string Class { get; set; }

        public IStyle Style { get; set; }

        public Element(bool selected)
        {
            this.Style = new Style();
            this.Id = string.Empty;
            this.Class = string.Empty;
            this.Neighbors = new Dictionary<ConsoleKey, IPressable>();
            this.IsSelected = selected;
            this.Children = new List<INodeElement>();
        }

        public void LinkTo(ConsoleKey key, IPressable element, bool doubly = true)
        {
            this.Neighbors.Add(key, element);

            if (!element.Neighbors.ContainsKey(reverseKeys[key]) && doubly)
            {
                element.Neighbors.Add(reverseKeys[key], this);
            }
        }

        public void FireEvent()
        {
            this.OnClick(this);
        }

        public Action<IElement> OnClick { get; set; }

        public bool IsSelected { get; set; }

        public IDictionary<ConsoleKey, IPressable> Neighbors { get; private set; }

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

        public IList<INodeElement> Children { get; private set; }

        public INodeElement AddChild(INodeElement element)
        {
            this.Children.Add(element);
            return this;
        }

        public INodeElement RemoveElement(INodeElement element)
        {
            if (this.Children.Contains(element))
            {
                this.Children.Remove(element);
            }

            return this;
        }
    }
}