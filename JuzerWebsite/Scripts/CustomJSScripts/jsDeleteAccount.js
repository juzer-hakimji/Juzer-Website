$(document).ready(function () {
    AddEventHandlers();
});

function AddEventHandlers() {
    $("#btnDeleteAccnt").on('click', DeleteAccountHandler);
}

function DeleteAccountHandler() {
    var Password = $("#txtPass").val();
    CallAjaxMethod("Authentication/ValidatePassword", 'POST', { p_Password: Password }).then(function (result) {
        ShowResult(result);
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
