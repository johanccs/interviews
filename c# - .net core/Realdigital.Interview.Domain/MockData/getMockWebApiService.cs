using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Realdigital.Interview.Domain.Contracts;
using Realdigital.Interview.Domain.DomainEntities;

namespace Realdigital.Interview.Domain.MockData
{
    public class GetMockWebApiService:IGetWebApi
    {
        public IList<ProductReturnType> GetProductByName(string productName)
        {
            throw new System.NotImplementedException();
        }

        public IList<ProductReturnType> GetProductById(string productId)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"MockData\single-product.json");
            var json = "";

            using (StreamReader sr = new StreamReader(filePath))
            {
                json = sr.ReadToEnd();

                ApiResponseProduct product = JsonConvert.DeserializeObject<ApiResponseProduct>(json);

                var prod = new ProductReturnType(product.BarCode, product.BarCode,new List<PriceReturnType>());
                var productList = new List<ProductReturnType>();

                foreach (var priceRecord in product.PriceRecords)
                {
                    prod.Prices.Add(new PriceReturnType(priceRecord.SellingPrice, priceRecord.CurrencyCode));
                }

                productList.Add(prod);

                return productList;
            }
        }
    }
}