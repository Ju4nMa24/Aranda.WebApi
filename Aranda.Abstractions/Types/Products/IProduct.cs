using System;
using System.ComponentModel.DataAnnotations;

namespace Aranda.Abstractions.Types.Products
{
    public interface IProduct
    {
        [Key]
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string BriefDescription { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
