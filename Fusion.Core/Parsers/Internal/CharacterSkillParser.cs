using System.Xml.Linq;
using Fusion.Core.Extensions;
using Fusion.Core.Parsers.Abstract;
using Fusion.Core.Types;

namespace Fusion.Core.Parsers.Internal
{
    public class CharacterSkillParser : IInternalParser<CharacterSkill>
    {
        public CharacterSkill Parse(XElement element)
        {
            var skill = new CharacterSkill
                            {
                                TypeId = element.AttributeAsLong("typeID"),
                                SkillPoints = element.AttributeAsInt("skillpoints"),
                                Level = element.AttributeAsInt("level")
                                //skill.Published = // We dont' really care
                            };
            return skill;
        }
    }
}