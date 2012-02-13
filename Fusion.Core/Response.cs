using System;
using System.Xml.Linq;
using Fusion.Core.Types;

namespace Fusion.Core
{
    public class Response<T>
    {
        public string Version { get; set; }

        public DateTime CurrentTime { get; set; }

        public DateTime CachedUntil { get; set; }

        public XElement Result { get; set; }

        public bool HasErrors
        {
            get { return Error != null; }
        }

        public Error Error { get; set; }

        public T Data { get; set; }
    }
}