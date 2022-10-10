using System.ComponentModel.DataAnnotations;

namespace ETrade.Application.Validations.CustomValidation.Account;

public class CustomDateAttribute:ValidationAttribute
{
    public CustomDateAttribute() : base("{0} uygun tarih girilmelidir.")
    {
        
    }
    public override bool IsValid(object value)
    {
        DateTime propValue = Convert.ToDateTime(value);
        if (propValue <= DateTime.Now && propValue >= DateTime.Now.AddYears(-100) || propValue==DateTime.MinValue)
        {
            return true;
        }
        return false;
    }

}