using Aranda.Abstractions.Repositories.CategoriesRepositories;
using Aranda.Business.Commands.Categories;
using Aranda.Common.Generics;
using Aranda.Common.Types.Categories;
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
    public class CategoryProccesor : IRequestHandler<CategoryCommand, CategoryResponse>
    {
        #region INSTANTIATE
        private readonly CategoryResponse _categoryResponse;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<CategoryProccesor> _logger;
        private readonly IMapper _mapper;
        #endregion
        public CategoryProccesor(ICategoryRepository categoryRepository, ILogger<CategoryProccesor> logger, IMapper mapper)
        {
            _categoryResponse = new();
            _categoryRepository = categoryRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CategoryResponse> Handle(CategoryCommand request, CancellationToken cancellationToken)

        {
            try
            {
                //Validation of input parameters.
                if (!(new CategoryValidator()).Validate(request).IsValid)
                {
                    _categoryResponse.InnerContext = Resource.ErrorResponse(new()
                    {
                        Header = Constants.HeaderErrorMessage,
                        Parameter = Constants.Code01,
                        ResponseType = _categoryResponse
                    }).InnerContext;
                    _categoryResponse.InnerContext.Result.Details = (new CategoryValidator()).Validate(request).Errors.ToArray();
                    _categoryResponse.StatusCode = HttpStatusCode.BadRequest.ToString();
                    string detail = JsonConvert.SerializeObject(_categoryResponse.InnerContext);
                    _logger.LogWarning(detail);
                    return await Task.FromResult(_categoryResponse);
                }
                if (_categoryRepository.Create(_mapper.Map<Category>(request)).Result)
                {
                    _categoryResponse.InnerContext.Result.Success = true;
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
                else
                {
                    _categoryResponse.InnerContext = Resource.ErrorResponse(new()
                    {
                        Header = Constants.HeaderErrorMessage,
                        Parameter = Constants.Code02,
                        ResponseType = _categoryResponse
                    }).InnerContext;
                    _categoryResponse.StatusCode = HttpStatusCode.BadRequest.ToString();
                    string detail = JsonConvert.SerializeObject(_categoryResponse.InnerContext);
                    _logger.LogWarning(detail);
                    return _categoryResponse;
                }
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
