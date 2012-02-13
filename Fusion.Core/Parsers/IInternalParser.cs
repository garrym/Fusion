using System.Xml.Linq;

namespace Fusion.Core.Parsers
{
    public interface IInternalParser<T>
    {
        T Parse(XElement element);
    }
}