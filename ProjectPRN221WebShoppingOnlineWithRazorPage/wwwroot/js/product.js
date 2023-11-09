var dataTable;

$(document).ready(function () {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/Product",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "title", "width": "25%" },
            { "data": "productCode", "width": "25%" },
            //{ "data": "description", "width": "15%" },
            {
                "data": "image",
                "render": function (data, type, row) {
                    if (type === 'display') {
                        var id = row.id;
                        return `<img src="${data}" id="imgproduct" data-id="${id}" alt="Product Image" style="max-width: 90px; max-height: 90px;" />`;
                    }
                    return data;
                },
                "width": "15%"
            },
            { "data": "price", "width": "25%" },
            { "data": "quantity", "width": "25%" },
            {
                "data": "isActive",
                "render": function (data) {
                    if (data === true) {
                        return `<i class="bi bi-check-circle text-success"></i>`;
                    } else {
                        return `<i class="bi bi-ban"></i>`;
                    }
                },
                "width": "15%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="btn-group w-75">  
                        <a href="/Admin/Products/Update?id=${data}" class="btn btn-sm btn-success text-white mx-1">  <i class="bi bi-pencil-square"></i> Update</a> 
                        <a href="javascript:void(0);" onClick="Delete('/api/Product/' + ${data})" class="btn btn-sm btn-danger text-white mx-1"> <i class="bi bi-trash3-fill"> Delete</i>

                    </div>`
                },
                "width": "15%"
            },
        ],
        "order": [6,'desc'],
        "width": "100%"
    });
});


$(document).ready(function () {
    $("#dialog").dialog({
        autoOpen: false,
        show: "fade",
        hide: "fade",
        modal: "true",
        height: '650',
        width: '1000',
        resizable: true,
        title: 'Quản lí ảnh sản phẩm',
        close: function () {
            window.location.reload();
        }
    });

    $('body').on("click", "#imgproduct", function () {
        var proid = $(this).attr("data-id");
        $("#dialog #myIframe").attr("src", "/Admin/ProductImages/Index?id=" + proid);
        $('#dialog').dialog('open');
        return false;
    });
});


function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success("Xóa danh mục thành công", "Thông báo");
                    } else {
                        toastr.error("Xóa danh mục thất bại", "Thông báo");
                    }
                }
            })
        }
    })
}