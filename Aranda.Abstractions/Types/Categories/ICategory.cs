using System;
using System.ComponentModel.DataAnnotations;

namespace Aranda.Abstractions.Types.Categories
{
    public interface ICategory
    {
        [Key]
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
