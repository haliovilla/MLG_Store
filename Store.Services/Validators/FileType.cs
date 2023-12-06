using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MLGStore.Services.Validators
{
    public class FileType : ValidationAttribute
    {
        private readonly string[] validFileTypes;

        public FileType(string[] validFileTypes)
        {
            this.validFileTypes = validFileTypes;
        }

        public FileType(GroupFileType groupFileType)
        {
            if (groupFileType == GroupFileType.Image)
                validFileTypes = new string[] { "image/jpeg", "image/png" };
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

            if (!validFileTypes.Contains(formFile.ContentType))
            {
                return new ValidationResult($"Invalid file type. Valid file types are: {string.Join(", ", validFileTypes)}");
            }

            return ValidationResult.Success;
        }
    }
}
