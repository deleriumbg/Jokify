using System.ComponentModel.DataAnnotations;
using Jokify.Services.DataServices;

namespace Jokify.Web.Model
{
    public class ValidCategoryIdAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var service = (ICategoriesService) validationContext
                .GetService(typeof(ICategoriesService));

            if (service.IsCategoryIdValid((int) value))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Invalid category id!");
            }
        }
    }
}