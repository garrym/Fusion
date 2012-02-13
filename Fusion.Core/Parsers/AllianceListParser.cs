using System.Xml.Linq;
using Fusion.Core.Extensions;
using Fusion.Core.Types;
using Fusion.Core.Types.Collections;

namespace Fusion.Core.Parsers
{
    public class AllianceListParser : Parser, IParser<AllianceCollection>
    {
        public Response<AllianceCollection> Parse(XDocument document)
        {
            var response = BuildResponse<AllianceCollection>(document);
            if (response.HasErrors)
                return response;

            var allianceList = new AllianceCollection();
            foreach (var element in document.Root.Element("result").Element("rowset").Elements("row"))
            {
                var alliance = new Alliance
                                   {
                                       Name = element.AttributeAsString("name"),
                                       ShortName = element.AttributeAsString("shortName"),
                                       AllianceId = element.AttributeAsLong("allianceID"),
                                       ExecutorCorpId = element.AttributeAsLong("executorCorpID"),
                                       MemberCount = element.AttributeAsInt("memberCount"),
                                       StartDate = element.AttributeAsDateTime("startDate")
                                   };
                allianceList.Add(alliance);
            }
            response.Data = allianceList;
            return response;
        }
    }
}