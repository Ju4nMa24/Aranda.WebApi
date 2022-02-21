using Aranda.Abstractions.Types.Categories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aranda.Abstractions.Repositories.CategoriesRepositories
{
    public interface ICategoryRepository
    {
        Task<bool> Create(ICategory category);
        bool Find(Guid categoryId);
        Task<IEnumerable<ICategory>> GetAll();
    }
}
