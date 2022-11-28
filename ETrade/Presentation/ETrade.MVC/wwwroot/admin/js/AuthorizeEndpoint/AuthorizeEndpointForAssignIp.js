var $ = jQuery.noConflict();
$(document).ready(function ($) {
    var tree = $('#tree').tree({
        primaryKey: 'id',
        uiLibrary: 'bootstrap4',
        dataSource: '/Admin/AuthorizeEndpoint/GetAuthorizeEndpointsforAssignIp',
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

    //IpModal Save
    $("#endpointIpModalPartial").on('click', '.endpointIpModalSaveBtn', function (e) {
        var ipIds = [];
        var ipEndpointId;
        var ipAreaName;
        var ipMenuName;
        //var Id = $('#endpointIpModalForm .modal-body input[type="checkbox"]').attr("data-id");
        $.each($('#endpointIpModalForm .modal-body input[type="checkbox"]:checked'), function () {
            if ($(this).is('[data-areaname]')) {
                var ip_areaName = $(this).attr("data-areaname");
            }
            if ($(this).is('[data-menuname]')) {
                var ip_menuName = $(this).attr("data-menuname");
            }
            if ($(this).is('[data-id]')) {
                var ip_id = $(this).attr("data-id");
            }
            if ($(this).is('[data-endpointId]')) {
                var ip_endpointId = $(this).attr("data-endpointId");
            }
            ipIds.push(ip_id);
            ipEndpointId=ip_endpointId
            ipAreaName=ip_areaName;
            ipMenuName=ip_menuName;
        })
        if(ipIds.length===0) {
            if($('#endpointIpModalForm .modal-body input[type="checkbox"]').is('[data-areaname]')){
                ipAreaName = $('#endpointIpModalForm .modal-body input[type="checkbox"]').attr("data-areaname");
            }
            if($('#endpointIpModalForm .modal-body input[type="checkbox"]').is('[data-menuname]')){
                ipMenuName = $('#endpointIpModalForm .modal-body input[type="checkbox"]').attr("data-menuname");
            }
            if($('#endpointIpModalForm .modal-body input[type="checkbox"]').is('[data-endpointId]')){
                ipEndpointId = $('#endpointIpModalForm .modal-body input[type="checkbox"]').attr("data-endpointId");
            }
        }
        
        $.ajax({
            url: '/Admin/AuthorizeEndpoint/AssignIpAddresses',
            type: "POST",
            data: { "ipAreaName": ipAreaName ,"ipMenuName":ipMenuName,"ipEndpointId":ipEndpointId, "ipIds":ipIds},
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
    
    $('#tree').on('click', '.CustomEndpointClass', function () {
        if ($(this).is('[data-areaname]')) {
            var areaName = $(this).attr("data-areaname");
        }
        if ($(this).is('[data-menuname]')) {
            var menuName = $(this).attr("data-menuname");
        }
        if ($(this).is('[data-id]')) {
            var Id = $(this).attr("data-id");
        }
        console.log(areaName);
        console.log(menuName);
        console.log(Id);
        $.ajax({
            url: '/Admin/AuthorizeEndpoint/GetIpAdressesByEndpoint',
            type: 'POST',
            data: { "areaName": areaName ,"menuName":menuName, "id":Id},
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

    /*
    $('#tree').on('click', '.IpAdressesByAreaName', function () {
        var areaName = $(this).attr("data-areaname");
        console.log(areaName);
        $.ajax({
            url: '/Admin/AuthorizeEndpoint/GetIpAdressesByAreaName',
            type: 'POST',
            data: { "areaName": areaName },
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
    
    $('#tree').on('click', '.IpAdressesByAreaNameandMenuName', function () {
        var areaName = $(this).attr("data-areaname");
        var menuName = $(this).attr("data-menuname");
        $.ajax({
            url: '/Admin/AuthorizeEndpoint/GetIpAdressesByAreaNameandMenuName',
            type: 'POST',
            data: { "areaName": areaName ,"menuName":menuName},
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
    
   */
    
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


