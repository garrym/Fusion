using System;
using System.Xml.Linq;

namespace Fusion.Core.CacheProviders
{
    public class DatabaseCacheProvider : ICacheProvider
    {
        #region ICacheProvider Members

        public void Add(string url, XDocument document)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string url)
        {
            throw new NotImplementedException();
        }

        public XDocument Get(string url)
        {
            throw new NotImplementedException();
        }

        public void Delete(string url)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}