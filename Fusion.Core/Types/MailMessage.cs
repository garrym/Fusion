using System;
using System.Collections.Generic;

namespace Fusion.Core.Types
{
    public class MailMessage
    {
        public long MessageId;
        public bool Read;
        public long SenderId;
        public DateTime SentDate;
        public string Title;
        public IList<long> ToCharacterIds;
        public long? ToCorpOrAllianceId;
        public IList<long> ToListIds;
    }
}