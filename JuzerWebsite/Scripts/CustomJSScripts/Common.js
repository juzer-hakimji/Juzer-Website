$.notifyDefaults({
    placement: {
        from: "top",
        align: "center"
    }
});

function objectifyForm(formArray) {
    var returnArray = {};
    for (var i = 0; i < formArray.length; i++) {
        returnArray[formArray[i]['name']] = formArray[i]['value'];
    }
    return returnArray;
}

function CallAjaxMethod(URL, RequestType, Data) {
    return $.ajax({
        url: URL,
        type: RequestType,
        data: Data,
        dataType: 'json'
    }).then(function (e) {
        return e;
    });
}

function InitializeForm(FormSelector) {
    $(FormSelector).submit(function () {
        return false;
    });
}

function CloseModalWhenEsc(Selector) {
    $(document).keyup(function (event) {
        if (event.which == '27') {
            Selector.removeClass('is-visible');
            fn_FormReset('form');
        }
    });
}

function ShowResult(message, type) {
    $.notify({
        icon: '',
        title: '',
        message: message,
    }, {
        });
}

function fn_FormValidation(FormSelector) {
    var formValid = $(FormSelector).validate().form();
    if (!formValid)
        return false;
    else
        return true;
}

function fn_ShowValidationErrors() {
    $('span.cd-error-message').each(function (i, obj) {
        if ($(this).children().length) {
            $(this).addClass("is-visible");
        }
    });
}

function fn_FormReset(FormSelector) {
    $(FormSelector).trigger("reset");
    $('#hdnEditId').val('');
}

