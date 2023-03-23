using System.ComponentModel.DataAnnotations;
using System.Text;
using bbqueue.Domain.Models;
using bbqueue.Infrastructure.Exceptions;

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
                throw new ApiException(ExceptionEvents.EnumValidatorTypeNotEnum); 
            }

            this.values = Enum.GetNames(t);
            this.type = t;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                throw new ApiException(ExceptionEvents.ValidatorEmptyValue);
            }
            if(!type.IsEnum)
            {
                throw new ApiException(ExceptionEvents.EnumValidatorTypeNotEnum);
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
            for(int i=0; i<values.Length;i++)
            {
                var nextItem = " " + values[i] + (i + 1==values.Length ? "": ",");
                sb.Append(nextItem);
            }
            throw new ApiException(ExceptionEvents.EnumValidatorValueNotInScope, sb.ToString());
        }
    }
}
