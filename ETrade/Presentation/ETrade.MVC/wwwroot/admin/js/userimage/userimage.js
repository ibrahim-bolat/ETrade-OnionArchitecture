var $=jQuery.noConflict();
$(document).ready(function ($) {
    $(".userImageSetProfilButton").on("click", function() {
        var Id = $(this).attr("data-id");
        var userId = $(this).attr("data-userid");
        Swal.fire({
            title: 'Profil Olsun mu?',
            text: "Resmi Profil Olarak Ayarlamak İstediğinizden Emin misiniz?",
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
                    url: '/Admin/UserImage/SetProfilImage',
                    data: {
                        id: Id,
                        userId:userId
                    },
                    dataType: "json",
                    success: function(setProfilResult) {
                        if (setProfilResult.success) {
                            Swal.fire({
                                title: 'Profil Ayarlandı',
                                text: "Profil Resmi Başarıyla Ayarlandı",
                                icon: 'success',
                            }).then((result) => {
                                window.location = '/Admin/Account/Profile?id=' + userId;
                            })
                        } else {
                            Swal.fire({
                                title: 'Hata',
                                text: "Profil Ayarlanamadı",
                                icon: 'error',
                            });
                        }
                    },
                    error: function(errormessage) {
                        Swal.fire({
                            title: 'Hata',
                            text: "Profil Ayarlanamadı",
                            icon: 'error',
                        });
                    }
                });
            }
            return false;
        });
    });
    
    $(".userImageDeleteProfilButton").on("click", function() {
        var Id = $(this).attr("data-id");
        var userId = $(this).attr("data-userid");
        Swal.fire({
            title: 'Resmi Silmek İstediğinizden Emin misiniz??',
            text: "Silme işlemine onay verdikten sonra işlemi tekrardan geri alamazsınız!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Sil!',
            cancelButtonText: 'İptal'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: "POST",
                    url: '/Admin/UserImage/DeleteUserImage',
                    data: {
                        id: Id
                    },
                    dataType: "json",
                    success: function(setProfilResult) {
                        if (setProfilResult.success) {
                            Swal.fire({
                                title: 'Resim Silindi?',
                                text: "Resim Silme İşlemi Başarıyla Gerçekleşti",
                                icon: 'success',
                            }).then((result) => {
                                window.location = '/Admin/Account/Profile?id=' + userId;
                            })
                        } else {
                            Swal.fire({
                                title: 'Hata',
                                text: "Resim Silinemedi",
                                icon: 'error',
                            });
                        }
                    },
                    error: function(errormessage) {
                        Swal.fire({
                            title: 'Hata',
                            text: "Resim Silinemedi",
                            icon: 'error',
                        });
                    }
                });
            }
            return false;
        });
    });

    <!-- CreateUserImage -->
    if(app.ToastMessages.createUserImageMessage==="True"){
        toastMessage(5000,"success","Resim Eklendi.",
            "Resim Ekleme İşlemi Başarıyla Gerçekleştirildi.");
    }

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