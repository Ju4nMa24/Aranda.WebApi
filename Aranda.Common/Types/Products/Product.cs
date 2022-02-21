using Aranda.Abstractions.Types.Products;
using System;

namespace Aranda.Common.Types.Products
{
    public class Product : IProduct
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string BriefDescription { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime CreationDate { get; set; }
    }
    public class ProductList
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string BriefDescription { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
