using System;
using System.IO;
using System.Xml.Linq;
using Fusion.Core.Parsers;
using NUnit.Framework;

namespace Fusion.Core.Tests.Parsers
{
    [TestFixture]
    public class AccountBalanceParserTests
    {
        [Test]
        public void Ensure_Parsed_Correctly()
        {
            var parser = new AccountBalanceParser();

            var document = XDocument.Load(File.OpenRead(@"Resources/AccountBalance.xml"));
            var result = parser.Parse(document);

            Assert.AreEqual(100.10, result.Data);
            //Assert.AreEqual(new DateTime(2014, 1, 1), result.LocalTime);
            Assert.AreEqual(new DateTime(2014, 1, 1), result.CurrentTime);
        }
    }
}
