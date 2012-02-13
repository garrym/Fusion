using System.Collections.Generic;

namespace Fusion.Core.Types
{
    public class Asset
    {
        public IList<Asset> Assets;
        public int Flag;
        public long ItemId;
        public long? LocationId;
        public int Quantity;
        public bool Singleton;
        public long TypeId;
    }
}