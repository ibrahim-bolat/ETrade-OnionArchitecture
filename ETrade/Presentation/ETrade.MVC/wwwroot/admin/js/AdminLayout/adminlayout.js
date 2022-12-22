var $=jQuery.noConflict();
$(document).ready(function ($) {
    $("#logoutButton").on("click", function() {
        Swal.fire({
            title: 'Çıkış Yapmak İstediğinizden Emin misiniz?',
            text: "Çıkış yapmaya onay verdiğiniz zaman sistemden çıkarak giriş sayfasına yönlendirileceksiniz!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet!',
            cancelButtonText: 'İptal'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: "POST",
                    url: '/Admin/Account/Logout',
                    dataType: "json",
                    success: function (logoutResult) {
                        if (logoutResult.success) {
                            window.location = '/Admin/Account/Login';
                        } else {
                            Swal.fire({
                                title: 'Hata',
                                text: "Çıkış Yapılamadı!!",
                                icon: 'error',
                            });
                        }
                    },
                    error: function(errormessage) {
                        Swal.fire({
                            title: 'Hata',
                            text: "Çıkış Yapılamadı!!",
                            icon: 'error',
                        });
                    }
                });
            }
            return false;
        });
    });
});

