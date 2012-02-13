using System.Xml.Linq;
using Fusion.Core.Enums;
using Fusion.Core.Extensions;
using Fusion.Core.Types;
using Fusion.Core.Types.Collections;

namespace Fusion.Core.Parsers
{
    public class MarketOrderParser : Parser, IParser<MarketOrderCollection>
    {
        public Response<MarketOrderCollection> Parse(XDocument document)
        {
            var response = BuildResponse<MarketOrderCollection>(document);
            if (response.HasErrors)
                return response;

            var marketOrders = new MarketOrderCollection();
            foreach (var element in document.Root.Element("result").Element("rowset").Elements("row"))
            {
                var marketOrder = new MarketOrder
                                      {
                                          OrderId = element.AttributeAsLong("orderID"),
                                          CharacterId = element.AttributeAsLong("charID"),
                                          StationId = element.AttributeAsLong("stationID"),
                                          VolumeEntered = element.AttributeAsInt("volEntered"),
                                          VolumeRemaining = element.AttributeAsInt("volRemaining"),
                                          MinimumVolume = element.AttributeAsInt("minVolume"),
                                          OrderState = element.AttributeAsEnum<OrderState>("orderState"),
                                          TypeId = element.AttributeAsLong("typeID"),
                                          Range = element.AttributeAsInt("range"),
                                          AccountKey = element.AttributeAsInt("range"),
                                          Duration = element.AttributeAsInt("duration"),
                                          Escrow = element.AttributeAsDecimal("escrow"),
                                          Price = element.AttributeAsDecimal("price"),
                                          Bid = element.AttributeAsInt("bid"),
                                          Issued = element.AttributeAsDateTime("issued")
                                      };
                marketOrders.Add(marketOrder);
            }
            response.Data = marketOrders;
            return response;
        }
    }
}