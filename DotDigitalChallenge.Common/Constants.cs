using System;

namespace DotDigitalChallenge.Common
{
    public static class Constants
    {
        public static class AppSettings
        {
            public const string Username = "ApiSettings:Username";
            public const string Password = "ApiSettings:Password";
            public const string BaseUrl = "ApiSettings:BaseUrl";
        }

        public static class Account
        {
            public const string Information = "/v2/account-info";
        }

        public static class Contacts
        {
            public const string CreateSingle = "/v2/contacts";

            public const string CreateBulk = "/v2/contacts/import";
        }

        public static class Campaigns
        {
            public const string Create = "/v2/campaigns";

            public const string Send = "/v2/campaigns/send";

            public const string Summary = "/v2/campaigns/1/summary";

            public const string Clicks = "/v2/campaigns/1/clicks";
        }
    }
}
