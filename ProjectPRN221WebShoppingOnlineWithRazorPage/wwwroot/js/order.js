var dataTable;

$(document).ready(function () {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/Order",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "code", "width": "15%" },
            { "data": "customerName", "width": "15%" },
            { "data": "orderTotal", "width": "5%" },
            { "data": "shipAddress", "width": "35%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "status", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="btn-group w-75">  
                        <a href="/Admin/Orders/Detail?id=${data}" class="btn btn-sm btn-info text-white mx-1">  <i class="bi bi-pencil-square"></i> Detail</a> 
                        <a href="javascript:void(0);" data-id=${data} class=" btn btn-sm btn-warning  btnUpdateStatus text-white mx-1"> <i> Update Status</i>
                    </div>`
                },
                "width": "15%"
            },
        ],
        "width": "100%"
    });


    $('body').on('click', '.btnUpdateStatus', function () {
        //lấy ra được ID của đơn hàng
        var id = $(this).data('id');
        // gắn nó vào giá trị của thẻ input ở modal
        $('#txtOrderId').val(id);
        // hiện ra modal
        $('#modal-default').modal('show');
    });

    //$('body').on('click', '#btnLuu', function () {
    //    var id = $('#txtOrderId').val();
    //    var tt = $('#ddTrangThai').val();
    //    $.ajax({
    //        url: '/admin/order/UpdateTT',
    //        type: 'Post',
    //        data: { id: id, TrangThai: tt },
    //        success: function (rs) {
    //            if (rs.Success) {
    //                location.reload();
    //            }
    //        }
    //    });
    //});



});




