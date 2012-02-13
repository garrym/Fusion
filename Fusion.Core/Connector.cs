using System;
using System.Net;
using System.Xml.Linq;
using Fusion.Core.Authentication;
using Fusion.Core.CacheProviders;
using Fusion.Core.Enums;
using Fusion.Core.Exceptions;
using Fusion.Core.Parsers;
using Fusion.Core.Types;
using Fusion.Core.Types.Collections;

namespace Fusion.Core
{
    public class Connector : IConnector
    {
        private readonly ICacheProvider cache;

        public Connector(ICacheProvider cacheProvider)
        {
            cache = cacheProvider;
        }

        //public bool VerifyKey()
        //{
        //    //http://apitest.eveonline.com/account/APIKeyInfo.xml.aspx?keyID=x&vCode=VERYVERYSECRET
        //    return true;
        //}

        public IAuthenticationKey DefaultAuthenticationKey { get; set; }

        public Response<ServerStatus> GetServerStatus()
        {
            var request = new Request(RequestTypes.Server.Status);
            return ProcessRequest(request, new ServerStatusParser());
        }

        public Response<CharacterCollection> GetCharacters(IAuthenticationKey key = null)
        {
            key = key ?? DefaultAuthenticationKey;
            var request = new Request(RequestTypes.Account.Characters, key);
            return ProcessRequest(request, new CharacterParser());
        }

        public Response<decimal> GetAccountBalance(long characterId, IAuthenticationKey key = null)
        {
            key = key ?? DefaultAuthenticationKey;
            var request = new Request(RequestTypes.Character.AccountBalance, key);
            request.AddParameter(RequestParameter.CharacterId, characterId);
            return ProcessRequest(request, new AccountBalanceParser());
        }

        public Response<WalletTransactionCollection> GetWalletTransactions(long characterId, IAuthenticationKey key = null)
        {
            key = key ?? DefaultAuthenticationKey;
            var request = new Request(RequestTypes.Character.WalletTransactions, key);
            request.AddParameter(RequestParameter.CharacterId, characterId);
            return ProcessRequest(request, new WalletTransactionParser());
        }

        public Response<WalletJournalItemCollection> GetWalletJournal(long characterId, IAuthenticationKey key = null)
        {
            key = key ?? DefaultAuthenticationKey;
            var request = new Request(RequestTypes.Character.WalletJournal, key);
            request.AddParameter(RequestParameter.CharacterId, characterId);
            return ProcessRequest(request, new WalletJournalParser());
        }

        public Response<AssetCollection> GetAssetList(long characterId, IAuthenticationKey key = null)
        {
            key = key ?? DefaultAuthenticationKey;
            var request = new Request(RequestTypes.Character.AssetList, key);
            request.AddParameter(RequestParameter.CharacterId, characterId);
            return ProcessRequest(request, new AssetsParser());
        }

        public Response<MailMessageCollection> GetMailMessages(long characterId, IAuthenticationKey key = null)
        {
            key = key ?? DefaultAuthenticationKey;
            var request = new Request(RequestTypes.Character.MailMessages, key);
            request.AddParameter(RequestParameter.CharacterId, characterId);
            return ProcessRequest(request, new MailMessageParser());
        }

        public Response<MailBodyCollection> GetMailBodies(long characterId, long[] ids, IAuthenticationKey key = null)
        {
            key = key ?? DefaultAuthenticationKey;
            var request = new Request(RequestTypes.Character.MailBodies, key);
            request.AddParameter(RequestParameter.CharacterId, characterId);
            request.AddParameter(RequestParameter.Ids, ids);
            return ProcessRequest(request, new MailBodyParser());
        }

        public Response<AllianceCollection> GetAllianceList(IAuthenticationKey key = null)
        {
            key = key ?? DefaultAuthenticationKey;
            var request = new Request(RequestTypes.Eve.AllianceList, key);
            return ProcessRequest(request, new AllianceListParser());
        }

        public Response<ContactCollection> GetContactList(long characterId, IAuthenticationKey key = null)
        {
            key = key ?? DefaultAuthenticationKey;
            var request = new Request(RequestTypes.Character.ContactList, key);
            request.AddParameter(RequestParameter.CharacterId, characterId);
            return ProcessRequest(request, new ContactListParser());
        }

        public Response<MarketOrderCollection> GetMarketOrders(long characterId, IAuthenticationKey key = null)
        {
            key = key ?? DefaultAuthenticationKey;
            var request = new Request(RequestTypes.Character.MarketOrders, key);
            request.AddParameter(RequestParameter.CharacterId, characterId);
            return ProcessRequest(request, new MarketOrderParser());
        }

        public Response<KillLogCollection> GetKillLogs(long characterId, IAuthenticationKey key = null)
        {
            key = key ?? DefaultAuthenticationKey;
            var request = new Request(RequestTypes.Character.KillLog, key);
            request.AddParameter(RequestParameter.CharacterId, characterId);
            return ProcessRequest(request, new KillLogParser());
        }

        public Response<CharacterSheet> GetCharacterSheet(long characterId, IAuthenticationKey key = null)
        {
            key = key ?? DefaultAuthenticationKey;
            var request = new Request(RequestTypes.Character.CharacterSheet, key);
            request.AddParameter(RequestParameter.CharacterId, characterId);
            return ProcessRequest(request, new CharacterSheetParser());
        }

        public Response<RefTypeCollection> GetRefTypes(IAuthenticationKey key = null)
        {
            key = key ?? DefaultAuthenticationKey;
            var request = new Request(RequestTypes.Eve.RefTypes, key);
            return ProcessRequest(request, new RefTypesParser());
        }

        public Response<ErrorCollection> GetErrorList()
        {
            var request = new Request(RequestTypes.Eve.ErrorList);
            return ProcessRequest(request, new ErrorListParser());
        }

        private Response<T> ProcessRequest<T>(Request request, IParser<T> parser)
        {
            if (!request.IsValid())
                throw new CustomException("Request for {0} is not valid", request.RequestType.ToString());

            XDocument document;
            if (cache.Exists(request.Url))
            {
                document = cache.Get(request.Url);
            }
            else
            {
                var webRequest = (HttpWebRequest) WebRequest.Create(request.Url);
                webRequest.Timeout = 10000;
                var webResponse = (HttpWebResponse) webRequest.GetResponse();
                if (webResponse.StatusCode == HttpStatusCode.OK)
                {
                    var responseStream = webResponse.GetResponseStream();
                    document = XDocument.Load(responseStream);
                    if (!Parser.ContainsErrors(document))
                        cache.Add(request.Url, document);
                }
                else
                {
                    throw new ApplicationException("Something went wrong");
                }
            }
            return parser.Parse(document);
        }
    }
}