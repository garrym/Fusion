using System.Xml.Linq;
using Fusion.Core.Enums;
using Fusion.Core.Parsers.Abstract;

namespace Fusion.Core.Parsers
{
    public class AuthenticationKeyTypeParser : Parser<AuthenticationKeyType>
    {
        protected override AuthenticationKeyType ParseData(XDocument document)
        {
            var response = BuildResponse(document);
            if (response.HasErrors && response.Error.Code == 106)
                return AuthenticationKeyType.Limited;
            return AuthenticationKeyType.Full;
        }
    }
}