const langCode = "en"
// https://github.com/globalizejs/globalize#installation
$.when(
    $.get("/assets/js/cldr-core/supplemental/likelySubtags.json", null, null, "json"),
    $.get("/assets/js/cldr-core/supplemental/numberingSystems.json", null, null, "json"),
    $.get("/assets/js/cldr-core/supplemental/timeData.json", null, null, "json"),
    $.get("/assets/js/cldr-core/supplemental/weekData.json", null, null, "json"),

    $.get("/assets/js/cldr-numbers-modern/main/" + langCode + "/numbers.json", null, null, "json"),
    $.get("/assets/js/cldr-numbers-modern/main/" + langCode + "/currencies.json", null, null, "json"),

    $.get("/assets/js/cldr-dates-modern/main/" + langCode + "/ca-generic.json", null, null, "json"),
    $.get("/assets/js/cldr-dates-modern/main/" + langCode + "/ca-gregorian.json", null, null, "json"),
    $.get("/assets/js/cldr-dates-modern/main/" + langCode + "/dateFields.json", null, null, "json"),
    $.get("/assets/js/cldr-dates-modern/main/" + langCode + "/timeZoneNames.json", null, null, "json")
).then(function () {
    return [].slice.apply(arguments, [0]).map(function (result) {
                    Globalize.load(result[0]);
    });
}).then(function () {
                    // Initialise Globalize to the current culture
                    Globalize.locale(langCode);
});

$(function () {
                    $('[type="datetime-local"]').each(function (index, value) {
                        $(value).attr('type', 'text');
                        $(value).val(value.defaultValue);
                        $(value).flatpickr({
                            locale: langCode,
                            enableTime: true,
                            altFormat: "H:i d.m.Y",
                            altInput: true,
                            // dateFormat: "Z", // iso format (causes -3h during summer)
                            // use direct conversion, let backend deal with utc/whatever conversions
                            dateFormat: "Y-m-d H:i:s",
                            disableMobile: true,
                            time_24hr: true,
                        });
                    });

    $('[type="time"]').each(function (index, value) {
                    $(value).attr('type', 'text');
        $(value).val(value.defaultValue);
        $(value).flatpickr({
                    locale: langCode,
            enableTime: true,
            noCalendar: true,

            altFormat: "H:i",
            altInput: true,
            dateFormat: "H:i", // 24hn HH:mm
            disableMobile: true,

            time_24hr: true,
        });
    });

    $('[type="date"]').each(function (index, value) {
                    $(value).attr('type', 'text');
        $(value).val(value.defaultValue);
        $(value).flatpickr({
                    locale: langCode,
            altFormat: "d.m.Y",
            altInput: true,
            disableMobile: true,
            dateFormat: "Y-m-d", // YYYY-MM-DD
        });
    });
});
