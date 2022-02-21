using Aranda.Abstractions.Types.Products;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aranda.Abstractions.Repositories.ProductsRepositories
{
    public interface IProductImagesRepository
    {
        Task<bool> Create(IProductImages productImages);
        IEnumerable<IProductImages> GetAll(Guid productId);
    }
}
