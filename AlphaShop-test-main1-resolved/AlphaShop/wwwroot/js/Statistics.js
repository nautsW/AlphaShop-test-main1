$(document).ready(function () {
    var year = $('#year_Choice').val();
    GetDataList(year);
    $('#year_Choice').on('change', function () {
        var year = $(this).val();
        GetReportList(year);
    });
});
//xử lí event chọn treong combobox

function GetDataList(year) {
    $.get('/Manage/GetRevenueData?year=' + year, function (resp) {
        console.log(resp);
        InitialLoad__forRvnData(resp);
    });
    $.get('/Manage/GetPrdOrdTimeData?year=' + year, function (resp) {
        console.log(resp);
        InitialLoad__forPotData(resp);
    });
    $.get('/Manage/GetWebAccessData?year=' + year, function (resp) {
        console.log(resp);
        InitialLoad__forWaData(resp);
    });
    $.get('/Manage/GetOrdCountData?year=' + year, function (resp) {
        console.log(resp);
        InitialLoad__forOcData(resp);
    });
}

//xử lí event onchange của combobox year
function GetReportList(year) {
    $.get('/Manage/GetRevenueData?year=' + year, function (resp) {
        console.log(resp);
        RegisterChartProduct(resp);
    });
}



//func load mẫu
// function InitialLoad(lsData) {

//     //khởi tạo dữ liệu
//     var lsLabel = [];
//     var lsDataSource = [];

//     $.each(lsData, function (index, item) {
//         lsLabel.push(item.month);
//         lsDataSource.push(item.total);
//     });

//     //lấy id của từng chart
//     var ctx__rvn = document.getElementById("revenueBarChart");
//     var ctx__pot = document.getElementById("PrdOrderedTimeChart");
//     var ctx__wa = document.getElementById("WebAccessChart");
//     var ctx__oc = document.getElementById("OrderCountChart");

//     //khởi tạo data cho từng chart
//     var revenue_ChartData = {
//         labels: lsLabel,
//         datasets: [
//             {
//                 label: 'Doanh thu(VNĐ)',
//                 borderColor: 'rgb(0, 191, 255)',
//                 backgroundColor: 'rgba(0, 191, 255, 0.5)',
//                 borderWidth: 2,
//                 data: lsDataSource
//             }
//         ]
//     };
//     var PrdOrdTime_ChartData = {
//         labels: lsLabel,
//         datasets: [
//             {
//                 label: 'Số lượng đặt',
//                 borderColor: 'rgb(132, 191, 104)',
//                 backgroundColor: 'rgba(132, 191, 104, 0.5)',
//                 borderWidth: 2,
//                 data: lsDataSource
//             }
//         ]
//     };
//     var WA_ChartData = {
//         labels: lsLabel,
//         datasets: [
//             {
//                 label: 'Lượng truy cập',
//                 borderColor: 'rgb(244, 116, 59)',
//                 backgroundColor: 'rgba(244, 116, 59, 0.5)',
//                 borderWidth: 2,
//                 data: lsDataSource,
//                 cubicInterpolationMode: 'monotone',
//                 tension: 0.4
//             }
//         ]
//     };
//     var OrdCount_ChartData = {
//         labels: lsLabel,
//         datasets: [
//             {
//                 label: 'Đơn được nhận',
//                 borderColor: 'rgb(95, 79, 214)',
//                 backgroundColor: 'rgba(95, 79, 214, 0.5)',
//                 borderWidth: 2,
//                 data: lsDataSource
//             },
//             {
//                 label: 'Đơn bị hủy',
//                 borderColor: 'rgb(30, 100, 180)',
//                 backgroundColor: 'rgba(30, 100, 180, 0.5)',
//                 borderWidth: 2,
//                 data: lsDataSource
//             }
//         ]
//     };

//     var revenue_BarChart = new Chart(ctx__rvn, {
//         type: 'bar',
//         data: revenue_ChartData,
//     });
//     var pot_BarChart = new Chart(ctx__pot, {
//         type: 'bar',
//         data: PrdOrdTime_ChartData,
//     });
//     var WA_ChartData = new Chart(ctx__wa, {
//         type: 'line',
//         data: WA_ChartData,
//     });
//     var oc_BarChart = new Chart(ctx__oc, {
//         type: 'bar',
//         data: OrdCount_ChartData,
//     });
//     //đưa data vào chart

// }


//REVENUE

