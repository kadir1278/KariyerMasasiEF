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