using Euronext.Domain.Entities;
using FluentValidation;

namespace EuroNext.Validators
{
    public class RequestValidator
    {
        public DateOnly date {  get; set; }
        public WeatherForecast forecast { get; set; }

        public class ForecastValidator : AbstractValidator<WeatherForecast>
        {
            public ForecastValidator() 
            {
                RuleFor(x => x.Date).NotEmpty();
                RuleFor(x => x.Date).Must(x=>x> DateOnly.FromDateTime(DateTime.Now)).WithMessage("Cannot accept dates in the past") ;
             }
        }

        public class DateValidator : AbstractValidator<DateOnly>
        {
            public DateValidator()
            {
                RuleFor(x => x).NotEmpty();
                RuleFor(x => x).Must(x => x > DateOnly.FromDateTime(DateTime.Now)).WithMessage("Cannot accept dates in the past");
            }
        }
    }
}
