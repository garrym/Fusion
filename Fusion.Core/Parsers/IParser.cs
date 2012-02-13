using System.Xml.Linq;

namespace Fusion.Core.Parsers
{
    public interface IParser<T>
    {
        Response<T> Parse(XDocument document);
    }
}