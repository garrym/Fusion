using System.Xml.Linq;
using Fusion.Core.Extensions;
using Fusion.Core.Types;
using Fusion.Core.Types.Collections;

namespace Fusion.Core.Parsers
{
    public class ContactListParser : Parser, IParser<ContactCollection>
    {
        public Response<ContactCollection> Parse(XDocument document)
        {
            var response = BuildResponse<ContactCollection>(document);
            if (response.HasErrors)
                return response;

            var contacts = new ContactCollection();

            foreach (var element in document.Root.Element("result").Element("rowset").Elements("row"))
            {
                var contact = new Contact
                                  {
                                      ContactId = element.AttributeAsLong("contactID"),
                                      Name = element.AttributeAsString("contactName"),
                                      InWatchList = element.AttributeAsBool("inWatchlist"),
                                      Standing = element.AttributeAsDecimal("standing")
                                  };
                contacts.Add(contact);
            }

            response.Data = contacts;
            return response;
        }
    }
}