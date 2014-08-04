using System.Xml.Linq;
using Fusion.Core.Parsers.Abstract;
using Fusion.Core.Parsers.Internal;
using Fusion.Core.Types.Collections;

namespace Fusion.Core.Parsers
{
    public class ErrorListParser : Parser<ErrorCollection>
    {
        protected override ErrorCollection ParseData(XDocument document)
        {
            var errorParser = new ErrorParser();
            var errorList = new ErrorCollection();
            foreach (var element in document.Root.Element("root").Element("rowset").Elements("row"))
            {
                var error = errorParser.Parse(element);
                errorList.Add(error);
            }
            return errorList;
        }
    }
}