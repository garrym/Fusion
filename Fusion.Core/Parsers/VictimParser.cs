using System.Xml.Linq;
using Fusion.Core.Extensions;
using Fusion.Core.Types;

namespace Fusion.Core.Parsers
{
    public class VictimParser : IInternalParser<Victim>
    {
        public Victim Parse(XElement element)
        {
            var victim = new Victim
                             {
                                 CharacterId = element.AttributeAsLong("characterID"),
                                 Name = element.AttributeAsString("characterName"),
                                 CorporationId = element.AttributeAsLong("corporationID"),
                                 CorporationName = element.AttributeAsString("corporationName"),
                                 DamageTaken = element.AttributeAsInt("damageTaken"),
                                 ShipTypeId = element.AttributeAsLong("shipTypeID")
                             };
            return victim;
        }
    }
}