﻿namespace GnomUi.TreeComponents
{
    using System;

    using GnomUi.Contracts;

    public class TextElement : Element, ITextElement
    {
        public TextElement(string content)
            : base(false)
        {
            this.Content = content;
        }

        public string Content { get; set; }

        public override string[] ToStringArray()
        {
            return new string[] { this.Content };
        }
    }
}
