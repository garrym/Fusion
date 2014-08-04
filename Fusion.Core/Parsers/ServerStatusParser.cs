using System;
using System.Xml.Linq;
using Fusion.Core.Parsers.Abstract;
using Fusion.Core.Types;

namespace Fusion.Core.Parsers
{
    public class ServerStatusParser : Parser<ServerStatus>
    {
        protected override ServerStatus ParseData(XDocument document)
        {
            var status = new ServerStatus();
            status.CurrentTime = DateTime.Parse(document.Root.Element("currentTime").Value);
            status.ServerOpen = (document.Root.Element("result").Element("serverOpen").Value.Equals("True")) ? true : false;
            // TODO: What happens if server offline?
            status.OnlinePlayers = Int32.Parse(document.Root.Element("result").Element("onlinePlayers").Value);
            return status;
        }
    }
}