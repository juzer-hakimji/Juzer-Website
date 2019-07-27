$(document).ready(function () {
    var $form_modal = $('.cd-user-modal'),
        $form_signup = $form_modal.find('#cd-signup'),
        //$tab_signup = $form_modal_tab.children('li').eq(1).children('a'),
        $AddNoteBtn = $('#AddNote');

    $('#tblNotesList').DataTable({
        "ajax": {
            url: "/Notes/GetListData",
            dataSrc: ''
        },
        "columns": [
            { "data": "NoteId" },
            { "data": "IsImportant" },
            { "data": "CreatedDate" },
            { "data": "Subject" },
            { "data": "NoteText" },
        ]
    });


    //open modal
    $AddNoteBtn.on('click', function (event) {
        $form_modal.addClass('is-visible');
        $form_signup.addClass('is-selected');
        fn_SetDatePicker();
        //if ($(event.target).is($main_nav)) {
        //    // on mobile open the submenu
        //    $(this).children('ul').toggleClass('is-visible');
        //} else {
        // on mobile close submenu
        //$main_nav.children('ul').removeClass('is-visible');
        //show modal layer
        //show the selected form
        //($(event.target).is('.cd-signup')) ? signup_selected() : login_selected();
        //$form_login.removeClass('is-selected');
        //$form_forgot_password.removeClass('is-selected');
        //$tab_login.removeClass('selected');
        //$tab_signup.addClass('selected');
    });

    //close modal
    $('.cd-user-modal').on('click', function (event) {
        if ($(event.target).is($form_modal) || $(event.target).is('.cd-close-form')) {
            $form_modal.removeClass('is-visible');
        }
    });
    //close modal when clicking the esc keyboard button
    $(document).keyup(function (event) {
        if (event.which == '27') {
            $form_modal.removeClass('is-visible');
        }
    });

    $('#btnAddNote').on('click', function () {
        var formValid = $("#cd-form-Notes").validate().form();
        if (!formValid) return false;
        var SerializedArray = $('#cd-form-Notes').serializeArray();
        var SerializedObj = objectifyForm(SerializedArray);
        $.ajax(function () {
            type: 'POST',
                url : "Notes/Save",
                    data: SerializedObj,
                        dataType : 'json',
                            success: function(result) {
                                if (result == false) {
                                    $('#NotesSaveValidate').text('Invalid Note Details');
                                    $form_modal.removeClass('is-visible');
                                }
                            });
    });
});

function fn_SetDatePicker() {
    $('#CreatedDate').datepicker({
        //dateFormat: "dd/M/yy",
        changeMonth: true,
        changeYear: true
        //yearRange: "-60:+0"
    });
}

//function signup_selected() {
//    $form_login.removeClass('is-selected');
//    $form_signup.addClass('is-selected');
//    $form_forgot_password.removeClass('is-selected');
//    $tab_login.removeClass('selected');
//    $tab_signup.addClass('selected');
//}