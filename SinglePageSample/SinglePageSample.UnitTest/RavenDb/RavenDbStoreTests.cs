using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SinglePageSample.UnitTest.Bootstrappers;
using SinglePageSample.Db.DbStore;
using SinglePageSample.Repository.Entities;

namespace SinglePageSample.UnitTest.RavenDb
{
    [TestClass]
    public class RavenDbStoreTests
    {
        public RavenDbStoreTests()
        {
            HotSpot.WireUp();
        }

        [TestMethod]
        public void RavenDbStoreShouldNotBeNull()
        {
            var ravendb = HotSpot.Resolve<IDbStore>();
            Assert.IsTrue(ravendb != null);
        }
    }
}
