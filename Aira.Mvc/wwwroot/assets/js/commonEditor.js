$(".s2-ddl").select2({
    minimumResultsForSearch: 10,
    dropdownParent: $("#modal_default")
}).on("change", function () {
    window.$(this).closest(".col-xxl-6").find(".field-validation-error").empty();
    // ReSharper disable once UnknownCssClass
    window.$(this).removeClass("input-validation-error");
});