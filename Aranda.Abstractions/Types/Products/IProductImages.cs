using System;
using System.ComponentModel.DataAnnotations;

namespace Aranda.Abstractions.Types.Products
{
    public interface IProductImages
    {
        [Key]
        public Guid ProductImagesId { get; set; }
        public Guid ProductId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
