using System.Xml.Linq;
using Fusion.Core.Extensions;
using Fusion.Core.Parsers.Abstract;
using Fusion.Core.Types;
using Fusion.Core.Types.Collections;

namespace Fusion.Core.Parsers
{
    public class MailBodyParser: Parser<MailBodyCollection>
    {
        protected override MailBodyCollection ParseData(XDocument document)
        {
            var mailBodies = new MailBodyCollection();
            foreach (var element in document.Root.Element("result").Element("rowset").Elements("row"))
            {
                var mailBody = new MailBody
                                   {
                                       MessageId = element.AttributeAsLong("messageID"),
                                       Content = element.Value
                                   };
                mailBodies.Add(mailBody);
            }
            return mailBodies;
        }
    }
}