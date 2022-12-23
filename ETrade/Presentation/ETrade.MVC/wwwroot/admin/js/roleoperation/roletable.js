var $=jQuery.noConflict();
$(document).ready(function ($) {
    var tables = $("#roleTable").DataTable({
        "pageLength": 10,
        "ordering": true,
        "order": [[0, "asc"]],
        "info": true,
        "paging": true,
        "searching": true,
        "responsive": true,
        "lengthMenu": [[10, 25, 50, 100, -1], [10, 25, 50, 100, "Tümü"]],
        "pagingType": "full_numbers",
        "processing": true,
        "serverSide": true,
        "filter": true,
        "language": {
            'url': '/lib/datatables/turkceDil.json'
        },
        "ajax": {
            "url": "/Admin/RoleOperation/GetAllRoles",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        },
            {
                "targets": [2,3],
                "searchable": false,
                "orderable": false
            }],
        "columns": [
            {"data": "Id", "name": "Id", "autoWidth": true},
            {"data": "Name", "name": "Rol Adı", "autoWidth": true},
            {"data": "Status", "name": "Durumu", "className": "text-center","autoWidth": true, "render": function (data, type, row, meta) {
                    return row.Status ? '<span class="fa fa-solid fa-check" style="color: green">Aktif</span>' : 
                        '<span class="fa fa-solid fa-times" style="color: red">Pasif</span>';
                }
            },
            {
                "data": "Id","className": "text-center","width": "50px", "render": function (data, type, row, meta) {
                    if(row.Status){
                        if(data === 1 || data === 2 || data === 3){
                            return'<a class="btn btn-primary mr-2" href="AuthorizeEndpoint/Index"><i class="fa fa-tasks">Yetkilendirme</i></a>' +
                                '<a class="btn btn-info mr-2" href="RoleOperation/UsersOfTheRole/' + data + '"><i class="fa fa-info-circle">Rolün Kullanıcıları</i></a>' +
                                '<a class="btn btn-warning mr-2 disabled">Default Rol</a>' +
                                '<a class="btn btn-warning disabled">Default Rol</a>';
                        }
                        else{
                            return'<a class="btn btn-primary mr-2" href="AuthorizeEndpoint/Index"><i class="fa fa-tasks">Yetkilendirme</i></a>' +
                                '<a class="btn btn-info mr-2" href="RoleOperation/UsersOfTheRole/' + data + '"><i class="fa fa-info-circle">Rolün Kullanıcıları</i></a>' +
                                '<a class="btn btn-secondary mr-2" onclick="getByIdforUpdate(' + data + ')"><i class="fa fa-pencil-square-o">Rol Güncelle</i></a>' +
                                '<a class="btn btn-danger" onclick="SetRolePassive(' + data + ')"><i class="fa fa-solid fa-times">Pasif Yap</i></a>';
                        }
                    }
                    else{
                        return '<a class="btn btn-primary mr-2" href="AuthorizeEndpoint/Index"><i class="fa fa-tasks">Yetkilendirme</i></a>' +
                        '<a class="btn btn-info mr-2" href="RoleOperation/UsersOfTheRole/' + data + '"><i class="fa fa-info-circle">Rolün Kullanıcıları</i></a>' +
                        '<a class="btn btn-secondary mr-2" onclick="getByIdforUpdate(' + data + ')"><i class="fa fa-pencil-square-o">Rol Güncelle</i></a>' +
                        '<a class="btn btn-success" onclick="SetRoleActive(' + data + ')"><i class="fa fa-solid fa-check">Aktif Yap</i></a>';
                    }
                }
            }
        ],
        dom: '<"dt-header"Bf>rt<"dt-footer"ip>',
        buttons: [
            "pageLength",
        ]
    });
    $(tables.table().body())
        .addClass('tbody');

    //Modal Form Create
    $('#roleCreateModalForm').on('submit', '#createModalForm', function () {
        var data = $(this).serialize();
        $.ajax({
            url: "/Admin/RoleOperation/CreateRole",
            type: "POST",
            data: data,
            success: function (result) {
                if (result.success) {
                    ReloadTable();
                    $('#roleCreateModal').modal('hide');
                    $(".modal-fade").modal("hide");
                    $(".modal-backdrop").remove();
                    clearCreateModalTextBox();
                    toastMessage(3000, "success", "Tebrikler","Rol Başarıyla OLuşturuldu");
                } else {
                    var mytag=$('<div></div>').html(result);
                    $('#createModalFormModalBody').html(mytag.find(".modal-body").html());
                }
            },
            error: function (errormessage) {
                toastMessage(3000, "error", "Hata","Rol Oluşturulamadı");
            }
        });
        return false;
    });

    //Modal Form Update
    $('#roleUpdateModalForm').on('submit', '#updateModalForm', function () {
        var data = $(this).serialize();
        $.ajax({
            url: "/Admin/RoleOperation/UpdateRole",
            type: "POST",
            data: data,
            success: function (result) {
                if (result.success) {
                    ReloadTable();
                    $('#roleUpdateModal').modal('hide');
                    $(".modal-fade").modal("hide");
                    $(".modal-backdrop").remove();
                    clearUpdateModalTextBox();
                    toastMessage(3000, "success", "Tebrikler","Rol Başarıyla Güncellendi");
                } else {
                    var mytag=$('<div></div>').html(result);
                    $('#updateModalFormModalBody').html(mytag.find(".modal-body").html());
                }
            },
            error: function (errormessage) {
                toastMessage(3000, "error", "Hata","Rol Güncellenemedi");
            }
        });
        return false;
    });

    //When Close Create Modal Reset ModelSate Errors and Form inputs
    $("#roleCreateModal").on("hidden.bs.modal", function () {
        var createModalForm = $(this).find("#createModalForm");
        ResetValidation(createModalForm);
        clearCreateModalTextBox();
    });
    
    //When Close Update Modal Reset ModelSate Errors and Form inputs
    $("#roleUpdateModal").on("hidden.bs.modal", function () {
        var updateModalForm = $(this).find("#updateModalForm");
        ResetValidation(updateModalForm);
        clearUpdateModalTextBox();
    });

});

