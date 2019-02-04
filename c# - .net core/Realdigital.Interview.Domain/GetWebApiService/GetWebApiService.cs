using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Realdigital.Interview.Domain.Helpers.Domain.Contracts;
using Realdigital.Interview.Domain.Helpers.Domain.DomainEntities;
using Realdigital.Interview.Domain.Helpers.Helpers;

namespace Realdigital.Interview.Domain.GetWebApiService
{
    public class GetWebApiService : IGetWebApi
    {
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

        private static List<ProductReturnType> BuildResultCollection(IEnumerable<ApiResponseProduct> responseObject)
        {
            //Remove initializing of List<object>
            var result = GetProducts(responseObject);

            return result;
        }

        private static List<ProductReturnType> GetProducts(IEnumerable<ApiResponseProduct> responseObject)
        {
            return (from product in responseObject
                let prices = GetPrices(product)
                select new ProductReturnType(product.BarCode, product.ItemName, prices)).ToList();
        }

        private static List<PriceReturnType> GetPrices(ApiResponseProduct product)
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

        private static IEnumerable<ApiResponseProduct> DeserializeApiResponseProducts(string response)
        {
            var responseProducts = JsonConvert.DeserializeObject<List<ApiResponseProduct>>(response);
            return responseProducts;
        }

        private static string GetResponse(string criteriaType, string productId)
        {
            string response;
            using (var client = new WebClient())
            {
                var data = "{\"" + criteriaType + "\"" + ":\"" + productId + "\"}";

                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                response = client.UploadString("http://192.168.0.241/eanlist?type=Web", "POST", data);
            }

            return response;
        }

        #endregion

    }
}
