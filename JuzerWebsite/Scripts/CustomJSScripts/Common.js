$.notifyDefaults({
    placement: {
        from: "top",
        align: "center"
    }
});

function objectifyForm(formArray) {//serialize data function

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
    //close modal when clicking the esc keyboard button
    $(document).keyup(function (event) {
        if (event.which == '27') {
            Selector.removeClass('is-visible');
        }
    });
}

function ShowResult(message,type) {
    $.notify({
        icon: '',
        title: '',
        message: message,
    }, {
            //type: type,
    });

    //alert(result);
}

//function ShowConfirmBox(p_MainMessage,p_SecondaryMessage) {
//    var Answer;
//     bootbox.confirm({
//        title: p_MainMessage,
//        message: p_SecondaryMessage,
//        buttons: {
//            cancel: {
//                label: '<i class="fa fa-times"></i> Cancel'
//            },
//            confirm: {
//                label: '<i class="fa fa-check"></i> Confirm'
//            }
//        },
//        callback: function (result) {
//            Answer = result;
//        }
//    });
//    return Answer;
//}

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

