$(function () {

    $(window).on('load', eventWindow.onLoad);
    $(document).on('click', '.item', eventMap.changeTargetMonitoring_OnClick);
    $('.date-time').combodate({
        minYear: 1975,
        maxYear: new Date().getFullYear(),
        value: buildCurrentDate()
    });

    $('.finding-history').click(function (e) {
        getHistory();
    });
});