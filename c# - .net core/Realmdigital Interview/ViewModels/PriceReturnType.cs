using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Realmdigital_Interview.ViewModels
{
    internal class PriceReturnType
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
