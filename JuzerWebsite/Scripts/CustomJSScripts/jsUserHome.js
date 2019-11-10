var DataTable;

$(document).ready(function () {
    InitializeGraphs();
    DataTableInit();
});

function InitializeGraphs() {
    CallAjaxMethod("/UserHome/GetGraphData", 'GET').then(function (result) {
        var myDoughnutChart = new Chart($('#MonthChart'), {
            type: 'doughnut',
            data: {
                datasets: [{
                    data: [result.MonthVM.MonthIncome, result.MonthVM.MonthExpense ],
                    backgroundColor: ["rgb(54, 162, 235)","rgb(255, 99, 132)"]
                }],

                // These labels appear in the legend and in the tooltips when hovering different arcs
                labels: [
                    'Income',
                    'Expense'
                ]
            }
            , options: {
                responsive: true
                //, maintainAspectRatio: false
            }
        });

        var myDoughnutChart = new Chart($('#YearChart'), {
            type: 'doughnut',
            data: {
                datasets: [{
                    data: [result.YearVM.YearIncome, result.YearVM.YearExpense],
                    backgroundColor: ["rgb(54, 162, 235)", "rgb(255, 99, 132)"]
                }],

                // These labels appear in the legend and in the tooltips when hovering different arcs
                labels: [
                     'Income',
                    'Expense'
                ]
            }
            , options: {
                responsive: true
                //,maintainAspectRatio: false
            }
        });
    });
}

function DataTableInit() {
    DataTable = $('#tblImpNotesList').DataTable({
        "ajax": {
            url: "/UserHome/GetListData",
            dataSrc: ''
        },
        "columns": [
            { "data": "CreatedDate" },
            { "data": "Subject" },
            { "data": "NoteText" },
        ]
    });
}
