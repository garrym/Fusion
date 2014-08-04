using System.Xml.Linq;
using Fusion.Core.Extensions;
using Fusion.Core.Parsers.Abstract;
using Fusion.Core.Types;

namespace Fusion.Core.Parsers.Internal
{
    public class VictimParser : InternalParser<Victim>
    {
        public override Victim Parse(XElement element)
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