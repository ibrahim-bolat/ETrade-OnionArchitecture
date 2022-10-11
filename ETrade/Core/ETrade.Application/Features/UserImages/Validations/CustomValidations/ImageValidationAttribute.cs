
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;

namespace ETrade.Application.Features.UserImages.Validations.CustomValidations;

public class ImageValidationAttribute:ValidationAttribute
{
    private readonly string[] _extensions;
    private readonly int _maxFileSize;
    private readonly int _minWidth;
    private readonly int _minHeight;
    public ImageValidationAttribute(string[] extensions,int maxFileSize,int minWidth,int minHeight)
    {
        _extensions = extensions;
        _maxFileSize = maxFileSize;
        _minWidth = minWidth;
        _minHeight = minHeight;
    }
    
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        int height, width;
        if (file != null)
        {
            var extension = Path.GetExtension(file.FileName);
            if (!_extensions.Contains(extension.ToLower()))
            {
                return new ValidationResult(GetErrorExtensionMessage());
            }
            if (file.Length > _maxFileSize)
            {
                return new ValidationResult(GetErrorMaxFileSizeMessage());
            }
            using (var image = Image.Load(file.OpenReadStream()))
            {
                width = image.Width;
                height = image.Height;
            }
            if (width < _minWidth && height < _minHeight)
            {
                return new ValidationResult(GetErrorMinDimensionMessage());
            }
        }
        return ValidationResult.Success;
    }
    public string GetErrorExtensionMessage()
    {
        return $"Bu dosya uzantısına izin verilmiyor!";
    }
    public string GetErrorMaxFileSizeMessage()
    {
        return $"Maximum İzin Verilen Dosya Boyutu { _maxFileSize/(1024*1024)} MegaBytedır..";
    }
    public string GetErrorMinDimensionMessage()
    {
        return $"Yüklenen Resmin Minumum Genişliği { _minWidth}, Minumum Yükseliği { _minHeight} olmalıdır.({ _minWidth}x{ _minHeight})";
    }

}