using Aranda.Abstractions.Repositories.ProductsRepositories;
using Aranda.Abstractions.Types.Products;
using Aranda.Common.Types.Products;
using Aranda.Repository.SqlServer.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aranda.Repository.SqlServer.Services
{
    /// <summary>
    /// Product repository (contains the definition of the actions to perform).
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly ArandaContext _arandaContext;
        public ProductRepository(ArandaContext arandaContext) => _arandaContext = arandaContext;
        public bool Create(IProduct product)
        {
            try
            {
                _arandaContext.Add(product);
                _arandaContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid productId)
        {
            try
            {
                await Task.Run(() => _arandaContext.Products.Remove(_arandaContext.Products.Find(productId)));
                _arandaContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public dynamic Find(Guid productId) => _arandaContext.Products.Where(p => p.ProductId == productId).FirstOrDefault();

        public async Task<IEnumerable<dynamic>> GetAll()
        {
            List<Product> products = await Task.Run(() => _arandaContext.Products.ToList());
            List<ProductList> list = new();
            foreach (Product item in products)
            {
                list.Add(new()
                {
                    BriefDescription = item.BriefDescription,
                    CategoryId = item.CategoryId,
                    CategoryName = _arandaContext.Categories.Where(c => c.CategoryId == item.CategoryId).FirstOrDefault()?.CategoryName,
                    CreationDate = item.CreationDate,
                    Name = item.Name,
                    ProductId = item.ProductId
                });
            }
            return list;
        }
        public bool Update(IProduct product, Guid categoryId)
        {
            try
            {
                Product productResponse = _arandaContext.Products.Where(p => p.ProductId == product.ProductId).FirstOrDefault();
                productResponse.Name = product.Name;
                productResponse.CategoryId = categoryId == Guid.Empty ? productResponse.CategoryId : categoryId;
                productResponse.BriefDescription = string.IsNullOrEmpty(product.BriefDescription) ? productResponse.BriefDescription : product.BriefDescription;
                _arandaContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
