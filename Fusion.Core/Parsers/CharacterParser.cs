using System.Xml.Linq;
using Fusion.Core.Extensions;
using Fusion.Core.Parsers.Abstract;
using Fusion.Core.Types;
using Fusion.Core.Types.Collections;

namespace Fusion.Core.Parsers
{
    public class CharacterParser: Parser<CharacterCollection>
    {
        protected override CharacterCollection ParseData(XDocument document)
        {
            var characters = new CharacterCollection();

            foreach (var element in document.Root.Element("result").Element("rowset").Elements("row"))
            {
                var character = new Character
                                    {
                                        Name = element.AttributeAsString("name"),
                                        CharacterId = element.AttributeAsLong("characterID"),
                                        CorporationName = element.AttributeAsString("corporationName"),
                                        CorporationId = element.AttributeAsLong("corporationID")
                                    };
                characters.Add(character);
            }
            return characters;
        }
    }
}