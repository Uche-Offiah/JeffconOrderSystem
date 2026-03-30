using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using OrderService.Application.DTOs;

namespace OrderService.Application.Validators
{
    public class CreateOrderRequestValidation : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidation()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than Zero");
        }
    }
}
