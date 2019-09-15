var DataTable;

$(document).ready(function () {
    AddEventHandlers();
    InitializeForm('#cd-form-Notes');
    DataTableInit();
});

function AddEventHandlers() {
    $("body").on('click', '.IsImp', ChangeNoteImportanceHandler);
    $("body").on('click', '.EditNote', EditNoteHandler);
    $("body").on('click', '.DeleteNote', DeleteNoteHandler);
    $("#AddNote").on('click', OpenAddNoteModalHandler);
    $(".cd-user-modal").on('click', CloseModalHandler);
    $("#btnAddNote").on('click', AddNoteHandler);
    CloseModalWhenEsc($('.cd-user-modal'));
}

function ChangeNoteImportanceHandler() {
    var CurrentRowdata = DataTable.row($(this).parents('tr')).data();
    if ($(this).children('i').attr("class") == 'fas fa-star') {
        //To mark not important
        CallAjaxMethod("/Notes/ChangeNoteImportance", 'PUT', { NoteId: CurrentRowdata.NoteId, IsImportant: false }).then(function (result) {
            ShowResult(result.Message);
        });
        $(this).children('i').attr("class", "far fa-star");
    }
    else {
        //To mark important
        CallAjaxMethod("/Notes/ChangeNoteImportance", 'PUT', { NoteId: CurrentRowdata.NoteId, IsImportant: true }).then(function (result) {
            ShowResult(result.Message);
        });
        $(this).children('i').attr("class", "fas fa-star");
    }
}

function EditNoteHandler() {
    var CurrentRowdata = DataTable.row($(this).parents('tr')).data();
    $('#hdnEditNoteId').val(CurrentRowdata.NoteId);
    $('#CreatedDate').val(CurrentRowdata.CreatedDate);
    $('#Subject').val(CurrentRowdata.Subject);
    $('#NoteText').val(CurrentRowdata.NoteText);
    fn_OpenModal();
}

function DeleteNoteHandler() {
    var CurrentRowdata = DataTable.row($(this).parents('tr')).data();
    bootbox.confirm({
        message: "Note will be deleted.Are you sure?",
        buttons: {
            confirm: {
                label: 'Yes',
                className: 'btn-success'
            },
            cancel: {
                label: 'No',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result == true) {
                CallAjaxMethod("/Notes/DeleteNote", 'PUT', { p_NoteId: CurrentRowdata.NoteId }).then(function (result) {
                    ShowResult(result.Message);
                    fn_InitDataTable();
                });
            }
        }
    });
    $('.bootbox').removeClass("fade");
}

function OpenAddNoteModalHandler() {
    fn_OpenModal();
}

function CloseModalHandler() {
    if ($(event.target).is($('.cd-user-modal')) || $(event.target).is('.cd-close-form')) {
        $('.cd-user-modal').removeClass('is-visible');
        fn_FormReset('form');
    }
}

function AddNoteHandler() {
    if (fn_FormValidation('#cd-form-Notes')) {
        var SerializedArray = $('#cd-form-Notes').serializeArray();
        var SerializedObj = objectifyForm(SerializedArray);
        SerializedObj["NoteId"] = $('#hdnEditNoteId').val() == "" ? null : $('#hdnEditNoteId').val();
        CallAjaxMethod("/Notes/SaveNote", 'POST', SerializedObj).then(function (result) {
            if (result.Success == true) {
                ShowResult(result.Message);
                fn_AfterSave();
            }
            else {
                ShowResult(result.Message);
            }
        });
    }
    else {
        fn_ShowValidationErrors();
    }
}


function fn_OpenModal() {
    $('.cd-user-modal').addClass('is-visible');
    $('.cd-user-modal').find('#cd-Notes').addClass('is-selected');
    $('#CreatedDate').datepicker({
        dateFormat: "dd/M/yy",
        changeMonth: true,
        changeYear: true
    });
}

function DataTableInit() {
    DataTable = $('#tblNotesList').DataTable({
        "ajax": {
            url: "/Notes/GetListData",
            dataSrc: ''
        },
        "columns": [
            {
                "data": "NoteId",
                "render": function (data, type, row) {
                    return '<a href="#"><i class="fa fa-pencil EditNote" title="Edit" ></i></a>&nbsp;&nbsp;&nbsp;<a href="#"><i class="fa fa-trash DeleteNote" title="Delete"></i></a>';
                }
            },
            {
                "data": "IsImportant",
                "render": function (IsImportant, type, row) {
                    var strData = "";
                    if (IsImportant) {
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

function fn_AfterSave() {
    fn_InitDataTable();
    $('.cd-user-modal').removeClass('is-visible');
    fn_FormReset('#cd-form-Notes');
}

function fn_InitDataTable() {
    $('#tblNotesList').DataTable().destroy();
    DataTableInit();
}
