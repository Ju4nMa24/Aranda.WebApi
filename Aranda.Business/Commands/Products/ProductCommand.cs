using Aranda.Common.Generics;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Aranda.Business.Commands.Products
{
    public class ProductCommand : Base.CommandRequest<ProductResponse>
    {
        [Required]
        [JsonPropertyName("Name")]
        public string Name { get; set; }
        [Required]
        [JsonPropertyName("BriefDescription")]
        public string BriefDescription { get; set; }
        [Required]
        [JsonPropertyName("CategoryId")]
        public Guid CategoryId { get; set; }
    }
    public class ProductResponse : Base.CommandResponse
    {
        [JsonPropertyName("StatusCode")]
        public string StatusCode { get; set; }
    }
    public class ProductValidator : AbstractValidator<ProductCommand>
    {
        public ProductValidator()
        {
            RuleFor(request => request.CategoryId.ToString()).Must(Validation.ValidateGuidAndNull).WithMessage("El id de la categoría no es valida.");
            RuleFor(request => request.Name)
                    .NotNull().NotEmpty().WithMessage("El nombre del producto es requerido.")
                    .MaximumLength(50).WithMessage("La longitud del nombre producto es invalida.")
                    .MinimumLength(2).WithMessage("La longitud del nombre Producto es invalida.");
            RuleFor(request => request.BriefDescription)
                    .NotNull().NotEmpty().WithMessage("La descripción es requerida.")
                    .MaximumLength(50).WithMessage("La longitud de la descripción del producto es invalida.")
                    .MinimumLength(2).WithMessage("La longitud de la descripción del Producto es invalida.");
        }
    }
}
