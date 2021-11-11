
function custom_data_table() {
    $.each($("[custom-data-table-show]"), function (key, val) {
        var value = $(val).attr("custom-data-table-show");
        if (eval(value))
            $(val).show();
        else
            $(val).hide();
    });
    $.each($("[custom-data-table-hide]"), function (key, val) {
        var value = $(val).attr("custom-data-table-hide");
        if (eval(value))
            $(val).hide();
        else
            $(val).show();
    });
    $.each($("[custom-data-table-remove]"), function (key, val) {
        var value = $(val).attr("custom-data-table-remove");
        if (eval(value))
            $(val).remove();
    });
}
Array.prototype.getJSONValueData = function (key, value) {
    var result = jQuery.grep(this, function (val) { return val[key] == value });
    if (result.length > 0) return result[0];
    return [];
}
var defaults = {
    elementName: null,
    elementClass: "table datatable-basic table-xxs text-nowrap",
    searchElementsClass: "form-control form-control-lg",
    url: null,
    data: null,
    exportUrl: null,
    selectAllUrl: null,
    editUrl: null,
    keyValue: null,
    commandColumn: false,
    commandColumnIndex: 0,
    viewUrl: null,
    viewNewTab: false,
    sameRecUrl: null,
    googleSearchUrl: null,
    deleteFunc: null,
    customActBtnsTemp: null,
    length: 50,
    lengthMenu: [],
    order: [0, "asc"],
    columns: [],
    responsive: false,
    ajaxParams: [],
    beforeCallBack: null,
    endCallBack: null,
    colvis: false,
    //dom: '<"dt-panelmenu clearfix"TlBfr><"table-scrollable"t><"dt-panelfooter clearfix"ip>',
    dom: 'r<"datatable-header"fBl><"datatable-scroll-wrap"t><"datatable-footer"ip>',
    groupFields: [],
    paging: true,
    footer: {},
    rowclickFn: null,
    rowclickUrl: null,
    headerHide: false,
};

