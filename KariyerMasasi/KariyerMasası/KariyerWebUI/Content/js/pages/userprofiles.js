$("#example").on("click", "#btnDelete", function () {
    var btn = $(this);
    bootbox.confirm("Silmek istediğinize emin misiniz ?", function (result) {
        if (result) {
            var ID = btn.data("id");
            $.ajax({
                type: "GET",
                url: "/yonetici-sil/" + ID,
                success: function () {
                    bootbox.alert("Yönetici silme işlemi başarılı.")
                    GetData();
                },
                error: function () {
                    bootbox.alert("Yönetici silinemedi lütfen tekrar deneyin. Sorunun devam etmesi durumunda 360MEKA ile irtibat kurunuz.");
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
        url: "/PartialUpdateAdmin/" + ID,
        type: "GET",
    })
        .done(function (partialViewResult) {
            $("#modalContent").html(partialViewResult);
            $('#detailModal').modal('show');
            init_custom_form_submit();
        });
});
$("#seminar-add-form").submit(function (e) {
    e.preventDefault();
    var btnClose = document.getElementById("closeUser");
    var seminar = {
        Name: $("#seminar-add-form").find('[name="SeminarName"]').val(),
        SeminarDate: $("#seminar-add-form").find('[name="SeminarDate"]').val(),
        Description: $("#seminar-add-form").find('[name="SeminarDescription"]').val(),
    }
    $.ajax({
        type: "POST",
        url: "/seminer-ekle",
        data: seminar,
        success: function () {
            btnClose.click();
            bootbox.confirm("Veri Eklendi", function () {
                window.location.reload();
            })
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
