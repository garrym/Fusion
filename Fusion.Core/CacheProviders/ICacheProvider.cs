using System.Xml.Linq;

namespace Fusion.Core.CacheProviders
{
    public interface ICacheProvider
    {
        void Add(string url, XDocument document);
        bool Exists(string url);
        XDocument Get(string url);
        void Delete(string url);
    }
}