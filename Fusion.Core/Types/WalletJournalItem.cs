using System;

namespace Fusion.Core.Types
{
    public class WalletJournalItem
    {
        public decimal Amount;
        public int ArgId1;
        public string ArgName1;
        public decimal Balance;
        public DateTime Date;
        public long OwnerId1;
        public long OwnerId2;
        public string OwnerName1;
        public string OwnerName2;
        public string Reason;
        public double RefId;
        public long RefTypeId;
        public decimal? TaxAmount;
        public long? TaxReceiverId;
    }
}