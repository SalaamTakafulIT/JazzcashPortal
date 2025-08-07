
$(() => {
    GetProductConfig();

    $('#productConfigModal').on('hidden.bs.modal', function () {
        resetClick_ProductConfig();
    });
});

function GetProductConfig() {

    $.ajax({
        url: "/ProductConfiguration/GetProductConfig",
        type: 'GET',
        dataType: 'json',
        success: function (data) {

            LoadProductConfiguration(data);
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

function LoadProductConfiguration(data) {
    if ($.fn.dataTable.isDataTable('#tblProductConfiguration')) {
        $('#tblProductConfiguration').DataTable().clear().destroy();
    }

    $('#tblProductConfiguration').DataTable({
        data: data,
        responsive: true,
        paging: true,
        processing: true,
        dom: 'lBfrtip',
        buttons: ['excelHtml5', 'print'],
        select: true,
        order: [[0, "asc"]],
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
        columns: [
            {
                data: "PRODUCT_ID", title: "Id",
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return `<a href="#" class="edit-productConfig" data-id="${data}">${data}</a>`;
                    }
                    return data;
                }
            },
            {
                data: "PRODUCT_NAME", title: "Name",
                render: function (data, type, row, meta) {
                    if (type === 'display') {
                        return `<a href="#" class="edit-productConfig" data-id="${row["PRODUCT_ID"]}">${data}</a>`;
                    }
                    return data;
                }
            },
            {
                data: "SUMCOVERED", title: "Sum Covered",
                render: function (data, type, row) {
                    return formatCell(data);
                }
            },
            {
                data: "NET_CONTRIBUTION", title: "Net Contribution",
                render: function (data, type, row) {
                    return formatCell(data);
                }
            },
        ]
    });
}

$('#productConfigSetup').on('submit', function (e) {
    e.preventDefault();

    if ($('#btnSave_ProductConfig').text() == 'Update') {
        UpdateProductConfig();
    } else {
        SaveProductConfig();
    }
});

function SaveProductConfig() {

    var model = $('#productConfigSetup').serialize();

    $.ajax({
        url: '/ProductConfiguration/SaveProductConfiguration',
        type: 'POST',
        data: model,
        success: function (res) {
            if (res.action) {

                SuccessNotify(res.message);
                GetProductConfig();
                resetClick_ProductConfig();
            }
            else {
                ErrorNotify(res.errorMessage);
            }
        },
        error: function () {

        },
        beforeSend: function () {
            showLoader();
        },
        complete: function () {
            hideLoader();
        },
    });
}

$('#tblProductConfiguration').on('click', '.edit-productConfig', function (e) {
    e.preventDefault();
    var id = $(this).data('id');
    $('#productConfigSetup')[0].reset();

    $.ajax({
        url: '/ProductConfiguration/EditProductConfig',
        type: 'GET',
        data: { Id: id },
        success: function (data) {
            if (data.length > 0) {

                $('#btnDelete_ProductConfig').show();
                $('#btnSave_ProductConfig').text('Update');
                adjustButtonsWidthAfter_ProductConfig();

                $('#PRODUCT_ID').val(data[0].PRODUCT_ID);
                $('#PRODUCT_NAME').val(data[0].PRODUCT_NAME);
                $('#SUMCOVERED').val(data[0].SUMCOVERED);
                $('#NET_CONTRIBUTION').val(data[0].NET_CONTRIBUTION);
                $('#productConfigModal').modal('show');
            }
            else {
                ErrorNotify("No records found.");
            }
        },
        error: function () {
            ErrorNotify('Failed to fetch details.');
        },
        beforeSend: function () {
            showLoader();
        },
        complete: function () {
            hideLoader();
        },
    });
});

function UpdateProductConfig() {
    var model = $('#productConfigSetup').serialize();
    model += '&PRODUCT_ID=' + $('#PRODUCT_ID').val();

    $.ajax({
        url: '/ProductConfiguration/UpdateProductConfiguration',
        type: 'POST',
        data: model,
        success: function (res) {
            if (res.action) {

                SuccessNotify(res.message);
                GetProductConfig();
                resetClick_ProductConfig();
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

$('#btnDelete_ProductConfig').on('click', function (e) {
    DeleteDetail_ProductConfig();
});

function DeleteDetail_ProductConfig() {

    Swal.fire({
        title: 'Are you sure?',
        text: "This record will be permanently deleted.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'Cancel',
        reverseButtons: true,
        customClass: {
            confirmButton: 'btn btn-primary',
            cancelButton: 'btn btn-secondary'
        }
    }).then((result) => {

        if (result.isConfirmed) {

            const id = $('#PRODUCT_ID').val();

            $.ajax({
                url: '/ProductConfiguration/DeleteProductConfiguration',
                type: 'POST',
                data: { Id: id },
                success: function (res) {
                    if (res.action) {

                        SuccessNotify(res.message);
                        GetProductConfig();
                        resetClick_ProductConfig();
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
}

function adjustButtonsWidthBefore_ProductConfig() {

    $('#divDelete_ProductConfig').hide();
    $('#divSave_ProductConfig').removeClass('col-lg-6 col-md-6 col-sm-6');
    $('#divSave_ProductConfig').addClass('col-lg-12 col-md-12 col-sm-12');
    $('#divDelete_ProductConfig').addClass('col-lg-6 col-md-6 col-sm-6');
}

function adjustButtonsWidthAfter_ProductConfig() {

    $('#divDelete_ProductConfig').show();
    $('#divSave_ProductConfig').removeClass('col-lg-12 col-md-12 col-sm-12');
    $('#divSave_ProductConfig').addClass('col-lg-6 col-md-6 col-sm-6');
    $('#divDelete_ProductConfig').addClass('col-lg-6 col-md-6 col-sm-6');
}

function resetClick_ProductConfig() {

    adjustButtonsWidthBefore_ProductConfig();
    $('#productConfigSetup')[0].reset();
    $('#btnSave_ProductConfig').text('Save');
    $('#btnDelete_ProductConfig').hide();
    $('#PRODUCT_ID').val('');
}