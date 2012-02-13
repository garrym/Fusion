using System;
using System.Linq;
using System.Xml.Linq;
using Fusion.Core.Enums;
using Fusion.Core.Extensions;
using Fusion.Core.Types;

namespace Fusion.Core.Parsers
{
    public class CharacterSheetParser : Parser, IParser<CharacterSheet>
    {
        public Response<CharacterSheet> Parse(XDocument document)
        {
            var response = BuildResponse<CharacterSheet>(document);
            if (response.HasErrors)
                return response;

            var element = document.Root.Element("result");
            var characterSheet = new CharacterSheet
                                     {
                                         CharacterId = element.Element("characterID").ValueAsLong(),
                                         Name = element.Element("name").Value,
                                         DateOfBirth = element.Element("DoB").ValueAsDateTime(),
                                         Race = element.Element("race").ValueAsEnum<Race>(),
                                         BloodLine = element.Element("bloodLine").Value,
                                         Ancestry = element.Element("ancestry").Value,
                                         Gender = element.Element("gender").ValueAsEnum<Gender>(),
                                         CorporationName = element.Element("corporationName").Value,
                                         CorporationId = element.Element("corporationID").ValueAsLong(),
                                         AllianceName = element.Element("allianceName").Value,
                                         AllianceId = element.Element("allianceID").ValueAsLong(),
                                         CloneName = element.Element("cloneName").Value,
                                         CloneSkillPoints = element.Element("cloneSkillPoints").ValueAsLong(),
                                         Balance = element.Element("balance").ValueAsDecimal()
                                     };
            characterSheet.LastUpdated = document.Root.Element("localTime").ValueAsDateTime();

            var characterSkillParser = new CharacterSkillParser();
            var skillElement = document.Root.Element("result").Elements("rowset").Where(x => x.Attribute("name") != null && x.Attribute("name").Value.Equals("skills", StringComparison.OrdinalIgnoreCase));
            if (skillElement != null)
            {
                foreach (var e in skillElement.Elements("row"))
                {
                    var skill = characterSkillParser.Parse(e);
                    characterSheet.Skills.Add(skill);
                }
            }

            response.Data = characterSheet;
            return response;
        }
    }
}