using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HostClientApplication.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EventId { get; set; }
        public double Price { get; set; }
        public int StockQty { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }
        public int ProductCategoryRefId { get; set; }
    }
}
