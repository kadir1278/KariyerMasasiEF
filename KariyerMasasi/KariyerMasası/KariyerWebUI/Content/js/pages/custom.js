function datepicker_init() {
    if ($('.datepicker').length > 0)
        $('.datepicker').datepicker({
            clearBtn: false,
            format: "dd.mm.yyyy",
            language: 'tr'
        });
}
