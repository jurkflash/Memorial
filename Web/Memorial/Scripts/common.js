function getURLLastParameter() {
    var sPageURL = window.location.href;
    var indexOfLastSlash = sPageURL.lastIndexOf("/");

    if (indexOfLastSlash > 0 && sPageURL.length - 1 != indexOfLastSlash)
        return sPageURL.substring(indexOfLastSlash + 1);
    else
        return 0;
}

$(function () {
    $('[data-utcTime]').each(function () {
        var date = new Date($(this).attr('data-utcTime') + "Z");

        var year = date.toLocaleString("default", { year: "numeric" });
        var month = date.toLocaleString("default", { month: "2-digit" });
        var day = date.toLocaleString("default", { day: "2-digit" });
        var hour = date.toLocaleString("default", { hour: "2-digit", hour12: false });
        var minute = date.toLocaleString("default", { minute: "2-digit" });
        var formattedDate = year + "-" + month + "-" + day + " " + hour + ":" + minute;
        $(this).html(formattedDate);
    });
});

$(function () {
    $('[data-utcDate]').each(function () {
        var date = new Date($(this).attr('data-utcDate') + "Z");

        var year = date.toLocaleString("default", { year: "numeric" });
        var month = date.toLocaleString("default", { month: "short" });
        var day = date.toLocaleString("default", { day: "2-digit" });
        var formattedDate = year + "-" + month + "-" + day;
        $(this).html(formattedDate);
    });
});