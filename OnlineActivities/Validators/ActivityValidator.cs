namespace WebApplication1.Validator


{
    using FluentValidation;
    using OnlineActivities.Models;
    using System.Text.RegularExpressions;
    

    public class ActivityValidator : AbstractValidator<Activity>
    {

        public ActivityValidator()
        {
            RuleFor(x => x.ActivityId).NotNull().WithMessage("Boş olamaz!");
            RuleFor(x => x.ActivityName).NotEmpty().WithMessage("Boş olamaz!");
            RuleFor(x => x.Date).NotEmpty().LessThan(p => DateTime.Now).WithMessage("Girilen tarih geçmiş!");
            RuleFor(x => x.Deadline).NotEmpty().LessThan(p => p.Date).WithMessage("Girilen tarih geçmiş!");
            RuleFor(x => x.Adress).NotEmpty().WithMessage("Adres Giriniz!");
            RuleFor(x => x.Availability).NotNull().WithMessage("Boş olamaz!");
            RuleFor(x => x.Cost).NotNull().WithMessage("Boş olamaz!");
            RuleFor(x => x.OrganizerId).NotNull().WithMessage("Boş olamaz!");
            RuleFor(x => x.CityId).NotNull().WithMessage("Boş olamaz!");
            RuleFor(x => x.CategoryId).NotNull().WithMessage("Boş olamaz!");
        }

    }
}
