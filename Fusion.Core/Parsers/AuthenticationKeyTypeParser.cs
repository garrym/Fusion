using System.Xml.Linq;
using Fusion.Core.Enums;

namespace Fusion.Core.Parsers
{
    public class AuthenticationKeyTypeParser : Parser, IParser<AuthenticationKeyType>
    {
        public Response<AuthenticationKeyType> Parse(XDocument document)
        {
            var response = BuildResponse<AuthenticationKeyType>(document);
            if (response.HasErrors && response.Error.Code == 106)
                response.Data = AuthenticationKeyType.Limited;
            else
                response.Data = AuthenticationKeyType.Full;
            return response;
        }
    }
}