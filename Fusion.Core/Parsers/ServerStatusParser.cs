using System;
using System.Xml.Linq;
using Fusion.Core.Types;

namespace Fusion.Core.Parsers
{
    public class ServerStatusParser : Parser, IParser<ServerStatus>
    {
        public Response<ServerStatus> Parse(XDocument document)
        {
            var response = BuildResponse<ServerStatus>(document);
            if (response.HasErrors)
                return response;

            var status = new ServerStatus();
            status.CurrentTime = DateTime.Parse(document.Root.Element("currentTime").Value);
            status.ServerOpen = (document.Root.Element("result").Element("serverOpen").Value.Equals("True")) ? true : false;
            // TODO: What happens if server offline?
            status.OnlinePlayers = Int32.Parse(document.Root.Element("result").Element("onlinePlayers").Value);
            response.Data = status;
            return response;
        }
    }
}