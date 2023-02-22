using System.ComponentModel.DataAnnotations;

namespace bbqueue.Infrastructure.Validators
{
    public class MaxValue : ValidationAttribute
    {
        int maxValue { get; set; }

        public MaxValue(int maxValue)
        {
            this.maxValue = maxValue;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null)
                return new ValidationResult("Поле не может быть пустым");
            int result = (int)value;
            return (result <= maxValue)
                ? ValidationResult.Success
                : new ValidationResult("Значение поля не может быть больше " + maxValue);
        }
    }
}
