using System;
using System.Xml.Linq;
using Fusion.Core.Parsers.Abstract;
using Fusion.Core.Types;

namespace Fusion.Core.Parsers
{
    public class AccountStatusParser : Parser<AccountStatus>
    {
        protected override AccountStatus ParseData(XDocument document)
        {
            var resultElement = document.Root.Element("result");

            var accountStatus = new AccountStatus
            {
                PaidUntil = DateTime.Parse(resultElement.Element("paidUntil").Value),
                CreateDate = DateTime.Parse(resultElement.Element("createDate").Value),
                LogonCount = int.Parse(resultElement.Element("logonCount").Value),
                LogonMinutes = int.Parse(resultElement.Element("logonMinutes").Value)
            };

            return accountStatus;
        }
    }
}