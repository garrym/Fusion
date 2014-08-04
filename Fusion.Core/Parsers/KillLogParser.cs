using System.Collections.Generic;
using System.Xml.Linq;
using Fusion.Core.Extensions;
using Fusion.Core.Parsers.Abstract;
using Fusion.Core.Parsers.Internal;
using Fusion.Core.Types;
using Fusion.Core.Types.Collections;

namespace Fusion.Core.Parsers
{
    public class KillLogParser: Parser<KillLogCollection>
    {
        protected override KillLogCollection ParseData(XDocument document)
        {
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
            return killLogs;
        }
    }
}