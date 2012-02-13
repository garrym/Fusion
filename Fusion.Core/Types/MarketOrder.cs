using System;
using Fusion.Core.Enums;

namespace Fusion.Core.Types
{
    public class MarketOrder
    {
        public int AccountKey;
        public int Bid;
        public long CharacterId;
        public int Duration;
        public decimal Escrow;
        public DateTime Issued;
        public int MinimumVolume;
        public long OrderId;
        public OrderState OrderState;
        public decimal Price;
        public int Range;
        public long StationId;
        public long TypeId;
        public int VolumeEntered;
        public int VolumeRemaining;
    }
}