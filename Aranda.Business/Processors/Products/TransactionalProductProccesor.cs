using Aranda.Abstractions.Repositories.ProductsRepositories;
using Aranda.Business.Commands.Products;
using Aranda.Common.Generics;
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
    public class TransactionalProductProccesor : IRequestHandler<TransactionalProductCommand, TransactionalProductResponse>
    {
        #region INSTANTIATE
        private readonly TransactionalProductResponse _transactionalProductResponse;
        private readonly IProductRepository _productRepository;
        private readonly IProductImagesRepository _productImagesRepository;
        private readonly ILogger<TransactionalProductProccesor> _logger;
        private readonly IMapper _mapper;
        #endregion
        public TransactionalProductProccesor(IProductRepository productRepository, ILogger<TransactionalProductProccesor> logger, IMapper mapper, IProductImagesRepository productImagesRepository)
        {
            _transactionalProductResponse = new();
            _productRepository = productRepository;
            _productImagesRepository = productImagesRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<TransactionalProductResponse> Handle(TransactionalProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string detail = string.Empty;
                switch (request.ProccessType)
                {
                    case Proccesors.GetAll:
                        _transactionalProductResponse.InnerContext.Result.Success = true;
                        _transactionalProductResponse.InnerContext = Resource.SuccessMessage(new()
                        {
                            Header = Constants.SuccessMessage,
                            Parameter = Constants.Code00,
                            ResponseType = _transactionalProductResponse
                        }).InnerContext;
                        _transactionalProductResponse.StatusCode = HttpStatusCode.OK.ToString();
                        _transactionalProductResponse.ProductList = await _productRepository.GetAll();
                        detail = JsonConvert.SerializeObject(_transactionalProductResponse.InnerContext);
                        _logger.LogInformation(detail);
                        return _transactionalProductResponse;
                    case Proccesors.Delete:
                        if (_productRepository.Delete(request.ProductRequest.ProductId).Result)
                        {
                            _transactionalProductResponse.InnerContext.Result.Success = true;
                            _transactionalProductResponse.InnerContext = Resource.SuccessMessage(new()
                            {
                                Header = Constants.SuccessMessage,
                                Parameter = Constants.Code00,
                                ResponseType = _transactionalProductResponse
                            }).InnerContext;
                            _transactionalProductResponse.StatusCode = HttpStatusCode.OK.ToString();
                        }
                        else
                        {
                            _transactionalProductResponse.InnerContext = Resource.ErrorResponse(new()
                            {
                                Header = Constants.HeaderErrorMessage,
                                Parameter = Constants.Code02,
                                ResponseType = _transactionalProductResponse
                            }).InnerContext;
                            _transactionalProductResponse.StatusCode = HttpStatusCode.BadRequest.ToString();
                        }
                        detail = JsonConvert.SerializeObject(_transactionalProductResponse.InnerContext);
                        _logger.LogInformation(detail);
                        return _transactionalProductResponse;
                    case Proccesors.Update:
                        if (_productRepository.Update(request.ProductRequest,request.CategoryId))
                        {
                            _transactionalProductResponse.InnerContext.Result.Success = true;
                            _transactionalProductResponse.InnerContext = Resource.SuccessMessage(new()
                            {
                                Header = Constants.SuccessMessage,
                                Parameter = Constants.Code00,
                                ResponseType = _transactionalProductResponse
                            }).InnerContext;
                            _transactionalProductResponse.StatusCode = HttpStatusCode.OK.ToString();
                        }
                        else
                        {
                            _transactionalProductResponse.InnerContext = Resource.ErrorResponse(new()
                            {
                                Header = Constants.HeaderErrorMessage,
                                Parameter = Constants.Code02,
                                ResponseType = _transactionalProductResponse
                            }).InnerContext;
                            _transactionalProductResponse.StatusCode = HttpStatusCode.BadRequest.ToString();
                        }
                        detail = JsonConvert.SerializeObject(_transactionalProductResponse.InnerContext);
                        _logger.LogInformation(detail);
                        return _transactionalProductResponse;
                    case Proccesors.Find:
                        _transactionalProductResponse.InnerContext.Result.Success = true;
                        _transactionalProductResponse.InnerContext = Resource.SuccessMessage(new()
                        {
                            Header = Constants.SuccessMessage,
                            Parameter = Constants.Code00,
                            ResponseType = _transactionalProductResponse
                        }).InnerContext;
                        _transactionalProductResponse.StatusCode = HttpStatusCode.OK.ToString();
                        _transactionalProductResponse.ProductResponse = _productRepository.Find(request.ProductRequest.ProductId);
                        detail = JsonConvert.SerializeObject(_transactionalProductResponse.InnerContext);
                        _logger.LogInformation(detail);
                        return _transactionalProductResponse;
                    case Proccesors.GetAllImages:
                        _transactionalProductResponse.InnerContext.Result.Success = true;
                        _transactionalProductResponse.InnerContext = Resource.SuccessMessage(new()
                        {
                            Header = Constants.SuccessMessage,
                            Parameter = Constants.Code00,
                            ResponseType = _transactionalProductResponse
                        }).InnerContext;
                        _transactionalProductResponse.StatusCode = HttpStatusCode.OK.ToString();
                        _transactionalProductResponse.ProductImages = _productImagesRepository.GetAll(request.ProductRequest.ProductId);
                        detail = JsonConvert.SerializeObject(_transactionalProductResponse.InnerContext);
                        _logger.LogInformation(detail);
                        return _transactionalProductResponse;
                    default:
                        _transactionalProductResponse.InnerContext = Resource.ErrorResponse(new()
                        {
                            Header = Constants.HeaderErrorMessage,
                            Parameter = Constants.Code02,
                            ResponseType = _transactionalProductResponse
                        }).InnerContext;
                        _transactionalProductResponse.StatusCode = HttpStatusCode.BadRequest.ToString();
                        detail = JsonConvert.SerializeObject(_transactionalProductResponse.InnerContext);
                        _logger.LogInformation(detail);
                        return _transactionalProductResponse;
                }
            }
            catch (Exception ex)
            {
                _transactionalProductResponse.InnerContext = Resource.ErrorResponse(new()
                {
                    Header = Constants.HeaderErrorMessage,
                    Parameter = Constants.Code99,
                    ResponseType = _transactionalProductResponse
                }).InnerContext;
                _transactionalProductResponse.StatusCode = HttpStatusCode.InternalServerError.ToString();
                _logger.LogError(ex.Message, ex.InnerException);
                return _transactionalProductResponse;
            }
        }
    }
}
