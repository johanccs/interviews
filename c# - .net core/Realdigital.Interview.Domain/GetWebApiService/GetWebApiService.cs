using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Realdigital.Interview.Domain.Contracts;
using Realdigital.Interview.Domain.DomainEntities;
using Realdigital.Interview.Domain.Helpers;
using Realdigital.Interview.Domain.Helpers.Helpers;

namespace Realdigital.Interview.Domain.GetWebApiService
{
    public class GetWebApiService : IGetWebApi
    {
        #region Fields

        private readonly IConfigBuilder _configBuilder;

        #endregion

        #region Constructor

        public GetWebApiService()
        {
            //Production version would not inject/create object like this. Inject with unity. 
            //Leave it for now due to time constraints
            _configBuilder = new ConfigBuilder();
        }

        #endregion

        #region Public methods

        public IList<ProductReturnType> GetProductById(string productId)
        {
            //Extract the using statement into a method with productId as parameter
            //Create inline variable with var
            var response = GetResponse("productId", productId);

            //Extract into own method.Give method a meaningful name
            var responseObject = DeserializeApiResponseProducts(response);

            //Extract iteration procedure into separate method for higher cohesion
            return BuildResultCollection(responseObject);
        }

        public IList<ProductReturnType> GetProductByName(string productName)
        {
            var response = GetResponse("productName", productName);

            var responseObject = DeserializeApiResponseProducts(response);

            //Use existing method to build the result 
            return BuildResultCollection(responseObject);
        }

        #endregion

        #region Private methods

        private List<ProductReturnType> BuildResultCollection(IEnumerable<ApiResponseProduct> responseObject)
        {
            //Remove initializing of List<object>
            var result = GetProducts(responseObject);

            return result;
        }

        private List<ProductReturnType> GetProducts(IEnumerable<ApiResponseProduct> responseObject)
        {
            return (from product in responseObject
                let prices = GetPrices(product)
                select new ProductReturnType(product.BarCode, product.ItemName, prices)).ToList();
        }

        private List<PriceReturnType> GetPrices(ApiResponseProduct product)
        {
            var prices = new List<PriceReturnType>();

            foreach (var responsePrice in product.PriceRecords)
            {
                //Replace magic string with Constant enums - Helpers -> CurrencyEnum
                if (responsePrice.CurrencyCode == CurrencyEnum.ZAR)
                    prices.Add(new PriceReturnType(responsePrice.SellingPrice, responsePrice.CurrencyCode));
            }

            return prices;
        }

        private IEnumerable<ApiResponseProduct> DeserializeApiResponseProducts(string response)
        {
            var responseProducts = JsonConvert.DeserializeObject<List<ApiResponseProduct>>(response);
            return responseProducts;
        }

        private string GetResponse(string criteriaType, string productId)
        {
            string response;
            using (var client = new WebClient())
            {
                var data = "{\"" + criteriaType + "\"" + ":\"" + productId + "\"}";
                //Remove magic address string by placing the webapi address inside a configuration file. 
                var address = _configBuilder.GetWebApiAddress();

                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                response = client.UploadString(address, "POST", data);
            }

            return response;
        }

        #endregion

    }
}
