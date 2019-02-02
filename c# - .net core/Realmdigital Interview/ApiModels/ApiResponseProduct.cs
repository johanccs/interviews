﻿using System.Collections.Generic;

namespace Realdigital.Interview.ApiModels
{
    public class ApiResponseProduct
    {
        public string BarCode { get; set; }
        public string ItemName { get; set; }
        public List<ApiResponsePrice> PriceRecords { get; set; }
    }
}
