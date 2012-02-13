using System.Xml.Linq;
using Fusion.Core.Extensions;
using Fusion.Core.Types;

namespace Fusion.Core.Parsers
{
    public class AttackerParser : IInternalParser<Attacker>
    {
        public Attacker Parse(XElement element)
        {
            var attacker = new Attacker
                               {
                                   CharacterId = element.AttributeAsLong("characterID"),
                                   Name = element.AttributeAsString("characterName"),
                                   CorporationId = element.AttributeAsLong("corporationID"),
                                   CorporationName = element.AttributeAsString("corporationName"),
                                   DamageDone = element.AttributeAsInt("damageDone"),
                                   FinalBlow = element.AttributeAsInt("finalBlow") == 1 ? true : false,
                                   WeaponTypeId = element.AttributeAsLong("weaponTypeID"),
                                   ShipTypeId = element.AttributeAsLong("shipTypeID")
                               };
            return attacker;
        }
    }
}