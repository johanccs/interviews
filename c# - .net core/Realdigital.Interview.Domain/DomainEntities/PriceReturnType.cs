namespace Realdigital.Interview.Domain.DomainEntities
{
    public class PriceReturnType
    {
        public  string Price { get; }
        public string Currency { get; }

        public PriceReturnType(string price, string currency)
        {
            this.Price = price;
            this.Currency = currency;
        }
    }
}
