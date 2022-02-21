using Aranda.Abstractions.Types.Categories;
using System;

namespace Aranda.Common.Types.Categories
{
    public class Category : ICategory
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
