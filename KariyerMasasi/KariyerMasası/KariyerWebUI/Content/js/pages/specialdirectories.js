async function GetSpecialData() {
    var url = '/ozel-durum-getir';
    $('#example').html("");
    var thead =
        '<thead class="thead-primary"><tr>' +
        '<th class="sorting_asc">Kullanıcı Adı</th>' +
        '<th class="sorting_asc">Özel Durum</th>' +
        '</tr></thead>';
    $('#example').append(thead);

    $.getJSON(url, function (data) {
        console.log(data)
        for (item in data.Result) {
            var tableData =
                '<tbody><tr role="row"><div class="d-flex align-items-center">' +
                '<td>' + data.Result[item].User + '</td>' +
                '<td>' + data.Result[item].SpecialType + '</td>' +
                '<td><div class="d-flex">' +
                '<a id="btnDelete" style="color:white" class="btn btn-danger shadow btn-xs sharp" data-id="' + data.Result[item].ID + '"><i class="fa fa-trash"></i></a>' +
                '</div></td></tr></tbody> '
            $('#example').append(tableData);
        };

    })
}
$(document).ready(GetSpecialData());

$("#example").on("click", "#btnDelete", function () {
    var btn = $(this);
    bootbox.confirm("Silmek istediğinize emin misiniz ?", function (result) {
        if (result) {
            var ID = btn.data("id");
            $.ajax({
                type: "GET",
                url: "/ozel-durum-sil/" + ID,
                success: function () {
                    bootbox.alert("Özel durum silme işlemi başarılı.")
                    GetSpecialData();
                },
                error: function () {
                    bootbox.alert("Özel durum silinemedi lütfen tekrar deneyin. Sorunun devam etmesi durumunda 360MEKA ile irtibat kurunuz.");
                }
            })
        }
    })
})

$("#special-add-form").submit(function (e) {
    e.preventDefault();
    var btnClose = document.getElementById("closeUser");
    var user = {
        UserID: $("#special-add-form").find('[name="AddUserID"]').val(),
        UserSpecialTypeID: $("#special-add-form").find('[name="AddUserSpecialTypeID"]').val(),

    }
    $.ajax({
        type: "POST",
        url: "/ozel-durum-ekle",
        data: user,
        success: function () {
            bootbox.alert("Veri Eklendi")
            GetSpecialData();
            btnClose.click();
        },
        error: function () {
            bootbox.alert("Eksik bilgileri doldurunuz")
        }
    });
});
