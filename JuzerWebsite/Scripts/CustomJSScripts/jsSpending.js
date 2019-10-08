var IncomeDataTable, ExpenseDataTable;
var Columns =

    $(document).ready(function () {
        AddEventHandlers();
        InitializeForm('#cd-form-Spending');
        fn_InitDataTable();
    });

function AddEventHandlers() {
    $("body").on('click', '.EditIncome', EditHandler);
    $("body").on('click', '.EditExpense', EditHandler);

    $("body").on('click', '.DeleteIncome', DeleteHandler);
    $("body").on('click', '.DeleteExpense', DeleteHandler);

    $("#AddIncome").on('click', OpenAddModalHandler);
    $("#AddExpense").on('click', OpenAddModalHandler);

    $(".cd-user-modal").on('click', CloseModalHandler);
    $("#btnAdd").on('click', AddHandler);
    CloseModalWhenEsc($('.cd-user-modal'));
}

function EditHandler() {
    var CurrentRowdata = DataTable.row($(this).parents('tr')).data();
    if ($(this).attr('class') == 'EditExpense') {
        fn_OpenModal(true);
    }
    else {
        fn_OpenModal(false);
    }
    $('#hdnEditId').val(CurrentRowdata.Id);
    $('#CreatedDate').val(CurrentRowdata.CreatedDate);
    $('#CategoryId').val(CurrentRowdata.CategoryId);
    $('#Amount').val(CurrentRowdata.Amount);
    $('#Note').val(CurrentRowdata.Note);
}

function DeleteHandler() {
    if ($(this).attr('class') == 'DeleteExpense') {
        $('#IsExpense').val(true);
    }
    else {
        $('#IsExpense').val(false);
    }
    var CurrentRowdata = DataTable.row($(this).parents('tr')).data();
    bootbox.confirm({
        message: $('#IsExpense').val() ? "Expense will be deleted.Are you sure?" : "Income will be deleted.Are you sure?",
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
                CallAjaxMethod("/Spending/DeleteExpenseOrIncome", 'POST', { Id: CurrentRowdata.Id, IsExpense: $('#IsExpense').val() }).then(function (result) {
                    ShowResult(result.Message);
                    fn_InitDataTable();
                });
            }
        }
    });
    $('.bootbox').removeClass("fade");
}

function OpenAddModalHandler() {
    var flag;
    if ($(this).attr('id') == 'AddExpense') {
        flag = true;
    }
    else {
        flag = false;
    }
    fn_OpenModal(flag);
}

function CloseModalHandler() {
    if ($(event.target).is($('.cd-user-modal')) || $(event.target).is('.cd-close-form')) {
        $('.cd-user-modal').removeClass('is-visible');
        fn_FormReset('form');
    }
}

function AddHandler() {
    if (fn_FormValidation('#cd-form-Spending')) {
        var SerializedArray = $('#cd-form-Spending').serializeArray();
        var SerializedObj = objectifyForm(SerializedArray);
        SerializedObj["Id"] = $('#hdnEditId').val() == "" ? null : $('#hdnEditId').val();
        SerializedObj["IsExpense"] = $('#IsExpense').val() == "" ? null : $('#IsExpense').val();
        CallAjaxMethod("/Spending/SaveExpenseOrIncome", 'POST', SerializedObj).then(function (result) {
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


function fn_OpenModal(IsExpense) {
    CallAjaxMethod('/Spending/GetCategoryList', 'POST', { IsExpense: IsExpense }).then(function (result) {
        $('#CategoryId').select2({
            data: result
        })
    });
    $('.cd-user-modal').addClass('is-visible');
    $('.cd-user-modal').find('#cd-Spending').addClass('is-selected');
    $('#CreatedDate').datepicker({
        dateFormat: "dd/M/yy",
        changeMonth: true,
        changeYear: true
    });
}

function DataTableInit() {
    CallAjaxMethod("/Spending/SaveExpenseOrIncome", 'GET').then(function (result) {
        ExpenseDataTable = $('#tblExpenseList').DataTable({
            data: result.ExpenseList,
            "columns": [
                {
                    "data": "Id",
                    "render": function (data, type, row) {
                        return '<a href="#"><i class="fa fa-pencil EditExpense" title="Edit" ></i></a>&nbsp;&nbsp;&nbsp;<a href="#"><i class="fa fa-trash DeleteExpense" title="Delete"></i></a>';
                    }
                },
                { "data": "CreatedDate" },
                { "data": "CategoryName" },
                { "data": "Amount" },
                { "data": "Note" }
            ]
        });

        IncomeDataTable = $('#tblIncomesList').DataTable({
            data: result.IncomeList,
            "columns": [
                {
                    "data": "Id",
                    "render": function (data, type, row) {
                        return '<a href="#"><i class="fa fa-pencil EditIncome" title="Edit" ></i></a>&nbsp;&nbsp;&nbsp;<a href="#"><i class="fa fa-trash DeleteIncome" title="Delete"></i></a>';
                    }
                },
                { "data": "CreatedDate" },
                { "data": "CategoryName" },
                { "data": "Amount" },
                { "data": "Note" }
            ]
        });
    });

}

function fn_AfterSave() {
    fn_InitDataTable();
    $('.cd-user-modal').removeClass('is-visible');
    fn_FormReset('#cd-form-Spending');
}

function fn_InitDataTable() {
    $('#tblIncomesList,#tblExpenseList').DataTable().destroy();
    DataTableInit();
}
