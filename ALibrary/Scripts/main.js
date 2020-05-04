$(document).ready(function () {

    $(".chosen-select").chosen();

    tinymce.init({ selector: 'textarea' });

    function readURLMulti(input, index) {

        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('.multi-image').eq(index).attr('src', e.target.result);
            };

            reader.readAsDataURL(input.files[0]);
        }
    }

    $('.multi').on('change', function () {
        console.log($(this).parent(".multi-group").index());
        readURLMulti(this, $(this).parent(".multi-group").index());

        var el = $(this).parent(".multi-group").clone(true).appendTo(".multi-images");
        el.children(".multi-image").attr('src', "");
        el.children(".delete-image").attr('data-href', "");
        el.children(".multi").prop('value', null);
    });

    $(".delete-image").click(function () {
        var url = $(this).attr("data-href");
        if (url != "") {
            console.log("not empty");
            document.location.href = url;
        } else {
            console.log("empty");
            $(this).parent(".multi-group").detach();
        }
    });
});