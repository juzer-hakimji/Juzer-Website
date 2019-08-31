$(document).ready(function () {
    AddEventHandlers();
    InitializeForm('#cd-form-DltAccnt');
});

function AddEventHandlers() {
    $("#btnDeleteAccnt").on('click', DeleteAccountHandler);
}

function DeleteAccountHandler() {
    var Password = $("#txtPass").val();
    CallAjaxMethod("/Authentication/ValidatePasswordAndDeleteUser", 'PUT', { p_Password: Password }).then(function (result) {
        if (result.Success) {
            ShowResult(result.Message);
            window.location.href = result.RedirectURL;
        }
        else {
            ShowResult(result.Message);
        }
    });

    //$.ajax(function () {
    //    type: 'POST',
    //        url : "Authentication/ValidatePassword",
    //            data: { p_Password: Password },
    //    dataType: 'json',
    //        success: function(result) {
    //            if (result == true) {
    //                alert("account deletion successful");
    //                window.location.replace("Home/Index");
    //            }
    //            else {
    //                $('#DeleteAccountValidate').text("Invalid Password");
    //            }
    //        }
    //});
}
