$(function () {
    "use strict";

    // Hàm để gọi AJAX
    var arrDoanhThu = [];
    var arrLoiNhuan = [];
    var arrDate = [];
    $.ajax({
        url: 'api/ThongKe',
        method: 'GET',
        data: { fromDate: '', toDate: '' },
        success: function (data) {
            debugger;
            if (data.success) {
                for (var i = 0; i < data.dataThongKeTheoNgay.length; i++) {
                    arrDate.push(data.dataThongKeTheoNgay[i].date);
                    arrDoanhThu.push(data.dataThongKeTheoNgay[i].doanhThu);
                    arrLoiNhuan.push(data.dataThongKeTheoNgay[i].loiNhuan);
                }
                console.log(arrDate);
                console.log(arrDoanhThu);
                console.log(arrLoiNhuan);
            }
            var ctx = document.getElementById("chartjs_balance_bar").getContext('2d');
            var myChart = new Chart(ctx, {
                type: 'bar',


                data: {
                    labels: arrDate,
                    datasets: [{
                        label: 'Doanh thu',
                        data: arrDoanhThu,
                        backgroundColor: "rgba(89, 105, 255,.8)",
                        borderColor: "rgba(89, 105, 255,1)",
                        borderWidth: 2

                    }, {
                        label: 'Lợi nhuận',
                        data: arrLoiNhuan,
                        backgroundColor: "rgba(255, 64, 123,.8)",
                        borderColor: "rgba(255, 64, 123,1)",
                        borderWidth: 2


                    }]

                },
                options: {
                    legend: {
                        display: true,

                        position: 'bottom',

                        labels: {
                            fontColor: '#71748d',
                            fontFamily: 'Circular Std Book',
                            fontSize: 14,
                        }
                    },

                    scales: {
                        xAxes: [{
                            ticks: {
                                fontSize: 14,
                                fontFamily: 'Circular Std Book',
                                fontColor: '#71748d',
                            }
                        }],
                        yAxes: [{
                            ticks: {
                                fontSize: 14,
                                fontFamily: 'Circular Std Book',
                                fontColor: '#71748d',
                            }
                        }]
                    }
                }

            });
        },
        error: function (err) {
            console.error('Lỗi khi gọi Ajax:', err);
        }
    });

   

});



// ============================================================== 
// Product Per Category
// ============================================================== 

var chart = c3.generate({
    bindto: "#account",
    color: { pattern: ["#5969ff", "#ff407b", "#25d5f2", "#ffc750", "#00FF00", "#CC0000", "#777777"] },
    data: {
        // iris data from R
        columns: [
           
        ],
        type: 'pie',

    }
});
// Gọi Ajax để lấy dữ liệu mới
$.ajax({
    url: 'api/Category/GetProductPerCategory',
    method: 'GET',
    success: function (data) {
        if (data.success) {
            // Xử lý dữ liệu từ máy chủ, ví dụ: data = { 'Cate1': 50, 'Cate2': 30, 'Cate3': 20 }
            // Cập nhật dữ liệu của biểu đồ
            var newData = [];
            for (var i = 0; i < data.listC.length; i++) {
                newData.push([data.listC[i].categoryName, data.listC[i].productCount]);
            }

            chart.load({
                columns: newData
            });
        }
    },
    error: function (err) {
        console.error('Lỗi khi gọi Ajax:', err);
    }
});

setTimeout(function () {
    chart.load({

    });
}, 1500);

setTimeout(function () {
    chart.unload({
        ids: 'data1'
    });
    chart.unload({
        ids: 'data2'
    });
},
    2500
);

// ============================================================== 





// ============================================================== 
// Revenue Cards
// ============================================================== 
$("#sparkline-revenue").sparkline([5, 5, 7, 7, 9, 5, 3, 5, 2, 4, 6, 7], {
    type: 'line',
    width: '99.5%',
    height: '100',
    lineColor: '#5969ff',
    fillColor: '#dbdeff',
    lineWidth: 2,
    spotColor: undefined,
    minSpotColor: undefined,
    maxSpotColor: undefined,
    highlightSpotColor: undefined,
    highlightLineColor: undefined,
    resize: true
});



$("#sparkline-revenue2").sparkline([3, 7, 6, 4, 5, 4, 3, 5, 5, 2, 3, 1], {
    type: 'line',
    width: '99.5%',
    height: '100',
    lineColor: '#ff407b',
    fillColor: '#ffdbe6',
    lineWidth: 2,
    spotColor: undefined,
    minSpotColor: undefined,
    maxSpotColor: undefined,
    highlightSpotColor: undefined,
    highlightLineColor: undefined,
    resize: true
});



$("#sparkline-revenue3").sparkline([5, 3, 4, 6, 5, 7, 9, 4, 3, 5, 6, 1], {
    type: 'line',
    width: '99.5%',
    height: '100',
    lineColor: '#25d5f2',
    fillColor: '#dffaff',
    lineWidth: 2,
    spotColor: undefined,
    minSpotColor: undefined,
    maxSpotColor: undefined,
    highlightSpotColor: undefined,
    highlightLineColor: undefined,
    resize: true
});



$("#sparkline-revenue4").sparkline([6, 5, 3, 4, 2, 5, 3, 8, 6, 4, 5, 1], {
    type: 'line',
    width: '99.5%',
    height: '100',
    lineColor: '#fec957',
    fillColor: '#fff2d5',
    lineWidth: 2,
    spotColor: undefined,
    minSpotColor: undefined,
    maxSpotColor: undefined,
    highlightSpotColor: undefined,
    highlightLineColor: undefined,
    resize: true,
});



































