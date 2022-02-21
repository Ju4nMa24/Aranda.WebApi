using Aranda.Abstractions.Repositories.CategoriesRepositories;
using Aranda.Abstractions.Repositories.ProductsRepositories;
using Aranda.Business.Commands.Products;
using Aranda.Common.Generics;
using Aranda.Common.Types.Products;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Aranda.Business.Processors.Products
{
    /// <summary>
    /// Business logic for the product process.
    /// </summary>
    public class ProductProccesor : IRequestHandler<ProductCommand, ProductResponse>
    {
        #region INSTANTIATE
        private readonly ProductResponse _productResponse;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<ProductProccesor> _logger;
        private readonly IMapper _mapper;
        #endregion
        public ProductProccesor(IProductRepository productRepository, ILogger<ProductProccesor> logger, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _productResponse = new();
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<ProductResponse> Handle(ProductCommand request, CancellationToken cancellationToken)
        {
            string detail = string.Empty;
            try
            {
                //Validation of input parameters.
                if (!(new ProductValidator()).Validate(request).IsValid)
                {
                    _productResponse.InnerContext = Resource.ErrorResponse(new()
                    {
                        Header = Constants.HeaderErrorMessage,
                        Parameter = Constants.Code01,
                        ResponseType = _productResponse
                    }).InnerContext;
                    _productResponse.InnerContext.Result.Details = (new ProductValidator()).Validate(request).Errors.ToArray();
                    _productResponse.StatusCode = HttpStatusCode.BadRequest.ToString();
                    detail = JsonConvert.SerializeObject(_productResponse.InnerContext);
                    _logger.LogWarning(detail);
                    return await Task.FromResult(_productResponse);
                }
                if (_categoryRepository.Find(request.CategoryId))
                {
                    if (_productRepository.Create(_mapper.Map<Product>(request)))
                    {
                        _productResponse.InnerContext.Result.Success = true;
                        _productResponse.InnerContext = Resource.SuccessMessage(new()
                        {
                            Header = Constants.SuccessMessage,
                            Parameter = Constants.Code00,
                            ResponseType = _productResponse
                        }).InnerContext;
                        _productResponse.StatusCode = HttpStatusCode.OK.ToString();
                        detail = JsonConvert.SerializeObject(_productResponse.InnerContext);
                        _logger.LogInformation(detail);
                        return _productResponse;
                    }
                    _productResponse.InnerContext = Resource.ErrorResponse(new()
                    {
                        Header = Constants.HeaderErrorMessage,
                        Parameter = Constants.Code02,
                        ResponseType = _productResponse
                    }).InnerContext;
                    _productResponse.StatusCode = HttpStatusCode.BadRequest.ToString();
                    detail = JsonConvert.SerializeObject(_productResponse.InnerContext);
                    _logger.LogWarning(detail);
                    return _productResponse;
                }
                else
                {
                    _productResponse.InnerContext = Resource.ErrorResponse(new()
                    {
                        Header = Constants.HeaderErrorMessage,
                        Parameter = Constants.Code02,
                        ResponseType = _productResponse
                    }).InnerContext;
                    _productResponse.StatusCode = HttpStatusCode.BadRequest.ToString();
                    detail = JsonConvert.SerializeObject(_productResponse.InnerContext);
                    _logger.LogWarning(detail);
                    return _productResponse;
                }
            }
            catch (Exception ex)
            {
                _productResponse.InnerContext = Resource.ErrorResponse(new()
                {
                    Header = Constants.HeaderErrorMessage,
                    Parameter = Constants.Code99,
                    ResponseType = _productResponse
                }).InnerContext;
                _productResponse.StatusCode = HttpStatusCode.InternalServerError.ToString();
                _logger.LogError(ex.Message, ex.InnerException);
                return _productResponse;
            }
        }
    }
}