function InitialLoad__forRvnData(lsData) {

    //khởi tạo dữ liệu
    var lsLabel = [];
    var lsDataSource = [];

    $.each(lsData, function (index, item) {
        lsLabel.push(item.month);
        lsDataSource.push(item.total);
    });

    //lấy id của từng chart
    var ctx__rvn = document.getElementById("revenueBarChart");

    //khởi tạo data cho từng chart
    var revenue_ChartData = {
        labels: lsLabel,
        datasets: [
            {
                label: 'Doanh thu(VNĐ)',
                borderColor: 'rgb(0, 191, 255)',
                backgroundColor: 'rgba(0, 191, 255, 0.5)',
                borderWidth: 2,
                data: lsDataSource
            }
        ]
    };
    var revenue_BarChart = new Chart(ctx__rvn, {
        type: 'bar',
        data: revenue_ChartData,
    });
}
//ProOrdCount
function InitialLoad__forPotData(lsData) {

    //khởi tạo dữ liệu
    var lsLabel = [];
    var lsDataSource = [];

    $.each(lsData, function (index, item) {
        lsLabel.push(item.month);
        lsDataSource.push(item.total);
    });

    //lấy id của từng chart

    var ctx__pot = document.getElementById("PrdOrderedTimeChart");


    //khởi tạo data cho từng chart

    var PrdOrdTime_ChartData = {
        labels: lsLabel,
        datasets: [
            {
                label: 'Số lượng đặt',
                borderColor: 'rgb(132, 191, 104)',
                backgroundColor: 'rgba(132, 191, 104, 0.5)',
                borderWidth: 2,
                data: lsDataSource
            }
        ]
    };

    var pot_BarChart = new Chart(ctx__pot, {
        type: 'bar',
        data: PrdOrdTime_ChartData,
        options: {
            indexAxis: 'y'
        }
    });

    //đưa data vào chart
}
//Webaccess
function InitialLoad__forWaData(lsData) {

    //khởi tạo dữ liệu
    var lsLabel = [];
    var lsDataSource = [];

    $.each(lsData, function (index, item) {
        lsLabel.push(item.month);
        lsDataSource.push(item.count);
    });

    //lấy id của từng chart

    var ctx__wa = document.getElementById("WebAccessChart");


    //khởi tạo data cho từng chart

    var WA_ChartData = {
        labels: lsLabel,
        datasets: [
            {
                label: 'Lượng truy cập',
                borderColor: 'rgb(244, 116, 59)',
                backgroundColor: 'rgba(244, 116, 59, 0.5)',
                borderWidth: 2,
                data: lsDataSource,
                cubicInterpolationMode: 'monotone',
                tension: 0.4
            }
        ]
    };
    var WA_ChartData = new Chart(ctx__wa, {
        type: 'line',
        data: WA_ChartData,
    });

}
//OrderCount
function InitialLoad__forOcData(lsData) {

    //khởi tạo dữ liệu
    var lsLabel = [];
    var lsDataSource1 = [];
    var lsDataSource2 = [];

    $.each(lsData, function (index, item) {
        lsLabel.push(item.month);
        lsDataSource1.push(item.accepted);
        lsDataSource2.push(item.denied);
    });

    //lấy id của từng chart

    var ctx__oc = document.getElementById("OrderCountChart");

    //khởi tạo data cho từng chart

    var OrdCount_ChartData = {
        labels: lsLabel,
        datasets: [
            {
                label: 'Đơn được nhận',
                borderColor: 'rgb(95, 79, 214)',
                backgroundColor: 'rgba(95, 79, 214, 0.5)',
                borderWidth: 2,
                data: lsDataSource1
            },
            {
                label: 'Đơn bị hủy',
                borderColor: 'rgb(30, 100, 180)',
                backgroundColor: 'rgba(30, 100, 180, 0.5)',
                borderWidth: 2,
                data: lsDataSource2
            }
        ]
    };

    var oc_BarChart = new Chart(ctx__oc, {
        type: 'bar',
        data: OrdCount_ChartData,
    });
    //đưa data vào chart

}





function RegisterChartProduct(lsData) {

    $("canvas#revenueBarChart").remove;
    $('#chart__holder-rv').html('<canvas class="Chart" id="revenueBarChart"></canvas>');


    var lsLabel = [];
    var lsDataSource = [];
    $.each(lsData, function (index, item) {
        lsLabel.push(item.month);
        lsDataSource.push(item.total);
    });
    console.log(lsLabel);
    console.log(lsDataSource);

    var ctx = document.getElementById("revenueBarChart");
    var barChartData = {
        labels: lsLabel,
        datasets: [
            {
                label: 'Doanh thu(VNĐ)',
                borderColor: 'rgb(0, 191, 255)',
                backgroundColor: 'rgba(0, 191, 255, 0.5)',
                borderWidth: 2,
                data: lsDataSource
            }
        ]
    };
    console.log(barChartData);
    var myBarChart = new Chart(ctx, {
        type: 'bar',
        data: barChartData,

    });

}
