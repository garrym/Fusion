using System.Xml.Linq;
using Fusion.Core.Extensions;
using Fusion.Core.Types;

namespace Fusion.Core.Parsers
{
    public class ErrorParser : IInternalParser<Error>
    {
        public Error Parse(XElement element)
        {
            var error = new Error
                            {
                                Code = element.AttributeAsInt("code"),
                                Message = element.Value
                            };
            return error;
        }
    }
}