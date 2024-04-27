showInPopup = (url, title) => {
    window.$.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            window.$("#modal_default .modal-body").html(res);
            window.$("#modal_default .modal-title").html(title);
            window.$("#modal_default").modal("show");
        }
    });
};

jQueryAjaxPost = form => {
    window.$.validator.unobtrusive.parse(form);
    if (window.$(form).valid()) {
        window.$.ajax({
            type: "POST",
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    if (res.result === "Redirect") {
                        window.location = res.url;
                    } else {
                        window.$("#view-data").html(res.html);
                        window.$("#modal_default .modal-body").html("");
                        window.$("#modal_default .modal-title").html("");
                        window.$("#modal_default").modal("hide");
                    }
                }
                else
                    window.$("#modal_default .modal-body").html(res.html);
            },
            error: function () {

            }
        });
    }
    return false;
};

