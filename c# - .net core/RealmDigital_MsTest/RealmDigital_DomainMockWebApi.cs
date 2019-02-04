using Microsoft.VisualStudio.TestTools.UnitTesting;
using Realdigital.Interview.Domain.Contracts;
using Realdigital.Interview.Domain.MockData;

namespace RealmDigital_MsTest
{
    [TestClass]
    public class RealmDigital_DomainMockWebApi
    {
        [TestMethod]
        public void GetMockProductData()
        {
            IGetWebApi getWebApi = new GetMockWebApiService();

            var result = getWebApi.GetProductById("0");

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }
    }
}