//Get Role By Id For Update
function getByIdforUpdate(Id) {
    clearUpdateModalTextBox();
    $.ajax({
        url: '/Admin/RoleOperation/GetRoleById/' + Id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (result.success) {
                $('#updateID').val(result.role.Id);
                $('#updateName').val(result.role.Name);
                $('#roleUpdateModal').modal('show');
            } else {
                toastMessage(3000, "error", "Hata","Role Getirilemedi");
            }

        },
        error: function (errormessage) {
            toastMessage(3000, "error", "Hata","Role Getirilemedi");
        }
    });
    return false;
}


//Set Role Active
function SetRoleActive(Id) {
    Swal.fire({
        title: 'Rolü aktif etmek İstediğinizden Emin misiniz?',
        text: "Rolü aktif etmeye onay verdiğiniz zaman rol aktif kullanıcılara atanabilecektir.!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Aktif Et!',
        cancelButtonText: 'İptal'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Admin/RoleOperation/SetRoleActive/' + Id,
                type: "POST",
                contentType: "application/json;charset=UTF-8",
                dataType: "json",
                success: function (activeResult) {
                    if (activeResult.success) {
                        Swal.fire({
                            title: 'Aktif Oldu?',
                            text: "Rol Başarıyla Aktif Edildi",
                            icon: 'success',
                        }).then((result) => {
                            ReloadTable();
                        })
                    } else {
                        Swal.fire({
                            title: 'Hata',
                            text: "Rol Aktif Edilemedi",
                            icon: 'error',
                        });
                    }
                },
                error: function (errormessage) {
                    Swal.fire({
                        title: 'Hata',
                        text: "Rol Aktif Edilemedi",
                        icon: 'error',
                    });
                }
            });
        }
    });
}

//Set Role Passive
function SetRolePassive(Id) {
    Swal.fire({
        title: 'Rolü pasif etmek İstediğinizden Emin misiniz?',
        text: "Rolü pasif etmeye onay verdiğiniz zaman bu role sahip kullanıcılar rolden çıkarılacaktır.!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Pasif Et!',
        cancelButtonText: 'İptal'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Admin/RoleOperation/SetRolePassive/' + Id,
                type: "POST",
                contentType: "application/json;charset=UTF-8",
                dataType: "json",
                success: function (activeResult) {
                    if (activeResult.success) {
                        Swal.fire({
                            title: 'Pasif Oldu?',
                            text: "Rol Başarıyla Pasif Edildi",
                            icon: 'success',
                        }).then((result) => {
                            ReloadTable();
                        })
                    } else {
                        Swal.fire({
                            title: 'Hata',
                            text: "Rol Pasif Edilemedi",
                            icon: 'error',
                        });
                    }
                },
                error: function (errormessage) {
                    Swal.fire({
                        title: 'Hata',
                        text: "Rol Pasif Edilemedi",
                        icon: 'error',
                    });
                }
            });
        }
    });
}

//remove ModelSate Errors and reset Form
function ResetValidation(currentForm) {
    currentForm[0].reset();
    currentForm.find("[data-valmsg-summary=true]")
        .removeClass("validation-summary-errors")
        .addClass("validation-summary-valid")
        .find("ul").empty();
    currentForm.find("[data-valmsg-replace=true]")
        .removeClass("field-validation-error")
        .addClass("field-validation-valid")
        .empty();
    currentForm.find("[data-val=true]")
        .removeClass("input-validation-error")
        .addClass("input-validation-valid")
        .empty();
}

//Clear Create Modal Form  Entire Features
function clearCreateModalTextBox() {
    $('#createName').val("");
    $('#createName-error').val("");
    $('#btnAdd').show();
    $('#createID').css('border-color', 'lightgrey');
    $('#createName').css('border-color', 'lightgrey');
}

//Clear Update Modal Form  Entire Features
function clearUpdateModalTextBox() {
    $('#updateID').val("");
    $('#updateName').val("");
    $('#updateID-error').val("");
    $('#updateName-error').val("");
    $('#btnUpdate').show();
    $('#updateID').css('border-color', 'lightgrey');
    $('#updateName').css('border-color', 'lightgrey');
}


//Disable Create Modal Form  Entire TextBox
function disabledCreateModalTextBox(value = true) {
    $('#createName').attr("disabled", value);
}

//Disable Update Modal Form  Entire TextBox
function disabledUpdateModalTextBox(value = true) {
    $('#updateName').attr("disabled", value);
}

//Reload DataTable
function ReloadTable() {
    // $('#example').DataTable().clear();                                                        
    $('#roleTable').DataTable().ajax.reload(null,false);
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