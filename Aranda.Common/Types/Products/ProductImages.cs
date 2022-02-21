using Aranda.Abstractions.Types.Products;
using System;

namespace Aranda.Common.Types.Products
{
    public class ProductImages : IProductImages
    {
        public Guid ProductImagesId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid ProductId { get; set; }
    }
}
