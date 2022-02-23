$(document).ready(function () {

    if ($(".header-slider").length > 0) {
        var swiper = new Swiper(".header-slider", {
            spaceBetween: 120,
            lazy: true,
            cssMode: true,
            speed: 300,
            pagination: {
                el: ".swiper-pagination",
                clickable: true,
            },
            // autoplay: {
            //   delay: 4000,
            //   disableOnInteraction: false,
            // },
        });
    };

    if ($(".mahsul-detay-slider").length > 0) {
        var swiper = new Swiper(".mahsul-detay-slider", {
            effect: "cards",
            grabCursor: true,
            navigation: {
                nextEl: ".swiper-next",
                prevEl: ".swiper-prev",
            },
        });
    };


    if ($(".home-mahsullar-slider").length > 0) {
        var swiper = new Swiper(".home-mahsullar-slider", {
            spaceBetween: 20,
            slidesPerView: 6,
            loop: true,
            lazy: true,
            cssMode: true,
            navigation: {
                nextEl: ".swiper-next",
                prevEl: ".swiper-prev",
            },

            breakpoints: {
                325: {
                    slidesPerView: 2,
                },
                768: {
                    slidesPerView: 3,
                },
                1024: {
                    slidesPerView: 4,
                },
                1280: {
                    slidesPerView: 5,
                },
                1600: {
                    slidesPerView: 6,
                },
            },
        });
    };



    $(".nav-item").hover(function () {
        $(this).addClass('active')
    });
    $(".nav-item").mouseleave(function () {
        $(this).removeClass("active");
    });

    /// Menü Açılımı
    $(".navicon").on("click", function () {
        $(this).toggleClass("navicon--active");
        $(".navbar-collapse").toggleClass("navbar-collapse--active");
        $("html").toggleClass("overflow");
        $(".overlay-body").toggle();
    });

    // sitedeki tüm dropdownları calıştırma  özelliği

    $(".js-down > .lang-item").click(function () {
        $(this).parent().parent().prev().find("span").text($(this).text());
    });

    //dropdown ok toggle yönü
    $(".dropdown > .btn").on("show.bs.dropdown", function () {
        $(this).children(".bi-chevron-down").toggleClass("rotate-90");
    });

    $(".dropdown > .btn").on("hidden.bs.dropdown", function () {
        $(".bi-chevron-down").removeClass("rotate-90");
    });



    var partnerimage = function (files) {
        var fileSize = files.files[0].size;
        var filename = files.files[0].name;
        var reader = new FileReader();
        // let random = Math.floor((Math.random() * 10000) + 1);
        var fSExt = new Array("Byte", "kb", "mb"),
            i = 0;
        while (fileSize > 900) {
            fileSize /= 1024;
            i++;
        }
        var exactSize = Math.round(fileSize * 100) / 100 + " " + fSExt[i];
        var formData = new FormData();
        formData.append("file", files.files[0]);
        reader.onload = function (e) {
            var editor = `
<div class="col">
<div class="file-info">
<div class="file-left">
<div class="file-text">
   <p class="file-name">${filename}</p>
   <p class="file-boyut">${exactSize}</p>
</div>
   </div>
  <button type="button" class="btn btn-delete deletefile " data-file="1">
  <i class="bi bi-trash-fill"></i>
 </button>
 <input type="hidden" name="files" value="1">
</div>
</div>
`;
            setTimeout(function () {
                $(files).parent().parent().find(".file-main").append(editor);
            }, 1500);
        };
        reader.readAsDataURL(files.files[0]);
    };

    /* resim change olayı  yukardaki function gönderme */
    $(document).on("change", ".photo-file-upload", function () {
        var url = $(this).val();
        var images = this.files[0];
        var sizeInMb = images.size / 1024;
        var sizeLimit = 1024 * 10; // 10 MB
        let count = $(".file-main").find(".file-info").length;
        // büyük size kontrolcüsü
        if ((sizeInMb < sizeLimit)) {
            if ((count <= 9)) {
                partnerimage(this);
                $(this).parent().parent().addClass("active");
                $('.custom-file-upload').addClass('disableds')
                setTimeout(function () {
                    $(".upload-box").removeClass("active");
                    $('.custom-file-upload').removeClass('disableds')
                }, 1000);
            } else {
                alert('Dosya sınırı ulaşıldı');
                return;
            }
        } else {
            alert('Dosya Boyutu Büyük');
            return;
        }
    });

    //yüklenilen file silme olayı
    $(document).on('click', '.deletefile', function () {
        $(this).parent().parent().remove();
    });

    $('.btn-more').click(function () {
        var pageSize = 10;
        var pageIndex = 1;
        var controller = $(this).attr('data-controller');
        var action = $(this).attr('data-action');
        $(this).addClass('active');
        $.ajax({
            type: 'POST',
            url: `/${controller}/${action}`,
            data: { "pageindex": pageIndex, "pagesize": pageSize },
            dataType: 'html',
            success: function (xhr) {
                $('#appendFilter').last().append(xhr);
                setTimeout(() => {
                    $(this).removeClass('active')
                }, 3000);
            },
            error: function () {
                alert("Error while retrieving data!");
            }
        });
    });


    $('.alaqa-form').on('submit', function (e) {
        e.preventDefault();
        $(this).find('.btn-send').addClass('active')
        $(this).find('.btn-send').attr('disabled', true);
        setTimeout(() => {
            $(this).find('.btn-send').removeClass('active')
            $(this).find('.btn-send').removeAttr('disabled');
        }, 3000);
    })


    // var myModalEl = document.getElementById('images-modal')
    // myModalEl.addEventListener('hidden.bs.modal', function () {
    // })


});


jQuery("img.svg").each(function () {
    var $img = jQuery(this);
    var imgID = $img.attr("id");
    var imgClass = $img.attr("class");
    var imgURL = $img.attr("src");
    jQuery.get(
        imgURL,
        function (data) {
            var $svg = jQuery(data).find("svg");
            if (typeof imgID !== "undefined") {
                $svg = $svg.attr("id", imgID);
            }
            if (typeof imgClass !== "undefined") {
                $svg = $svg.attr("class", imgClass + " replaced-svg");
            }
            $svg = $svg.removeAttr("xmlns:a");
            if (!$svg.attr("viewBox") && $svg.attr("height") && $svg.attr("width")) {
                $svg.attr(
                    "viewBox",
                    "0 0 " + $svg.attr("height") + " " + $svg.attr("width")
                );
            }
            $img.replaceWith($svg);
        },
        "xml"
    );
});