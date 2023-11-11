$(document).ready(function () {
    ShowCount();
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = 1;
        var quantityText = $("#quantity_value").text();
        if (quantityText != '') {
            quantity = parseInt(quantityText);
        }
        $.ajax({
            type: 'POST',
            dataType: 'json',
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            url: '/Carts/Index?handler=AddToCart',
            data: { id: id, quantity: quantity },
            success: function (rs) {
                if (rs.success) {
                    $('#checkout_items').html(rs.data);
                    alert("Thêm thành công vào giỏ hàng");
                }
            }
        });
    });

    $('body').on('click', '.btnDeleteItem', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var conf = confirm('Bạn có muốn xóa sản phẩm này khỏi giỏ hàng không ?');
        if (conf === true) {
            $.ajax({
                url: '/Carts/Index?handler=DeleteItem',
                type: 'Get',
                data: { id: id },
                success: function (rs) {
                    if (rs.success) {
                        // alert(rs.msg);
                        $('#checkout_items').html(rs.data);
                        $('#trow_' + id).remove();
                        window.location.reload();
                    }
                }
            });
        }

    });

    $('body').on('click', '.btnDeleteAllItem', function (e) {
        e.preventDefault();
        var conf = confirm('Bạn có chắc muốn xóa tất cả sản phẩm trong giỏ hàng chứ');

        if (conf === true) {
            $.ajax({
                url: '/Carts/Index?handler=DeleteAllItem',
                type: 'Get',
                success: function (rs) {
                    if (rs.success) {
                        // xóa hết những bản ghi trong cart -> chỉ cần gọi hàm load lại cart 
                        $('#checkout_items').html(0);
                        window.location.reload();
                    }
                }
            });
        }
    });



    $('body').on('click', '.btnUpdateItem', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = $('#Quantity_' + id).val();
        $.ajax({
            url: '/Carts/Index?handler=UpdateQuantityOfItem',
            type: 'Get',
            data: { id: id, quantity: quantity },
            success: function (rs) {
                if (rs.success) {
                    window.location.reload();
                }
            }
        });
    });


});