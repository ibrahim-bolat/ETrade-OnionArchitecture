var $ = jQuery.noConflict();
$(document).ready(function ($) {
    var tree = $('#tree').tree({
        primaryKey: 'id',
        uiLibrary: 'bootstrap4',
        dataSource: '/Admin/AuthorizeEndpoint/GetAuthorizeEndpointsforAssignIp',
        width: 800,
        checkboxes: true,
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
    $('#checkAll').on('click', function () {
        tree.checkAll();
    });
    $('#uncheckAll').on('click', function () {
        tree.uncheckAll();
    });

    //IpModal Save
    $('#endpointIpModalSave').click(function (e) {
        var ipIds = [];
        var Id = $('#endpointIpModalForm .modal-body input[type="checkbox"]').attr("data-id");
        $.each($('#endpointIpModalForm .modal-body input[type="checkbox"]:checked'), function () {
            var ip_id = $(this).attr("name");
            ipIds.push(ip_id);
        })
        var postData = {
            ipIds: ipIds
        };
        $.ajax({
            url: '/Admin/AuthorizeEndpoint/AssignIpAddresses/' + Id,
            type: "POST",
            data: postData,
            dataType: "json",
            traditional: true,
            success: function (result) {
                if (result.success) {
                    $("#endpointIpModal").modal("hide");
                    toastMessage(5000, "success", "IP Atama Başarıyla Gerçekleşti")
                } else {
                    toastMessage(3000, "error", "IP Atama İşlemi Yapılamadı!")
                }
            },
            error: function (errormessage) {
                toastMessage(3000, "error", "IP Atama İşlemi Yapılamadı!")
            }
        });
        return false;
    });
    
    $('#tree').on('click', '.IpAdressesByEndpointId', function () {
        var Id = $(this).attr("data-id");
        $.ajax({
            url: '/Admin/AuthorizeEndpoint/GetIpAdressesByEndpointId/' + Id,
            type: 'POST',
            contentType: "application/json;charset=UTF-8",
            dataType: 'html',
            success: function (modal) {
                $("#endpointIpModalPartial").html(modal);
                $("#endpointIpModal").modal("show");
            },
            error: function (errormessage) {
                toastMessage(3000, "error", "IP Adresleri Getirilemedi")
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


