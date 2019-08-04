var DataTable;
$(document).ready(function () {
    var $form_modal = $('.cd-user-modal'),
        $form_AddNote = $form_modal.find('#cd-Notes'),
        //$tab_signup = $form_modal_tab.children('li').eq(1).children('a'),
        $AddNoteBtn = $('#btnAddNote');

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

    DataTableInit();

    //Marked-unmarked Important
    $('.IsImp').on('click', function () {
        var CurrentRowdata = DataTable.row($(this).parents('tr')).data();
        if ($(this).children('i').attr("class") == 'fas fa-star') {
            //To mark not important
            $.ajax(function () {
                type: 'POST',
                    url : "Notes/ChangeNoteImportance",
                        data: { NoteId = CurrentRowdata[0], IsImportant = false },
                dataType: 'json',
                    success: function(result) {
                        if (result == true) {
                            alert("Note Marked as Important");
                        }
                    });
            $(this).children('i').attr("class", "far fa-star")
        }
        else {
            //To mark important
            $.ajax(function () {
                type: 'POST',
                    url : "Notes/ChangeNoteImportance",
                        data: { NoteId = CurrentRowdata[0], IsImportant = true },
                dataType: 'json',
                    success: function(result) {
                        if (result == true) {
                            alert("Note Marked as Not Important");
                        }
                    });
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

    //Delete Note
    $('.DeleteNote').on('click', function () {
        var CurrentRowdata = DataTable.row($(this).parents('tr')).data();
        var NoteId = CurrentRowdata[0];

        //Ask For Confirmation using confirm box
        $.ajax(function () {
            type: 'POST',
                url : "Notes/Delete",
                data: { p_NoteId = NoteId },
                        dataType : 'json',
                            success: function(result) {
                                if (result == true) {
                                    alert("Note Successfully deleted");
                                    DataTableInit();
                                }
                            });
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
    $form_AddNote.addClass('is-selected');
    $('#CreatedDate').datepicker({
        //dateFormat: "dd/M/yy",
        changeMonth: true,
        changeYear: true
        //yearRange: "-60:+0"
    });
}

function DataTableInit() {
    DataTable = $('#tblNotesList').DataTable({
        "ajax": {
            url: "/Notes/GetListData",
            dataSrc: ''
        },
        "columnDefs": [{
            "targets": 0,
            "data": null,
            "defaultContent": '<span class=""><i class="fa fa - pencil EditNote" title="Edit" ></i>&nbsp;<i class="fa fa - trash DeleteNote" title="Delete"></i></span>'
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
} 

//function signup_selected() {
//    $form_login.removeClass('is-selected');
//    $form_signup.addClass('is-selected');
//    $form_forgot_password.removeClass('is-selected');
//    $tab_login.removeClass('selected');
//    $tab_signup.addClass('selected');
//}