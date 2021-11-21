$("#seminar-add-form").submit(function (e) {
    e.preventDefault();
    var btnClose = document.getElementById("closeUser");
    var seminar = {
        Name: $("#seminar-add-form").find('[name="SeminarName"]').val(),
        Date: $("#seminar-add-form").find('[name="SeminarDate"]').val(),
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
$("#Reference-add-form").submit(function (e) {
    e.preventDefault();
    var btnClose = document.getElementById("closeUser");
    var seminar = {
        NameSurname: $("#Reference-add-form").find('[name="ReferenceNameSurname"]').val(),
        Phone: $("#Reference-add-form").find('[name="ReferencePhone"]').val(),
        EMail: $("#Reference-add-form").find('[name="ReferenceEMail"]').val(),
    }
    $.ajax({
        type: "POST",
        url: "/referans-ekle",
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

$(document).on("click", ("#btnSeminarDetail"), function () {
    var btn = $(this);
    var ID = btn.data("id");
    $("#ModalContent").html('');
    $('#detailSeminarModal').remove();

    $.ajax({
        url: "/seminer-guncelle/" + ID,
        type: "GET",
    })
        .done(function (partialViewResult) {
            $("#ModalContent").html(partialViewResult);
            $('#detailSeminarModal').modal('show');
        });
});
$(document).on("click", ("#btnReferenceDetail"), function () {
    var btn = $(this);
    var ID = btn.data("id");
    $("#ModalContent").html('');
    $('#detailReferenceModal').remove();

    $.ajax({
        url: "/referans-guncelle/" + ID,
        type: "GET",
    })
        .done(function (partialViewResult) {
            $("#ModalContent").html(partialViewResult);
            $('#detailReferenceModal').modal('show');
        });
});
