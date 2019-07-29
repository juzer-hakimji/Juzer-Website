var DataTable;
$(document).ready(function () {
    var $form_modal = $('.cd-user-modal'),
        $form_signup = $form_modal.find('#cd-signup'),
        //$tab_signup = $form_modal_tab.children('li').eq(1).children('a'),
        $AddNoteBtn = $('#AddNote');

    //DataTable = $('#tblNotesList').DataTable({
    //    "ajax": {
    //        url: "/Notes/GetListData",
    //        dataSrc: ''
    //    },
    //    "columns": [
    //        {
    //            "data": "Action",
    //            "render": function (data, type, row) {
    //                var strData = "";
    //                strData += '<span><span class="EditNote" data-id='+data.NoteId+'><i class="fa fa - pencil" title="Edit" ></i></span>&emsp;'

    //                if (data.IsImportant == true) {
    //                    strData += '<span class="IsImp"><i class="fas fa-star" title="Mark Important"></i></span></span>'
    //                }
    //                else {
    //                    strData += '<span class="IsImp"><i class="far fa-star" title="Mark Important"></i></span></span>'
    //                }
    //                return strData;
    //            }
    //        },
    //        { "data": "CreatedDate" },
    //        { "data": "Subject" },
    //        { "data": "NoteText" },
    //    ]
    //});

    DataTable = $('#tblNotesList').DataTable({
        "ajax": {
            url: "/Notes/GetListData",
            dataSrc: ''
        },
        "columnDefs": [{
            "targets": 0,
            "data": null,
            "defaultContent": '<span class="EditNote"><i class="fa fa - pencil" title="Edit" ></i></span>'
        }],
        "columns": [
            { "data": "NoteId" },
            {
                "data": "IsImportant",
                "render": function (data, type, row) {
                    var strData = "";
                    if (data.IsImportant == true) {
                        strData = '<span class="IsImp"><i class="fas fa-star" title="Mark Important"></i></span>';
                    }
                    else {
                        strData = '<span class="IsImp"><i class="far fa-star" title="Mark Important"></i></span>';
                    }
                    return strData;
                }
            },
            { "data": "CreatedDate" },
            { "data": "Subject" },
            { "data": "NoteText" },
        ]
    });

    //Marked-unmarked Important
    $('.IsImp').on('click', function () {
        if ($(this).children('i').attr("class") == 'fas fa-star') {
            //ajax call to mark important
            $(this).children('i').attr("class","far fa-star")
        }
        else {
            //ajax call to mark unimportant
            $(this).children('i').attr("class", "fas fa-star")
        }
    });

    //open modal for edit
    $('.EditNote').on('click', function () {
        var CurrentRowdata = DataTable.row($(this).parents('tr')).data();
        $('#hdnEditNoteId').val(CurrentRowdata[0]);
        $('#CreatedDate').val(CurrentRowdata[2]);
        $('#Subject').val(CurrentRowdata[3]);
        $('#NoteText').val(CurrentRowdata[4]);
        fn_OpenModal();
    });


    //open modal
    $AddNoteBtn.on('click', function (event) {

        fn_OpenModal();
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
        SerializedObj["NoteId"] = $('#hdnEditNoteId').val() == "" ? null : $('#hdnEditNoteId').val();
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

function fn_OpenModal() {
    $form_modal.addClass('is-visible');
    $form_signup.addClass('is-selected');
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