var columnDefaults = {
    fieldName: "",
    caption: "",
    JSONContent: null,
    JSONContentKey: "key",
    JSONContentValue: "value",
    JSONContentColor: null,
    filter: false,
    visible: true,
    exportVisible: true,
    sortable: true,
    isCheckBox: false,
    checkBoxClass: "",
    checkBoxSelectAll: false,
    editable: false,
    noVisible: false,
    dataType: 1,
    dateformat: "DD.MM.YYYY HH:mm",
    ObjectKey: null,// "Name",
    ObjectJson: false,
    separator: ' ',
    className: '',
    chkData: null,
    onchange: null,
    customContent: null
};
$.fn.initCustomDataTable = function (settings) {
    var ordInx = 0;
    //var table_key = null;
    var table_key = window.location.pathname + "_" + settings.elementName + "_dt_columns";
    var _defaults = JSON.parse(JSON.stringify(defaults));
    settings = $.extend(_defaults, settings);
    localStorage.setItem(window.location.pathname + "_" + settings.elementName + "_selected", null);
    localStorage.setItem(window.location.pathname + "_" + settings.elementName + "_settings", JSON.stringify(settings));
    var colIndex = 0;
    var thisId = "#" + settings.elementName + "_wrapper";
    var filters = false;
    var editable = false;
    var cachedDt = localStorage.getItem(table_key) != undefined ? JSON.parse(localStorage.getItem(table_key)) : undefined;
    var columns = [];
    var editableColumns = [];
    if (settings.rowclickFn || settings.rowclickUrl)
        settings.elementClass = settings.elementClass + " table-rowclick"
    $(this).append("<table id=" + settings.elementName + " class=\"" + settings.elementClass + "\" width='100%'></table>");
    var thead = document.createElement("thead");
    var filterThead = document.createElement("thead");
    filterThead.className="data-table-filters"
    var headerRow = document.createElement("tr");
    var headerFilterRow = document.createElement("tr");
    if (settings.commandColumn == true) {
        ordInx += 1;
        $(headerRow).append("<th></th>");
        $(headerFilterRow).append("<th></th>");
        var commanColumnHtml = "";
        if (settings.viewUrl != null) {
            commanColumnHtml += "<li><a href='" + settings.viewUrl + "'><i class='fas fa-edit'></i>&nbsp;Görüntüle</a></li>";
        }

        if (settings.viewNewTab) {
            commanColumnHtml += "<li><a href='" + settings.viewUrl + "' target=_blank ><i class='fas fa-external-link'></i>&nbsp;Yeni Pencerede Görüntüle</a></li>";
        }
        if (settings.sameRecUrl != null) {
            commanColumnHtml += "<li><a href='" + settings.sameRecUrl + "'><i class='fas fa-pen-square'></i>&nbsp;Benzer Kayıt</a></li>";
        }
        if (settings.deleteFunc != null) {
            commanColumnHtml += "<li><a href='javascript:void(0)' onclick=\"" + settings.deleteFunc + "\"><i class='fas fa-trash-alt'></i>&nbsp;Sil</a></li>";
        }
        if (settings.customActBtnsTemp != null) {
            //       commanColumnHtml += "<li class=divider></li>";
            if (settings.customActBtnsTemp == "tmp_companies" && settings.googleSearchUrl != null)
                commanColumnHtml += "<li><a href='http://google.com/search?q=" + settings.googleSearchUrl + "'  target=_blank><i class='fas fa-google'></i>&nbsp;Google Ara</a></li>";
            commanColumnHtml += $('#' + settings.customActBtnsTemp).html();
        }
        columns.push({
            data: settings.keyValue,
            defaultContent: "",
            render: function (data, type, row) {
                var strCustomContent = new String(commanColumnHtml);

                var keyCount = strCustomContent.search("{keyValue}");
                for (i = 0; i < keyCount; i++)
                    strCustomContent = strCustomContent.replace("{keyValue}", "{" + settings.keyValue + "}");

                Object.keys(row).forEach(function (key) {
                    var keyCount = strCustomContent.search(key);
                    for (i = 0; i < keyCount; i++)
                        strCustomContent = strCustomContent.replace("{" + key + "}", row[key])
                });
                return "<div class='btn-group'><a href='javascript:void(0);' class='btn btn-block btn-primary dropdown-toggle' data-toggle='dropdown' aria-expanded='true'><i class='fas fa-bars'></i>&nbsp;<span class='caret mln'></span></a><ul class='dropdown-menu' role='menu'>" + strCustomContent + "</ul>";
            },
            width: "0.1%",
            orderable: false,
            className: 'noVis text-nowrap',
        });
        colIndex++;
    }

    $.each(settings.columns, function (key, val) {
        var _columnDefaults = JSON.parse(JSON.stringify(columnDefaults));
        var value = $.extend(_columnDefaults, val);
        var valueProp = value.fieldName;
        var isCheckBox = value.isCheckBox;
        var checkBoxClass = value.checkBoxClass;
        var customContent = value.customContent;
        var isHidden = (cachedDt != undefined && cachedDt.getJSONValueData("colId", value.fieldName).visible != undefined ? !cachedDt.getJSONValueData("colId", value.fieldName).visible : false);
        var customContentFn = function (data, type, row) {
            return row[valueProp];
        };
        if (isCheckBox) {
            customContentFn = function (data, type, row) {
                var list = JSON.parse(localStorage.getItem(window.location.pathname + "_" + settings.elementName + "_selected")) || [];
                var strCustomContent = new String("<input type=checkbox class=\"" + checkBoxClass + "\" data-custom-onchange='" + value.onchange + "' onchange=\"" + settings.elementName + ".columns().selectChange($(this),'{" + valueProp + "}');\" " + (list.contains(null, new String(row[valueProp])) ? "checked" : "") + "/>");
                Object.keys(row).forEach(function (key) {
                    var keyCount = strCustomContent.search(key);
                    for (i = 0; i < keyCount; i++)
                        strCustomContent = strCustomContent.replace("{" + key + "}", row[key] || "")
                });
                return strCustomContent;
            };
            columns.push({
                data: valueProp,
                render: customContentFn,
                defaultContent: "s",
                orderable: false,
                className: 'noVis',
                width: value.width,
            });
            if (value.checkBoxSelectAll) {
                $(headerRow).append("<th class='text-center'><input type=checkbox class=\"" + checkBoxClass + " select-all-checkbox\"  data-custom-onchange='" + value.onchange + "'  onchange=\"" + settings.elementName + ".columns().selectAll($(this));\"/></th>");
            } else {
                $(headerRow).append("<th></th>");
            }
        }
        else {

            var customContentTemp = value.customContentTemp;
            var JSONContent = value.JSONContent;
            var JSONContentKey = value.JSONContentKey;
            var JSONContentValue = value.JSONContentValue;
            var JSONContentColor = value.JSONContentColor;

            switch (value.dataType) {
                case 3:
                    customContentFn = function (d) {
                        return d != null && d != undefined ? moment(d).format(value.dateformat) : "";
                    }
                    break;
                case 4:
                    customContentFn = function (d, type, row) {
                        return DataTableFormatMoney(value, row[value.fieldName], row[value.numberfixField]);
                    }
                    break;
                case 5:
                    customContentFn = function (data, type, row) {
                        return value.chkData[row[valueProp] ? 1 : 0];
                    };
                    break;
                case 6:
                    customContentFn = function (data, type, row) {
                        if (data != null) {
                            if (IsJsonString(data))
                                data = JSON.parse(data);
                            var res = value.ObjectKey != null ? value.ObjectKey.split(" ") : [];
                            if (Array.isArray(data))
                                if (res.length > 0) {
                                    return Array.prototype.map.call(res, function (objKey) {
                                        return Array.prototype.map.call(data, function (item) {
                                            if (objKey.indexOf('.') > -1) {
                                                var subValue = objKey.split('.');
                                                return item[subValue[0]][subValue[1]];
                                            }
                                            var strValue = item[objKey];
                                            switch (value.objectDataType) {
                                                case 4:
                                                    strValue = DataTableFormatMoney(value, strValue, item[value.numberfixField]);
                                                    break;
                                            }
                                            return "<span class='badge badge-rounded badge-default-bordered'>" + strValue + "</span>";
                                        }).join(value.separator);
                                    }).join(' ');
                                }
                                else {
                                    return Array.prototype.map.call(data, function (item) {
                                        switch (value.objectDataType) {
                                            case 4:
                                                item = DataTableFormatMoney(value, item, item[value.numberfixField]);
                                                break;
                                        }
                                        return "<span class='badge badge-rounded badge-default badge-sm'>" + item + "</span>";
                                    }).join(value.separator);
                                }
                            else if (res.length > 0)
                                return Array.prototype.map.call(res, function (objKey) { return data[objKey] }).join(value.separator);
                            else
                                return data;
                        }
                        else {
                            return "";
                        }
                    }
                    break;
                case 7:
                    customContentFn = function (data, type, row) {
                        if (data != null) {
                            return '<span class="badge badge-' + (data.length > 0 ? 'primary' : 'default') + '">' + data.length + '</span>';
                        }
                        else
                            return '<span class="badge badge-default">0</span>';
                    }
                    break;
            }

            if (customContent != null && customContent != undefined) {
                customContentFn = function (data, type, row) {
                    return FillCustomContent(value, customContent, row);
                };
            }
            if (customContentTemp != null && customContentTemp != undefined) {
                customContentFn = function (data, type, row) {
                    return FillCustomContent(value, $('#' + customContentTemp).html(), row);
                };
            }
            if (JSONContent != null && JSONContent != undefined && (customContent == null || customContent == undefined)) {
                customContentFn = function (data, type, row) {
                    var jsonContentRow = JSONContent.getJSONValueData(JSONContentKey, row[valueProp]);
                    var rowValue = jsonContentRow[JSONContentValue] || '';
                    if (JSONContentColor == 'color')
                        return "<span class='badge badge-default' style=\"background-color:" + jsonContentRow[JSONContentColor] + ";border-color:" + jsonContentRow[JSONContentColor] + "\">" + rowValue + "</span>";
                    else if (JSONContentColor == 'default-bordered')
                        return "<span class='badge badge-default-bordered badge-rounded'>" + rowValue + "</span>";
                    else if (JSONContentColor == 'class')
                        return "<span class='badge badge-" + jsonContentRow[JSONContentColor] + "'>" + rowValue + "</span>";


                    return rowValue;
                };
            }
            if (value.customContentFn != undefined) {
                customContentFn = value.customContentFn;
            }
            columns.push({
                data: valueProp,
                render: customContentFn,
                sortable: value.sortable,
                visible: !isHidden && value.visible,
                searchable: value.filter,
                defaultContent: "",
                className: ((value.noVisible ? "noVis " : "") + value.className).trim(),
                width: value.width,
            });
            if (value.editable) {
                editable = true;
                switch (value.dataType) {
                    case 2://Select
                        if (value.JSONContent != null && value.JSONContent != undefined) {
                            var editOptions = [];
                            $.each(value.JSONContent, function (key, value) {
                                editOptions.push({ "value": value[JSONContentKey], "label": value[JSONContentValue] });
                            });
                            editableColumns.push({
                                label: value.caption,
                                name: valueProp,
                                type: "select",
                                options: editOptions
                            });
                        }
                        break;
                    case 3:
                        editableColumns.push({
                            label: value.caption,
                            name: valueProp,
                            type: "datetime",
                            format: value.dateformat,
                        });
                        break;
                    case 4:
                        editableColumns.push({
                            label: value.caption,
                            name: valueProp,
                            attr: {
                                type: "number"
                            }
                        });
                        break;
                    case 5:
                        editableColumns.push({
                            label: value.caption,
                            name: valueProp,
                            type: "checkbox",
                            separator: "|",
                            ipOpts: [
                                { label: '', value: true }
                            ]
                        });
                        break;
                    default:
                        editableColumns.push({
                            label: value.caption,
                            name: valueProp,
                        });
                        break;
                }
            }

            $(headerRow).append("<th >" + value.caption + "</th>");//" + isHidden ? "style='display:none;'" : "" + "
        }
        var filterElement = "";
        if (value.filter && value.visible) {
            filters = true;
            if (value.isCheckBox) {
                filterElement = "<input type='checkbox' onchange='" + settings.elementName + ".draw()' class='" + value.checkBoxClass + "'  id='" + settings.elementName + "_chk_filter'/>";
            }
            else {
                switch (value.dataType) {
                    case 1:
                        filterElement = '<input type="text" class="mr-23 ' + settings.searchElementsClass + '" data-column="' + colIndex + '" />';
                        break;
                    case 2:
                        var optsString = "<option value=''>Tümü</option>";
                        for (var i = 0; i < JSONContent.length; i++) {
                            optsString += "<option value='" + JSONContent[i][JSONContentKey] + "'>" + JSONContent[i][JSONContentValue] + "</option>";
                        }
                        filterElement = '<select class="mr-23 ' + settings.searchElementsClass + '" data-column="' + colIndex + '" >' + optsString + "</select>";
                        break;
                    case 3:
                        filterElement = '<input type="text" class="mr-23 date ' + settings.searchElementsClass + '" data-column="' + colIndex + '" /></div>';
                        break;
                    case 4:
                        filterElement = '<input type="number" class="mr-23 ' + settings.searchElementsClass + '" data-column="' + colIndex + '" />';
                        break;
                    case 5:
                        var optsString = "<option value=''>Tümü</option>";
                        for (var i = 0; i < value.chkData.length; i++) {
                            optsString += "<option value='" + (i == 1) + "'>" + value.chkData[i] + "</option>";
                        }
                        filterElement = '<select class="mr-23 ' + settings.searchElementsClass + '" data-column="' + colIndex + '" >' + optsString + "</select>";
                        break;
                    default:
                        filterElement = '<input type="text" class="mr-23 ' + settings.searchElementsClass + '" data-column="' + colIndex + '" />';
                        break;
                }
            }
            $(headerFilterRow).append("<th class='p5' data-field-name='" + value.fieldName + "' " + (!isHidden && value.visible ? "" : "hidden") + ">" + filterElement + "</th>");
        }
        else {
            $(headerFilterRow).append("<th class='p5' data-field-name='" + value.fieldName + "' " + (!isHidden && value.visible ? "" : "hidden") + "></th>");
        }
        colIndex++;
    });
    if (!settings.headerHide)
        $(thead).append(headerRow);
    $('#' + settings.elementName).append(thead);
    if (filters) {
        $(filterThead).append(headerFilterRow);
        $('#' + settings.elementName).append(filterThead);
    }
    $('#' + settings.elementName).append("<tbody></tbody>");
    settings.order = ordInx > settings.order[0] && ordInx <= settings.columns.length - 1 ? [settings.order[0] + ordInx, "asc"] : settings.order;
    var buttons = [];
    if (settings.colvis) {
        buttons.push({
            extend: 'colvis',
            columns: ':not(.noVis)',
            className: 'btn-sm',
        });
    }
    console.log(settings.length);
    var dataTable = $('#' + settings.elementName).DataTable({
        "dom": settings.dom,
        "autoWidth": true,
        "stateSave": false,
        "responsive": settings.responsive,
        "serverSide": settings.data == null,
        "lengthChange": settings.lengthMenu.length > 0 ? true : false,
        "lengthMenu": settings.lengthMenu,
        "iDisplayLength": settings.lengthMenu.length > 0 ? null : settings.length,
        "searching": true,
        "paging": settings.paging,
        "buttons": buttons,
        fixedHeader: false,
        fixedColumns: false,
        //scrollX: true,
        scrollX: "100%",
        scrollY: '200vh',
        scrollCollapse: true,
        data: settings.data,
        "ajax": settings.data == null ? {
            "type": "POST",
            "url": settings.url,
            "contentType": 'application/json; charset=utf-8',
            //dataType: "json",
            "data": function (data) {
                if (settings.beforeCallBack != null) {
                    eval(settings.beforeCallBack);
                }
                $.each(settings.columns, function (key, value) {

                    if (value.ObjectKey != null && value.ObjectKey != "" && value.ObjectJson != true) {
                        var index = data.columns.length - settings.columns.length;//data.columns içerisinde Id alanı statick olarak var. bundan dolayı iki array arasındaki index farkını alıyoruz. settings.columns içersinde 1. indexe denk gelen değer data.columns içersinde 2 denk geliyor.
                        data.columns[key + (index)]["objectKey"] = value.ObjectKey;
                    }
                    //var col = data.columns.find("data", value.fieldName);
                    //col["objectKey"] = value.ObjectKey;
                });
                data["KeyField"] = settings.keyValue;
                $.each(settings.ajaxParams, function (key, value) {
                    var val = value.value != undefined ? value.value : $(value.element).val();
                    data[value.param] = value.isJson == 1 ? JSON.stringify(val || []) : val
                });
                if ($("#" + settings.elementName + "_chk_filter").length > 0 && $("#" + settings.elementName + "_chk_filter").prop("checked")) {
                    data["incIds"] = JSON.stringify(window[settings.elementName].getSelectedValues());
                }
                console.log(data.length);
                return JSON.stringify(data);
                //return data;
            },
            "error": ajaxError,
        } : null,
        "processing": settings.data == null,
        "columns": columns,
        "order": settings.order,
        "language": {
            "buttons": {
                colvis: '<span><i class="fas fa-th"></i> <span class="caret"></span></span>'
            },
            "searchPlaceholder": "aramak için yazın",
            "decimal": ".",
            "emptyTable": "Tabloda herhangi bir veri mevcut değil",
            "info": "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
            "infoEmpty": "Kayıt yok",
            "infoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
            "infoPostFix": "",
            "infoThousands": ",",
            "lengthMenu": "_MENU_",
            "loadingRecords": "<div style=position:relative;>Yükleniyor...</div>",
            "processing": "<div style=position:relative;>Yükleniyor...</div>",
            "search": "_INPUT_",
            "zeroRecords": "Eşleşen kayıt bulunamadı",
            "paginate": { 'first': 'İlk', 'last': 'Son', 'next': '&rarr;', 'previous': '&larr;' },
            "aria": {
                "sSortAscending": ": artan sütun sıralamasını aktifleştir",
                "sSortDescending": ": azalan sütun soralamasını aktifleştir"
            },
            "select": {
                style: 'os',
                selector: 'td:first-child'
            },
        },
        drawCallback: function (oSettings) {
            if (settings.headerHide) {
                $('#' + settings.elementName + '_wrapper .dataTables_scrollHead').remove();
            }
            if (settings.infoHide) {
                $("#" + settings.elementName + "_info").remove();
            }

            if (settings.endCallBack != null) {
                eval(settings.endCallBack);
            }
            //$(this).find('tbody tr').slice(-3).find('.dropdown, .btn-group').addClass('dropup');
            if (settings.groupFields.length > 0) {
                $.each(settings.groupFields, function (k, colIndex) {
                    var currentRow; var currentValue; var rowCount = 0;
                    $.each($('#' + settings.elementName + ' > tbody > tr'), function (key, value) {
                        var cell = $($(value).children('td')[colIndex]);
                        if (currentRow == undefined) {
                            currentRow = cell;
                            currentValue = cell.html();
                        }
                        if (currentValue == cell.html()) {
                            rowCount += 1;
                            if (rowCount != 1) {
                                cell.hide();
                                currentRow.attr('rowspan', rowCount);
                            }
                        }
                        else {
                            rowCount = 1;
                            currentRow = cell;
                            currentValue = cell.html();
                        }
                    });
                });
            }
            custom_data_table();
            if ($('.date').length > 0) {

                $('.date').datepicker({
                    autoclose: true,
                    language: $("#language").attr('lang'),
                    orientation: "bottom"
                });
            }
            if (window[settings.elementName].columns != undefined)
                window[settings.elementName].columns.adjust();
            if (settings.footer.url != null && settings.footer.url != undefined) {
                window[settings.elementName].fillFooter();
            }

        },
        preDrawCallback: function () {
            //$(this).find('tbody tr').slice(-3).find('.dropdown, .btn-group').removeClass('dropup');
        },
        createdRow: function (row, data, dataIndex) {
            if (settings.rowClassField != undefined && settings.rowClassField != null) {
                var rowClass = settings.rowClassArray.filter(f => f.value == data[settings.rowClassField])[0];
                if (rowClass != null) {
                    $(row).addClass(rowClass.className);
                }
            }
        }
        //scrollY:500,
        //"scrollCollapse": true,

    });
    window[settings.elementName] = dataTable;
    $("div.datatable-header").remove();
    $("div.dataTables_filter input").unbind();//Genel search keyup özelliği kapat
    $("div.dataTables_filter input").keyup(function (e) {//Genel search keyup enter'a basınca çalıştır
        if ((e.which == 13 || this.value == "") && dataTable.search() != this.value) {
            dataTable.search(this.value).draw();
        }
    });
    if (filters) {
        $(thisId).find('thead').find('tr').find('input:not([type=checkbox])').on('keyup', function (e) {   // for text boxes
            if ((e.which == 13 || $(this).val() == "") && dataTable.columns(i).search() != $(this).val()) {//columns search keyup properties close
                var i = $(this).attr('data-column');  // getting column index
                var v = $(this).val();  // getting search input value
                dataTable.columns(i).search(v).draw();

            }
        });
        $(thisId).find('thead').find('tr').find('select').on('change', function () {   // for select box
            var i = $(this).attr('data-column');
            var v = $(this).val();
            dataTable.columns(i).search(v).draw();
        });
    }
    $(thisId).on('column-visibility.dt', function (e, settings, column, state) {
        var cols = localStorage.getItem(table_key) != undefined ? JSON.parse(localStorage.getItem(table_key)) : [];
        var field = $(thisId).find('thead:nth-child(2)').find('th:nth-child(' + (column + 1) + ')');
        var visible;
        if (state) {
            if (filters)
                field.show();
            //$(thisId).find('thead:nth-child(1)').find('th:nth-child(' + (column + 1) + ')').show();
            visible = true;
        }
        else {
            if (filters)
                field.hide();
            //$(thisId).find('thead:nth-child(1)').find('th:nth-child(' + (column + 1) + ')').hide();
            visible = false;
        }
        $('tr.group').children('td').attr("colspan", $(thisId).find('thead:nth-child(1)').find('th').length);
        var col = cols.getJSONValueData("colId", field.data("fieldName"));
        if (col.visible != undefined) {
            col.visible = visible;
            cols.updateObjectInArray("colId", field.data("fieldName"), col);
        }
        else {
            cols.push({
                colId: field.data("fieldName"),
                visible: visible
            });
        }
        localStorage.setItem(table_key, JSON.stringify(cols));
    });
    if (settings.rowclickFn || settings.rowclickUrl) {
        $('#' + settings.elementName).find('tbody').on('dblclick', 'tr', function () {
            var data = window[settings.elementName].row(this).data();
            var strRowClick = settings.rowclickFn || settings.rowclickUrl;
            $.each(data, function (key, value) {
                if (key == settings.keyValue)
                    strRowClick = strRowClick.replace("{keyValue}", value);
                strRowClick = strRowClick.replace("{" + key + "}", value);
            });
            if (settings.rowclickFn)
                eval(strRowClick);
            else if (settings.rowclickUrl)
                window.location = strRowClick;
        });
    }
    return dataTable;
};
$.fn.dataTable.Api.register('processing()', function (show) {
    return this.iterator('table', function (ctx) {
        ctx.oApi._fnProcessingDisplay(ctx, show);
    });
});
var exportTableHtml = "";
$.fn.dataTable.Api.register('exportData()', function () {
    var settings = JSON.parse(localStorage.getItem(window.location.pathname + "_" + this.table().node().id + "_settings"));
    if (settings.beforeCallBack != null) {
        eval(settings.beforeCallBack);
    }
    window[settings.elementName].columns().processing(true);
    var exportColumns = [];
    var table_key = window.location.pathname + "_" + this.table().node().id + "_dt_columns";
    var colsVisible = localStorage.getItem(table_key) != undefined ? JSON.parse(localStorage.getItem(table_key)) : [];
    $.each(settings.columns, function (key, val) {
        var _columnDefaults = JSON.parse(JSON.stringify(columnDefaults));
        var value = $.extend(_columnDefaults, val);
        var isVisible = colsVisible != undefined && colsVisible.getJSONValueData("colId", value.fieldName).visible != undefined ? colsVisible.getJSONValueData("colId", value.fieldName).visible : true;
        if (!value.isCheckBox && isVisible && value.exportVisible) {
            exportColumns.push({
                fieldName: value.fieldName,
                fieldExport: value.fieldExport,
                caption: value.caption,
                dataType: value.dataType,
                numberfixField: value.numberfixField,
                postfix: value.postfix,
                JSONContentKey: value.JSONContentKey,
                JSONContentValue: value.JSONContentValue,
                JSONContent: value.JSONContent,
                dateformat: value.dateformat,
                ObjectKey: value.ObjectKey,
                chkData: value.chkData
            });
        }
    });
    var data = JSON.parse(this.ajax.params());
    $.each(settings.ajaxParams, function (key, value) {
        var val = value.value != undefined ? value.value : $(value.element).val();
        data[value.param] = value.isJson == 1 ? JSON.stringify(val || []) : val;
    });
    data["cols"] = JSON.stringify(exportColumns);
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        type: "POST",
        url: settings.exportUrl,
        data: JSON.stringify(data)
    }).done(function (result) {
        if (settings.endCallBack != null) {
            eval(settings.endCallBack);
        }
        var _result = JSON.parse(result);
        exportTableHtml = "<table><tr>"
        $.each(exportColumns, function (key, value) {
            exportTableHtml = exportTableHtml + "<th>" + value.caption + "</th>";
        });
        exportTableHtml = exportTableHtml + "</tr>";
        $.each(_result.data, function (key, value) {
            exportTableHtml = exportTableHtml + "<tr>";
            $.each(exportColumns, function (key2, col) {
                exportTableHtml = exportTableHtml + "<td>" + value[col.fieldName] + "</td>";
            });
            exportTableHtml = exportTableHtml + "</tr>";
        });
        exportTableHtml = exportTableHtml + "</table>";
        var downloadLink;
        var dataType = 'application/vnd.ms-excel';
        var filename = settings.exportFileName + ".xls";
        downloadLink = document.createElement("a");

        document.body.appendChild(downloadLink);
        if (navigator.msSaveOrOpenBlob) {
            var blob = new Blob(['\ufeff', exportTableHtml], {
                type: dataType
            });
            navigator.msSaveOrOpenBlob(blob, filename);
        } else {
            // Create a link to the file
            downloadLink.href = 'data:' + dataType + ', ' + exportTableHtml;

            // Setting the file name
            downloadLink.download = filename;

            //triggering the function
            downloadLink.click();
        }

        window[settings.elementName].columns().processing(false);
    });
    return;
});
$.fn.dataTable.Api.register('selectAll()', function (checkbox) {
    var table_id = this.table().node().id;
    var settings = JSON.parse(localStorage.getItem(window.location.pathname + "_" + table_id + "_settings"));
    if (checkbox.is(":checked")) {
        $.each($('#' + table_id).find("tbody > tr"), function (key, value) {
            $(value).find("input[type=checkbox]").prop("checked", true).change();
        });

        if (settings.beforeCallBack != null) {
            eval(settings.beforeCallBack);
        }
        var data = JSON.parse(this.ajax.params());
        $.each(settings.ajaxParams, function (key, value) {
            var val = value.value != undefined ? value.value : $(value.element).val();
            data[value.param] = value.isJson == 1 ? JSON.stringify(val || []) : val;
        });

        $.ajax({
            contentType: 'application/json; charset=utf-8',
            type: "POST",
            url: settings.selectAllUrl,
            data: JSON.stringify(data),
            success: function (result) {
                localStorage.setItem(window.location.pathname + "_" + table_id + "_selected", JSON.stringify(result));
                eval(checkbox.data("customOnchange"));
            },
            error: function (jqXhr, textStatus, errorThrown) {
                console.error(errorThrown);
            }
        });
    }
    else {
        $.each($('#' + table_id).find("tbody > tr"), function (key, value) {
            $(value).find("input[type=checkbox]").prop("checked", false).change();
        });
        localStorage.setItem(window.location.pathname + "_" + table_id + "_selected", null);
        eval(checkbox.data("customOnchange"));
    }
    return;
});
$.fn.dataTable.Api.register('selectChange()', function (checkbox, value) {
    var data = isNaN(value) ? value : Number(value);
    var table_id = this.table().node().id;
    var selectListCache = localStorage.getItem(window.location.pathname + "_" + table_id + "_selected");
    var sellectList = JSON.parse(selectListCache) || [];
    if (checkbox.is(":checked")) {
        if (sellectList.indexOf(data) < 0) {
            sellectList.push(data);
        }
        localStorage.setItem(window.location.pathname + "_" + table_id + "_selected", JSON.stringify(sellectList));
    }
    else {
        sellectList.splice(sellectList.indexOf(data), 1);//sil
        localStorage.setItem(window.location.pathname + "_" + table_id + "_selected", JSON.stringify(sellectList));
    }
    eval(checkbox.data("customOnchange"));
    return;
});
$.fn.dataTable.Api.register('getSelectedValues()', function () {
    return JSON.parse(localStorage.getItem(window.location.pathname + "_" + this.table().node().id + "_selected")) || [];
});
$.fn.dataTable.Api.register('setSelectedValues()', function (selecteds) {
    localStorage.setItem(window.location.pathname + "_" + this.table().node().id + "_selected", JSON.stringify(selecteds));
    return;
});
$.fn.dataTable.Api.register('fillFooter()', function () {
    var table_id = this.table().node().id;
    var $this = this;
    var settings = JSON.parse(localStorage.getItem(window.location.pathname + "_" + table_id + "_settings"));

    var data = JSON.parse(this.ajax.params());
    $.each(settings.ajaxParams, function (key, value) {
        var val = value.value != undefined ? value.value : $(value.element).val();
        data[value.param] = value.isJson == 1 ? JSON.stringify(val || []) : val;
    });

    $.ajax({
        contentType: 'application/json; charset=utf-8',
        type: "POST",
        url: settings.footer.url,
        data: JSON.stringify(data),
        async: true,
        cache: true,
        success: function (result) {
            var footerStartIndex = table_payment.settings()[0].aoColumns.find("data", settings.footer.startcolumnName).idx; //Kolon indexini bul
            footerStartIndex -= table_payment.settings()[0].aoColumns.filter(f => f.idx < 15).filter(f => !f.bVisible).length; //gizlenen kolon sayısını index değerinden çıkar
            $('#' + table_id).find('tfoot').remove();
            var tfoot = document.createElement("tfoot");
            $.each(result, function (key, value) {
                var tfootRow = document.createElement("tr");
                $(tfootRow).append("<th colspan=" + footerStartIndex + " ></th>");// 
                $.each(settings.footer.columns, function (key2, value2) {
                    $(tfootRow).append("<th class=text-right>" + value[value2.columnName] + "</th>");
                });
                $(tfootRow).append("<th colspan=" + ($('#' + table_id).find("thead").first("tr").find("th").length - (settings.footer.columns.length + footerStartIndex)) + " ></th>");
                $(tfoot).append(tfootRow);
            });
            window[settings.elementName + "_tfoot_cache"] = tfoot;
            $('#' + table_id).append(tfoot);
        },
        error: function (jqXhr, textStatus, errorThrown) {
            console.error(errorThrown);
        }
    });
    return;
});

