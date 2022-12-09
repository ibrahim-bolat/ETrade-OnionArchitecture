var $=jQuery.noConflict();
$(document).ready(function ($) {
    <!-- Phone Input Mask -->
    $("#phonemasc").inputmask("+\\90(999)999-99-99");
    $(document).on("ajaxComplete", function(e){
        $("#phonemasc").inputmask("+\\90(999)999-99-99");
    });
    //GetAllDistrictsByCityId
    $("#createAddressPartial").on('change', '#cityId', function () {
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
                    toastMessage(3000, "error", "Dikkat","İlçe Getirilemedi")
                }
            },
            error: function (errormessage) {
                toastMessage(3000, "error", "Dikkat","İlçe Getirilemedi")
            }
        });
    });

    //GetAllNeighborhoodsOrVillagesByDistrictId
    $("#createAddressPartial").on('change', '#districtId', function () {
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
                    toastMessage(3000, "error", "Dikkat","Mahalle ya da Köy Getirilemedi")
                }
            },
            error: function (errormessage) {
                toastMessage(3000, "error", "Dikkat","Mahalle ya da Köy Getirilemedi")
            }
        });
    });
    //GetAllStreetsByNeighborhoodOrVillageId
    $("#createAddressPartial").on('change', '#neighborhoodOrVillageId', function () {
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
                    toastMessage(3000, "error", "Dikkat","Cadde ya da Sokak Getirilemedi")
                }
            },
            error: function (errormessage) {
                toastMessage(3000, "error", "Dikkat","Cadde ya da Sokak Getirilemedi")
            }
        });
    });

    //Address Form Create
    $("#createAddressPartial").on("submit", "#createAddressForm", function () {
        var data = $(this).serialize();
        $.ajax({
            url: "/Admin/Address/CreateAddress",
            type: "POST",
            data: data,
            success: function (result) {
                if (result.success) {
                    ResetFormValue();
                    ResetValidation($("#createAddressForm"));
                    toastMessage(5000, "success", "Tebrikler","Adres Başarıyla Eklendi")
                } else {
                    $('#createAddressForm').replaceWith($('#createAddressForm',$.parseHTML(result)));
                }
            },
            error: function (errormessage) {
                toastMessage(3000, "error", "Dikkat","Adres Eklenemedi")
            }
        });
        return false;
    });
    

    //remove ModelSate Errors and reset Form
    function ResetValidation(currentForm) {
        currentForm.find("[data-valmsg-summary=true]")
            .removeClass("validation-summary-errors")
            .addClass("validation-summary-valid")
            .find("ul").empty();
        currentForm.find("[data-valmsg-replace=true]")
            .removeClass("field-validation-error")
            .addClass("field-validation-valid")
        currentForm.find("[data-val=true]")
            .removeClass("input-validation-error")
            .addClass("input-validation-valid")
    }    
    
    function ResetFormValue() {
        $(".clearcreateform").val("");
        $('#createAddressForm input[type=checkbox]').prop('checked', false);
        $("#addressType")[0].selectedIndex = 0;
        $("#cityId")[0].selectedIndex = 0;
        $("#districtId").empty().append("<option selected='selected' value=''>İlçe Seçiniz</option>");
        $("#neighborhoodOrVillageId").empty().append("<option selected='selected' value=''>Mahalle ya da Köy Seçiniz</option>");
        $("#streetId").empty().append("<option selected='selected' value=''>Cadde ya da Sokak Seçiniz</option>");
    }
    
    
    //Action Message
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

