function succesSubmit() {
    var btn = document.getElementById("closeUser")
    bootbox.alert("Veri Eklendi !", btn.click())
    setTimeout(function () {
        window.location.reload();
    }, 2000);
}
$("#workInfo").on("click", "#btnDelete", function () {
    var btn = $(this);
    bootbox.confirm("Silmek istediğinize emin misiniz ?", function (result) {
        if (result) {
            var ID = btn.data("id");
            $.ajax({
                type: "POST",
                url: "/kullanici-is-sil/" + ID,
                success: function () {
                    setTimeout(function () {
                        window.location.reload();
                    }, 2000);
                },
            })
        }
    })
})
$("#workInfo").on("click", ("#btnDetail"), function () {
    var btn = $(this);
    var ID = btn.data("id");

    $("#workModalContent").html('');
    $('#workEditModal').remove();

    $.ajax({
        url: "/kullanici-isbilgisi-duzenle/" + ID,
        type: "GET",
    })
        .done(function (partialViewResult) {
            $("#workModalContent").html(partialViewResult);
            $('#workEditModal').modal('show');
            $('.mdate').bootstrapMaterialDatePicker({
                format: 'DD.MM.YYYY',
                weekStart: 0,
                time: false
            });
            init_custom_form_submit();
        });
})

$("#educationInfo").on("click", "#btnDelete", function () {
    var btn = $(this);
    bootbox.confirm("Silmek istediğinize emin misiniz ?", function (result) {
        if (result) {
            var ID = btn.data("id");
            $.ajax({
                type: "POST",
                url: "/kullanici-egitim-sil/" + ID,
                success: function () {
                    setTimeout(function () {
                        window.location.reload();
                    }, 2000);
                },
            })
        }
    })
})
$("#educationInfo").on("click", ("#btnDetail"), function () {
    var btn = $(this);
    var ID = btn.data("id");

    $("#educationModalContent").html('');
    $('#educationEditModal').remove();

    $.ajax({
        url: "/kullanici-egitim-duzenle/" + ID,
        type: "GET",
    })
        .done(function (partialViewResult) {
            $("#educationModalContent").html(partialViewResult);
            $('#educationEditModal').modal('show');
            $('.mdate').bootstrapMaterialDatePicker({
                format: 'DD.MM.YYYY',
                weekStart: 0,
                time: false
            });
            init_custom_form_submit();
        });
})
