using FluentValidation;
using OnlineActivities.Models;
using System.Text.RegularExpressions;


namespace WebApplication1.Validator
{
    public class CompanyValidator : AbstractValidator<Company>

    {
        readonly Regex regEx = new Regex("[a-zA-Z0-9]");

        public CompanyValidator()
        {
            RuleFor(x => x.CompanyId).NotNull().WithMessage("Boş olamaz!");
            RuleFor(x => x.CompanyName).NotEmpty().WithMessage("Boş olamaz!");
            RuleFor(x => x.WebSiteDomain).NotEmpty().WithMessage("Boş olamaz!");
            RuleFor(x => x.Mail)                
                .NotEmpty().WithMessage("Boş olamaz!")
                .EmailAddress().WithMessage("Geçerli mail adresi giriniz!");
            RuleFor(x => x.Password).NotEmpty().Matches(regEx).WithMessage("Şifre en az bir harf,bir rakam içermelidir ve boş olamaz! ");

        }

    }

}
