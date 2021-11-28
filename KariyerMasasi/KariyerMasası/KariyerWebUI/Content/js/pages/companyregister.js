$(document).ready(function () {
    $("#business-area-select").select2({
        placeholder: "Şirketin çalışma alanları",
        tags: true,
        tokenSeperators: ['/', ',', ',', " "]
    })
    $("#department-select").select2({
        placeholder: " Şirket departmanları",
        tags: true,
        tokenSeperators: ['/', ',', ',', " "]
    });
    $("#city-select").select2({
        placeholder: " Şirketin aktif olduğu şehirler",
        tags: true,
        tokenSeperators: ['/', ',', ',', " "],
        createTag: function (params) {
            return undefined;
        }
    });
});