using FluentValidation;

namespace EnterpriseBilling.Models.Validators
{
    public class CustomerProfileValidator : AbstractValidator<CustomerProfile>
    {
        public CustomerProfileValidator()
        {
            RuleFor(x => x.CompanyName).NotEmpty().NotNull().WithMessage("The Company Name is required.")
                .Length(0, 250).WithMessage("The Company Name cannot be more than 250 characters.");
            //RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("The Email Address is required.")
            //    .EmailAddress().WithMessage("Invalid Email Address.");
        }
    }
}
