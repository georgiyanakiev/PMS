$(".changeAccommodationType").click(function () {
    var accommodationTypeID = $(this).attr("data-id");
    $("div.accommodationTypesRow").hide();
    $("div.accommodationTypesRow[data-id=" + accommodationTypeID + "]").show();


});