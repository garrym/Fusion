using System.Xml.Linq;
using Fusion.Core.Enums;
using Fusion.Core.Extensions;
using Fusion.Core.Types;
using Fusion.Core.Types.Collections;

namespace Fusion.Core.Parsers
{
    public class WalletTransactionParser : Parser, IParser<WalletTransactionCollection>
    {
        public Response<WalletTransactionCollection> Parse(XDocument document)
        {
            var response = BuildResponse<WalletTransactionCollection>(document);
            if (response.HasErrors)
                return response;

            var walletTransactions = new WalletTransactionCollection();

            foreach (var element in document.Root.Element("result").Element("rowset").Elements("row"))
            {
                var w = new WalletTransaction
                            {
                                TransactionDateTime = element.AttributeAsDateTime("transactionDateTime"),
                                TransactionId = element.AttributeAsLong("transactionID"),
                                Quantity = element.AttributeAsInt("quantity"),
                                TypeName = element.AttributeAsString("typeName"),
                                TypeId = element.AttributeAsLong("typeID"),
                                Price = element.AttributeAsDecimal("price"),
                                ClientId = element.AttributeAsLong("clientID"),
                                ClientName = element.AttributeAsString("clientName"),
                                StationId = element.AttributeAsLong("stationID"),
                                StationName = element.AttributeAsString("stationName"),
                                TransactionType = element.AttributeAsEnum<TransactionType>("transactionType"),
                                TransactionFor = element.AttributeAsEnum<TransactionFor>("transactionFor")
                            };
                walletTransactions.Add(w);
            }
            response.Data = walletTransactions;
            return response;
        }
    }
}