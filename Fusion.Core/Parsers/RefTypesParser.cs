using System.Xml.Linq;
using Fusion.Core.Extensions;
using Fusion.Core.Parsers.Abstract;
using Fusion.Core.Types;
using Fusion.Core.Types.Collections;

namespace Fusion.Core.Parsers
{
    public class RefTypesParser: Parser<RefTypeCollection>
    {
        protected override RefTypeCollection ParseData(XDocument document)
        {
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
            return refTypes;
        }
    }
}