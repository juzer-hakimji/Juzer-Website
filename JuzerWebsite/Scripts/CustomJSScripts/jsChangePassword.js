﻿$(document).ready(function () {
    AddEventHandlers();
});

function AddEventHandlers() {
    $("#btnChangePassword").on('click', ChangePasswordHandler);
}

function ChangePasswordHandler() {
    var SerializedObj = $('#cd-form-ChangePass').serialize();
    CallAjaxMethod("User/ChangePassword", 'PUT', SerializedObj).then(function (result) {
        ShowResult(result);
    });
}
