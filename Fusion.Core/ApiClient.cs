using System;
using System.Net;
using System.Xml.Linq;
using Fusion.Core.Authentication;
using Fusion.Core.CacheProviders;
using Fusion.Core.Enums;
using Fusion.Core.Exceptions;
using Fusion.Core.Parsers;
using Fusion.Core.Parsers.Abstract;
using Fusion.Core.Types;
using Fusion.Core.Types.Collections;

namespace Fusion.Core
{
    public class ApiClient : IApiClient
    {
        private readonly AuthenticationKey key;
        private readonly ICacheProvider cacheProvider;

        public ApiClient(AuthenticationKey key, ICacheProvider cacheProvider)
        {
            this.key = key;
            this.cacheProvider = cacheProvider;
        }

        //public bool VerifyKey()
        //{
        //    //http://apitest.eveonline.com/account/APIKeyInfo.xml.aspx?keyID=x&vCode=VERYVERYSECRET
        //    return true;
        //}

        public Response<ServerStatus> GetServerStatus()
        {
            var request = new Request(RequestTypes.Server.Status);
            return SendRequest(request, new ServerStatusParser());
        }

        private void AssertKey()
        {
            if (key == null)
                throw new Exception("Key not set");
        }

        #region Account

        public Response<AccountStatus> GetAccountStatus()
        {
            AssertKey();
            var request = new Request(RequestTypes.Account.AccountStatus, key);
            return SendRequest(request, new AccountStatusParser());
        }

        #endregion


        public Response<CharacterCollection> GetCharacters()
        {
            AssertKey();
            var request = new Request(RequestTypes.Account.Characters, key);
            return SendRequest(request, new CharacterParser());
        }

        public Response<decimal> GetAccountBalance(long characterId)
        {
            AssertKey();
            var request = new Request(RequestTypes.Character.AccountBalance, key);
            request.AddParameter(RequestParameter.CharacterId, characterId);
            return SendRequest(request, new AccountBalanceParser());
        }

        public Response<WalletTransactionCollection> GetWalletTransactions(long characterId)
        {
            AssertKey();
            var request = new Request(RequestTypes.Character.WalletTransactions, key);
            request.AddParameter(RequestParameter.CharacterId, characterId);
            return SendRequest(request, new WalletTransactionParser());
        }

        public Response<WalletJournalItemCollection> GetWalletJournal(long characterId)
        {
            AssertKey();
            var request = new Request(RequestTypes.Character.WalletJournal, key);
            request.AddParameter(RequestParameter.CharacterId, characterId);
            return SendRequest(request, new WalletJournalParser());
        }

        public Response<AssetCollection> GetAssetList(long characterId)
        {
            AssertKey();
            var request = new Request(RequestTypes.Character.AssetList, key);
            request.AddParameter(RequestParameter.CharacterId, characterId);
            return SendRequest(request, new AssetsParser());
        }

        public Response<MailMessageCollection> GetMailMessages(long characterId)
        {
            AssertKey();
            var request = new Request(RequestTypes.Character.MailMessages, key);
            request.AddParameter(RequestParameter.CharacterId, characterId);
            return SendRequest(request, new MailMessageParser());
        }

        public Response<MailBodyCollection> GetMailBodies(long characterId, long[] ids)
        {
            AssertKey();
            var request = new Request(RequestTypes.Character.MailBodies, key);
            request.AddParameter(RequestParameter.CharacterId, characterId);
            request.AddParameter(RequestParameter.Ids, ids);
            return SendRequest(request, new MailBodyParser());
        }

        public Response<AllianceCollection> GetAllianceList()
        {
            AssertKey();
            var request = new Request(RequestTypes.Eve.AllianceList, key);
            return SendRequest(request, new AllianceListParser());
        }

        public Response<ContactCollection> GetContactList(long characterId)
        {
            AssertKey();
            var request = new Request(RequestTypes.Character.ContactList, key);
            request.AddParameter(RequestParameter.CharacterId, characterId);
            return SendRequest(request, new ContactListParser());
        }

        public Response<MarketOrderCollection> GetMarketOrders(long characterId)
        {
            AssertKey();
            var request = new Request(RequestTypes.Character.MarketOrders, key);
            request.AddParameter(RequestParameter.CharacterId, characterId);
            return SendRequest(request, new MarketOrderParser());
        }

        public Response<KillLogCollection> GetKillLogs(long characterId)
        {
            AssertKey();
            var request = new Request(RequestTypes.Character.KillLog, key);
            request.AddParameter(RequestParameter.CharacterId, characterId);
            return SendRequest(request, new KillLogParser());
        }

        public Response<CharacterSheet> GetCharacterSheet(long characterId)
        {
            AssertKey();
            var request = new Request(RequestTypes.Character.CharacterSheet, key);
            request.AddParameter(RequestParameter.CharacterId, characterId);
            return SendRequest(request, new CharacterSheetParser());
        }

        public Response<RefTypeCollection> GetRefTypes()
        {
            AssertKey();
            var request = new Request(RequestTypes.Eve.RefTypes, key);
            return SendRequest(request, new RefTypesParser());
        }

        public Response<ErrorCollection> GetErrorList()
        {
            var request = new Request(RequestTypes.Eve.ErrorList);
            return SendRequest(request, new ErrorListParser());
        }

        private Response<T> SendRequest<T>(Request request, Parser<T> parser)
        {
            if (!request.IsValid())
                throw new ApiException("Request for {0} is not valid", request.RequestType);

            XDocument document;
            if (cacheProvider.Exists(request.Url))
            {
                document = cacheProvider.Get(request.Url);
                return parser.Parse(document);
            }

            var webRequest = (HttpWebRequest)WebRequest.Create(request.Url);
            webRequest.Timeout = 10000;
            using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
            {
                if (webResponse.StatusCode != HttpStatusCode.OK)
                    throw new ApiException("API returned status code of: {0}", (int)webResponse.StatusCode);

                using (var responseStream = webResponse.GetResponseStream())
                {
                    document = XDocument.Load(responseStream);
                    if (!parser.ContainsErrors(document))
                        cacheProvider.Add(request.Url, document);

                    return parser.Parse(document);
                }
            }
        }
    }
}