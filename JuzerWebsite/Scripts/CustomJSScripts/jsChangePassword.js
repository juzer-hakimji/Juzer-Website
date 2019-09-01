﻿$(document).ready(function () {
    AddEventHandlers();
    InitializeForm('#cd-form-ChangePass');
});

function AddEventHandlers() {
    $("#btnChangePassword").on('click', ChangePasswordHandler);
}

function ChangePasswordHandler() {
    var SerializedObj = $('#cd-form-ChangePass').serialize();
    CallAjaxMethod("/User/ChangePassword", 'PUT', SerializedObj).then(function (result) {
        if (result.Success) {
            ShowResult(result.Message);
            setTimeout(function () {
                    window.location.href = result.RedirectURL;
            }, 1500);
        }
        else {
            ShowResult(result.Message);
        }
    });
}