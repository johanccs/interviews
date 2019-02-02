﻿using System.Collections.Generic;

namespace Realdigital.Interview.ViewModels
{
    internal class ProductReturnType
    {
        public string Id { get;}
        public string ItemName { get;}
        public List<PriceReturnType> Prices { get;}

        public ProductReturnType(string id, string itemName, List<PriceReturnType>prices)
        {
            this.Id = id;
            this.ItemName = itemName;
            this.Prices = prices;
        }
    }
}
