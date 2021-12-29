async function GetData() {
    var searchText = document.getElementById("search").value;
    var url = '/sirket-pasif-getir/' + searchText;
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
                '<tbody><tr role="row"><div class="d-flex align-items-center">' +
                '<td><span class="w-space-no">' + data.Result[item].Name + '</span></div></td>' +
                '<td>' + data.Result[item].Country + '</td>' +
                '<td>' + data.Result[item].EMail + '</td>' +
                '<td>' + data.Result[item].Phone + '</td>' +
                '<td><div class="d-flex">' +
                '<a id="btnDetail" style="color:white" class="btn btn-primary shadow btn-xs sharp mr-1" data-id="' + data.Result[item].ID + '"><i class="fa fa-pencil"></i></a>' +
                '<a id="btnChangeStatus" style="color:white" class="btn btn-danger shadow btn-xs sharp" data-id="' + data.Result[item].ID + '"><i class="fa fa-check-circle-o"></i></a>' +
                '</div></td>' +
                '</tr></tbody> '
            $('#example').append(deger);
        };

    })
}
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
        });
})
$(document).ready(GetData());

$("#example").on("click", "#btnChangeStatus", function () {
    var btn = $(this);
    var ID = btn.data("id");
    bootbox.confirm("Onaylamak istediğinize emin misiniz ?", function (result) {
        if (result) {
            $.ajax({
                type: "GET",
                url: "/sirket-onayla/" + ID,
                success: function () {
                    bootbox.alert("Şirket onaylama işlemi başarılı.")
                    GetData();
                },
                error: function () {
                    bootbox.alert("Şirket onaylanmadı lütfen tekrar deneyin. Sorunun devam etmesi durumunda 360MEKA ile irtibat kurunuz.");
                }
            })
        }
    })
})
