using System.Xml.Linq;
using Fusion.Core.Extensions;
using Fusion.Core.Parsers.Abstract;
using Fusion.Core.Types;

namespace Fusion.Core.Parsers.Internal
{
    public class AttackerParser : InternalParser<Attacker>
    {
        public override Attacker Parse(XElement element)
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