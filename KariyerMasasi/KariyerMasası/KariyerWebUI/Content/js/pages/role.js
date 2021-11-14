async function GetData() {
    var searchText = document.getElementById("search").value;
    var url = '/yetki-getir/' + searchText;
    $('#example').html("");
    var thead =
        '<thead class="thead-primary"><tr>' +
        '<th class="sorting_asc">Sistem Yetkileri</th>' +
        '</tr></thead>';
    $('#example').append(thead);

    $.getJSON(url, function (data) {
        console.log(data)
        for (item in data.Result) {
            var tableData =
                '<tbody><tr role="row"><div class="d-flex align-items-center">' +
                '<td>' + data.Result[item].Name + '</td>' +
                '<td><div class="d-flex">' +
                //'<a id="btnDelete" style="color:white" class="btn btn-danger shadow btn-xs sharp" data-id="' + data.Result[item].ID + '"><i class="fa fa-trash"></i></a>' +
                '</div></td></tr></tbody> '
            $('#example').append(tableData);
        };

    })
}
$("#role-add-form").submit(function (e) {
    e.preventDefault();
    var btnClose = document.getElementById("closeUser");
    var user = {
        Name: $("#role-add-form").find('[name="AddName"]').val(),
    }
    console.log(user);
    $.ajax({
        type: "POST",
        url: "/yetki-ekle",
        data: user,
        success: function () {
            bootbox.alert("Veri Eklendi");
            btnClose.click();
            document.getElementsByName("AddName")[0].value = ""
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
$("#example").on("click", "#btnDelete", function () {
    var btn = $(this);
    bootbox.confirm("Silmek istediğinize emin misiniz ?", function (result) {
        if (result) {
            var ID = btn.data("id");
            $.ajax({
                type: "GET",
                url: "/yetki-sil/" + ID,
                success: function () {
                    bootbox.alert("Sistem yetki silme işlemi başarılı.")
                    GetData();
                },
                error: function () {
                    bootbox.alert("Sistem yetki silinemedi lütfen tekrar deneyin. Sorunun devam etmesi durumunda 360MEKA ile irtibat kurunuz.");
                }
            })
        }
    })
})


