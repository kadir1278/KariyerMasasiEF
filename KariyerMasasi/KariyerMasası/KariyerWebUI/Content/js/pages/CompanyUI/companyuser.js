async function GetData() {
    var searchText = document.getElementById("search").value;
    var url = '/sirket-personel-getir/' + searchText;
    $('#example').html("");
    var thead =
        '<thead class="thead-primary"><tr>' +
        '<th class="sorting_asc">İsim</th>' +
        '<th class="sorting_asc">Soyisim</th>' +
        '<th class="sorting_asc">Ünvan</th>' +
        '<th class="sorting_asc">Telefon</th>' +
        '<th class="sorting_asc">E-Mail</th>' + 
        '<th class="sorting_asc">Firma</th>' +
        '<th class="sorting_asc">İşlemler</th>' +
        '</tr></thead>';
    $('#example').append(thead);

    $.getJSON(url, function (data) {
        for (item in data.Result) {
            var deger =
                '<tbody><tr role="row">' +
                '<div class="d-flex align-items-center">' +
                '<td><span class="w-space-no">' + data.Result[item].Name + '</span></div> </td>' +
                '<td>' + data.Result[item].Surname + '</td>' +
                '<td>' + data.Result[item].Title + '</td>' +
                '<td>' + data.Result[item].Phone + '</td>' +
                '<td>' + data.Result[item].EMail + '</td>' +
                '<td>' + data.Result[item].Company + '</td>' +
                '<td><div class="d-flex">' +
                '<a id="btnDetail" style="color:white" class="btn btn-primary shadow btn-xs sharp mr-1" data-id="' + data.Result[item].ID + '"><i class="fa fa-pencil"></i></a>' +
                '<a id="btnDelete" style="color:white" class="btn btn-danger shadow btn-xs sharp" data-id="' + data.Result[item].ID + '"><i class="fa fa-trash"></i></a>' +
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
                url: "/sirket-personel-sil/" + ID,
                success: function () {
                    bootbox.alert("Şirket personeli silme işlemi başarılı.")
                    GetData();
                },
                error: function () {
                    bootbox.alert("Şirket personeli silinemedi lütfen tekrar deneyin. Sorunun devam etmesi durumunda 360MEKA ile irtibat kurunuz.");
                }
            })
        }
    })
})
$(document).on("click", ("#btnDetail"), function () {
    var btn = $(this);
    var ID = btn.data("id");
    $("#modalContent").html('');
    $('#detailModal').remove();

    $.ajax({
        url: "/sirket-kullanici-guncelle/" + ID,
        type: "GET",
    })
        .done(function (partialViewResult) {
            $("#modalContent").html(partialViewResult);
            $('#detailModal').modal('show');
        });
})
$(document).ready(GetData());
$("#companyuser-add-form").submit(function (e) {
    e.preventDefault();
    var btnClose = document.getElementById("CloseCompanyUser");
    var companyuser = {
        Name: $("#companyuser-add-form").find('[name="AddName"]').val(),
        Surname: $("#companyuser-add-form").find('[name="AddSurname"]').val(),
        Title: $("#companyuser-add-form").find('[name="AddTitle"]').val(),
        EMail: $("#companyuser-add-form").find('[name="AddEMail"]').val(),
        Password: $("#companyuser-add-form").find('[name="AddPassword"]').val(),
        Phone: $("#companyuser-add-form").find('[name="AddPhone"]').val(),
      //  CompanyID: $("#companyuser-add-form").find('[name="AddCompanyID"]').val(),
    }
    $.ajax({
        type: "POST",
        url: "/sirket-personel-ekle",
        data: companyuser,
        success: function () {
            bootbox.alert("Veri Eklendi")
            btnClose.click();
        },
        error: function (xhr, ajaxOptions, thrownError) {

            var iframe = document.createElement('iframe');
            $('#errorContent').html(iframe);
            iframe.contentWindow.document.open();
            iframe.contentWindow.document.write(xhr.responseText);
            iframe.contentWindow.document.close();
            iframe.onload = function () {
                console.log(iframe.contentWindow.document.title);
                bootbox.alert(iframe.contentWindow.document.title)
                btnClose.click();
            };
        }
    });
});
