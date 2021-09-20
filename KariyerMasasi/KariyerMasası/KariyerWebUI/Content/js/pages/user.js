
async function GetUserData() {
    var searchText = document.getElementById("search").value;
    var url = '/GetUserData/' + searchText;
    $('#example').html("");
    var thead =
        '<thead class="thead-primary"><tr>' +
        '<th class="sorting_asc">İsim</th>' +
        '<th class="sorting_asc">Soyisim</th>' +
        '<th class="sorting_asc">E-Mail</th>' +
        '<th class="sorting_asc">Telefon</th>' +
        '<th class="sorting_asc">İşlemler</th>' +
        '</tr></thead>';
    $('#example').append(thead);

    $.getJSON(url, function (data) {
        for (item in data.Result) {
            var deger =
                '<tbody><tr role="row">' +
                '<div class="d-flex align-items-center">' +
                '<td>'+
                '<img src="' + data.Result[item].Photo+'" class="rounded-lg mr-2" width="24" alt="">' +
                '<span class="w-space-no">' + data.Result[item].Name + '</span></div> ' +
                '</td>'+
                '<td>' + data.Result[item].Surname + '</td>' +
                '<td>' + data.Result[item].EMail + '</td>' +
                '<td>' + data.Result[item].Phone + '</td>' +
                '<td><div class="d-flex">' +
                '<a id="btnDetail" href="#" class="btn btn-primary shadow btn-xs sharp mr-1" data-id="' + data.Result[item].ID + '"><i class="fa fa-pencil"></i></a>' +
                '<a id="btnDelete" href="#" class="btn btn-danger shadow btn-xs sharp" data-id="' + data.Result[item].ID + '"><i class="fa fa-trash"></i></a>' +
                '</div></td>' +
                '</tr></tbody> '
            $('#example').append(deger);
        };

    })
}
function upload_user_photo() {
    var fileinput = document.createElement('input')
    var input = $(fileinput);
    input.attr("type", "file");
    input.attr("accept", "image/*");
    fileinput.addEventListener("change", function (event) {
        var reader = new FileReader();
        reader.readAsDataURL(fileinput.files[0]);
        reader.onload = function (e) {
            $('#img_user_photo').attr('src', e.target.result);
            $('#img_user_photo').removeAttr('hidden');
            $('#Photo').val(e.target.result);
        }
    });
    input.trigger('click');
}
function update_user_photo() {
    var fileinput = document.createElement('input')
    var input = $(fileinput);
    input.attr("type", "file");
    input.attr("accept", "image/*");
    fileinput.addEventListener("change", function (event) {
        var reader = new FileReader();
        reader.readAsDataURL(fileinput.files[0]);
        reader.onload = function (e) {
            $('#img_update_user_photo').attr('src', e.target.result);
            $('#img_update_user_photo').removeAttr('hidden');
            $('#Photo').val(e.target.result);
        }
    });
    input.trigger('click');
} 
$("#example").on("click", "#btnDelete", function () {
    var btn = $(this);
    bootbox.confirm("Silmek istediğinize emin misiniz ?", function (result) {
        if (result) {
            var ID = btn.data("id");
            $.ajax({
                type: "GET",
                url: "/kullanici-sil/" + ID,
                success: function () {
                    btn.parent().parent().parent().remove();
                    GetUserData();
                },
            })
        }
    })
})
$(document).on("click", ("#refresh"), function () {
    GetUserData();
})
$(document).on("click", ("#clear"), function () {
    $('#example').html("");
})
$(document).on("click", ("#btnDataAdd"), function () {
    init_custom_form_submit();
})
$(document).on("click", ("#btnDetail"), function () {
    var btn = $(this);
    var ID = btn.data("id");

    $("#modalContent").html('');
    $('#detailModal').remove();

    $.ajax({
        url: "/PartialUpdateUser/" + ID,
        type: "GET",
    })
        .done(function (partialViewResult) {
            $("#modalContent").html(partialViewResult);
            $('#detailModal').modal('show');
            init_custom_form_submit();
        });
})
$(document).ready(GetUserData());