using System.Xml.Linq;
using Fusion.Core.Extensions;
using Fusion.Core.Parsers.Abstract;
using Fusion.Core.Types;

namespace Fusion.Core.Parsers.Internal
{
    public class ErrorParser : InternalParser<Error>
    {
        public override Error Parse(XElement element)
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