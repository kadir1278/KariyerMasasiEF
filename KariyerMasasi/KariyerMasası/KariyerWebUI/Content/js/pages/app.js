
function Guid() {
    //function s4() {
    //    return Math.floor((1 + Math.random()) * 0x10000)
    //        .toString(16)
    //        .substring(1);
    //}
    //return s4() + s4() + s4() + s4() + s4() + s4() + s4() + s4();

    var d = new Date().getTime();//Timestamp
    var d2 = (performance && performance.now && (performance.now() * 1000)) || 0;//Time in microseconds since page-load or 0 if unsupported
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16;//random number between 0 and 16
        if (d > 0) {//Use timestamp until depleted
            r = (d + r) % 16 | 0;
            d = Math.floor(d / 16);
        } else {//Use microseconds since page-load if supported
            r = (d2 + r) % 16 | 0;
            d2 = Math.floor(d2 / 16);
        }
        return (c === 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
}

function ajaxError(xhr, ajaxOptions, thrownError) {
    //console.log(xhr);
    //console.log(ajaxOptions);
    //console.log(thrownError);
    //if (xhr.responseJSON != undefined)


    toastr["warning"]("Hata !", xhr.responseJSON != null ? xhr.responseJSON : "İletişim Hatası!");

    //  new PNotify({ title: 'Hata !', text: xhr.responseJSON != null ? xhr.responseJSON : "İletişim Hatası!", type: 'danger' });
    Loading(false);

}
function select2AjaxInit(selector, url, tags, allowClear, placeholder, templateResult) {
    if (tags == undefined)
        tags = false;
    if ($(selector).length > 0) {
        $.each($(selector), function (key, value) {
            $(value).select2({
                tags: tags,
                language: "tr",
                insertTag: tags ? function (data, tag) {
                    if ($(value).attr("uppercase") == "true") {
                        tag.text = $.trim(tag.text).toUpperCase();
                        tag.id = $.trim(tag.id).toUpperCase();
                    }
                    data.unshift(tag);
                } : function () { },
                placeholder: placeholder == undefined || placeholder == null ? $(selector).attr("placeholder") : placeholder,
                allowClear: allowClear,
                dropdownCssClass: '',
                containerCssClass: !$(selector).attr("multiple") ? 'form-control' : '',
                maximumSelectionLength: $(selector).attr("maximumSelectionLength"),
                ajax: {
                    url: url,
                    dataType: 'json',
                    delay: 100,
                    data: function (params) {
                        var data = { term: params.term==null?'':params.term, page: params.page || 1, pageSize: params.pageSize || 25 };
                        $.each($(selector).dataBegins('filter-'), function (key, val) {
                            data[val.key] = val.value;
                        });
                        $.each($(selector).dataBegins('dynamic-filter-'), function (key, val) {
                            data[val.key] = $(val.value).val();
                        });
                        return data;
                    },
                    processResults: function (data, params) {
                        params.page = params.page || 1;
                        params.pageSize = params.pageSize || 25;
                        return {
                            results: data,
                            pagination: {
                                more: (params.page * params.pageSize) < data.totalCount
                            }
                        };
                    },
                    cache: true,
                    //error:ajaxError,
                },
                templateResult: templateResult
            });
            if ($(value).attr("selector-text") || $(value).attr("selector-id")) {
                $(value).on('change', function () {
                    var id = $(this).find('option:selected').val();
                    var text = $(this).find('option:selected').text();
                    if ($(value).attr("selector-text")) {
                        $($(value).attr("selector-text")).val(text);
                    }
                    if ($(value).attr("selector-id")) {
                        $($(value).attr("selector-id")).val(id > 0 ? id : 0);
                    }
                    if (id > 0)
                        $($(value).attr("selector-new")).addClass("hidden");
                    else
                        $($(value).attr("selector-new")).removeClass("hidden");


                });
            }
        });
    }
}
function LoadingCloseAll() {
    //$.each($(".blockUI.blockOverlay"), function (key, value) {
    //    $(value).parent().unblock();
    //});
    $('#preloader').hide();
}
function Loading(show, element) {
    if (show) {
        //if (element != undefined) {
        //    $(element).block({
        //        message: '<i class="icon-spinner4 spinner"></i>',
        //        //timeout: 2000, //unblock after 2 seconds
        //        overlayCSS: {
        //            backgroundColor: '#fff',
        //            opacity: 0.8,
        //            cursor: 'wait'
        //        },
        //        css: {
        //            border: 0,
        //            padding: 0,
        //            backgroundColor: 'transparent'
        //        }
        //    });
        //}
        //else {
        //    $.blockUI({
        //        message: '<i class="icon-spinner4 spinner"></i>',
        //        //timeout: 2000, //unblock after 2 seconds
        //        overlayCSS: {
        //            backgroundColor: '#fff',
        //            opacity: 0.8,
        //            cursor: 'wait'
        //        },
        //        css: {
        //            border: 0,
        //            padding: 0,
        //            backgroundColor: 'transparent'
        //        }
        //    });
        //}

        $('#preloader').show();
    } else {
        //if (element != undefined) {
        //    $(element).unblock();
        //}
        //else {
        //    $.unblockUI();
        //}

        $('#preloader').hide();
    }
}
function datepicker_init() {
    if ($('.datepicker').length>0)
    $('.datepicker').datepicker({
        clearBtn: false,
        format: "dd.mm.yyyy",
        language: 'tr'
    });
}
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
function init_responsive_dropdown() {
    $(document).click(function (event) {
        //hide all our dropdowns
        $('.dropdown-menu[data-parent]').hide();
    });
    $('.table-responsive [data-toggle="dropdown"],.dataTables_scroll [data-toggle="dropdown"],[class^="scroll-"] [data-toggle="dropdown"],[class*=" scroll-"] [data-toggle="dropdown"]').click(function () {
        // if the button is inside a modal
        //if ($('body').hasClass('modal-open')) {
        //    throw new Error("This solution is not working inside a responsive table inside a modal, you need to find out a way to calculate the modal Z-index and add it to the element")
        //    return true;
        //}
        var ts = $(this).attr('data-attachedul');
        $buttonGroup = $(this).parent();
        var ts = $(this).attr('data-attachedul');
        if (ts == null || ts == undefined || ts == '') {
            ts = +new Date;
            $ul = $(this).siblings('ul');
            $ul.attr('data-parent', ts);
            $(this).attr('data-attachedul', ts);
            $(window).resize(function () {
                $ul.css('display', 'none').data('top');
                $buttonGroup.removeClass("show");
            });
        } else {
            $ul = $('[data-parent=' + $(this).attr('data-attachedul') + ']');
        }
        if (!$buttonGroup.hasClass('show')) {
            $ul.css('display', 'none');
            return;
        }
        dropDownFixPosition($(this).parent(), $ul);
        function dropDownFixPosition(button, dropdown) {
            var dropDownTop = button.offset().top + button.outerHeight();
            var dropDownLeft = button.offset().left;
            if ($('body').hasClass('modal-open')) {
                if (dropdown.parents('.modal-md').length > 0) {
                    dropDownTop -= 78;
                    dropDownLeft -= 765;
                }
                else if (dropdown.parents('.modal-lg').length > 0) {
                    dropDownTop -= 78;
                    dropDownLeft -= 624;
                }
            }
            dropdown.css('top', dropDownTop + "px");
            dropdown.css('left', dropDownLeft + "px");
            dropdown.css('position', "absolute");

            dropdown.css('width', dropdown.width());
            dropdown.css('heigt', dropdown.height());
            $(".dropdown-menu").css('display', 'none');
            dropdown.css('display', 'block');
            if ($('body').hasClass('modal-open')) {
                dropdown.appendTo(dropdown.parents('.modal-body'));
            }
            else {
                dropdown.appendTo('body');
            }
        }
    });
}
function init_custom_form_submit() {
    $("[submit-form]").click(function () {
        $this = $(this);
        //if ($($this.attr("submit-form")).valid()) {
            if ($($this.attr("submit-form")).hasAttr("before-submit")) {
                eval($($this.attr("submit-form")).attr("before-submit") + "()");
            }
            var formData = $($this.attr("submit-form")).formData();
            console.log(formData);
            Loading(true);
            $.ajax({
                url: $($this.attr("submit-form")).attr("action"),
                data: formData,
                type: "POST",
                success: function (result) { eval($($this.attr("submit-form")).attr("submit-success") + "('" + JSON.stringify(result).replaceAll("'","\\'") + "')"); },
                error: ajaxError,
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
function initSummernote(selector) {
    $(selector).summernote();
}
$(document).ready(function () {
    if ($('.select2').length > 0)
        $('.select2').select2();

    datepicker_init();

    init_responsive_dropdown();
    init_custom_form_submit();
    validationInit();
    //init_text_editor();

});


function set_ui_language(code) {
    $.ajax({
        url: "/set-culture",
        data: { cultureName: code },
        type: "GET",
        success: function (result) {
            if (result.result) {
                window.location.reload();
            }
        },
        error: ajaxError,
    });
}

function set_service_status() {
    var status = $('#customSwitch11').is(':checked');//Burada header'a eklenen swicthin is checked durumu kontrol edilecek!!!!(Emir ERGENÇ)
    $.ajax({
        url: "/stores/set-service-status",
        data: { status: status },
        type: "GET",
        success: function (result) {
            toastr["success"]("Başarılı", status ? "Hizmet verme durumunuz açıldı!" : "Hizmet verme durumunuz kapatıldı!");
        },
        error: ajaxError,
    });
}

async function GetNotification() {
    var url = '/stores-application/header-notifications';
    $('#notification').html("");
    //var main =
    //    '<div class="media mr-2"><img alt="image" width="50" src="/images/profile/pati.png"></div>' +
    //    '<div class="media-body">' +
    //    '<h6 class="mb-1">Yeni Firma Başvuruları</h6>';
       
    //$('#notification').append(main);

    $.getJSON(url, function (data) {
        $.each(data,function(key,value) {
            var content =
                '<li><div class="timeline-panel"><div class="media mr-2"><img alt="image" width="50" src="/images/profile/pati.png"></div>' +
                '<div class="media-body">' +
                '<h6 class="mb-1">Yeni Firma Başvurusu</h6>' +
                '<small class="d-block">'+value.Name+'</small> </div> </div> </li>';
            $('#notification').append(content);
        });
    })
}
$(document).ready(GetNotification());
