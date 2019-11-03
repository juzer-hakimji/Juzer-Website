var IncomeDataTable, ExpenseDataTable, IsExpense;

$(document).ready(function () {
    AddEventHandlers();
    InitializeForm('#cd-form-Spending');
    fn_InitDataTable();
    //$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
    //    //$.fn.dataTable.tables({ visible: true, api: true }).columns.adjust();
    //    ExpenseDataTable.columns.adjust();
    //});
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
    if ($(this).attr('class').includes("EditExpense")) {
        fn_SetIsExpense(true);
        var CurrentRowdata = ExpenseDataTable.row($(this).parents('tr')).data();
        fn_OpenModal(CurrentRowdata.CategoryId);
    }
    else {
        fn_SetIsExpense(false);
        var CurrentRowdata = IncomeDataTable.row($(this).parents('tr')).data();
        fn_OpenModal(CurrentRowdata.CategoryId);
    }
    $('#hdnEditId').val(CurrentRowdata.Id);
    $('#CreatedDate').val(CurrentRowdata.CreatedDate);
    //$('#CategoryId').val(CurrentRowdata.CategoryId).trigger('change.select2');
    $('#Amount').val(CurrentRowdata.Amount);
    $('#Note').val(CurrentRowdata.Note);
}

function DeleteHandler() {
    if ($(this).attr('class').includes("DeleteExpense")) {
        fn_SetIsExpense(true);
        //IsExpense = true;
        //$('#IsExpense').val(true);
        var CurrentRowdata = ExpenseDataTable.row($(this).parents('tr')).data();
    }
    else {
        //IsExpense = false;
        fn_SetIsExpense(false);
        //$('#IsExpense').val(false);
        var CurrentRowdata = IncomeDataTable.row($(this).parents('tr')).data();
    }
    bootbox.confirm({
        message: IsExpense ? "Expense will be deleted.Are you sure?" : "Income will be deleted.Are you sure?",
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
                CallAjaxMethod("/Spending/DeleteExpenseOrIncome", 'POST', { Id: CurrentRowdata.Id, IsExpense: IsExpense }).then(function (result) {
                    ShowResult(result.Message);
                    fn_InitDataTable();
                });
            }
        }
    });
    $('.bootbox').removeClass("fade");
}

function OpenAddModalHandler() {
    //var flag;
    if ($(this).attr('id') == 'AddExpense') {
        fn_SetIsExpense(true);
        //IsExpense = true;
        //flag = true;
    }
    else {
        fn_SetIsExpense(false);
        //IsExpense = false;
        //flag = false;
    }
    fn_OpenModal();
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
        //SerializedObj["IsExpense"] = $('#IsExpense').val() == "" ? null : $('#IsExpense').val();
        SerializedObj["IsExpense"] = IsExpense;
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

function fn_OpenModal(CategoryId) {
    $('#CategoryId').empty();
    CallAjaxMethod('/Spending/GetCategoryList', 'POST', { IsExpense: IsExpense }).then(function (result) {
        $('#CategoryId').select2({
            data: result
        })
        $('#CategoryId').val(CategoryId).trigger('change.select2');
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
    CallAjaxMethod("/Spending/GetListData", 'GET').then(function (result) {
        ExpenseDataTable = $('#tblExpenseList').DataTable({
            "bAutoWidth": false,
            data: result.ExpenseList,
            "columns": [
                {
                    "data": "Id",
                    "render": function (data, type, row) {
                        return '<a href="#"><i class="fa fa-pencil EditExpense" title="Edit" ></i></a>&nbsp;&nbsp;&nbsp;<a href="#"><i class="fa fa-trash DeleteExpense" title="Delete"></i></a>';
                    }
                },
                { "data": "CreatedDate" },
                {
                    "data": "CategoryId",
                    "render": function (data, type, row) {
                        return row.CategoryName;
                    }
                },
                //{ "data": "CategoryName" },
                { "data": "Amount" },
                { "data": "Note" }
                //,
                //{ "data": "CategoryId" }
            ]
        });

        IncomeDataTable = $('#tblIncomesList').DataTable({
            "bAutoWidth": false,
            data: result.IncomeList,
            "columns": [
                {
                    "data": "Id",
                    "render": function (data, type, row) {
                        return '<a href="#"><i class="fa fa-pencil EditIncome" title="Edit" ></i></a>&nbsp;&nbsp;&nbsp;<a href="#"><i class="fa fa-trash DeleteIncome" title="Delete"></i></a>';
                    }
                },
                { "data": "CreatedDate" },
                {
                    "data": "CategoryId",
                    "render": function (data, type, row) {
                        return row.CategoryName;
                    }
                },
                //{ "data": "CategoryName" },
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

function fn_SetIsExpense(p_IsExpense) {
    IsExpense = p_IsExpense;
}