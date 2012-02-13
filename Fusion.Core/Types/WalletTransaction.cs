using System;
using Fusion.Core.Enums;

namespace Fusion.Core.Types
{
    public class WalletTransaction
    {
        public long ClientId;
        public string ClientName;
        public decimal Price;
        public long Quantity;
        public long StationId;
        public string StationName;
        public DateTime TransactionDateTime;
        public TransactionFor TransactionFor;
        public long TransactionId;
        public TransactionType TransactionType;
        public long TypeId;
        public string TypeName;
    }
}