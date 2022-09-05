using FluentValidation;
using OnlineActivities.Models;
using System.Text.RegularExpressions;


namespace WebApplication1.Validator
{
    public class OrganizerValidator : AbstractValidator<Organizer>

    {
        readonly Regex regEx = new Regex("[a-zA-Z0-9]");

        public OrganizerValidator()
        {
            RuleFor(x => x.OrganizerId).NotNull().WithMessage("Boş olamaz!");
            RuleFor(x => x.RoleId).NotEmpty().WithMessage("Boş olamaz!");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Boş olamaz!");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Boş olamaz!");
            RuleFor(x => x.Mail)
                .NotEmpty().WithMessage("Boş olamaz!")
                .EmailAddress().WithMessage("Geçerli mail adresi giriniz!");
            RuleFor(x => x.Password).NotEmpty().Matches(regEx).WithMessage("Şifre en az bir harf,bir rakam içermelidir ve boş olamaz! ");
            RuleFor(x => x.PasswordAgain).NotEmpty().Equal(x => x.Password).WithMessage("Şifre farklı olamaz!! ");
        }

    }
}

