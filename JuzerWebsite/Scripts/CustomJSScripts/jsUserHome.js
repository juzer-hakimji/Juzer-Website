var DataTable;

$(document).ready(function () {
    DataTableInit();
});

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
