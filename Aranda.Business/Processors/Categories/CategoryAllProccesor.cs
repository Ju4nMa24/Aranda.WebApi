using Aranda.Abstractions.Repositories.CategoriesRepositories;
using Aranda.Business.Commands.Categories;
using Aranda.Common.Generics;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Aranda.Business.Processors.Categories
{
    public class CategoryAllProccesor : IRequestHandler<CategoryAllCommand, CategoryAllResponse>
    {
        #region INSTANTIATE
        private readonly CategoryAllResponse _categoryResponse;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryAllProccesor> _logger;
        private readonly IMapper _mapper;
        #endregion
        public CategoryAllProccesor(ICategoryRepository categoryRepository, ILogger<CategoryAllProccesor> logger, IMapper mapper)
        {
            _categoryResponse = new();
            _categoryRepository = categoryRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CategoryAllResponse> Handle(CategoryAllCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _categoryResponse.InnerContext.Result.Success = true;
                _categoryResponse.Categories = await _categoryRepository.GetAll();
                _categoryResponse.InnerContext = Resource.SuccessMessage(new()
                {
                    Header = Constants.SuccessMessage,
                    Parameter = Constants.Code00,
                    ResponseType = _categoryResponse
                }).InnerContext;
                _categoryResponse.StatusCode = HttpStatusCode.OK.ToString();
                string detail = JsonConvert.SerializeObject(_categoryResponse.InnerContext);
                _logger.LogInformation(detail);
                return _categoryResponse;
            }
            catch (Exception ex)
            {
                _categoryResponse.InnerContext = Resource.ErrorResponse(new()
                {
                    Header = Constants.HeaderErrorMessage,
                    Parameter = Constants.Code99,
                    ResponseType = _categoryResponse
                }).InnerContext;
                _categoryResponse.StatusCode = HttpStatusCode.InternalServerError.ToString();
                _logger.LogError(ex.Message, ex.InnerException);
                return _categoryResponse;
            }
        }
    }
}
