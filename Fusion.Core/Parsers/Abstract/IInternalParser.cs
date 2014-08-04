using System.Xml.Linq;

namespace Fusion.Core.Parsers.Abstract
{
    public interface IInternalParser<T>
    {
        T Parse(XElement element);
    }
}