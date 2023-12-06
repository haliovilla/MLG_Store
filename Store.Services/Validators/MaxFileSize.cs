using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MLGStore.Services.Validators
{
    public class MaxFileSize : ValidationAttribute
    {
        private readonly int maxSize;

        public MaxFileSize(int maxSize)
        {
            this.maxSize = maxSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile formFile = value as IFormFile;

            if (formFile == null)
            {
                return ValidationResult.Success;
            }

            if (formFile.Length > maxSize * 1024 * 1024)
            {
                return new ValidationResult($"Maximum file size allowed is {maxSize}mb");
            }

            return ValidationResult.Success;
        }
    }
}
