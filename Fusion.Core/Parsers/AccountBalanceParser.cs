using System.Xml.Linq;
using Fusion.Core.Extensions;
using Fusion.Core.Parsers.Abstract;

namespace Fusion.Core.Parsers
{
    public class AccountBalanceParser : Parser<decimal>
    {
        protected override decimal ParseData(XDocument document)
        {
            var element = document.Root.Element("result").Element("rowset").Element("row");
            var balance = element.AttributeAsDecimal("balance");
            return balance;
        }
    }
}