using System.Collections.Generic;
using System.Xml.Linq;
using Fusion.Core.Extensions;
using Fusion.Core.Types;
using Fusion.Core.Types.Collections;

namespace Fusion.Core.Parsers
{
    public class KillLogParser : Parser, IParser<KillLogCollection>
    {
        public Response<KillLogCollection> Parse(XDocument document)
        {
            var response = BuildResponse<KillLogCollection>(document);
            if (response.HasErrors)
                return response;

            var attackerParser = new AttackerParser();
            var victimParser = new VictimParser();

            var killLogs = new KillLogCollection();
            foreach (var element in document.Root.Element("result").Element("rowset").Elements("row"))
            {
                var killLog = new KillLog
                                  {
                                      Id = element.AttributeAsLong("killID"),
                                      SolarSystemId = element.AttributeAsLong("solarSystemID"),
                                      Time = element.AttributeAsDateTime("killTime"),
                                      MoonId = element.AttributeAsLong("moonID"),
                                      Victim = victimParser.Parse(element.Element("victim")),
                                      Attackers = new List<Attacker>()
                                  };

                // Get attackers
                foreach (var attackerElement in element.Element("rowset").Elements("row"))
                    killLog.Attackers.Add(attackerParser.Parse(attackerElement));

                killLogs.Add(killLog);
            }
            response.Data = killLogs;
            return response;
        }
    }
}