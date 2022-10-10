using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Constants;

public class CustomIdentityErrorDescriber:IdentityErrorDescriber
    {
        public override IdentityError ConcurrencyFailure() => new IdentityError { Code = "ConcurrencyFailure", Description = "Eş zamanlama hatası." };
        public override IdentityError DefaultError() => new IdentityError { Code = "DefaultError", Description = "Bilinmeyen bir hata oluştu.." };
        public override IdentityError DuplicateEmail(string email) => new IdentityError { Code = "DuplicateEmail", Description = $"\"{ email }\" başka bir kullanıcı tarafından kullanılmaktadır." };
        public override IdentityError DuplicateUserName(string userName) => new IdentityError { Code = "DuplicateUserName", Description = $"\"{ userName }\" kullanıcı adı kullanılmaktadır." };
        public override IdentityError DuplicateRoleName(string role) => new IdentityError { Code = "DuplicateRoleName", Description = $"\"{ role }\" zaten kullanılmaktadır."};
        public override IdentityError InvalidEmail(string email) => new IdentityError { Code = "InvalidEmail", Description = "Geçersiz email." };
        public override IdentityError InvalidUserName(string userName) => new IdentityError { Code = "InvalidUserName", Description = "Geçersiz kullanıcı adı." };
        public override IdentityError InvalidRoleName(string role) => new IdentityError { Code = "InvalidRoleName", Description = $"\"{ role }\" rol adı geçersiz."  };
        public override IdentityError InvalidToken() => new IdentityError { Code = "InvalidToken", Description = "Geçersiz token."  };
        public override IdentityError LoginAlreadyAssociated() => new IdentityError { Code = "LoginAlreadyAssociated", Description = "Bu girişe sahip bir kullanıcı zaten var."  };
        public override IdentityError PasswordMismatch() => new IdentityError { Code = "PasswordMismatch", Description = "Yazdığınız şifre hesabızda kayıtlı olan şifre ile eşleşmiyor."  };
        public override IdentityError PasswordRequiresDigit() => new IdentityError { Code = "PasswordRequiresDigit", Description = "Şifreler en az bir rakam içermelidir ('0'-'9')." };
        public override IdentityError PasswordRequiresLower() => new IdentityError { Code = "PasswordRequiresLower", Description = "Şifreler en az bir küçük harf içermelidir ('a'-'z')." };
        public override IdentityError PasswordRequiresNonAlphanumeric() => new IdentityError { Code = "PasswordRequiresNonAlphanumeric", Description = "Şifreler en az bir harf-rakam olmayan karakter içermelidir (!+.-?..)." };
        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars) => new IdentityError { Code = "PasswordRequiresUniqueChars", Description = $"Şifreler en az \"{ uniqueChars }\" benzersiz karakter içermelidir." };
        public override IdentityError PasswordRequiresUpper() => new IdentityError { Code = "PasswordRequiresUpper", Description = "Şifreler en az bir büyük harf içermelidir ('A'-'Z')." };
        public override IdentityError PasswordTooShort(int length) => new IdentityError { Code = "PasswordTooShort", Description = $"Şifreler en az \"{ length }\" karakter uzunluğunda olmalıdır." };
        public override IdentityError RecoveryCodeRedemptionFailed() => new IdentityError { Code = "RecoveryCodeRedemptionFailed", Description = "Kurtarma kodu kullanılamadı." };
        public override IdentityError UserAlreadyHasPassword() => new IdentityError { Code = "UserAlreadyHasPassword", Description = "Kullanıcının zaten bir şifresi var." };
        public override IdentityError UserAlreadyInRole(string role) => new IdentityError { Code = "UserAlreadyInRole", Description = $"Kullanıcının zaten \"{ role }\" rolü vardır." };
        public override IdentityError UserNotInRole(string role) => new IdentityError { Code = "UserNotInRole", Description = $"Kullanıcının \"{ role }\" rolü yoktur." };
        public override IdentityError UserLockoutNotEnabled() => new IdentityError { Code = "UserLockoutNotEnabled", Description = "Kullanıcı kilidi aktif değildir.." };
    }