
function GetHomePolicy() {

    $.ajax({
        url: "/HomePolicy/GetHomePolicy",
        type: 'GET',
        dataType: 'json',
        success: function (data) {

            LoadHomePolicy(data);
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

function LoadHomePolicy(data) {
    if ($.fn.dataTable.isDataTable('#tblHomePolicy')) {
        $('#tblHomePolicy').DataTable().clear().destroy();
    }

    $('#tblHomePolicy').DataTable({
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
            { "data": "" },
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
                        if (data == "A") {
                            return '<span class="badge fs-10 badge-phoenix badge-phoenix-success">Active <span class="ms-1 fas fa-check" data-fa-transform="shrink-2"></span></span>';
                        } else if (data == "R") {
                            return '<span class="badge fs-10 badge-phoenix badge-phoenix-danger">Cancel <span class="ms-1 fas fa-ban" data-fa-transform="shrink-2"></span></span>';
                        }
                    }
                    return data;
                }
            },
            {
                data: "POLICY_TRANSACTION_ID",
                title: "Action",
                width: "150px",
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        if (row["ENDORSEMENT_CODE"] == "" || row["ENDORSEMENT_CODE"] == null) {
                            return `<button type="button" class="btn btn-danger reversePolicy" data-trans_id="${data}" data-policy_id="${data}"><i class="fa fa-undo"></i> Reverse</button>`;
                        }
                        else {
                            return `<button type="button" class="btn btn-danger reversePolicy" data-id="${data}" disabled><i class="fa fa-undo"></i> Reverse</button>`;
                        }
                    }
                    return data;
                }
            }
        ]
    });
}

$('#tblHomePolicy').on('click', '.reversePolicy', function (e) {
    e.preventDefault();

    Swal.fire({
        title: 'Are you sure?',
        text: "This record will be Reversed.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, reverse it!',
        cancelButtonText: 'Cancel',
        reverseButtons: true,
        customClass: {
            confirmButton: 'btn btn-primary',
            cancelButton: 'btn btn-secondary'
        }
    }).then((result) => {
        if (result.isConfirmed) {

            const transCode = $(this).data('trans_id');
            const policyCode = $(this).data('policy_id');
            $.ajax({
                url: '/HomePolicy/ReversePolicy',
                type: 'POST',
                data: { policy_code: policyCode, trans_code: transCode },
                success: function (res) {
                    if (res.action) {

                        SuccessNotify(res.message);
                        SearchHomePolicy();
                    }
                    else {
                        ErrorNotify(res.errorMessage);
                    }
                },
                error: function () {

                },
                beforeSend: function () {
                    $('#loading').show();
                },
                complete: function () {
                    $('#loading').hide();
                },
            });
        }
    });
});

$('#btnSearch_HP').on('click', function () {
    SearchHomePolicy();
});

function SearchHomePolicy() {

    var model = $('#HomePolicyForm').serialize();

    $.ajax({
        url: '/HomePolicy/SearchHomePolicy',
        type: 'POST',
        data: model,
        success: function (res) {
            if (res.success) {
                var data = res.data;
                LoadHomePolicy(data);
            }
            else {
                ErrorNotify(res.error);
            }
        },
        error: function () {

        },
        beforeSend: function () {
            $('#loading').show();
        },
        complete: function () {
            $('#loading').hide();
        },
    });
}