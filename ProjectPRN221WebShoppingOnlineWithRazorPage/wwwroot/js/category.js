var dataTable;

$(document).ready(function () {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/Category",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "25%" },
            { "data": "description", "width": "15%" },
            {
                "data": "icon",
                "render": function (data, type, row) {
                    if (type === 'display') {
                        return `<img src="${data}" alt="Category Icon" style="max-width: 50px; max-height: 50px;" />`;
                    }
                    return data; // Trả về URL hình ảnh cho các mục dữ liệu khác
                },
                "width": "15%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="btn-group w-75">  
                        <a href="/Admin/Categories/UpSert?id=${data}" class="btn btn-sm btn-success text-white mx-1">  <i class="bi bi-pencil-square"></i> Update</a> 
                        <a href="javascript:void(0);" onClick="Delete('/api/Category/' + ${data})" class="btn btn-sm btn-danger text-white mx-1"> <i class="bi bi-trash3-fill"> Delete</i>
                    </div>`
                },
                "width": "15%"
            },
        ],
        "width": "100%"
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