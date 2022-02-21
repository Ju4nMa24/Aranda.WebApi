using Aranda.Common.Generics;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Aranda.Business.Commands.Categories
{
    public class CategoryCommand : Base.CommandRequest<CategoryResponse>
    {
        [Required]
        [JsonPropertyName("CategoryName")]
        public string CategoryName { get; set; }
    }
    public class CategoryResponse : Base.CommandResponse
    {
        [JsonPropertyName("StatusCode")]
        public string StatusCode { get; set; }
    }
    public class CategoryValidator : AbstractValidator<CategoryCommand>
    {
        public CategoryValidator()
        {
            RuleFor(request => request.CategoryName)
                    .NotNull().NotEmpty().WithMessage("El nombre de la categoría es requerida.")
                    .MaximumLength(50).WithMessage("La longitud del nombre es invalida.")
                    .MinimumLength(2).WithMessage("La longitud del nombre es invalida.")
                    .Must(Validation.ValidateRegexWithRegexPersonNameAndNull).WithMessage("El nombre de la categoría no es valida.");
        }
    }
}
