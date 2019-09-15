$(document).ready(function () {
    AddEventHandlers();
    InitializeForm('#cd-form-AddAdmin , #cd-form-RemoveAdmin');
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
        fn_FormReset('form');
    }
}

function AddAdminHandler() {
    CallAjaxMethod("/Analysis/AddOrRemoveAdmin", 'PUT', { __RequestVerificationToken : $('#cd-form-AddAdmin input[name="__RequestVerificationToken"]').val(), UserIds: $('.UserListAdd').val().toString(), IsAdmin: true }).then(function (result) {
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
}

function RemoveAdminHandler() {
    CallAjaxMethod("/Analysis/AddOrRemoveAdmin", 'PUT', { __RequestVerificationToken : $('#cd-form-RemoveAdmin input[name="__RequestVerificationToken"]').val(), UserIds: $('.UserListRemove').val().toString() , IsAdmin: false }).then(function (result) {
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
}
