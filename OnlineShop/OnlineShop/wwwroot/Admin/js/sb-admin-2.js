(function ($) {
    "use strict"; // Start of use strict

    // Toggle the side navigation
    $("#sidebarToggle, #sidebarToggleTop").on('click', function (e) {
        $("body").toggleClass("sidebar-toggled");
        $(".sidebar").toggleClass("toggled");
        if ($(".sidebar").hasClass("toggled")) {
            $('.sidebar .collapse').collapse('hide');
        };
    });

    // Close any open menu accordions when window is resized below 768px
    $(window).resize(function () {
        if ($(window).width() < 768) {
            $('.sidebar .collapse').collapse('hide');
        };
    });

    // Prevent the content wrapper from scrolling when the fixed side navigation hovered over
    $('body.fixed-nav .sidebar').on('mousewheel DOMMouseScroll wheel', function (e) {
        if ($(window).width() > 768) {
            var e0 = e.originalEvent,
                delta = e0.wheelDelta || -e0.detail;
            this.scrollTop += (delta < 0 ? 1 : -1) * 30;
            e.preventDefault();
        }
    });

    // Scroll to top button appear
    $(document).on('scroll', function () {
        var scrollDistance = $(this).scrollTop();
        if (scrollDistance > 100) {
            $('.scroll-to-top').fadeIn();
        } else {
            $('.scroll-to-top').fadeOut();
        }
    });

    // Smooth scrolling using jQuery easing
    $(document).on('click', 'a.scroll-to-top', function (e) {
        var $anchor = $(this);
        $('html, body').stop().animate({
            scrollTop: ($($anchor.attr('href')).offset().top)
        }, 1000, 'easeInOutExpo');
        e.preventDefault();
    });

    $(document).ready(function () { /*Đảm bảo đoạn mã bên trong
                                    chỉ chạy khi Html đã được tải và sẵn sàng*/
        //Các giá trị thông báo
        var successMessage = $('#success-message').val();
        var errorMessage = $('#error-message').val();
        //Url chuyển hướng màn hình
        var redirectUrl = $('#redirect-url').val();
        //Khi có thông báo thành công
        if (successMessage) {
            //Hiển thị thông báo
            Swal.fire({
                icon: 'success', //Biểu tượng thành công
                title: 'Thành công', //Tiêu đề
                text: successMessage, //Thông báo
                timer: 2000, // Thời gian hiển thị popup (ms)
                showConfirmButton: false //Hiển thị nút comfirm không
            }).then((result) => { //Sau khi thông báo
                window.location.href = redirectUrl; //Chuyển hướng Url
            });
        }
        //Khi có thông báo không thành công
        if (errorMessage) {
            //Hiển thị thông báo
            Swal.fire({
                icon: 'error', //Biểu tượng lỗi
                title: 'Không thành công', //Tiêu đề
                text: errorMessage, //Thông báo
                timer: 2000, // Thời gian hiển thị popup (ms)
                showConfirmButton: false //Hiển thị nút confirm không
            });
        }
    });

})(jQuery); // End of use strict
