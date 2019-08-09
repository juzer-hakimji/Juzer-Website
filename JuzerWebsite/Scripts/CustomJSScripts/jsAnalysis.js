$(document).ready(function () {
    AddEventHandlers();
    InitializeForm('#cd-form-AddAdmin , #cd-form-RemoveAdmin');

        //$tab_signup = $('.cd-user-modal')_tab.children('li').eq(1).children('a'),
    //    $AddAdminBtn = $('#AddAdmin');
    //$RemoveAdminBtn = $('#btnRemoveAdmin');

    //close modal
    //$('.cd-user-modal').on('click', function (event) {
    //if ($(event.target).is($('.cd-user-modal')) || $(event.target).is('.cd-close-form')) {
    //    $('.cd-user-modal').removeClass('is-visible');
    //}
    //});
    
});

function AddEventHandlers() {
    $("#AddAdmin").on('click', OpenAddAdminModalHandler);
    $("#RemoveAdmin").on('click', OpenRemoveAdminModalHandler);
    $(".cd-user-modal").on('click', CloseModalHandler);
    $("#btnAddAdmin").on('click', AddAdminHandler);
    $("#btnRemoveAdmin").on('click', RemoveAdminHandler);
    CloseModalWhenEsc();
}

function OpenAddAdminModalHandler() {
    $('.cd-user-modal').addClass('is-visible');
    $('.cd-user-modal').find('#cd-AddAdmin').addClass('is-selected');
    $('.UserListAdd').select2({
        ajax: {
            url: '/Analysis/GetAddAdminList',
            dataType: 'json',
            type: "GET",
            processResults: function (data) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            id: item.UserId,
                            text: item.Email
                        }
                    })
                };
            }
        }
    });
}

function OpenRemoveAdminModalHandler() {
    $('.cd-user-modal').addClass('is-visible');
    $('.cd-user-modal').find('#cd-RemoveAdmin').addClass('is-selected');
    $('.UserListRemove').select2();
}

function CloseModalHandler(event) {
    if ($(event.target).is($('.cd-user-modal')) || $(event.target).is('.cd-close-form')) {
        $('.cd-user-modal').removeClass('is-visible');
    }
}

function AddAdminHandler() {
    var UserIds = $('.UserListAdd').val();
    CallAjaxMethod("Analysis/AddOrRemoveAdmin", 'POST', { UserIds: UserIds, IsAdmin: true }).then(function (result) {
        ShowResult(result); // It will show Dashboard
    });
    //$.ajax(function () {
    //    type: 'POST',
    //        url : "Analysis/AddOrRemoveAdmin",
    //            data: { UserIds: UserIds, IsAdmin : true },
    //    dataType: 'json',
    //        success: function(result) {
    //            if (result == true) {
    //                alert("Admin Creation successful");
    //                $('.cd-user-modal').removeClass('is-visible');
    //            }
    //        }
    //});
}

function RemoveAdminHandler() {
    var UserIds = $('.UserListRemove').val();
    CallAjaxMethod("Analysis/AddOrRemoveAdmin", 'POST', { UserIds: UserIds, IsAdmin: false }).then(function (result) {
        ShowResult(result);
    });
    //$.ajax(function () {
    //    type: 'POST',
    //        url : "Analysis/AddOrRemoveAdmin",
    //            data: { UserIds: UserIds, IsAdmin : false },
    //    dataType: 'json',
    //        success: function(result) {
    //            if (result == true) {
    //                alert("Admin Deletion successful");
    //                $('.cd-user-modal').removeClass('is-visible');
    //            }
    //        });
}