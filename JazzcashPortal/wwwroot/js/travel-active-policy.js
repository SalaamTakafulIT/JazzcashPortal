
function LoadTravelActivePolicy(data) {
    if ($.fn.dataTable.isDataTable('#tblTravelActivePolicy')) {
        $('#tblTravelActivePolicy').DataTable().clear().destroy();
    }

    $('#tblTravelActivePolicy').DataTable({
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
            /*{ "data": "" },*/
            {
                data: "POLICY_NO", title: "Policy No",
                render: function (data, type, row) {
                    return formatCell(data);
                }
            },
            {
                data: "CLIENT_NAME", title: "Client Name",
                render: function (data, type, row) {
                    return formatCell(data);
                }
            },
            {
                data: "CONTACT_NO", title: "Contact #",
                render: function (data, type, row) {
                    return formatCell(data);
                }
            },
            {
                data: "CNIC", title: "CNIC",
                render: function (data, type, row) {
                    return formatCell(data);
                }
            },
            {
                data: "FROM_DATE", title: "From Date",
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
                data: "TO_DATE", title: "To Date",
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
                data: "NO_OF_DAYS", title: "No Of Days",
                render: function (data, type, row) {
                    return formatCell(data);
                }
            },
            {
                data: "PRODUCT_TYPE", title: "Product Type",
                render: function (data, type, row) {
                    return formatCell(data);
                }
            },
            {
                data: "PLAN_NAME", title: "Plan Name",
                render: function (data, type, row) {
                    return formatCell(data);
                }
            },
            {
                data: "DEPT_FROM", title: "Dept From",
                render: function (data, type, row) {
                    return formatCell(data);
                }
            },
            {
                data: "DEPT_TO", title: "Dept To",
                render: function (data, type, row) {
                    return formatCell(data);
                }
            }
        ]
    });
}

$('#btnSearch_HP').on('click', function (e) {
    // condition to skip validation
    if (userType === "V") {
        SearchTravelActivePolicy();
        return;
    }

    // normal validation
    const form = document.getElementById('TravelActivePolicyForm');
    if (form.checkValidity()) {
        e.preventDefault();
        SearchTravelActivePolicy();
    } else {
        form.reportValidity();
    }
});

function SearchTravelActivePolicy() {

    var model = $('#TravelActivePolicyForm').serialize();

    $.ajax({
        url: '/TravelActivePolicy/SearchTravelActivePolicy',
        type: 'POST',
        data: model,
        success: function (res) {
            if (res.success) {
                var data = res.data;
                LoadTravelActivePolicy(data);
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