using System.Xml.Linq;
using Fusion.Core.Extensions;
using Fusion.Core.Parsers.Abstract;
using Fusion.Core.Types;
using Fusion.Core.Types.Collections;

namespace Fusion.Core.Parsers
{
    public class AllianceListParser: Parser<AllianceCollection>
    {
        protected override AllianceCollection ParseData(XDocument document)
        {
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
            return allianceList;
        }
    }
}