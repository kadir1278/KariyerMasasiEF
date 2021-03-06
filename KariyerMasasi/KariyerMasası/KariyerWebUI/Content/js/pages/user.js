async function GetData() {
    var searchText = document.getElementById("search").value;
    var url = '/kullanici-getir/' + searchText;
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
            var tableData =
                '<tbody><tr role="row"><div class="d-flex align-items-center">' +
                '<td>' + data.Result[item].Name + '</td>' +
                '<td>' + data.Result[item].Surname + '</td>' +
                '<td>' + data.Result[item].EMail + '</td>' +
                '<td>' + data.Result[item].Phone + '</td>' +
                '<td><div class="d-flex">' +
                '<a id="btnDetail" style="color:white" class="btn btn-primary shadow btn-xs sharp mr-1" data-id="' + data.Result[item].ID + '"><i class="fa fa-pencil"></i></a>' +
                '<a id="btnDelete" style="color:white" class="btn btn-danger shadow btn-xs sharp" data-id="' + data.Result[item].ID + '"><i class="fa fa-trash"></i></a>' +
                '</div></td></tr></tbody> '
            $('#example').append(tableData);
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
           // $('#Photo').val(e.target.result);
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
                    bootbox.alert("Kullanıcı silme işlemi başarılı.")
                    GetData();
                },
                error: function () {
                    bootbox.alert("Kullanıcı silinemedi lütfen tekrar deneyin. Sorunun devam etmesi durumunda 360MEKA ile irtibat kurunuz.");
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
        url: "/PartialUpdateUser/" + ID,
        type: "GET",
        success: function (partialViewResult) {
            $("#modalContent").html(partialViewResult);
            $('#detailModal').modal('show');
        },
        error: function () {
            bootbox.alert("Kullanıcı güncelleme modelinde bir problem oluştu. Lütfen sayfayı yenileyip tekrar deneyiniz. Problemin devam etmesi durumunda 360MEKA ile irtibat kurunuz.");
        }

    })
})
$("#user-add-form").submit(function (e) {
    e.preventDefault();
    var btnClose = document.getElementById("closeUser");
    var user = {
        Name: $("#user-add-form").find('[name="AddName"]').val(),
        Surname: $("#user-add-form").find('[name="AddSurname"]').val(),
        Phone: $("#user-add-form").find('[name="AddPhone"]').val(),
        EMail: $("#user-add-form").find('[name="AddEMail"]').val(),
        Password: $("#user-add-form").find('[name="AddPassword"]').val(),
        Country: $("#user-add-form").find('[name="AddCountry"]').val(),
        City: $("#user-add-form").find('[name="AddCity"]').val(),
        Town: $("#user-add-form").find('[name="AddTown"]').val(),
        Address: $("#user-add-form").find('[name="AddAddress"]').val(),
        Type: $("#user-add-form").find('[name="AddType"]').val(),
        Description: $("#user-add-form").find('[name="AddDescription"]').val(),
        Title: $("#user-add-form").find('[name="AddTitle"]').val(),
        MilitaryStatus: $("#user-add-form").find('[name="AddMilitaryStatus"]').val(),
        Gender: $("#user-add-form").find('[name="AddGender"]').val(),
        MarriageStatus: $("#user-add-form").find('[name="AddMarriageStatus"]').val(),
        Hobby: $("#user-add-form").find('[name="AddHobby"]').val(),
        BusinessAreaID: $("#user-add-form").find('[name="AddBusinessAreaID"]').val(),
        Photo: $("#img_user_photo").attr("src")
    }
    $.ajax({
        type: "POST",
        url: "/kullanici-ekle",
        data: user,
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
$(document).ready(GetData());

