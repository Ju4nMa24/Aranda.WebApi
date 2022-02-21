using Aranda.Abstractions.Repositories.ProductsRepositories;
using Aranda.Abstractions.Types.Products;
using Aranda.Repository.SqlServer.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aranda.Repository.SqlServer.Services.Products
{
    /// <summary>
    /// Product Images repository (contains the definition of the actions to perform).
    /// </summary>
    public class ProductImageRepository : IProductImagesRepository
    {
        private readonly ArandaContext _arandaContext;
        public ProductImageRepository(ArandaContext arandaContext) => _arandaContext = arandaContext;
        public Task<bool> Create(IProductImages productImages)
        {
            try
            {
                _arandaContext.Add(productImages);
                _arandaContext.SaveChanges();
                return Task.FromResult(true);
            }
            catch
            {
                return Task.FromResult(false);
            }
        }

        public IEnumerable<IProductImages> GetAll(Guid productId) => _arandaContext.ProductImages.Where(p => p.ProductId == productId).ToList();
    }
}
