var $=jQuery.noConflict();
$(document).ready(function ($) {
    var roleId = $("#roleId").attr("data-id");
    var tables = $("#usersOfTheRoleTable").DataTable({
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
            "url": "/Admin/RoleOperation/UsersOfTheRole?id=" + roleId,
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
            {"data": "Id", "name": "Id", "autoWidth": true},
            {"data": "FirstName", "name": "Ad", "autoWidth": true},
            {"data": "LastName", "name": "Soyad", "autoWidth": true},
            {"data": "UserName", "name": "Kullanıcı Adı", "autoWidth": true},
            {"data": "Email", "name": "Email", "autoWidth": true},
            {
                "data": "Id", "width": "50px", "render": function (data) {
                    return  '<a class="btn btn-danger" onclick="RemoveUserFromRole(' + data + ','+ roleId + ')"><i class="fa fa-trash-o">Rolden Çıkar</i></a>';
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

});


//Remove User From Role
function RemoveUserFromRole(userId,roleId) {
    Swal.fire({
        title: 'Kullanıcıyı rolden çıkarmak İstediğinizden Emin misiniz?',
        text: "Kullanıcıyı rolden çıkarmaya onay verdiğiniz zaman kullanıcı ilgili rolden çıkarılarak yetkileiri kullanamayacaktır!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Çıkar!',
        cancelButtonText: 'İptal'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: '/Admin/RoleOperation/RemoveUserFromRole/',
                data: {
                    userId: userId,
                    roleId: roleId
                },
                dataType: "json",
                success: function (activeResult) {
                    if (activeResult.success) {
                        Swal.fire({
                            title: 'Çıkarıldı?',
                            text: "Kullanıcı Başarıyla Rolden Çıkarıldı",
                            icon: 'success',
                        }).then((result) => {
                            ReloadTable();
                        })
                    } else {
                        Swal.fire({
                            title: 'Hata',
                            text: "Kullanıcı Rolden Çıkarılamadı",
                            icon: 'error',
                        });
                    }
                },
                error: function (errormessage) {
                    Swal.fire({
                        title: 'Hata',
                        text: "Kullanıcı Rolden Çıkarılamadı",
                        icon: 'error',
                    });
                }
            });
        }
    });
}


//Reload DataTable
function ReloadTable() {
    // $('#example').DataTable().clear();                                                        
    $('#usersOfTheRoleTable').DataTable().ajax.reload(null,false);
}
 