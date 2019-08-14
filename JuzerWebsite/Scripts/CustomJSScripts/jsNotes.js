var DataTable;

$(document).ready(function () {
    AddEventHandlers();
    InitializeForm('#cd-form-Notes');
    DataTableInit();

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


    //open modal
    //$AddNoteBtn.on('click', function (event) {

       
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
    //});

    //close modal
    //$('.cd-user-modal').on('click', function (event) {
    //   
    //});

});

function AddEventHandlers() {
    $(".IsImp").on('click', ChangeNoteImportanceHandler);
    $(".EditNote").on('click', EditNoteHandler);
    $(".DeleteNote").on('click', DeleteNoteHandler);
    $("#AddNote").on('click', OpenAddNoteModalHandler);
    $(".cd-user-modal").on('click', CloseModalHandler);
    $("#btnAddNote").on('click', AddNoteHandler);
    CloseModalWhenEsc();
}

function ChangeNoteImportanceHandler() {
    var CurrentRowdata = DataTable.row($(this).parents('tr')).data();
    if ($(this).children('i').attr("class") == 'fas fa-star') {
        //To mark not important
        CallAjaxMethod("Notes/ChangeNoteImportance", 'POST', { NoteId: CurrentRowdata[0], IsImportant: false }).then(function (result) {
            ShowResult(result.Message);
        });
        //$.ajax(function () {
        //    type: 'POST',
        //        url : "Notes/ChangeNoteImportance",
        //            data: { NoteId: CurrentRowdata[0], IsImportant : false },
        //    dataType: 'json',
        //        success: function(result) {
        //            if (result == true) {
        //                alert("Note Marked as Important");
        //            }
        //        }
        //});
        $(this).children('i').attr("class", "far fa-star")
    }
    else {
        //To mark important
        CallAjaxMethod("Notes/ChangeNoteImportance", 'POST', { NoteId: CurrentRowdata[0], IsImportant: true }).then(function (result) {
            ShowResult(result);
        });
        //$.ajax(function () {
        //    type: 'POST',
        //        url : "Notes/ChangeNoteImportance",
        //            data: { NoteId: CurrentRowdata[0], IsImportant : true },
        //    dataType: 'json',
        //        success: function(result) {
        //            if (result == true) {
        //                alert("Note Marked as Not Important");
        //            }
        //        }
        //});
        $(this).children('i').attr("class", "fas fa-star")
    }
}

function EditNoteHandler() {
    var CurrentRowdata = DataTable.row($(this).parents('tr')).data();
    $('#hdnEditNoteId').val(CurrentRowdata[0]);
    $('#CreatedDate').val(CurrentRowdata[2]);
    $('#Subject').val(CurrentRowdata[3]);
    $('#NoteText').val(CurrentRowdata[4]);
    fn_OpenModal();
}

function DeleteNoteHandler() {
    var CurrentRowdata = DataTable.row($(this).parents('tr')).data();
    var NoteId = CurrentRowdata[0];

    //Ask For Confirmation using confirm box

    CallAjaxMethod("Notes/Delete", 'POST', { p_NoteId: NoteId }).then(function (result) {
        ShowResult(result.Message);
    });
    //$.ajax(function () {
    //    type: 'POST',
    //        url : "Notes/Delete",
    //            data: { p_NoteId: NoteId },
    //    dataType: 'json',
    //        success: function(result) {
    //            if (result == true) {
    //                alert("Note Successfully deleted");
    //                DataTableInit();
    //            }
    //        }
    //});
}

function OpenAddNoteModalHandler() {
    fn_OpenModal();
}

function CloseModalHandler() {
    if ($(event.target).is($('.cd-user-modal')) || $(event.target).is('.cd-close-form')) {
            $('.cd-user-modal').removeClass('is-visible');
        }
}

function AddNoteHandler() {
    var formValid = $("#cd-form-Notes").validate().form();
    if (!formValid) return false;
    //var SerializedArray = $('#cd-form-Notes').serializeArray();
    //var SerializedObj = objectifyForm(SerializedArray);
    var SerializedObj = $('#cd-form-Notes').serialize();
    SerializedObj["NoteId"] = $('#hdnEditNoteId').val() == "" ? null : $('#hdnEditNoteId').val();
    CallAjaxMethod("Notes/Save", 'POST', SerializedObj).then(function (result) {
        ShowResult(result.Message);
    });
    //$.ajax(function () {
    //    type: 'POST',
    //        url : "Notes/Save",
    //            data: SerializedObj,
    //                dataType : 'json',
    //                    success: function(result) {
    //                        if (result == false) {
    //                            $('#NotesSaveValidate').text('Invalid Note Details');
    //                            $('.cd-user-modal').removeClass('is-visible');
    //                        }
    //                    }
    //});
}


function fn_OpenModal() {
    $('.cd-user-modal').addClass('is-visible');
    $('.cd-user-modal').find('#cd-Notes').addClass('is-selected');
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