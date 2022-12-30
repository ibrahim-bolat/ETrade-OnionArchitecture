var $=jQuery.noConflict();
$(document).ready(function ($) {
    <!-- Edit Profile -->
    <!-- Phone Input Mask -->
    $("#userphonemasc").inputmask("+\\90(999)999-99-99");

    <!-- EditPasswordAccount -->
    if(app.ToastMessages.editPasswordMessage==="True"){
        toastMessage(3000,"success","Şifre Güncellendi.",
            "Şifre Güncelleme İşlemi Başarıyla Gerçekleştirildi.");
    }
    <!-- Forget Pass -->
    if(app.ToastMessages.emailSendMessage==="True"){
        toastMessage(3000,"success","Doğrulama Maili Gönderildi.",
            "Şifre talebi için e-posta adresinize bilgilerindirici mail gönderilmiştir.");
    }
    if(app.ToastMessages.emailSendMessage==="False"){
        toastMessage(3000,"error","Doğrulama Maili Gönderilemedi!!!",
            "Şifre talebi için mail gönderme işleminde hata oluştu. Lütfen mail sunucu bilgilerini kontrol ediniz!");
    }

    <!-- Login -->
    if(app.ToastMessages.loginMessage==="True"){
        toastMessage(3000,"success","Tebrikler Kayıt Oldunuz.",
            "Artık giriş yapabilirsiniz.");
    }    
    if(app.ToastMessages.updatePasswordMessage==="True"){
        toastMessage(3000,"success","Tebrikler Şifre Güncellendi.",
            "Şifre Güncelleme İşlemi Başarıyla Gerçekleşmiştir.");
    }
    if(app.ToastMessages.facebookLoginMessage==="False"){
        toastMessage(3000,"error","Facebook ile Giriş Hatalı",
            "Facebook ile Giriş İşleminiz Başarısız Oldu!");
    }
    if(app.ToastMessages.googleLoginMessage==="False"){
        toastMessage(3000,"error","Google ile Giriş Hatalı",
            "Google ile Giriş İşleminiz Başarısız Oldu!");
    }
    <!-- Profile -->
    if(app.ToastMessages.updateAddressMessage==="True"){
        toastMessage(3000,"success","Tebrikler",
            "Adres Başarıyla Güncellendi.");
    }

    <!-- UpdatePassword -->
    if(app.ToastMessages.updatePasswordMessage==="False"){
        toastMessage(3000,"error","Hata! Şifre Güncellenemedi!!!",
            "Şifreyi güncellerken beklenmeyen hatayla karşılaşıldı.Kurallara uygun şifre oluşturmayı deneyiniz!");
    }
    
    $(".externallogin").click(function (e) {
        e.preventDefault();
        const id = $(this).attr("id");
        const returnUrl = $(this).attr("data-returnUrl");
        const isPersistent = $('#loginIsPersistent').is(':checked');
        let providerName;
        if (id.trim()==="facebookLoginButton"){
            providerName="Facebook";
        }
        if (id.trim()==="googleLoginButton"){
            providerName="Google";
        }
        window.location.href = app.Urls.externalLoginUrl+'?providerName='+ providerName + '&isPersistent=' + isPersistent+'&returnUrl='+returnUrl;
    });
    
    <!-- Toast Message -->
    function toastMessage(time, icon,title,text) {
        const Toast = Swal.mixin({
            toast: true,
            position: 'top-end',
            showConfirmButton: false,
            timer: time,
            timerProgressBar: true,
            didOpen: (toast) => {
                toast.addEventListener('click', Swal.close)
                toast.addEventListener('mouseenter', Swal.stopTimer)
                toast.addEventListener('mouseleave', Swal.resumeTimer)
            }
        })
        Toast.fire({
            icon: icon,
            title: title,
            text: text
        })
    }
    
});

