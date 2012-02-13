using System.Xml.Linq;
using Fusion.Core.Extensions;
using Fusion.Core.Types;
using Fusion.Core.Types.Collections;

namespace Fusion.Core.Parsers
{
    public class WalletJournalParser : Parser, IParser<WalletJournalItemCollection>
    {
        public Response<WalletJournalItemCollection> Parse(XDocument document)
        {
            var response = BuildResponse<WalletJournalItemCollection>(document);
            if (response.HasErrors)
                return response;

            var walletJournalItems = new WalletJournalItemCollection();
            foreach (var element in document.Root.Element("result").Element("rowset").Elements("row"))
            {
                var walletJournalItem = new WalletJournalItem
                                            {
                                                Date = element.AttributeAsDateTime("date"),
                                                RefId = element.AttributeAsLong("refID"),
                                                RefTypeId = element.AttributeAsLong("refTypeID"),
                                                OwnerName1 = element.AttributeAsString("ownerName1"),
                                                OwnerId1 = element.AttributeAsInt("ownerID1"),
                                                OwnerName2 = element.AttributeAsString("ownerName2"),
                                                OwnerId2 = element.AttributeAsInt("ownerID2"),
                                                ArgName1 = element.AttributeAsString("argName1"),
                                                ArgId1 = element.AttributeAsInt("argID1"),
                                                Amount = element.AttributeAsDecimal("amount"),
                                                Balance = element.AttributeAsDecimal("balance"),
                                                Reason = element.AttributeAsString("reason"),
                                                TaxReceiverId = element.AttributeAsNullableLong("taxReceiverID"),
                                                TaxAmount = element.AttributeAsNullableDecimal("taxAmount")
                                            };
                walletJournalItems.Add(walletJournalItem);
            }
            response.Data = walletJournalItems;
            return response;
        }
    }
}