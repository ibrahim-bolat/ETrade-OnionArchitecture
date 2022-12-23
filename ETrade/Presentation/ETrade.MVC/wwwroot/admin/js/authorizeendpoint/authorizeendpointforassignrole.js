var $ = jQuery.noConflict();
$(document).ready(function ($) {
    var tree = $('#tree').tree({
        primaryKey: 'id',
        uiLibrary: 'bootstrap4',
        dataSource: '/Admin/AuthorizeEndpoint/GetAuthorizeEndpointsforAssignRole',
        width: 800,
        icons: {
            expand: '<i class="gj-icon chevron-right"></i>',
            collapse: '<i class="gj-icon chevron-down"></i>'
        }
    });

    tree.on('dataBound', function () {
        tree.expandAll();
    });
    $('#expandAll').on('click', function () {
        tree.expandAll();
    });
    $('#collapseAll').on('click', function () {
        tree.collapseAll();
    });

    //RoleModal Save
    $('#endpointRoleModalSave').click(function (e) {
        var roleIds = [];
        var Id = $('#endpointRoleModalForm .modal-body input[type="checkbox"]').attr("data-id");
        $.each($('#endpointRoleModalForm .modal-body input[type="checkbox"]:checked'), function () {
            var role_id = $(this).attr("name");
            roleIds.push(role_id);
        })
        var postData = {
            roleIds: roleIds
        };
        $.ajax({
            url: '/Admin/AuthorizeEndpoint/AssignRolesByEndpointId/' + Id,
            type: "POST",
            data: postData,
            dataType: "json",
            traditional: true,
            success: function (result) {
                if (result.success) {
                    $("#endpointRoleModal").modal("hide");
                    toastMessage(3000, "success", "Tebrikler,","Rol Atama Başarıyla Gerçekleşti");
                } else {
                    toastMessage(3000, "error", "Hata","Rol Atama İşlemi Yapılamadı!");
                }
            },
            error: function (errormessage) {
                toastMessage(3000, "error", "Hata","Rol Atama İşlemi Yapılamadı!");
            }
        });
        return false;
    });
});

//Get Role By Id For Rol Save
function getRolesByEndpointId(Id) {
    var html = "";
    $.ajax({
        url: '/Admin/AuthorizeEndpoint/GetRolesByEndpointId/' + Id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            if (result.success) {
                $.each(result.roles, function (index, value) {
                    var isChecked = "";
                    if(value.HasAssign === true)
                        isChecked="checked";
                    if(index%2===0){
                        html+=`<div class="row mb-2">`
                    }
                    html+=`<div class="col-6">
                          <div class="custom-control custom-switch custom-control-inline">
                          <input class="custom-control-input" type="checkbox" data-id="${Id}" name="${value.Id}" id="roleHasAssign${value.Id}" ${isChecked}>
                          <label class="custom-control-label" for="roleHasAssign${value.Id}">${value.Name}</label>
                          </div>
                          </div>`
                    if(index%2===1){
                        html+=`</div>`
                    }
                });
                $('#endpointRoleModal .modal-body').html(html);
                $('#endpointRoleModal').modal('show');
            } else {
                toastMessage(3000, "error", "Hata","Roller Getirilemedi");
            }

        },
        error: function (errormessage) {
            toastMessage(3000, "error", "Hata","Roller Getirilemedi");
        }
    });
    return false;
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