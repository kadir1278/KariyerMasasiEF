$(document).ready(function () {

    var ozelDurumContent = '<div class="col-md-12" id="ozelDurum"><div class="form-group">'
        + '<div class="form-group">'
        + '<select class="form-control" name="disabled">'
        + '<option value="Görme Engelli">Görme Engelli</option>  '
        + ' </select>'
        + '     </div>'
        + '</div>';
    $("#type-select").on('change', function () {      
        if (this.value != 'Rehber' && !($("#business-area").html() && $("#department").html() && $("#city").html())) {
            $.ajax({
                url: '/kullanici-tip',
                type: 'GET',
                success: function (result) {
                    
                    $("#type").after(result);
                    if ($("#type-select").val() == 'Özel Durum') {
                        $("#disabled").removeAttr('hidden');
                    }
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
                        }
                    });
                }
            });
            
               
            
        }
        else if (this.value == 'Rehber') {
            $("#business-area").remove();
            $("#department").remove()
            $("#city").remove()
        }
    });

});