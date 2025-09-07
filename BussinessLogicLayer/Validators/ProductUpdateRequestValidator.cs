using BussinessLogicLayer.DTO;
using FluentValidation;


namespace BussinessLogicLayer.Validators
{
    public class ProductUpdateRequestValidator:AbstractValidator<ProductUpdateRequest>
    {
        public ProductUpdateRequestValidator()
        {
            RuleFor(temp => temp.ProductID).NotEmpty().WithMessage("Product ID should not be blank");
            RuleFor(temp => temp.ProductName).NotEmpty().WithMessage("Product Name can't be blank");
            RuleFor(temp => temp.category).IsInEnum().WithMessage("Cannot be null.");
            RuleFor(temp => temp.UnitPrice).InclusiveBetween(0, double.MaxValue).WithMessage($"Unit Price should be btween 0 to {double.MaxValue}");
            RuleFor(temp => temp.QuantityInStock).InclusiveBetween(0, int.MaxValue).WithMessage($"Quantity should be btween 0 to {int.MaxValue}");

        }
    }
}
