using BussinessLogicLayer.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Validators
{
    public class ProductAddRequestValidator:AbstractValidator<ProductAddRequest>
    {
        public ProductAddRequestValidator()
        {
            //Product Name
            RuleFor(temp => temp.ProductName).NotEmpty().WithMessage("Product Name can't be blank");
            RuleFor(temp => temp.category).IsInEnum().WithMessage("Category cannot be null.");
            RuleFor(temp => temp.UnitPrice).InclusiveBetween(0, double.MaxValue).WithMessage($"Unit Price should be btween 0 to {double.MaxValue}");
            RuleFor(temp => temp.QuantityInStock).InclusiveBetween(0, int.MaxValue).WithMessage($"Quantity should be btween 0 to {int.MaxValue}");

        }
    }
}
