using System.Xml.Linq;
using Fusion.Core.Extensions;
using Fusion.Core.Parsers.Abstract;
using Fusion.Core.Types;
using Fusion.Core.Types.Collections;

namespace Fusion.Core.Parsers
{
    public class MailMessageParser: Parser<MailMessageCollection>
    {
        protected override MailMessageCollection ParseData(XDocument document)
        {
            var mailMessages = new MailMessageCollection();
            foreach (var element in document.Root.Element("result").Element("rowset").Elements("row"))
            {
                var message = new MailMessage
                                  {
                                      MessageId = element.AttributeAsLong("messageID"),
                                      SenderId = element.AttributeAsLong("senderID"),
                                      SentDate = element.AttributeAsDateTime("sentDate"),
                                      Title = element.AttributeAsString("title"),
                                      ToCorpOrAllianceId = element.AttributeAsNullableLong("toCorpOrAllianceID"),
                                      ToCharacterIds = element.AttributeAsList<long>("toCharacterIDs"),
                                      ToListIds = element.AttributeAsList<long>("toListID")
                                  };

                if (element.Attribute("read") != null)
                    message.Read = (element.AttributeAsString("read").Equals("1")) ? true : false;
                else
                    message.Read = false;
                mailMessages.Add(message);
            }
            return mailMessages;
        }
    }
}