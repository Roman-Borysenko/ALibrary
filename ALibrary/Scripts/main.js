$(document).ready(function () {

    $(".chosen-select").chosen();

    function readURL(input) {

        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#image').attr('src', e.target.result);
            };

            reader.readAsDataURL(input.files[0]);
        }
    }

    $('#file').on('change', function () {
        console.log("change");
        readURL(this);
        $(this).clone(true).prop('value', null).appendTo(".images");
    });
});