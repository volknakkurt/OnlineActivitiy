using FluentValidation;
using OnlineActivities.Models;
using System.Net.Mail;
using System.Text.RegularExpressions;


namespace WebApplication1.Validator
{
    public class ParticipantValidator : AbstractValidator<Participant>

    {
        readonly Regex regEx = new Regex("[a-zA-Z0-9]");

        public ParticipantValidator()
        {
            RuleFor(x => x.ParticipantId).NotNull().WithMessage("Boş olamaz!");
            RuleFor(x => x.RoleId).NotEmpty().WithMessage("Boş olamaz!");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Boş olamaz!");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Boş olamaz!");
            RuleFor(x => x.JoinToAc).NotEmpty().WithMessage("Katılmak istiyorsan 'katılıyorum' yazmalısın");
            RuleFor(x => x.Mail)
                .NotEmpty().WithMessage("Boş olamaz!")
                .EmailAddress().WithMessage("Geçerli mail adresi giriniz!") ;
            RuleFor(x => x.Password).NotEmpty().Matches(regEx).WithMessage("Şifre en az bir harf,bir rakam içermelidir ve boş olamaz! ");
            RuleFor(x => x.PasswordAgain).NotEmpty().Equal(x => x.Password).WithMessage("Şifre farklı olamaz!! ");
        }

    }

}
