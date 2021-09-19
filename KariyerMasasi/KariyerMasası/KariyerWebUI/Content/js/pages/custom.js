jQuery.fn.extend({
    formData: function () {
        var form = $(this).serializeArray();
        var formData = {};
        for (var i in form) {
            formData[form[i]["name"]] = form[i]["value"];
        }
        if ($(this).find(".summernote").length > 0) {
            $.each($(this).find(".summernote"), function (key, value) {
                formData[$(value).attr("id")] = $(value).summernote('code');
            });
        }
        return formData;
    }
});
$.fn.dataBegins = function (s) {
    var matched = [];
    var input = this;
    input.each(function (index) {
        $.each(this.attributes, function (index, attr) {
            if (attr.name.indexOf("data-" + s) === 0) {
                matched.push({ key: attr.name.replace("data-" + s, ''), value: $(input).data(attr.name.replace("data-", '')) });
            }
        });
    });
    return matched;
};
$.fn.hasAttr = function (s) {
    var attr = $(this).attr(s);
    if (typeof attr !== typeof undefined && attr !== false) {
        return true;
    }
    return false;
};
function init_custom_form_submit() {
    $("[submit-form]").click(function () {
        $this = $(this);
        //if ($($this.attr("submit-form")).valid()) {
        if ($($this.attr("submit-form")).hasAttr("before-submit")) {
            eval($($this.attr("submit-form")).attr("before-submit") + "()");
        }
        var formData = $($this.attr("submit-form")).formData();
        console.log(formData);
        $.ajax({
            url: $($this.attr("submit-form")).attr("action"),
            data: formData,
            type: "POST",
            success: function (result) { eval($($this.attr("submit-form")).attr("submit-success") + "('" + JSON.stringify(result).replaceAll("'", "\\'") + "')"); },
        });
        //}
    });
}
function validationInit() {
    if ($("form").length > 0)
        $("form").each(function (key, value) {
            var form = $(value);
            if (!form.hasAttr("validation-inited")) {
                var error = $('.alert-danger', form);
                var success = $('.alert-success', form);
                var validator = form.validate({
                    lang: "tr",
                    ignore: ".ignore,:hidden:not(#summernote),.note-editable.panel-body",
                    onkeyup: function (element, event) {
                        return false;
                    },
                    highlight: function (element, errorClass) {
                        $(element).parents('.form-group').removeClass('has-feedback').removeClass('has-error');
                        //$(element).parent().children('.form-control-feedback').remove();
                        $(element).removeAttr('data-popup');
                        //$(element).removeAttr('data-original-title');
                        //$('[data-popup="tooltip"]').tooltip();
                    },
                    unhighlight: function (element, errorClass) {
                        $(element).parents('.form-group').removeClass('has-feedback').removeClass('has-error');
                        //$(element).parent().children('.form-control-feedback').remove();
                        //$(element).removeAttr('data-popup');
                        $(element).removeAttr('title');
                        $('[data-popup="tooltip"]').tooltip();

                        //if ($(element).hasClass("success-check") && $(element).val() != "") {
                        //    if ($(element).parent().children('.form-control-feedback').length == 0)
                        //        $(element).parent().append('<div class="form-control-feedback"><i></i></div>');
                        //    $(element).parents('.form-group').addClass('has-feedback')
                        //    $(element).parent().children('.form-control-feedback').children('i').addClass('icon-checkmark-circle text-success');
                        //}
                    },
                    errorPlacement: function (error, element) {
                        element.parents('.form-group').addClass('has-feedback').addClass('has-error');
                        //if (element.parent().children('.form-control-feedback').length == 0)
                        //    element.parent().append('<div class="form-control-feedback"><i></i></div>');
                        //element.parent().children('.form-control-feedback').children('i').addClass('icon-cancel-circle2');
                        //element.parent().children('.select2-container').attr('data-popup', 'tooltip');
                        element.parent().children('.select2-container').attr('title', error.html());
                        //element.attr('data-popup', 'tooltip');
                        element.parents('.form-group').attr('title', error.html());
                        //$('[data-popup="tooltip"]').tooltip();
                    },
                });
                form.data('validator', validator);
                if ($("input[form='" + form.attr("id") + "']").length > 0) {
                    $("input[form='" + form.attr("id") + "']").each(function (inputK, inputV) {
                        validator.element(inputV);
                    });
                }
            }
        });
    if ($("input[data-confirm]").length > 0) {
        $.each($("input[data-confirm]"), function (key, value) {
            $(value).rules("add", {
                equalTo: "#" + $(value).attr('data-confirm'),
            });
        });
    }
    if ($("input[data-remote]").length > 0) {
        $.each($("input[data-remote]"), function (key, value) {
            $(value).rules("add", {
                remote: {
                    url: $(value).attr('data-remote'),
                    type: "POST",
                    async: true,
                    data: $(value).parents('form').data("id") != null && $(value).parents('form').data("id") != undefined ? { id: $(value).parents('form').data("id") || 0, value: function () { return $(value).val(); } } :
                        { value: function () { return $(value).val(); } },
                    error: ajaxError,
                    //complete: function (data) {
                    //    if (data.responseText != "true") {
                    //        $(value).parent().find(".form-control-feedback").remove();
                    //    }
                    //    else {
                    //        $(value).parents(".form-group").addClass("has-feedback");
                    //        $(value).parent().append('<div class="form-control-feedback text-success"><i class="fal fa-check-circle"></i></div>');
                    //    }
                    //}
                }
            });
        });
    }
}



