
function GetCancelPolicies() {

    $.ajax({
        url: "/CancelPolicies/GetCancelPolicies",
        type: 'GET',
        dataType: 'json',
        success: function (data) {

            LoadCancelPolicies(data);
        },
        error: function (request) {

        }, beforeSend: function () {
            showLoader();
        },
        complete: function () {
            hideLoader();
        },
    });
}

function LoadCancelPolicies(data) {
    if ($.fn.dataTable.isDataTable('#tblCancelPolicy')) {
        $('#tblCancelPolicy').DataTable().clear().destroy();
    }

    $('#tblCancelPolicy').DataTable({
        data: data,
        responsive: true,
        paging: true,
        processing: true,
        dom: 'lBfrtip',
        buttons: ['excelHtml5', 'print'],
        select: true,
        order: [['ENT_DATE', "asc"]],
        buttons: [
            {
                extend: 'colvis',
                text: '<i class="bi bi-eye"></i> Show/Hide Columns',
                className: 'btn btn-info me-1 mb-1 btn-sm'
            },
            {
                extend: 'excelHtml5',
                text: '<i class="bi bi-file-earmark-excel"></i> Export to Excel',
                className: 'btn btn-warning me-1 mb-1 btn-sm',
                exportOptions: { columns: ':visible' }
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="bi bi-file-earmark-pdf"></i> Export to PDF',
                className: 'btn btn-danger me-1 mb-1 btn-sm',
                exportOptions: { columns: ':visible' },
                orientation: 'landscape',
                pageSize: 'A4',
                title: 'Case Studies'
            }
        ],
        "columnDefs": [
            {
                "data": null,
                "defaultContent": "",
                "targets": 0
            }
        ],
        columns: [
            {
                data: "ASSORTEDSTRING", title: "Assorted String",
                render: function (data, type, row) {
                    return formatCell(data);
                }
            },
            {
                data: "ENDORSEMENT", title: "Endorsment",
                render: function (data, type, row) {
                    return formatCell(data);
                }
            },
            {
                data: "NAME", title: "Name",
                render: function (data, type, row) {
                    return formatCell(data);
                }
            },
            {
                data: "PHONE_NO", title: "Contact #",
                render: function (data, type, row) {
                    return formatCell(data);
                }
            },
            {
                data: "AMOUNT", title: "Amount",
                render: function (data, type, row) {
                    return formatCell(data);
                }
            },
            {
                data: "DAILER_RESPONSE", title: "Dailer Response",
                render: function (data, type, row) {
                    return formatCell(data);
                }
            },
            {
                data: "ENT_DATE", title: "Ent Date",
                render: function (data, type, row) {
                    if (type === 'display') {
                        if (!data) return '';
                        return new Date(data).toLocaleString('en-GB', {
                            day: '2-digit', month: 'short', year: 'numeric',
                            hour: '2-digit', minute: '2-digit', second: '2-digit',
                            hour12: true
                        });
                    }
                    return data;
                }
            },
            {
                data: "CNIC", title: "Cnic",
                render: function (data, type, row) {
                    return formatCell(data);
                }
            },
            {
                data: "ADDRESS", title: "Address",
                render: function (data, type, row) {
                    return formatCell(data);
                }
            },
            {
                data: "STATUS", title: "Status",
                render: function (data, type, row, meta) {
                    if (type == 'display') {
                        if (data == "R") {
                            return '<span class="badge fs-10 badge-phoenix badge-phoenix-danger">Cancel <span class="ms-1 fas fa-ban" data-fa-transform="shrink-2"></span></span>';
                        }
                    }
                    return data;
                }
            }
        ]
    });
}