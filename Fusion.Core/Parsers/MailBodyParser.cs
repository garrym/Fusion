using System.Xml.Linq;
using Fusion.Core.Extensions;
using Fusion.Core.Types;
using Fusion.Core.Types.Collections;

namespace Fusion.Core.Parsers
{
    public class MailBodyParser : Parser, IParser<MailBodyCollection>
    {
        public Response<MailBodyCollection> Parse(XDocument document)
        {
            var response = BuildResponse<MailBodyCollection>(document);
            if (response.HasErrors)
                return response;

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
            response.Data = mailBodies;
            return response;
        }
    }
}