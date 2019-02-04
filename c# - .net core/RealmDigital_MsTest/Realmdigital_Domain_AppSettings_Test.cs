using Microsoft.VisualStudio.TestTools.UnitTesting;
using Realdigital.Interview.Domain.Contracts;
using Realdigital.Interview.Domain.Helpers;

namespace RealmDigital_MsTest
{
    [TestClass]
    public class Realmdigital_Domain_AppSettings_Test
    {
        [TestMethod]
        public void ExtractWebApiAddress_Success()
        {
            IConfigBuilder configBuilder = new ConfigBuilder();
            var webApiAddr = configBuilder.GetWebApiAddress();

            Assert.IsNotNull(webApiAddr);
        }

        [TestMethod]
        public void ExtractWebApiAddress_Length_Success()
        {
            IConfigBuilder configBuilder = new ConfigBuilder();
            var webApiAddr = configBuilder.GetWebApiAddress();

            Assert.IsTrue(webApiAddr.Length > 0);
        }
    }
}
