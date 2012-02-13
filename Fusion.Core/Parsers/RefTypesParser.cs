using System.Xml.Linq;
using Fusion.Core.Extensions;
using Fusion.Core.Types;
using Fusion.Core.Types.Collections;

namespace Fusion.Core.Parsers
{
    public class RefTypesParser : Parser, IParser<RefTypeCollection>
    {
        public Response<RefTypeCollection> Parse(XDocument document)
        {
            var response = BuildResponse<RefTypeCollection>(document);
            if (response.HasErrors)
                return response;

            var refTypes = new RefTypeCollection();
            foreach (var element in document.Root.Element("result").Element("rowset").Elements("row"))
            {
                var refType = new RefType
                                  {
                                      Id = element.AttributeAsInt("refTypeID"),
                                      Name = element.AttributeAsString("refTypeName")
                                  };

                refTypes.Add(refType);
            }
            response.Data = refTypes;
            return response;
        }
    }
}