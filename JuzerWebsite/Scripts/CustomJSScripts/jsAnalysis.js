$(document).ready(function () {
    var $form_modal = $('.cd-user-modal'),
        $form_AddAdmin = $form_modal.find('#cd-AddAdmin'),
        $form_RemoveAdmin = $form_modal.find('#cd-RemoveAdmin'),
        //$tab_signup = $form_modal_tab.children('li').eq(1).children('a'),
        $AddAdminBtn = $('#btnAddAdmin');
    $RemoveAdminBtn = $('#btnRemoveAdmin');

    //open modal Add Admin
    $AddAdminBtn.on('click', function (event) {
        $form_modal.addClass('is-visible');
        $form_AddAdmin.addClass('is-selected');
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
    });

    //open modal Remove Admin
    $RemoveAdminBtn.on('click', function (event) {
        $form_modal.addClass('is-visible');
        $form_RemoveAdmin.addClass('is-selected');
        $('.UserListRemove').select2();
    });

    //close modal
    $('.cd-user-modal').on('click', function (event) {
        if ($(event.target).is($form_modal) || $(event.target).is('.cd-close-form')) {
            $form_modal.removeClass('is-visible');
        }
    });
    //close modal when clicking the esc keyboard button
    $(document).keyup(function (event) {
        if (event.which == '27') {
            $form_modal.removeClass('is-visible');
        }
    });

    $('#btnAddAdmin').on('click', function () {
        var UserIds = $('#UserList').val();
        $.ajax(function () {
            type: 'POST',
                url : "Analysis/AddOrRemoveAdmin",
                    data: { UserIds = UserIds, IsAdmin = true },
            dataType: 'json',
                success: function(result) {
                    if (result == true) {
                        alert("Admin Creation successful");
                        $form_modal.removeClass('is-visible');
                    }
                });
    });
    $('#btnRemoveAdmin').on('click', function () {
        var UserIds = $('#UserList').val();
        $.ajax(function () {
            type: 'POST',
                url : "Analysis/AddOrRemoveAdmin",
                    data: { UserIds = UserIds, IsAdmin = false },
            dataType: 'json',
                success: function(result) {
                    if (result == true) {
                        alert("Admin Deletion successful");
                        $form_modal.removeClass('is-visible');
                    }
                });
    });

});