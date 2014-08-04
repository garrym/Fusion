namespace Fusion.Core
{
    public static class RequestTypes
    {
        public static class Account
        {
            public static readonly RequestType AccountStatus = new RequestType("/account/AccountStatus.xml.aspx", true);
            public static readonly RequestType ApiKeyInfo = new RequestType("/account/APIKeyInfo.xml.aspx", true);
            public static readonly RequestType Characters = new RequestType("/account/Characters.xml.aspx", true);
        }

        public static class Character
        {
            public static readonly RequestType AccountBalance = new RequestType("/char/AccountBalance.xml.aspx", true).AddRequiredParameter("characterID");
            public static readonly RequestType ContactList = new RequestType("/char/ContactList.xml.aspx", true).AddRequiredParameter("characterID");
            public static readonly RequestType KillLog = new RequestType("/char/KillLog.xml.aspx", true).AddRequiredParameter("characterID");
            public static readonly RequestType MarketOrders = new RequestType("/char/MarketOrders.xml.aspx", true).AddRequiredParameter("characterID");
            public static readonly RequestType NpcStandings = new RequestType("/char/Standings.xml.aspx", true).AddRequiredParameter("characterID");
            //public static RequestType CalendarEvents = new RequestType("", true);
            public static readonly RequestType AssetList = new RequestType("/char/AssetList.xml.aspx", true).AddRequiredParameter("characterID");
            //public static RequestType ContactNotifications = new RequestType("", true);
            //public static RequestType MailingLists = new RequestType("", true);
            //public static RequestType Medals = new RequestType("", true);
            //public static RequestType Research = new RequestType("", true);
            public static readonly RequestType WalletJournal = new RequestType("/char/WalletJournal.xml.aspx", true).AddRequiredParameter("characterID");
            //public static RequestType CalendarEventAttendees = new RequestType("", true);
            //public static RequestType FactionalWarfareStatistics = new RequestType("", true);
            public static readonly RequestType MailBodies = new RequestType("/char/MailBodies.xml.aspx", true).AddRequiredParameter("characterID").AddRequiredParameter("ids");
            //public static RequestType NotificationTexts = new RequestType("", true);
            //public static RequestType SkillInTraining = new RequestType("", true);
            public static readonly RequestType WalletTransactions = new RequestType("/char/WalletTransactions.xml.aspx", true).AddRequiredParameter("characterID");
            public static readonly RequestType CharacterSheet = new RequestType("/char/CharacterSheet.xml.aspx", true).AddRequiredParameter("characterID");
            //public static RequestType IndustryJobs = new RequestType("", true);
            public static readonly RequestType MailMessages = new RequestType("/char/MailMessages.xml.aspx", true).AddRequiredParameter("characterID");
            //public static RequestType Notifications = new RequestType("", true);
            //public static RequestType SkillQueue = new RequestType("", true);
        }

        public static class Eve
        {
            public static readonly RequestType RefTypes = new RequestType("/eve/RefTypes.xml.aspx", false);
            public static readonly RequestType ErrorList = new RequestType("/eve/ErrorList.xml.aspx", false);
            public static readonly RequestType AllianceList = new RequestType("/eve/AllianceList.xml.aspx", false);
        }

        public static class Server
        {
            public static readonly RequestType Status = new RequestType("/server/ServerStatus.xml.aspx", false);
        }
    }
}