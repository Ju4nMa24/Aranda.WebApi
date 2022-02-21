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
    public class ProductImagesProccesor : IRequestHandler<ProductImagesCommand, ProductImagesResponse>
    {
        #region INSTANTIATE
        private readonly ProductImagesResponse _productResponse;
        private readonly IProductImagesRepository _productImagesRepository;
        private readonly ILogger<ProductImagesProccesor> _logger;
        private readonly IMapper _mapper;
        #endregion
        public ProductImagesProccesor(IProductImagesRepository productImagesRepository, ILogger<ProductImagesProccesor> logger, IMapper mapper)
        {
            _productResponse = new();
            _productImagesRepository = productImagesRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ProductImagesResponse> Handle(ProductImagesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //Validation of input parameters.
                if (!(new ProductImagesValidator()).Validate(request).IsValid)
                {
                    _productResponse.InnerContext = Resource.ErrorResponse(new()
                    {
                        Header = Constants.HeaderErrorMessage,
                        Parameter = Constants.Code01,
                        ResponseType = _productResponse
                    }).InnerContext;
                    _productResponse.InnerContext.Result.Details = (new ProductImagesValidator()).Validate(request).Errors.ToArray();
                    _productResponse.StatusCode = HttpStatusCode.BadRequest.ToString();
                    string detail = JsonConvert.SerializeObject(_productResponse.InnerContext);
                    _logger.LogWarning(detail);
                    return await Task.FromResult(_productResponse);
                }
                bool state = Parallel.ForEach(request.ImagesUrl, url =>
                {
                    _productImagesRepository.Create(new ProductImages()
                    {
                        ProductImagesId = Guid.NewGuid(),
                        CreationDate = DateTime.UtcNow,
                        ProductId = Guid.Parse(request.ProductId),
                        ImageUrl = url
                    });
                }).IsCompleted;
                if (state)
                {
                    _productResponse.InnerContext.Result.Success = true;
                    _productResponse.InnerContext = Resource.SuccessMessage(new()
                    {
                        Header = Constants.SuccessMessage,
                        Parameter = Constants.Code00,
                        ResponseType = _productResponse
                    }).InnerContext;
                    _productResponse.StatusCode = HttpStatusCode.OK.ToString();
                    string detail = JsonConvert.SerializeObject(_productResponse.InnerContext);
                    _logger.LogInformation(detail);
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
                    string detail = JsonConvert.SerializeObject(_productResponse.InnerContext);
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
