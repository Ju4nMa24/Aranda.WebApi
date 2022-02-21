using Aranda.Abstractions.Types.Products;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aranda.Abstractions.Repositories.ProductsRepositories
{
    public interface IProductRepository
    {
        bool Create(IProduct product);
        Task<IEnumerable<dynamic>> GetAll();
        dynamic Find(Guid productId);
        Task<bool> Delete(Guid productId);
        bool Update(IProduct product, Guid categoryId);
    }
}
