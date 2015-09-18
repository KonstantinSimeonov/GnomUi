using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GnomUi
{
    public interface INodeElement : IElement
    {
        IList<IElement> Children { get; }
        INodeElement AddChild(IElement element);
        INodeElement RemoveElement(IElement element);
    }
}
