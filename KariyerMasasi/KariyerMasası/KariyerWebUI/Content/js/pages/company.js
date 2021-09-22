
async function GetCompanyData() {
    var searchText = document.getElementById("search").value;
    var url = '/GetCompanyData/' + searchText;
    $('#example').html("");
    var thead =
        '<thead class="thead-primary"><tr>' +
        '<th class="sorting_asc">İsim</th>' +
        '<th class="sorting_asc">Ülke</th>' +
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
                '<td>' +
              //  '<img src="' + data.Result[item].Logo + '" class="rounded-lg mr-2" width="24" alt="">' +
                '<span class="w-space-no">' + data.Result[item].Name + '</span></div> ' +
                '</td>' +
                '<td>' + data.Result[item].Country + '</td>' +
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
$("#example").on("click", "#btnDelete", function () {
    var btn = $(this);
    bootbox.confirm("Silmek istediğinize emin misiniz ?", function (result) {
        if (result) {
            var ID = btn.data("id");
            $.ajax({
                type: "GET",
                url: "/sirket-sil/" + ID,
                success: function () {
                    btn.parent().parent().parent().remove();
                    GetCompanyData();
                },
            })
        }
    })
})
$(document).on("click", ("#refresh"), function () {
    GetCompanyData();
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
        url: "/PartialUpdateCompany/" + ID,
        type: "GET",
    })
        .done(function (partialViewResult) {
            $("#modalContent").html(partialViewResult);
            $('#detailModal').modal('show');
            init_custom_form_submit();
        });
})
$(document).ready(GetCompanyData());