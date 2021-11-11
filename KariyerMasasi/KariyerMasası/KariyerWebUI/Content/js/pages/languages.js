async function GetData() {
    var searchText = document.getElementById("search").value;
    var url = '/dil-getir/' + searchText;
    $('#example').html("");
    var thead =
        '<thead class="thead-primary"><tr>' +
        '<th class="sorting_asc">Sistem Dili</th>' +
        '</tr></thead>';
    $('#example').append(thead);

    $.getJSON(url, function (data) {
        console.log(data)
        for (item in data.Result) {
            var tableData =
                '<tbody><tr role="row"><div class="d-flex align-items-center">' +
                '<td>' + data.Result[item].Name + '</td>' +
                '<td><div class="d-flex">' +
                //'<a id="btnDetail" style="color:white" class="btn btn-primary shadow btn-xs sharp mr-1" data-id="' + data.Result[item].ID + '"><i class="fa fa-pencil"></i></a>' +
                '<a id="btnDelete" style="color:white" class="btn btn-danger shadow btn-xs sharp" data-id="' + data.Result[item].ID + '"><i class="fa fa-trash"></i></a>' +
                '</div></td></tr></tbody> '
            $('#example').append(tableData);
        };

    })
}
$("#addBtn").click(function () {
    var btnClose = document.getElementById("closeUser");
    var user = {
        Name: $("#lang-add-form").find('[name="AddName"]').val(),
    }
    console.log(user);
    $.ajax({
        type: "POST",
        url: "/dil-ekle",
        data: user,
        success: function () {
            bootbox.alert("Veri Eklendi");
            btnClose.click();
            document.getElementsByName("AddName")[0].value = ""
        },
        error: function () {
            bootbox.alert("Eksik bilgileri doldurunuz")
            btnClose.click();
        }
    });
});
$(document).ready(GetData());
$("#example").on("click", "#btnDelete", function () {
    var btn = $(this);
    bootbox.confirm("Silmek istediğinize emin misiniz ?", function (result) {
        if (result) {
            var ID = btn.data("id");
            $.ajax({
                type: "GET",
                url: "/dil-sil/" + ID,
                success: function () {
                    bootbox.alert("Dil silme işlemi başarılı.")
                    GetData();
                },
                error: function () {
                    bootbox.alert("Dil silinemedi lütfen tekrar deneyin. Sorunun devam etmesi durumunda 360MEKA ile irtibat kurunuz.");
                }
            })
        }
    })
})


