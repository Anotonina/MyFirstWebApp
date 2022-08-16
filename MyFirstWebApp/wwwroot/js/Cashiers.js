$(function () {
    $.ajaxSetup({ cache: false });

    $(".cashierItem").click(function (e) {
        if (e.target.tagName == "TD") {

            var clickId = $(this).attr('id');
            $.get("/Cashiers/PartialDetails/" + clickId, function (data) {
                $('#dialogContent').html(data);
                $('#modDialog').modal('show');
            });
        }
    })
 });
