using System.Xml.Linq;

namespace Fusion.Core.Parsers.Abstract
{
    public abstract class InternalParser<T>
    {
        public abstract T Parse(XElement element);
    }
}