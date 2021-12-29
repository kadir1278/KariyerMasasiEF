function upload_user_photo() {
    var fileinput = document.createElement('input')
    var input = $(fileinput);
    input.attr("type", "file");
    input.attr("accept", "image/*");
    fileinput.addEventListener("change", function (event) {
        var reader = new FileReader();
        reader.readAsDataURL(fileinput.files[0]);
        reader.onload = function (e) {
            $('#Photo').val(e.target.result);
            if ($("#img_user").attr('src') == null) {
                $("#Photo").after(
                    " <div class='form-group col-md-12'>" +
                    "<img style='width: 250px; height: 250px;' id='img_user'  src=' " + e.target.result + "'  id='img_user_photo' alt=''>" +
                    "</div>"
                );
            }
            else {
                $("#img_user").attr('src', e.target.result)
            }
        }
    });
    input.trigger('click');
}
$(document).ready(function () {
    $("#business-area-select").select2({
        placeholder: "Çalışmak istediğiniz alanlar",
        tags: true,
        tokenSeperators: ['/', ',', ',', " "]
    })
    $("#department-select").select2({
        placeholder: "Çalışmak istediğiniz departmanlar",
        tags: true,
        tokenSeperators: ['/', ',', ',', " "]
    });
    $("#special-select").select2({
        placeholder: "Özel Durum",
        tags: true,
        tokenSeperators: ['/', ',', ',', " "]
    });
    $("#city-select").select2({
        placeholder: "Çalışmak istediğiniz şehirler",
        tags: true,
        tokenSeperators: ['/', ',', ',', " "],
        createTag: function (params) {
            return undefined;
        },
        
    });

    $("#type-select").on('change', function () {
        if (this.value != 'Rehber') {            
            if ($("#type-select").val() == 'Özel Durum') {
                $("#special-type").show();
            }
            else {
                $("#special-type").hide();
            }
            $("#business-area").show();
            $("#department").show();
            $("#city").show();
            $(".select2-container--default").css('width','100%')
            $(".select2-search__field").css('width','547.6px')
            
        }
        else {
            $("#business-area").hide();
            $("#department").hide();
            $("#city").hide();
            $("#special-type").hide();
        }
    });

});

