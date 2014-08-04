using System;
using System.Xml.Linq;
using Fusion.Core.Extensions;
using Fusion.Core.Types;

namespace Fusion.Core.Parsers.Abstract
{
    public abstract class Parser<T>
    {
        public Response<T> Parse(XDocument document)
        {
            var response = BuildResponse(document);
            if (response.HasErrors)
                return response;

            response.Data = ParseData(document);
            return response;
        }

        protected abstract T ParseData(XDocument document);

        protected Response<T> BuildResponse(XDocument document)
        {
            var response = new Response<T>();
            if (ContainsErrors(document))
                response.Error = ParseError(document);

            response.CurrentTime = DateTime.Parse(document.Root.Element("currentTime").Value);
            response.CachedUntil = DateTime.Parse(document.Root.Element("cachedUntil").Value);

            return response;
        }

        public bool ContainsErrors(XDocument document)
        {
            return document.Root != null && document.Root.Element("error") != null;
        }

        private Error ParseError(XDocument document)
        {
            if (document.Root == null)
                return new Error { Code = -1, Message = "Unknown error" };

            var element = document.Root.Element("error");
            var error = new Error
                            {
                                Code = element.AttributeAsInt("code"),
                                Message = element.Value
                            };
            return error;
        }
    }
}