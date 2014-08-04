using System;

namespace Fusion.Core.Types
{
    public class AccountStatus
    {
        public DateTime PaidUntil
        { get; set; }

        public DateTime CreateDate
        { get; set; }

        public int LogonCount
        { get; set; }

        public int LogonMinutes
        { get; set; }
    }
}