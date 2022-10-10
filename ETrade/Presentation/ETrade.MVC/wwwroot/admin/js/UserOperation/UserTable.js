var $=jQuery.noConflict();
$(document).ready(function ($) {
    var tables = $("#userTable").DataTable({
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
            "url": "/Admin/UserOperation/Users",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
            },
            {
                "targets": [5],
                "searchable": false,
                "orderable": false
            }],
        "columns": [
            {"data": "id", "name": "Id", "autoWidth": true},
            {"data": "firstName", "name": "Name", "autoWidth": true},
            {"data": "lastName", "name": "SurName", "autoWidth": true},
            {"data": "userName", "name": "UserName", "autoWidth": true},
            {"data": "email", "name": "Email", "autoWidth": true},
            {
                "data": "id", "width": "50px", "render": function (data) {
                    return '<a class="btn btn-success mr-2" onclick="return getRole(' + data + ')"><i class="fa fa-tasks">Rol Atama</i></a>' +
                        '<a class="btn btn-info mr-2" href="Account/Profile/' + data + '"><i class="fa fa-info-circle">Detay</i></a>' +
                        '<a class="btn btn-secondary mr-2" href="Account/EditProfile/'+data+'"><i class="fa fa-pencil-square-o">Profil Güncelle</i></a>' +
                        '<a class="btn btn-warning mr-2" href="Account/EditPassword/'+data+'"><i class="fa fa-pencil-square-o">Şifre Güncelle</i></a>' +
                        '<a class="btn btn-danger" onclick="getbyIdforDelete(' + data + ')"><i class="fa fa-trash-o">Sil</i></a>';
                }
            }
        ],
        dom: '<"dt-header"Bf>rt<"dt-footer"ip>',
        buttons: [
            "pageLength",
            {
                extend: 'excelHtml5',
                text: '<i class="fa fa-file-excel-o"> Excel</i>',
                filename: 'Kullanıcı Listesi',
                title: 'Kullanıcı Listesi',
                exportOptions: {
                    columns: [1, 2, 3, 4]
                },
                className: "btn-export-excel"
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="fa fa-file-pdf-o"> Pdf</i>',
                filename: 'Kullanıcı Listesi',
                title: 'Kullanıcı Listesi',
                pageSize: 'A4',
                exportOptions: {
                    columns: [1, 2, 3, 4]
                },
                className: "btn-export-pdf",
                customize: function (doc) {
                    doc.styles.tableHeader.alignment = 'left';
                    doc.content[1].table.widths = [80, 80, 80, '*'];
                    var objLayout = {};
                    objLayout['hLineWidth'] = function (i) {
                        return .8;
                    };
                    objLayout['vLineWidth'] = function (i) {
                        return .5;
                    };
                    objLayout['paddingLeft'] = function (i) {
                        return 8;
                    };
                    objLayout['paddingRight'] = function (i) {
                        return 8;
                    };
                    doc.content[1].layout = objLayout;
                },
            },
            {
                extend: 'print',
                text: '<i class="fa fa-file-o"> Yazdır</i>',
                title: 'Kullanıcı Listesi',
                exportOptions: {
                    columns: [1, 2, 3, 4]
                },
                className: "btn-export-print"
            }

        ]
    });

    $(tables.table().body())
        .addClass('tbody');

    //Modal Form Create
    $('#userCreateModalForm').on('submit', '#createModalForm', function () {
        var data = $(this).serialize();
        $.ajax({
            url: "/Admin/UserOperation/Add",
            type: "POST",
            data: data,
            success: function (result) {
                if (result.success) {
                    ReloadTable();
                    $('#userCrateModal').modal('hide');
                    $(".modal-fade").modal("hide");
                    $(".modal-backdrop").remove();
                    clearCreateModalTextBox();
                    toastMessage(5000, "success", "Kayıt Başarıyla Eklendi")
                } else {
                    var mytag=$('<div></div>').html(result);
                    $('#createModalFormModalBody').html(mytag.find(".modal-body").html());
                }
            },
            error: function (errormessage) {
                toastMessage(3000, "error", "Kayıt Eklenemedi")
            }
        });
        return false;
    });
    
    
    //Modal Form Delete
    $('#userDeleteModalForm').on('submit', '#deleteModalForm', function (e) {
        e.preventDefault();
        var Id = $('#deleteID').val();
        $.ajax({
            url: '/Admin/UserOperation/Delete/' + Id,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                if (result.success) {
                    ReloadTable();
                    $('#userDeleteModal').modal('hide');
                    clearDeleteModalTextBox();
                    toastMessage(5000, "success", "Kayıt Başarıyla Silindi")
                } else {
                    toastMessage(3000, "error", "Kayıt Siinemedi")
                }
            },
            error: function (errormessage) {
                toastMessage(3000, "error", "Kayıt Siinemedi")
            }
        });
        return false;
    });

    //RoleModal Save
    $('#roleModalSave').click( function (e) {
        var roles = [];
        var user_id = $('#roleModalForm .modal-body input[type="checkbox"]').attr("data-id");
        $.each($('#roleModalForm .modal-body input[type="checkbox"]:checked'), function() {
            var role_id = $(this).attr("name");
            roles.push(role_id);
        })
        var postData = { roles: roles };
        $.ajax({
            url: '/Admin/UserOperation/SaveRole/' + user_id,
            type: "POST",
            data: postData,
            dataType: "json",
            traditional: true,
            success: function (result) {
                if (result.success) {
                    $("#roleModal").modal("hide");
                    toastMessage(5000, "success", "Kayıt Başarıyla Eklendi")
                } else {
                    toastMessage(3000, "error", "Kayıt Eklenemedi")
                }
            },
            error: function (errormessage) {
                toastMessage(3000, "error", "Kayıt Eklenemedi")
            }
        });
        return false;
    });

    //When Close Create Modal Reset ModelSate Errors and Form inputs
    $("#userCrateModal").on("hidden.bs.modal", function () {
        var createModalForm = $(this).find("#createModalForm");
        ResetValidation(createModalForm);
        clearCreateModalTextBox();
    });
    
});

