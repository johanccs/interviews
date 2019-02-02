using Realdigital.Interview.Domain.DomainEntities;

namespace Realdigital.Interview.Domain.Contracts
{
    public interface IGetWebApi
    {
        ProductReturnType GetProductByName(string productName);
        ProductReturnType GetProductById(string productId);
    }
}
