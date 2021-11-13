async function GetUserRoleData() {
    var url = '/kullanici-yetki-getir';
    $('#example').html("");
    var thead =
        '<thead class="thead-primary"><tr>' +
        '<th class="sorting_asc">Kullanıcı Adı</th>' +
        '<th class="sorting_asc">Sistem Yetkisi</th>' +
        '</tr></thead>';
    $('#example').append(thead);

    $.getJSON(url, function (data) {
        console.log(data)
        for (item in data.Result) {
            var tableData =
                '<tbody><tr role="row"><div class="d-flex align-items-center">' +
                '<td>' + data.Result[item].User + '</td>' +
                '<td>' + data.Result[item].Role + '</td>' +
                '<td><div class="d-flex">' +
                '<a id="btnDelete" style="color:white" class="btn btn-danger shadow btn-xs sharp" data-id="' + data.Result[item].ID + '"><i class="fa fa-trash"></i></a>' +
                '</div></td></tr></tbody> '
            $('#example').append(tableData);
        };

    })
}
$(document).ready(GetUserRoleData());

$("#example").on("click", "#btnDelete", function () {
    var btn = $(this);
    bootbox.confirm("Silmek istediğinize emin misiniz ?", function (result) {
        if (result) {
            var ID = btn.data("id");
            $.ajax({
                type: "GET",
                url: "/kullanici-yetki-sil/" + ID,
                success: function () {
                    bootbox.alert("Kullanıcı yetki silme işlemi başarılı.")
                    GetUserRoleData();
                },
                error: function () {
                    bootbox.alert("Kullanıcı yetki silinemedi lütfen tekrar deneyin. Sorunun devam etmesi durumunda 360MEKA ile irtibat kurunuz.");
                }
            })
        }
    })
})

$("#userrole-add-form").submit(function (e) {
    e.preventDefault();
    var btnClose = document.getElementById("closeUser");
    var user = {
        UserID: $("#userrole-add-form").find('[name="AddUserID"]').val(),
        RoleID: $("#userrole-add-form").find('[name="AddRoleID"]').val(),

    }
    $.ajax({
        type: "POST",
        url: "/kullanici-yetki-ekle",
        data: user,
        success: function () {
            bootbox.alert("Veri Eklendi")
            GetUserRoleData();
            btnClose.click();
        },
        error: function () {
            bootbox.alert("Eksik bilgileri doldurunuz")
        }
    });
});
