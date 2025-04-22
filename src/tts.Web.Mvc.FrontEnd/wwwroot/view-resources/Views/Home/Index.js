$(document).ready(function () {
    // Xử lý bật/tắt cho nút chọn vị trí
    $('#address-form').on('click', function () {
        $('.address-form').css('display', 'flex');
    });

    $('#address-close').on('click', function () {
        $('.address-form').css('display', 'none');
    });

    // Xử lý slider
    let currentIndex = 0;
    const $slides = $('.slide');

    function showSlide(index) {
        $slides.removeClass('active');
        $slides.eq(index).addClass('active');
    }

    function nextSlide() {
        currentIndex = (currentIndex + 1) % $slides.length;
        showSlide(currentIndex);
    }

    showSlide(currentIndex);
    setInterval(nextSlide, 3000); // Chuyển ảnh mỗi 3 giây

    //// Đồng hồ đếm ngược khuyến mãi
    //let time = 300; // 5 phút = 300 giây
    //const $timerDisplay = $('#timer');

    //const countdown = setInterval(() => {
    //    const minutes = Math.floor(time / 60);
    //    const seconds = time % 60;
    //    $timerDisplay.text(`${String(minutes).padStart(2, '0')}:${String(seconds).padStart(2, '0')}`);

    //    if (time <= 0) {
    //        clearInterval(countdown);
    //        alert("Khuyến mại đã kết thúc!");
    //    }
    //    time--;
    //}, 1000);

    // Slider ảnh (commented code - optional to activate)
    /*
    const images = [
        ['../image/banner-middle.png', '../image/banner-middle1.png'],
        ['../image/banner-middle2.png', '../image/banner-middle3.png'],
        ['../image/banner-middle4.png', '../image/banner-middle5.png']
    ];

    let imgIndex = 0;

    const $img1 = $('#image1');
    const $img2 = $('#image2');

    function changeImages() {
        imgIndex = (imgIndex + 1) % images.length;

        $img1.css('transform', 'translateX(-250%)');
        $img2.css('transform', 'translateX(-250%)');

        setTimeout(() => {
            $img1.attr('src', images[imgIndex][0]);
            $img2.attr('src', images[imgIndex][1]);

            $img1.css('transition', 'none');
            $img2.css('transition', 'none');

            $img1.css('transform', 'translateX(0)');
            $img2.css('transform', 'translateX(0)');

            setTimeout(() => {
                $img1.css('transition', 'transform 1s ease');
                $img2.css('transition', 'transform 1s ease');
            }, 0);
        }, 1000);
    }

    setInterval(changeImages, 2000);
    */
});

$(function () {
    // Function to load products
    function loadProducts() {
        $.ajax({
            url: '/api/services/app/Product/GetProductPaged',
            method: 'GET',
            data: {
                MaxResultCount: 5,
                SkipCount: 0,
                Sorting: "creationTime desc"
            },
            success: function(response) {
                if (response.success && response.result && response.result.items) {
                    displayProducts(response.result.items);
                } else {
                    console.error('Invalid response format:', response);
                }
            },
            error: function(xhr, status, error) {
                console.error('Error loading products:', error);
            }
        });
    }

    // Function to display products
    function displayProducts(products) {
        var container = $('.display-products-Fsale');
        container.empty();

        products.forEach(function(product) {
            var productHtml = `
                <div class="item-product " data-product-id="${product.id}">
                    <div class="picture-run">
                        <img class="item-img" src="${product.imageUrl || '/img/default-product.png'}" alt="${product.name}">
                    </div>
                    <h3>${product.name}</h3>
                    <div>
                        <strong class="price">
                            ${product.price.toLocaleString('vi-VN')}₫
                            ${product.discount > 0 ? `
                                <span class="price-and-discount">
                                    <label class="price-old">${(product.price * (1 + product.discount/100)).toLocaleString('vi-VN')}₫</label>
                                    <small>-${product.discount}%</small>
                                </span>
                            ` : ''}
                        </strong>
                    </div>
                    <div class="item-bnt">
                        <button class="buy-now-btn">Mua ngay</button>
                    </div>
                </div>
            `;
            container.append(productHtml);
        });

        // Add click event for buy now buttons
        $('.buy-now-btn').click(function() {
            var productId = $(this).closest('.item-product').data('product-id');
            window.location.href = '/Home/Viewdetails?id=' + productId;
        });
    }

    // Load products when page loads
    $(document).ready(function() {
        loadProducts();
    });
});
    