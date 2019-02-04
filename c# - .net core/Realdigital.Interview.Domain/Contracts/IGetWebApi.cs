using System.Collections.Generic;
using Realdigital.Interview.Domain.Helpers.Domain.DomainEntities;

namespace Realdigital.Interview.Domain.Helpers.Domain.Contracts
{
    public interface IGetWebApi
    {
        IList<ProductReturnType> GetProductByName(string productName);
        IList<ProductReturnType> GetProductById(string productId);
    }
}
