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

    //$('.cd-switcher').on('click', function (event) {
    //    event.preventDefault();
    //    $(event.target).is($('.cd-switcher').children('li').eq(0).children('a')) ? OpenAddAdminModalHandler() : OpenRemoveAdminModalHandler();
    //});
});

function AddEventHandlers() {
    $("#AddAdmin").on('click', OpenAddAdminModalHandler);
    $("#RemoveAdmin").on('click', OpenRemoveAdminModalHandler);
    $(".cd-user-modal").on('click', CloseModalHandler);
    $("#btnAddAdmin").on('click', AddAdminHandler);
    $("#btnRemoveAdmin").on('click', RemoveAdminHandler);
    $('.cd-switcher').on('click',fn_SwitchTabHandler);
    CloseModalWhenEsc($('.cd-user-modal'));
}

function fn_SwitchTabHandler(event){
    event.preventDefault();
    $(event.target).is($('.cd-switcher').children('li').eq(0).children('a')) ? OpenAddAdminModalHandler() : OpenRemoveAdminModalHandler();
}

function OpenAddAdminModalHandler() {
    $('.cd-user-modal').addClass('is-visible');
    $('.cd-user-modal').find('#cd-AddAdmin').addClass('is-selected');
    $('.cd-user-modal').find('#cd-RemoveAdmin').removeClass('is-selected');
    $('.cd-switcher').children('li').eq(0).children('a').addClass('selected');
    $('.cd-switcher').children('li').eq(1).children('a').removeClass('selected');
    //$('.UserListAdd').select2({
    //    ajax: {
    //        url: '/Analysis/GetAddAdminList',
    //        dataType: 'json',
    //        type: "GET",
    //        processResults: function (data) {
    //            return {
    //                results: $.map(data, function (item) {
    //                    return {
    //                        id: item.UserId,
    //                        text: item.SignUpEmail
    //                    }
    //                })
    //            };
    //        }
    //    }
    //});
    CallAjaxMethod('/Analysis/GetAddAdminList', 'GET').then(function (result) {
        $('.UserListAdd').select2({
            data: result
        })
    });
}

function OpenRemoveAdminModalHandler() {
    $('.cd-user-modal').addClass('is-visible');
    $('.cd-user-modal').find('#cd-RemoveAdmin').addClass('is-selected');
    $('.cd-user-modal').find('#cd-AddAdmin').removeClass('is-selected');
    $('.cd-switcher').children('li').eq(1).children('a').addClass('selected');
    $('.cd-switcher').children('li').eq(0).children('a').removeClass('selected');
    $('.UserListRemove').select2();
}

function CloseModalHandler(event) {
    if ($(event.target).is($('.cd-user-modal')) || $(event.target).is('.cd-close-form')) {
        $('.cd-user-modal').removeClass('is-visible');
    }
}

function AddAdminHandler() {
    var UserIds = $('.UserListAdd').val().toString();
    CallAjaxMethod("/Analysis/AddOrRemoveAdmin", 'PUT', { UserIds: UserIds, IsAdmin: true }).then(function (result) {
        if (result.Success) {
            ShowResult(result.Message);
            setTimeout(function () {
                window.location.href = result.RedirectURL;
            }, 1500);
        }
        else {
            ShowResult(result.Message);
        }
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
    var UserIds = $('.UserListRemove').val().toString();
    CallAjaxMethod("/Analysis/AddOrRemoveAdmin", 'PUT', { UserIds: UserIds, IsAdmin: false }).then(function (result) {
        if (result.Success) {
            ShowResult(result.Message);
            setTimeout(function () {
                window.location.href = result.RedirectURL;
            }, 1500);
        }
        else {
            ShowResult(result.Message);
        }
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
