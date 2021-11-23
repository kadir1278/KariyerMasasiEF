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
$("#computer-add-form").submit(function (e) {
    e.preventDefault();
    var btnClose = document.getElementById("closeUser");
    var computer = {
        Name: $("#computer-add-form").find('[name="computerName"]').val(),
        Description: $("#computer-add-form").find('[name="computerDescription"]').val(),
    }
    $.ajax({
        type: "POST",
        url: "/bilgisayar-bilgisi-ekle",
        data: computer,
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
$("#certificate-add-form").submit(function (e) {
    e.preventDefault();
    var btnClose = document.getElementById("closeUser");
    var computer = {
        Name: $("#certificate-add-form").find('[name="CertificateName"]').val(),
        FinishDate: $("#certificate-add-form").find('[name="CertificateFinishDate"]').val(),
        InstitutionFromName: $("#certificate-add-form").find('[name="CertificateInstitutionFromName"]').val(),
        LanguageID: $("#certificate-add-form").find('[name="CertificateLanguageID"]').val(),
        File: $("#img_certificate_photo").attr("src")
    }
    $.ajax({
        type: "POST",
        url: "/sertifika-ekle",
        data: computer,
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
$("#education-add-form").submit(function (e) {
    e.preventDefault();
    var btnClose = document.getElementById("closeUser");
    var education = {
        SchoolName: $("#education-add-form").find('[name="EducationSchoolName"]').val(),
        GraduationYear: $("#education-add-form").find('[name="EducationGraduationYear"]').val(),
        GraduationStatus: $("#education-add-form").find('[name="EducationGraduationStatus"]').val(),
        GraduationGrade: $("#education-add-form").find('[name="EducationGraduationGrade"]').val(),
        Department: $("#education-add-form").find('[name="EducationDepartment"]').val(),
        Description: $("#education-add-form").find('[name="EducationDescription"]').val(),
    }
    $.ajax({
        type: "POST",
        url: "/egitim-ekle",
        data: education,
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
$(document).on("click", ("#btnComputerDetail"), function () {
    var btn = $(this);
    var ID = btn.data("id");
    $("#ModalContent").html('');
    $('#detailComputerModal').remove();

    $.ajax({
        url: "/bilgisayar-bilgisi-guncelle/" + ID,
        type: "GET",
    })
        .done(function (partialViewResult) {
            $("#ModalContent").html(partialViewResult);
            $('#detailComputerModal').modal('show');
        });
});
$(document).on("click", ("#btnCertificateDetail"), function () {
    var btn = $(this);
    var ID = btn.data("id");
    $("#ModalContent").html('');
    $('#detailCertificateModal').remove();

    $.ajax({
        url: "/sertifika-guncelle/" + ID,
        type: "GET",
    })
        .done(function (partialViewResult) {
            $("#ModalContent").html(partialViewResult);
            $('#detailCertificateModal').modal('show');
        });
});
$(document).on("click", ("#btnEducationDetail"), function () {
    var btn = $(this);
    var ID = btn.data("id");
    $("#ModalContent").html('');
    $('#detailEducationModal').remove();

    $.ajax({
        url: "/egitim-guncelle/" + ID,
        type: "GET",
    })
        .done(function (partialViewResult) {
            $("#ModalContent").html(partialViewResult);
            $('#detailEducationModal').modal('show');
        });
});


function upload_certificate_photo() {
    var fileinput = document.createElement('input')
    var input = $(fileinput);
    input.attr("type", "file");
    input.attr("accept", "application/pdf");
    fileinput.addEventListener("change", function (event) {
        var reader = new FileReader();
        reader.readAsDataURL(fileinput.files[0]);
        reader.onload = function (e) {
            $('#img_certificate_photo').attr('src', e.target.result);
            $('#img_certificate_photo').removeAttr('hidden');
            $('#File').val(e.target.result);
        }
    });
    input.trigger('click');
}
function update_certificate_photo() {
    var fileinput = document.createElement('input')
    var input = $(fileinput);
    input.attr("type", "file");
    input.attr("accept", "application/pdf");
    fileinput.addEventListener("change", function (event) {
        var reader = new FileReader();
        reader.readAsDataURL(fileinput.files[0]);
        reader.onload = function (e) {
            $('#img_update_certificate_photo').attr('src', e.target.result);
            $('#img_update_certificate_photo').removeAttr('hidden');
            $('#File').val(e.target.result);
        }
    });
    input.trigger('click');
}
