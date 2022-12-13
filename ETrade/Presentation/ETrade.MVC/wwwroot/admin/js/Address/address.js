var $=jQuery.noConflict();
$(document).ready(function ($) {
    <!-- Phone Input Mask -->
    $("#phonemasc").inputmask("+\\90(999)999-99-99");
    $(document).on("ajaxComplete", function(e){
        $("#phonemasc").inputmask("+\\90(999)999-99-99");
    });
    //GetAllDistrictsByCityId
    $(".addresspartial").on('change', '#cityId', function () {
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
    $(".addresspartial").on('change', '#districtId', function () {
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
    $(".addresspartial").on('change', '#neighborhoodOrVillageId', function () {
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
                    ResetCreateFormValue();
                    ResetValidation($("#createAddressForm"));
                    toastMessage(5000, "success", "Tebrikler","Adres Başarıyla Eklendi")
                } else {
                    //$('#createAddressForm').html($($.parseHTML(result)).find("#createAddressForm").html());
                    $('#createAddressForm').html($('#createAddressForm',$.parseHTML(result)).html());
                }
            },
            error: function (errormessage) {
                toastMessage(3000, "error", "Dikkat","Adres Eklenemedi")
            }
        });
        return false;
    });

    //Address Form Update
    $("#updateAddressPartial").on("submit", "#updateAddressForm", function () {
        var data = $(this).serialize();
        var userId = $('#updateAddressForm #userId').attr("data-userId");
        $.ajax({
            url: "/Admin/Address/UpdateAddress",
            type: "POST",
            data: data,
            success: function (result) {
                if (result.success) {
                    ResetUpdateFormValue();
                    ResetValidation($("#updateAddressForm"));
                    window.location = app.Urls.profileUrl + userId;
                } else {
                    //$('#createAddressForm').html($($.parseHTML(result)).find("#createAddressForm").html());
                    $('#updateAddressForm').html($('#updateAddressForm',$.parseHTML(result)).html());
                }
            },
            error: function (errormessage) {
                toastMessage(3000, "error", "Dikkat","Adres Güncellenemedi")
            }
        });
        return false;
    });
    //Address Form Delete
    $("#detailAddressPartial").on("click", "#addressdeletebutton", function () {
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
    
    function ResetCreateFormValue() {
        $(".clearcreateform").val("");
        $('#createAddressForm input[type=checkbox]').prop('checked', false);
        $("#addressType")[0].selectedIndex = 0;
        $("#cityId")[0].selectedIndex = 0;
        $("#districtId").empty().append("<option selected='selected' value=''>İlçe Seçiniz</option>");
        $("#neighborhoodOrVillageId").empty().append("<option selected='selected' value=''>Mahalle ya da Köy Seçiniz</option>");
        $("#streetId").empty().append("<option selected='selected' value=''>Cadde ya da Sokak Seçiniz</option>");
    }

    function ResetUpdateFormValue() {
        $(".clearupdateform").val("");
        $('#updateAddressForm input[type=checkbox]').prop('checked', false);
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

