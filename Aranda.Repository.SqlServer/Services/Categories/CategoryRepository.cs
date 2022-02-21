using Aranda.Abstractions.Repositories.CategoriesRepositories;
using Aranda.Abstractions.Types.Categories;
using Aranda.Repository.SqlServer.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aranda.Repository.SqlServer.Services.Categories
{
    /// <summary>
    /// Category repository (contains the definition of the actions to perform).
    /// </summary>
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ArandaContext _arandaContext;
        public CategoryRepository(ArandaContext arandaContext) => _arandaContext = arandaContext;
        public async Task<bool> Create(ICategory category)
        {
            try
            {
                await Task.Run(() => _arandaContext.Add(category));
                _arandaContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Find(Guid categoryId) => _arandaContext.Categories.Find(categoryId).CategoryId != Guid.Empty;

        public async Task<IEnumerable<ICategory>> GetAll() => await Task.Run(() => _arandaContext.Categories.OrderByDescending(c => c.CreationDate).ToList());
    }
}
