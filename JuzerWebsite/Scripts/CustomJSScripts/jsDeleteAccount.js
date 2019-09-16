$(document).ready(function () {
    AddEventHandlers();
    InitializeForm('#cd-form-DltAccnt');
});

function AddEventHandlers() {
    $("#btnDeleteAccnt").on('click', DeleteAccountHandler);
}

function DeleteAccountHandler() {
    CallAjaxMethod("/Authentication/ValidatePasswordAndDeleteUser", 'POST', { __RequestVerificationToken: $('#cd-form-DltAccnt input[name="__RequestVerificationToken"]').val(), p_Password: $("#txtPass").val() }).then(function (result) {
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
