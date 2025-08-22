
function GetDashboardData() {

    $.ajax({
        url: "/Dashboard/GetDashboardData",
        type: 'GET',
        dataType: 'json',
        success: function (data) {

            if (data.length > 0) {
                $('#ddlYTDActiveSale').text(data[0].YTD_ACTIVE_SALE);
                $('#ddlYTDActivePremium').text(data[0].YTD_ACTIVE_PREMIUM);
                $('#ddlYTDRefundSale').text(data[0].YTD_REFUND_SALE);
                $('#ddlYTDRefundPremium').text(data[0].YTD_REFUND_PREMIUM);

                $('#ddlMTDActiveSale').text(data[0].MTD_ACTIVE_SALE);
                $('#ddlMTDActivePremium').text(data[0].MTD_ACTIVE_PREMIUM);
                $('#ddlMTDRefundSale').text(data[0].MTD_REFUND_SALE);
                $('#ddlMTDRefundPremium').text(data[0].MTD_REFUND_PREMIUM);

                $('#ddlTODAYActiveSale').text(data[0].TODAY_ACTIVE_SALE);
                $('#ddlTODAYActivePremium').text(data[0].TODAY_ACTIVE_PREMIUM);
                $('#ddlTODAYRefundSale').text(data[0].TODAY_REFUND_SALE);
                $('#ddlTODAYRefundPremium').text(data[0].TODAY_REFUND_PREMIUM);
            }
            FillChart_ActiveSale(data);
            FillChart_ActivePremium(data);
            FillChart_RefundSale(data);
            FillChart_RefundPremium(data);
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

function FillChart_ActiveSale(d) {

    var data = {
        YTD_Active_Sale: d[0]?.YTD_ACTIVE_SALE ?? 0,
        MTD_Active_Sale: d[0]?.MTD_ACTIVE_SALE ?? 0,
        TODAY_Active_Sale: d[0]?.TODAY_ACTIVE_SALE ?? 0,
    };

    var chart = new CanvasJS.Chart("chartContainerActiveSale", {
        animationEnabled: true,
        theme: "light2",
        title: {
            text: "Active Sale"
        },
        axisY: {
            includeZero: true,
            title: "No. of Sales"
        },
        data: [{
            type: "column",
            indexLabel: "{y}",
            indexLabelFontColor: "#5A5757",
            indexLabelPlacement: "outside",
            dataPoints: [
                { label: "YTD Active Sale", y: data.YTD_Active_Sale },
                { label: "MTD Active Sale", y: data.MTD_Active_Sale },
                { label: "Today Active Sale", y: data.TODAY_Active_Sale },
            ]
        }]
    });

    chart.render();
}

function FillChart_ActivePremium(d) {

    var data = {
        YTD_Active_Premium: d[0]?.YTD_ACTIVE_PREMIUM ?? 0,
        MTD_Active_Premium: d[0]?.MTD_ACTIVE_PREMIUM ?? 0,
        TODAY_Active_Premium: d[0]?.TODAY_ACTIVE_PREMIUM ?? 0,
    };

    var chart = new CanvasJS.Chart("chartContainerActivePremium", {
        animationEnabled: true,
        theme: "light2",
        title: {
            text: "Active Premium"
        },
        axisY: {
            includeZero: true,
            title: "Total Sales"
        },
        data: [{
            type: "column",
            indexLabel: "{y}",
            indexLabelFontColor: "#5A5757",
            indexLabelPlacement: "outside",
            dataPoints: [
                { label: "YTD Active Premium", y: data.YTD_Active_Premium },
                { label: "MTD Active Premium", y: data.MTD_Active_Premium },
                { label: "Today Active Premium", y: data.TODAY_Active_Premium },
            ]
        }]
    });

    chart.render();
}

function FillChart_RefundSale(d) {

    var data = {
        YTD_Refund_Sale: d[0]?.YTD_REFUND_SALE ?? 0,
        MTD_Refund_Sale: d[0]?.MTD_REFUND_SALE ?? 0,
        TODAY_Refund_Sale: d[0]?.TODAY_REFUND_SALE ?? 0
    };

    var chart = new CanvasJS.Chart("chartContainerRefundSale", {
        animationEnabled: true,
        theme: "light2",
        title: {
            text: "Refund Sale"
        },
        axisY: {
            includeZero: true,
            title: "No. of Refund"
        },
        data: [{
            type: "column",
            indexLabel: "{y}",
            indexLabelFontColor: "#5A5757",
            indexLabelPlacement: "outside",
            dataPoints: [
                { label: "YTD Refund Sale", y: data.YTD_Refund_Sale },
                { label: "MTD Refund Sale", y: data.MTD_Refund_Sale },
                { label: "Today Refund Sale", y: data.TODAY_Refund_Sale }
            ]
        }]
    });

    chart.render();
}

function FillChart_RefundPremium(d) {

    var data = {
        YTD_Refund_Premium: d[0]?.YTD_REFUND_PREMIUM ?? 0,
        MTD_Refund_Premium: d[0]?.MTD_REFUND_PREMIUM ?? 0,
        TODAY_Refund_Premium: d[0]?.TODAY_REFUND_PREMIUM ?? 0
    };

    var chart = new CanvasJS.Chart("chartContainerRefundPremium", {
        animationEnabled: true,
        theme: "light2",
        title: {
            text: "Refund Premium"
        },
        axisY: {
            includeZero: true,
            title: "Total Refund"
        },
        data: [{
            type: "column",
            indexLabel: "{y}",
            indexLabelFontColor: "#5A5757",
            indexLabelPlacement: "outside",
            dataPoints: [
                { label: "YTD Refund Premium", y: data.YTD_Refund_Premium },
                { label: "MTD Refund Premium", y: data.MTD_Refund_Premium },
                { label: "Today Refund Premium", y: data.TODAY_Refund_Premium }
            ]
        }]
    });

    chart.render();
}