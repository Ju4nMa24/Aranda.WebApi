using Aranda.Business.Commands.Categories;
using Aranda.Business.Commands.Products;
using Aranda.Common.Types.Categories;
using Aranda.Common.Types.Products;
using AutoMapper;

namespace Aranda.WebApi.Mappers
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Entity Association Mapping.
        /// </summary>
        public MappingProfile()
        {
            //Product entites.
            CreateMap<Product, ProductCommand>();
            CreateMap<ProductCommand, Product>();
            //Product Images entities.
            CreateMap<ProductImages, ProductImagesCommand>();
            CreateMap<ProductImagesCommand, ProductImages>();
            //Category entites.
            CreateMap<CategoryCommand, Category>();
            CreateMap<Category, CategoryCommand>();

        }
    }
}
