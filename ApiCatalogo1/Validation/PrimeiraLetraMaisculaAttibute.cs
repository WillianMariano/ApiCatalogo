using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo1.Validation
{
    public class PrimeiraLetraMaisculaAttibute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value.ToString()[0].ToString() != value.ToString()[0].ToString().ToUpper())
            {
                return new ValidationResult("Primeira letra deve ser maiúscula");
            }
            return ValidationResult.Success;
        }
    }
}
