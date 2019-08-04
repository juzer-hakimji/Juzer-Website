$(document).ready(function () {

    $('#btnDeleteAccnt').on('click', function () {
        var Password = $("#txtPass").val();
        $.ajax(function () {
            type: 'POST',
                url : "Authentication/ValidatePassword",
                    data: { p_Password = Password },
                    dataType: 'json',
                    success: function(result) {
                        if (result == true) {
                            alert("account deletion successful");
                            window.location.replace("Home/Index");
                        }
                        else {
                            $('#DeleteAccountValidate').text("Invalid Password");
                        }
                });
    });

});