
using System.ComponentModel.DataAnnotations;


namespace la_mia_pizzeria_static.ValidationAttributes
{
    public class ValidImagePath : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var imgext = Path.GetExtension((string)value);

            if (imgext != ".jpg" && imgext != ".png" && imgext != ".webp")
                return new ValidationResult("Puoi inserire solo immagini di tipo \".jpg\" e \".png\"");
            return ValidationResult.Success;
        }
    }
}
