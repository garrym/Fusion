using System.Xml.Linq;
using Fusion.Core.Types;
using Fusion.Core.Types.Collections;

namespace Fusion.Core.Parsers
{
    public class ErrorListParser : Parser, IParser<ErrorCollection>
    {
        public Response<ErrorCollection> Parse(XDocument document)
        {
            var response = BuildResponse<ErrorCollection>(document);
            if (response.HasErrors)
                return response;

            var errorParser = new ErrorParser();
            var errorList = new ErrorCollection();
            foreach (var element in document.Root.Element("root").Element("rowset").Elements("row"))
            {
                var error = errorParser.Parse(element);
                errorList.Add(error);
            }
            response.Data = errorList;
            return response;
        }
    }
}