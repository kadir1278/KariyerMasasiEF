
async function GetGeneralData() {
    var searchText = document.getElementById("search").value;
    var url = '/GetGeneralData/' + searchText;
    $('#example').html("");
    var thead =
        '<thead class="thead-primary"><tr>' +
        '<th class="sorting_asc">Dil</th>' +
        '<th class="sorting_asc">Çalışma Alanı</th>' +
        '<th class="sorting_asc">Özel Yetenek</th>' +
        '</tr></thead>';
    $('#example').append(thead);

    $.getJSON(url, function (data) {
        for (item in data.Result) {
            var deger =
                '<tbody><tr role="row">' +
                '<div class="d-flex align-items-center">' +
                '<td>' + data.Result[item].Lang + '</td>' +
                '<td>' + data.Result[item].Business + '</td>' +
                '<td>' + data.Result[item].Special + '</td>' +
                '<td><div class="d-flex">' +
                '<a id="btnDetail" href="#" class="btn btn-primary shadow btn-xs sharp mr-1" data-id="' + data.Result[item].LangID + '"><i class="fa fa-pencil"></i></a>' +
                '<a id="btnDelete" href="#" class="btn btn-danger shadow btn-xs sharp" data-id="' + data.Result[item].LangID + '"><i class="fa fa-trash"></i></a>' +
                '</div></td>' +
                '</tr></tbody> '
            $('#example').append(deger);
        };

    })
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
                    GetGeneralData();
                },
            })
        }
    })
})
$(document).on("click", ("#refresh"), function () {
    GetGeneralData();
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
$(document).ready(GetGeneralData());