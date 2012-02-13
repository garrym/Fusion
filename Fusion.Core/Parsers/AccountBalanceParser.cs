using System.Xml.Linq;
using Fusion.Core.Extensions;

namespace Fusion.Core.Parsers
{
    public class AccountBalanceParser : Parser, IParser<decimal>
    {
        public Response<decimal> Parse(XDocument document)
        {
            var response = BuildResponse<decimal>(document);
            if (response.HasErrors)
                return response;

            var element = document.Root.Element("result").Element("rowset").Element("row");
            var balance = element.AttributeAsDecimal("balance");
            response.Data = balance;
            return response;
        }
    }
}