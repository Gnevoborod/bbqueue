using System.ComponentModel.DataAnnotations;

namespace bbqueue.Infrastructure.Validators
{
    public class MinValue:ValidationAttribute
    {
        int minValue {get; set; }

        public MinValue(int minValue)
        {
            this.minValue = minValue;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null)
                return new ValidationResult("Поле не может быть пустым");
            int result = (int)value;
            return (result >= minValue)
                ? ValidationResult.Success
                : new ValidationResult("Значение поля не может быть меньше " + minValue);
        }
    }
}
