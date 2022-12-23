var $=jQuery.noConflict();
$(document).ready(function ($) {
    var tables = $("#deletedUserTable").DataTable({
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
            "url": "/Admin/DeletedAccount/DeletedUsers",
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
                    return  '<a class="btn btn-danger" onclick="SetUserActive(' + data + ')"><i class="fa fa-trash-o">Aktif Et</i></a>';
                }
            }
        ],
        dom: '<"dt-header"Bf>rt<"dt-footer"ip>',
        buttons: [
            "pageLength",
            {
                extend: 'excelHtml5',
                text: '<i class="fa fa-file-excel-o"> Excel</i>',
                filename: 'Silinmmiş Kullanıcı Listesi',
                title: 'Silinmmiş Kullanıcı Listesi',
                exportOptions: {
                    columns: [1, 2, 3, 4]
                },
                className: "btn-export-excel"
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="fa fa-file-pdf-o"> Pdf</i>',
                filename: 'Silinmmiş Kullanıcı Listesi',
                title: 'Silinmmiş Kullanıcı Listesi',
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
    
});


//Set User Active
function SetUserActive(Id) {
    Swal.fire({
        title: 'Kullanıcıyı aktif etmek İstediğinizden Emin misiniz?',
        text: "Kullanıcıyı aktif etmeye onay verdiğiniz zaman kullanıcı sisteme giriş yetkisi dahil birçok yetkiye sahip olacaktır!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Aktif Et!',
        cancelButtonText: 'İptal'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "POST",
                url: '/Admin/DeletedAccount/SetActiveUser',
                data: {
                    userId: Id
                },
                dataType: "json",
                success: function (activeResult) {
                    if (activeResult.success) {
                        Swal.fire({
                            title: 'Aktif Oldu?',
                            text: "Kullanıcı Başarıyla Aktif Edildi",
                            icon: 'success',
                        }).then((result) => {
                            ReloadTable();
                        })
                    } else {
                        Swal.fire({
                            title: 'Hata',
                            text: "Kullanıcı Aktif Edilemedi",
                            icon: 'error',
                        });
                    }
                },
                error: function (errormessage) {
                    Swal.fire({
                        title: 'Hata',
                        text: "Kullanıcı Aktif Edilemedi",
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
    $('#deletedUserTable').DataTable().ajax.reload(null,false);
}
 