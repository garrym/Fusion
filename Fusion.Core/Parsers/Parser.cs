using System;
using System.Xml.Linq;
using Fusion.Core.Extensions;
using Fusion.Core.Types;

namespace Fusion.Core.Parsers
{
    public class Parser
    {
        protected static Response<T> BuildResponse<T>(XDocument document)
        {
            var response = new Response<T>();
            if (ContainsErrors(document))
                response.Error = ParseError(document);

            // TODO: Populate all other properties, such as cachedUntil, etc.

            response.CurrentTime = DateTime.Parse(document.Root.Element("currentTime").Value);
            response.CachedUntil = DateTime.Parse(document.Root.Element("cachedUntil").Value);

            return response;
        }

        internal static bool ContainsErrors(XDocument document)
        {
            return document.Root != null && document.Root.Element("error") != null;
        }

        private static Error ParseError(XDocument document)
        {
            if (document.Root == null)
                return new Error {Code = -1, Message = "Unknown error"};

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