var $=jQuery.noConflict();
$(document).ready(function ($) {
    <!-- Phone Input Mask -->
    $("#phonemasc").inputmask("+\\90(999)999-99-99");
    $(document).on("ajaxComplete", function(e){
        $("#phonemasc").inputmask("+\\90(999)999-99-99");
    });
    //GetAllDistrictsByCityId
    $("#createAddressCardBody").on('change', '#cityId', function () {
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
                    toastMessage(3000, "error", "İlçe Getirilemedi")
                }
            },
            error: function (errormessage) {
                toastMessage(3000, "error", "İlçe Getirilemedi")
            }
        });
    });

    //GetAllNeighborhoodsOrVillagesByDistrictId
    $("#createAddressCardBody").on('change', '#districtId', function () {
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
                    toastMessage(3000, "error", "Mahalle ya da Köy Getirilemedi")
                }
            },
            error: function (errormessage) {
                toastMessage(3000, "error", "Mahalle ya da Köy Getirilemedi")
            }
        });
    });
    //GetAllStreetsByNeighborhoodOrVillageId
    $("#createAddressCardBody").on('change', '#neighborhoodOrVillageId', function () {
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
                    toastMessage(3000, "error", "Cadde ya da Sokak Getirilemedi")
                }
            },
            error: function (errormessage) {
                toastMessage(3000, "error", "Cadde ya da Sokak Getirilemedi")
            }
        });
    });

    //Address Form Create
    $("#createAddressCardBody").on('submit', '#createAddressForm', function () {
        var data = $(this).serialize();
        $.ajax({
            url: "/Admin/Address/CreateAddress",
            type: "POST",
            data: data,
            dataType: 'html',
            success: function (result) {
                if (result.success) {
                    toastMessage(5000, "success", "Adres Başarıyla Eklendi")
                } else {
                    var mytag=$('<div></div>').html(result);
                    $('#createAddressCardBody').html(mytag.find("#createAddressCardBody").html());
                }
            },
            error: function (errormessage) {
                toastMessage(3000, "error", "Adres Eklenemedi")
            }
        });
        return false;
    });

    //Action Message
    function toastMessage(time, icon, message) {
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
            title: message,
        })
    }
});

