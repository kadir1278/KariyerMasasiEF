async function GetData() {
    var searchText = document.getElementById("search").value;
    var url = '/sirket-aktif-getir/' + searchText;
    $('#example').html("");
    var thead =
        '<thead class="thead-primary"><tr>' +
        '<th class="sorting_asc">İsim</th>' +
        '<th class="sorting_asc">Ülke</th>' +
        '<th class="sorting_asc">E-Mail</th>' +
        '<th class="sorting_asc">Telefon</th>' +
        '<th class="sorting_asc">İşlemler</th>' +
        '</tr></thead>';
    $('#example').append(thead);

    $.getJSON(url, function (data) {
        for (item in data.Result) {
            var deger =
                '<tbody><tr role="row"><div class="d-flex align-items-center">' +
                '<td><span class="w-space-no">' + data.Result[item].Name + '</span></div></td>' +
                '<td>' + data.Result[item].Country + '</td>' +
                '<td>' + data.Result[item].EMail + '</td>' +
                '<td>' + data.Result[item].Phone + '</td>' +
                '<td><div class="d-flex">' +
                '<a id="btnDetail" style="color:white" class="btn btn-primary shadow btn-xs sharp mr-1" data-id="' + data.Result[item].ID + '"><i class="fa fa-pencil"></i></a>' +
                '<a id="btnDelete" style="color:white" class="btn btn-danger shadow btn-xs sharp" data-id="' + data.Result[item].ID + '"><i class="fa fa-trash"></i></a>' +
                '</div></td>' +
                '</tr></tbody> '
            $('#example').append(deger);
        };

    })
}
$(document).on("click", ("#btnDetail"), function () {
    var btn = $(this);
    var ID = btn.data("id");

    $("#modalContent").html('');
    $('#detailModal').remove();

    $.ajax({
        url: "/PartialUpdateCompany/" + ID,
        type: "GET",
    })
        .done(function (partialViewResult) {
            $("#modalContent").html(partialViewResult);
            $('#detailModal').modal('show');
        });
})
function upload_company_photo() {
    var fileinput = document.createElement('input')
    var input = $(fileinput);
    input.attr("type", "file");
    input.attr("accept", "image/*");
    fileinput.addEventListener("change", function (event) {
        var reader = new FileReader();
        reader.readAsDataURL(fileinput.files[0]);
        reader.onload = function (e) {
            $('#img_company_photo').attr('src', e.target.result);
            $('#img_company_photo').removeAttr('hidden');
        }
    });
    input.trigger('click');
}
function upload_tax_file() {
    var fileinput = document.createElement('input')
    var input = $(fileinput);
    input.attr("type", "file");
    input.attr("accept", "application/pdf");
    fileinput.addEventListener("change", function (event) {
        var reader = new FileReader();
        reader.readAsDataURL(fileinput.files[0]);
        reader.onload = function (e) {
            $('#img_tax_file').attr('src', "/Content/images/pdf.png");
            $('#img_tax_file').attr('alt', e.target.result);
            $('#img_tax_file').removeAttr('hidden');
        }
    });
    input.trigger('click');
}
function update_company_photo() {
    var fileinput = document.createElement('input')
    var input = $(fileinput);
    input.attr("type", "file");
    input.attr("accept", "image/*");
    fileinput.addEventListener("change", function (event) {
        var reader = new FileReader();
        reader.readAsDataURL(fileinput.files[0]);
        reader.onload = function (e) {
            $('#imgu_company_photo').attr('src', e.target.result);
            $('#imgu_company_photo').removeAttr('hidden');
            $('#Logo').val(e.target.result);
        }
    });
    input.trigger('click');
}
function update_tax_file() {
    var fileinput = document.createElement('input')
    var input = $(fileinput);
    input.attr("type", "file");
    input.attr("accept", "application/pdf");
    fileinput.addEventListener("change", function (event) {
        var reader = new FileReader();
        reader.readAsDataURL(fileinput.files[0]);
        reader.onload = function (e) {
            $('#imgu_tax_file').attr('src', "/Content/images/pdf.png");
            $('#imgu_tax_file').attr('alt', e.target.result);
            $('#imgu_tax_file').removeAttr('hidden');
            $('#TaxFile').val(e.target.result);

        }
    });
    input.trigger('click');
}

$("#company-add-form").submit(function (e) {
    e.preventDefault();
    var btnClose = document.getElementById("closeUser");
    var company = {
        Name: $("#company-add-form").find('[name="AddName"]').val(),
        Phone: $("#company-add-form").find('[name="AddPhone"]').val(),
        Fax: $("#company-add-form").find('[name="AddFax"]').val(),
        EMail: $("#company-add-form").find('[name="AddEMail"]').val(),
        Country: $("#company-add-form").find('[name="AddCountry"]').val(),
        City: $("#company-add-form").find('[name="AddCity"]').val(),
        Town: $("#company-add-form").find('[name="AddTown"]').val(),
        Address: $("#company-add-form").find('[name="AddAddress"]').val(),
        TaxNumber: $("#company-add-form").find('[name="AddTaxNumber"]').val(),
        TaxAddress: $("#company-add-form").find('[name="AddTaxAddress"]').val(),
        BusinessAreaID: $("#company-add-form").find('[name="AddBusinessAreaID"]').val(),
        DepartmentID: $("#company-add-form").find('[name="AddDepartmentID"]').val(),
        TaxFile: $("#img_tax_file").attr("alt"),
        Logo: $("#img_company_photo").attr("src"),
    }
    $.ajax({
        type: "POST",
        url: "/sirket-ekle",
        data: company,
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
$("#example").on("click", "#btnDelete", function () {
    var btn = $(this);
    bootbox.confirm("Silmek istediğinize emin misiniz ?", function (result) {
        if (result) {
            var ID = btn.data("id");
            $.ajax({
                type: "GET",
                url: "/sirket-sil/" + ID,
                success: function () {
                    bootbox.alert("Şirket silme işlemi başarılı.")
                    GetData();
                },
                error: function () {
                    bootbox.alert("Şirket silinemedi lütfen tekrar deneyin. Sorunun devam etmesi durumunda 360MEKA ile irtibat kurunuz.");
                }
            })
        }
    })
})
