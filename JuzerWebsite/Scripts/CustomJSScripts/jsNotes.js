$(document).ready(function () {
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
});