//Get Role By Id For Update
function getRole(Id) {
    var html ="";
    $.ajax({
        url: '/Admin/UserOperation/GetRole/' + Id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (result.success) {
                $.each(result.roles, function(index, value) {
                    var isChecked = "";
                    if(value.hasAssign == true)
                        isChecked="checked";
                    html+=`<div class="custom-control custom-switch custom-control-inline">
                          <input class="custom-control-input" type="checkbox" data-id="${Id}" name="${value.id}" id="roleHasAssign${value.id}" ${isChecked}>
                          <label class="custom-control-label" for="roleHasAssign${value.id}">${value.name}</label>
                          </div>`
                });
                $('#roleModal .modal-body').html(html);
                $('#roleModal').modal('show');
            } else {
                toastMessage(3000, "error", "Kayıt Getirilemedi")
            }

        },
        error: function (errormessage) {
            toastMessage(3000, "error", "Kayıt Getirilemedi")
        }
    });
    return false;
}

//Get User By Id For Delete
function getbyIdforDelete(Id) {
    clearDeleteModalTextBox();
    disabledDeleteModalTextBox(true);
    $.ajax({
        url: '/Admin/UserOperation/getbyId/' + Id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (result.success) {
                $('#deleteID').val(result.user.id);
                $('#deleteFirstName').val(result.user.firstName);
                $('#deleteLastName').val(result.user.lastName);
                $('#deleteUserName').val(result.user.userName);
                $('#deleteEmail').val(result.user.email);
                $('#userDeleteModal').modal('show');
            } else {
                toastMessage(3000, "error", "Kayıt Getirilemedi")
            }

        },
        error: function (errormessage) {
            toastMessage(3000, "error", "Kayıt Getirilemedi")
        }
    });
    return false;
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
    $('#createFirstName').val("");
    $('#createLastName').val("");
    $('#createUserName').val("");
    $('#createEmail').val("");
    $('#createPassword').val("");
    $('#createRePassword').val("");
    $('#createFirstName-error').val("");
    $('#createLastName-error').val("");
    $('#createUserName-error').val("");
    $('#createEmail-error').val("");
    $('#createPassword-error').val("");
    $('#createRePassword-error').val("");
    $('#btnAdd').show();
    $('#createID').css('border-color', 'lightgrey');
    $('#createFirstName').css('border-color', 'lightgrey');
    $('#createLastName').css('border-color', 'lightgrey');
    $('#createUserName').css('border-color', 'lightgrey');
    $('#createEmail').css('border-color', 'lightgrey');
    $('#createPassword').css('border-color', 'lightgrey');
    $('#createRePassword').css('border-color', 'lightgrey');
    
}

//Clear Delete Modal Form  Entire Features
function clearDeleteModalTextBox() {
    $('#deleteID').val("");
    $('#deleteFirstName').val("");
    $('#deleteLastName').val("");
    $('#deleteUserName').val("");
    $('#deleteEmail').val("");
    $('#deleteID-error').val("");
    $('#deleteFirstName-error').val("");
    $('#deleteLastName-error').val("");
    $('#deleteUserName-error').val("");
    $('#deleteEmail-error').val("");
    $('#lbldeletealert').show();
    $('#deleteID').css('border-color', 'lightgrey');
    $('#deleteFirstName').css('border-color', 'lightgrey');
    $('#deleteLastName').css('border-color', 'lightgrey');
    $('#deleteUserName').css('border-color', 'lightgrey');
    $('#deleteEmail').css('border-color', 'lightgrey');

}

//Disable Create Modal Form  Entire TextBox
function disabledCreateModalTextBox(value = true) {
    $('#createFirstName').attr("disabled", value);
    $('#createLastName').attr("disabled", value);
    $('#createUserName').attr("disabled", value);
    $('#createEmail').attr("disabled", value);
    $('#createPassword').attr("disabled", value);
    $('#createRePassword').attr("disabled", value);
}


//Disable Delete Modal Form  Entire TextBox
function disabledDeleteModalTextBox(value = true) {
    $('#deleteFirstName').attr("disabled", value);
    $('#deleteLastName').attr("disabled", value);
    $('#deleteUserName').attr("disabled", value);
    $('#deleteEmail').attr("disabled", value);
}

//Reload DataTable
function ReloadTable() {
    // $('#example').DataTable().clear();                                                        
    $('#userTable').DataTable().ajax.reload(null,false);
}

//Action Message
function toastMessage(time, icon, message) {
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: time,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer)
            toast.addEventListener('mouseleave', Swal.resumeTimer)
        }
    })

    Toast.fire({
        icon: icon,
        title: message,
    })
}                  