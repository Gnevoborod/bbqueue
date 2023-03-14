using System.ComponentModel.DataAnnotations;
using System.Text;
using bbqueue.Domain.Models;

namespace bbqueue.Infrastructure.Validators
{
    public class EnumValueScope : ValidationAttribute
    {
        string[] values;
        Type type;

        public EnumValueScope(Type t)
        {
            if(!t.IsEnum)
            {
                type = t;
                throw new Exception("Используемый в ограничении тип - не enum");
            }

            this.values = Enum.GetNames(t);
            this.type = t;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Пустое значение");
            }
            if(!type.IsEnum)
            {
                return new ValidationResult("Используемый в ограничении тип - не enum");
            }
            string compare = new(value.ToString());//какая-то абсолютно идиотская вышла заглушка, но иначе ругалось
            foreach(var nextItem in values)
            {
                if (nextItem.ToLower()==compare.ToLower())
                {
                    var enumItems = Enum.GetNames(type);
                    foreach (var enumItem in enumItems)
                    {
                        if (compare.ToLower() == enumItem.ToString().ToLower())
                            return ValidationResult.Success;

                    }
                }
                
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("Значение не найдено среди допустимых: ");
            for(int i=0; i<values.Length;i++)
            {
                var nextItem = " " + values[i] + (i + 1==values.Length ? "": ",");
                sb.Append(nextItem);
            }
            return new ValidationResult(sb.ToString());
        }
    }
}
