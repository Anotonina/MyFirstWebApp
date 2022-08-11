$(function () {
    $.ajaxSetup({ cache: false });
    $(".cashierItem").click(function (e) {
        var clickId = $(this).attr('id');
        e.preventDefault();
        $.get("/Cashiers/PartialDetails/" + clickId, function (data) {
            $('#dialogContent').html(data);
            $('#modDialog').modal('show');
        });
    });
})