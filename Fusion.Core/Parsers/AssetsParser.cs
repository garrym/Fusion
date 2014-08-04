using System.Xml.Linq;
using Fusion.Core.Extensions;
using Fusion.Core.Parsers.Abstract;
using Fusion.Core.Types;
using Fusion.Core.Types.Collections;

namespace Fusion.Core.Parsers
{
    public class AssetsParser: Parser<AssetCollection>
    {
        protected override AssetCollection ParseData(XDocument document)
        {
            return Parse(document.Root.Element("result").Element("rowset"));
        }

        private AssetCollection Parse(XContainer containerElement)
        {
            var assets = new AssetCollection();

            foreach (var element in containerElement.Elements("row"))
            {
                var a = new Asset
                            {
                                ItemId = element.AttributeAsLong("itemID"),
                                LocationId = element.AttributeAsNullableLong("locationID"),
                                TypeId = element.AttributeAsLong("typeID"),
                                Quantity = element.AttributeAsInt("quantity"),
                                Flag = element.AttributeAsInt("flag"),
                                Singleton = element.AttributeAsString("singleton").Equals("1")
                            };

                if (element.HasElements)
                    a.Assets = Parse(element.Element("rowset"));

                assets.Add(a);
            }
            return assets;
        }
    }
}