Array.prototype.remove = function (key, value) {
    if (value) {
        if (key == null) {
            for (i = 0; i < this.length; i++) {
                if (this[i] == value) {
                    this.splice(i, 1);
                    break;
                }
            }
        }
        else {
            for (i = 0; i < this.length; i++) {
                if (this[i][key] == value) {
                    this.splice(i, 1);
                    break;
                }
            }
        }
    }
}
Array.prototype.contains = function (key, value) {
    try {
        if (this.length == 0) return false;

        if (key == null) {
            for (i in this) {
                if (this[i] == value) return true;
            }
            return false;
        }
        else {
            for (i in this) {
                if (this[i][key] == value) {
                    return true;
                }
            }
            return false;
        }
    }
    catch (err) {

    }
}
Array.prototype.updateObjectInArray = function (key, value, newObject) {
    for (i = 0; i < this.length; i++) {
        if (this[i][key] == value)
            this[i] = newObject;
    }
}
Array.prototype.find = function (key, value) {
    var result = jQuery.grep(this, function (val) { return val[key] == value });
    if (result.length > 0) return result[0];
    return [];
}


function IsJsonString(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}
function FillCustomContent(value, customContent, row) {
    var strCustomContent = new String(customContent);
    Object.keys(row).forEach(function (key) {
        if (strCustomContent.indexOf("{" + key + "}") > -1) {
            var keyCount = strCustomContent.search(key);
            var strValue = row[key];
            switch (value.dataType) {
                case 4:
                    strValue = DataTableFormatMoney(value, strValue, row[value.numberfixField]);
                    break;
            }
            for (i = 0; i < keyCount; i++)
                strCustomContent = strCustomContent.replace("{" + key + "}", strValue || "")
        }
        if (row[key] != null && row[key] != undefined && typeof (row[key]) == "object") {
            Object.keys(row[key]).forEach(function (subKey) {
                if (strCustomContent.indexOf("{" + key + "." + subKey + "}") > -1) {
                    var objectStrValue = row[key][subKey];
                    switch (value.dataType) {
                        case 4:
                            objectStrValue = DataTableFormatMoney(value, objectStrValue, row[value.numberfixField]);
                            break;
                    }
                    keyCount = strCustomContent.search(key + "." + subKey);
                    for (i = 0; i < keyCount; i++) {
                        strCustomContent = strCustomContent.replace("{" + key + "." + subKey + "}", objectStrValue || "")
                    }
                }
            });
        }
    });

    return strCustomContent;
};
function DataTableFormatMoney(value, d, numberfix) {
    if (value.zeroValue != null && d == 0)
        return value.zeroValue;
    var prefix = value.prefix != undefined && value.prefix != null ? value.prefix.getJSONValueData(value.JSONContentKey, numberfix)[value.JSONContentValue] : '';
    var postfix = value.postfix != undefined && value.postfix != null ? value.postfix.getJSONValueData(value.JSONContentKey, numberfix)[value.JSONContentValue] : '';

    var thousands = '.';
    var decimal = ',';
    var precision = value.precision == undefined ? 2 : value.precision;
    if (typeof d !== 'number' && !parseFloat(d)) {
        return d;
    }

    var negative = d < 0 ? '-' : '';
    var flo = parseFloat(d);

    if (isNaN(flo)) {
        return __htmlEscapeEntities(d);
    }

    flo = flo.toFixed(precision);
    d = Math.abs(flo);

    var intPart = parseInt(d, 10);
    var floatPart = precision ?
        decimal + (d - intPart).toFixed(precision).substring(2) :
        '';

    return negative + (prefix || '') +
        intPart.toString().replace(
            /\B(?=(\d{3})+(?!\d))/g, thousands
        ) +
        floatPart + ' ' + (postfix || '');
}