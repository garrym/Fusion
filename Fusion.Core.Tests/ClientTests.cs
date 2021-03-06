﻿using System;
using Fusion.Core.Authentication;
using Fusion.Core.CacheProviders;
using Fusion.Core.Types;
using Moq;
using NUnit.Framework;

namespace Fusion.Core.Tests
{
    [TestFixture]
    public class ClientTests
    {
        private IApiClient GetApiClient()
        {
            var key = new AuthenticationKey(628894, "TQL1KBeSIysqCHZ6slJnWLJM8kQvELjfBqoLD87a1gY9UKj64x2kpPUh2gNsuvgE");
            var mockCacheProvider = new Mock<ICacheProvider>();
            var client = new ApiClient(key, mockCacheProvider.Object);
            return client;
        }

        [Test]
        public void Ensure_AccountStatus_Works()
        {
            var client = GetApiClient();

            Response<AccountStatus> accountStatus = null;
            Assert.DoesNotThrow(() => accountStatus = client.GetAccountStatus());

            Assert.Greater(accountStatus.Data.PaidUntil, default(DateTime));
            Assert.Greater(accountStatus.Data.LogonCount, default(int));
            Assert.Greater(accountStatus.Data.LogonMinutes, default(int));
            Assert.Greater(accountStatus.Data.CreateDate, default(DateTime));
        }
    }
}
