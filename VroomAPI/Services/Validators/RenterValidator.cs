using FluentValidation;
using VroomAPI.Models.Renters;

namespace VroomAPI.Services.Validators
{
    public class RenterValidator : AbstractValidator<Renter>, IValidator<Renter>
    {
        public RenterValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(25).WithMessage("Name must not exceed 25 characters.")
                .MinimumLength(2).WithMessage("Name must be at least 2 characters long.")
                .Matches(@"^[а-яА-Я\s'-]+$").WithMessage("Name can only contain letters, spaces, hyphens, and apostrophes.")
                .Must(NotContainWhiteSpaces).WithMessage("Name cannot be only spaces");

            RuleFor(x => x.LastName)
               .NotEmpty().WithMessage("LastName is required.")
               .MaximumLength(25).WithMessage("Name must not exceed 25 characters.")
               .MinimumLength(2).WithMessage("Name must be at least 2 characters long.")
               .Matches(@"^[а-яА-Я\s'-]+$").WithMessage("Name can only contain letters, spaces, hyphens, and apostrophes.")
               .Must(NotContainWhiteSpaces).WithMessage("Name cannot be only spaces");

            RuleFor(x => x.MiddleName)
               .NotEmpty().WithMessage("MiddleName is required.")
               .MaximumLength(25).WithMessage("Name must not exceed 25 characters.")
               .MinimumLength(2).WithMessage("Name must be at least 2 characters long.")
               .Matches(@"^[а-яА-Я\s'-]+$").WithMessage("Name can only contain letters, spaces, hyphens, and apostrophes.")
               .Must(NotContainWhiteSpaces).WithMessage("Name cannot be only spaces");


            RuleFor(x => x.DateOfBirth)
               .NotEmpty().WithMessage("Date of birth is required.");

            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email is required.")
               .EmailAddress().WithMessage("Invalid email format.").
                Must(NotContainWhiteSpaces).WithMessage("Name cannot be only spaces");

            RuleFor(x => x.Phone)
               .NotEmpty().WithMessage("Phone number is required.")
               .Matches(@"^\+?(\d{1,3})?[-.\s]?\(?(\d{3})\)?[-.\s]?(\d{3})[-.\s]?(\d{4})$")
               .WithMessage("Invalid phone number format. Use country code with numbers.")
               .Must(NotContainWhiteSpaces).WithMessage("Name cannot be only spaces");

            RuleFor(x => x.Password)
              .NotEmpty().WithMessage("Password is required")
              .MinimumLength(8).WithMessage("Password must be at least 8 characters.");
            //.Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8}$")
            //.WithMessage("Password must contain at least one uppercase letter, one lowercase letter, and one digit.");

            RuleFor(x => x.RegistrationDate)
                .NotEmpty().WithMessage("Registration Date is required.");

            RuleFor(x => x.Rating)
                .InclusiveBetween(0, 5).When(x => x.Rating.HasValue).WithMessage("Rating must be between 0 and 5.");

            RuleFor(x => x.DriverLicenseNumber)
                .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.DriverLicenseNumber))
                .WithMessage("Driver License Number must not exceed 50 characters.")
                .Must(NotContainWhiteSpaces).WithMessage("Name cannot be only spaces");
        }
        private bool NotContainWhiteSpaces(string name)
        {
            return !string.IsNullOrWhiteSpace(name);
        }
    }
}
