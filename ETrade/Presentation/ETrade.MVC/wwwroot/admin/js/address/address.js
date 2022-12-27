var $=jQuery.noConflict();
$(document).ready(function ($) {
    <!-- Phone Input Mask -->
    
    $("#phonemasc").inputmask("+\\90(999)999-99-99");
    $(document).on("ajaxComplete", function(e){
        $("#phonemasc").inputmask("+\\90(999)999-99-99");
    });
    
    //GetAllDistrictsByCityId
    $(".addressForm").on('change', '#cityId', function () {
        $("#districtId").empty();
        $("#neighborhoodOrVillageId").empty();
        $("#streetId").empty();
        var cityId = $("#cityId").val();
        $.ajax({
            url: "/Admin/Address/GetAllDistrictsByCityId",
            data: { cityId: cityId },
            type: "POST",
            dataType: "json",
            success: function (result) {
                if (result.success) {
                    $("#districtId").append("<option selected='selected' value=''>İlçe Seçiniz</option>");
                    $("#neighborhoodOrVillageId").append("<option selected='selected' value=''>Mahalle ya da Köy Seçiniz</option>");
                    $("#streetId").append("<option selected='selected' value=''>Cadde ya da Sokak Seçiniz</option>");
                    $.each(result.districts, (index,value) =>{
                        $("#districtId").append(`<option  value="${value.Value}">${value.Text}</option>`);
                    });
                }
                else {
                    toastMessage(3000, "error", "Dikkat","İlçe Getirilemedi");
                }
            },
            error: function (errormessage) {
                toastMessage(3000, "error", "Dikkat","İlçe Getirilemedi");
            }
        });
    });
    
    //GetAllNeighborhoodsOrVillagesByDistrictId
    $(".addressForm").on('change', '#districtId', function () {
        $("#neighborhoodOrVillageId").empty();
        $("#streetId").empty();
        var districtId = $("#districtId").val();
        $.ajax({
            url: "/Admin/Address/GetAllNeighborhoodsOrVillagesByDistrictId",
            data: { districtId: districtId },
            type: "POST",
            dataType: "json",
            success: function (result) {
                if (result.success) {
                    $("#neighborhoodOrVillageId").append("<option selected='selected' value=''>Mahalle ya da Köy Seçiniz</option>");
                    $("#streetId").append("<option selected='selected' value=''>Cadde ya da Sokak Seçiniz</option>");
                    $.each(result.neighborhoodsOrVillages, (index,value) =>{
                        $("#neighborhoodOrVillageId").append(`<option  value="${value.Value}">${value.Text}</option>`);
                    });
                }
                else {
                    toastMessage(3000, "error", "Dikkat","Mahalle ya da Köy Getirilemedi");
                }
            },
            error: function (errormessage) {
                toastMessage(3000, "error", "Dikkat","Mahalle ya da Köy Getirilemedi");
            }
        });
    });
    //GetAllStreetsByNeighborhoodOrVillageId
    $(".addressForm").on('change', '#neighborhoodOrVillageId', function () {
        $("#streetId").empty();
        var neighborhoodOrVillageId = $("#neighborhoodOrVillageId").val();
        $.ajax({
            url: "/Admin/Address/GetAllStreetsByNeighborhoodOrVillageId",
            data: { neighborhoodOrVillageId: neighborhoodOrVillageId },
            type: "POST",
            dataType: "json",
            success: function (result) {
                if (result.success) {
                    $("#streetId").append("<option selected='selected' value=''>Cadde ya da Sokak Seçiniz</option>");
                    $.each(result.streets, (index,value) =>{
                        $("#streetId").append(`<option  value="${value.Value}">${value.Text}</option>`);
                    });
                }
                else {
                    toastMessage(3000, "error", "Dikkat","Cadde ya da Sokak Getirilemedi");
                }
            },
            error: function (errormessage) {
                toastMessage(3000, "error", "Dikkat","Cadde ya da Sokak Getirilemedi");
            }
        });
    });

    // search city dropdownlist
    $("#cityId").select2();
    
    // search district dropdownlist
    $("#districtId").select2();
    
    // search neighborhood or village dropdownlist
    $("#neighborhoodOrVillageId").select2();
    
    // search street dropdownlist
    $("#streetId").select2();
    

    
    
    //Address Form Delete
    $("#addressdeletebutton").click(function () {
        var Id = $(this).attr("data-id");
        Swal.fire({
            title: 'Adresi Silmek İstediğinizden Emin misiniz?',
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
                    url: '/Admin/Address/DeleteAddress',
                    data: {
                        addressId: Id
                    },
                    dataType: "json",
                    success: function(deleteResult) {
                        if (deleteResult.success) {
                            Swal.fire({
                                title: 'Silindi?',
                                text: "Adres Başarıyla Silindi",
                                icon: 'success',
                            }).then((result) => {
                                var userId = deleteResult.userId;
                                window.location = app.Urls.profileUrl + userId;
                            })
                        } else {
                            Swal.fire({
                                title: 'Hata',
                                text: "Adres Silinemedi",
                                icon: 'error',
                            });
                        }
                    },
                    error: function(errormessage) {
                        Swal.fire({
                            title: 'Hata',
                            text: "Adres Silinemedi",
                            icon: 'error',
                        });
                    }
                });
            }
            return false;
        });
    });

    <!-- CreateAddress -->
    if(app.ToastMessages.createAddressMessage==="True"){
        toastMessage(3000,"success","Adres Kaydedildi",
            "Adres Kaydetme İşlemi Başarıyla Gerçekleştirildi.");
    }
    
    //Toast Message
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

