using FluentValidation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Aranda.Business.Commands.Products
{
    public class ProductImagesCommand : Base.CommandRequest<ProductImagesResponse>
    {
        [Required]
        [JsonPropertyName("ImagesUrl")]
        public List<string> ImagesUrl { get; set; }
        [Required]
        [JsonPropertyName("ProductId")]
        public string ProductId { get; set; }
    }
    public class ProductImagesResponse : Base.CommandResponse
    {
        [JsonPropertyName("StatusCode")]
        public string StatusCode { get; set; }
    }
    public class ProductImagesValidator : AbstractValidator<ProductImagesCommand>
    {
        public ProductImagesValidator()
        {
            RuleFor(request => request.ProductId)
                    .NotNull().NotEmpty().WithMessage("El id del producto es requerido.")
                    .MinimumLength(10).WithMessage("La longitud del id del producto es invalida.");
        }
    }
}
