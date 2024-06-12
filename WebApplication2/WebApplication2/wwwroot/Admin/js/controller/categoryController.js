var category = {
    //Khởi tạo và thiết lập sự kiện
    init: function () {
        category.registerEvent();
    },
    registerEvent: function () {
        //Chọn các phần tử của class btn-active
        //Huỷ bỏ tất cả các sự kiện click trước đó
        //Đăng ký sự kiện click mới
        $('.btn-active').off('click').on('click', function (e) {
            //Ngăn chặn hành động mặc đinh của thẻ a
            e.preventDefault();
            var btn = $(this);
            //Lấy giá trị của data-id
            var id = btn.data('id');
            $.ajax({
                url: "Category/ChangeStatus", //Url thay đổi trạng thái
                data: { id: id }, //Dữ liệu gửi đến
                dataType: "json",//Định dạng dữ liệu là JSON
                type: "POST",//Loại HTTP method để gửi yêu cầu
                //Hàm callback được gọi khi Ajax thành công
                success: function (response) {
                    if (response.status == true) {
                        btn.text('Active');
                    } else {
                        btn.text('Blocked');
                    }
                }
            });
        });
    }
}
//Đảm bảo mã JavaScript chỉ được thực thi sau khi toàn bộ tài liệu HTML đã được tải
$(document).ready(function () {
    category.init();
});
