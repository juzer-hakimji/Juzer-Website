jQuery(document).ready(function ($) {
    $('.preloader').fadeOut(); // set duration in brackets    
    InitializeForm('#cd-form-Login,#cd-form-SignUp,#cd-form-ResetPass,#contact-form');
    var $form_modal = $('.cd-user-modal'),
        $form_login = $form_modal.find('#cd-login'),
        $form_signup = $form_modal.find('#cd-signup'),
        $form_forgot_password = $form_modal.find('#cd-reset-password'),
        $form_modal_tab = $('.cd-switcher'),
        $tab_login = $form_modal_tab.children('li').eq(0).children('a'),
        $tab_signup = $form_modal_tab.children('li').eq(1).children('a'),
        $forgot_password_link = $form_login.find('.cd-form-bottom-message a'),
        $back_to_login_link = $form_forgot_password.find('.cd-form-bottom-message a'),
        $main_nav = $('.UserPopUp');
    //open modal
    $main_nav.on('click', function (event) {
        //show modal layer
        $form_modal.addClass('is-visible');
        //show the selected form
        ($(event.target).is('.cd-signup')) ? signup_selected() : login_selected();
    });

    //close modal
    $('.cd-user-modal').on('click', function (event) {
        if ($(event.target).is($form_modal) || $(event.target).is('.cd-close-form')) {
            $form_modal.removeClass('is-visible');
            fn_FormReset('form');
        }
    });
    //close modal when clicking the esc keyboard button
    $(document).keyup(function (event) {
        if (event.which == '27') {
            $form_modal.removeClass('is-visible');
            fn_FormReset('form');
        }
    });

    //switch from a tab to another
    $form_modal_tab.on('click', function (event) {
        event.preventDefault();
        ($(event.target).is($tab_login)) ? login_selected() : signup_selected();
    });

    //hide or show password
    $('.hide-password').on('click', function () {
        var $this = $(this),
            $password_field = $this.prev('input');

        ('password' == $password_field.attr('type')) ? $password_field.attr('type', 'text') : $password_field.attr('type', 'password');
        ('Hide' == $this.text()) ? $this.text('Show') : $this.text('Hide');
        //focus and move cursor to the end of input field
        $password_field.putCursorAtEnd();
    });

    //show forgot-password form 
    $forgot_password_link.on('click', function (event) {
        event.preventDefault();
        forgot_password_selected();
    });

    //back to login from the forgot-password form
    $back_to_login_link.on('click', function (event) {
        event.preventDefault();
        login_selected();
    });

    function login_selected() {
        $form_login.addClass('is-selected');
        $form_signup.removeClass('is-selected');
        $form_forgot_password.removeClass('is-selected');
        $tab_login.addClass('selected');
        $tab_signup.removeClass('selected');
    }

    function signup_selected() {
        $form_login.removeClass('is-selected');
        $form_signup.addClass('is-selected');
        $form_forgot_password.removeClass('is-selected');
        $tab_login.removeClass('selected');
        $tab_signup.addClass('selected');
    }

    function forgot_password_selected() {
        $form_login.removeClass('is-selected');
        $form_signup.removeClass('is-selected');
        $form_forgot_password.addClass('is-selected');
    }

    //REMOVE THIS - it's just to show error messages 
    $form_login.find('input[type="submit"]').on('click', function (event) {
        event.preventDefault();
        $form_login.find('input[type="email"]').toggleClass('has-error').next('span').toggleClass('is-visible');
    });
    $form_signup.find('input[type="submit"]').on('click', function (event) {
        event.preventDefault();
        $form_signup.find('input[type="email"]').toggleClass('has-error').next('span').toggleClass('is-visible');
    });


    //IE9 placeholder fallback
    //credits http://www.hagenburger.net/BLOG/HTML5-Input-Placeholder-Fix-With-jQuery.html
    if (!Modernizr.input.placeholder) {
        $('[placeholder]').focus(function () {
            var input = $(this);
            if (input.val() == input.attr('placeholder')) {
                input.val('');
            }
        }).blur(function () {
            var input = $(this);
            if (input.val() == '' || input.val() == input.attr('placeholder')) {
                input.val(input.attr('placeholder'));
            }
        }).blur();
        $('[placeholder]').parents('form').submit(function () {
            $(this).find('[placeholder]').each(function () {
                var input = $(this);
                if (input.val() == input.attr('placeholder')) {
                    input.val('');
                }
            })
        });
    }


    //homene style

    (function ($) {

        "use strict";

        // PRE loader
        //$(window).on('load', function () {
        //    $('.preloader').fadeOut(); // set duration in brackets    
        //});


        //Navigation Section
        $('.navbar-collapse a').on('click', function () {
            $(".navbar-collapse").collapse('hide');
        });

        $(window).scroll(function () {
            if ($(".navbar").offset().top > 50) {
                $(".navbar-fixed-top").addClass("top-nav-collapse");
            } else {
                $(".navbar-fixed-top").removeClass("top-nav-collapse");
            }
        });


         //Smoothscroll js
        //$(function () {
        //    $('.custom-navbar a, #home a').bind('click', function (event) {
        //        var $anchor = $(this);
        //        $('html, body').stop().animate({
        //            scrollTop: $($anchor.attr('href')).offset().top - 49
        //        }, 1000);
        //        event.preventDefault();
        //    });
        //});


        // WOW Animation js
        new WOW({ mobile: false }).init();

    })(jQuery);
    //homene style end

    $('#btnLogin').on('click', function () {
        if (fn_FormValidation('#cd-form-Login')) {
            var SerializedObj = $('#cd-form-Login').serialize();
            $.ajax({
                type: 'POST',
                url: "/Authentication/Login",
                data: SerializedObj,
                dataType: 'json',
                success: function (result) {
                    if (result.Success == false) {
                        ShowResult(result.Message);
                        fn_FormReset('#cd-form-Login');
                    }
                    else if (result.Success == true) {
                        window.location.href = result.RedirectURL;
                    }
                }
            });
        }
        else {
            fn_ShowValidationErrors();
        }
    });

    $('#btnCrtAcct').on('click', function () {
        if (fn_FormValidation('#cd-form-SignUp')) {
            var SerializedObj = $('#cd-form-SignUp').serialize();
            ShowResult("Registering , Please Wait!");
            $.ajax({
                type: 'POST',
                url: "/User/Save",
                data: SerializedObj,
                dataType: 'json',
                success: function (result) {
                    if (result.Success) {
                        ShowResult(result.Message);
                        setTimeout(function () {
                            window.location.href = result.RedirectURL;
                        }, 1500);
                    }
                    else {
                        ShowResult(result.Message);
                    }
                }
            });
        }
        else {
            fn_ShowValidationErrors();
        }
    });

    $('#btnResetPass').on('click', function () {
        if (fn_FormValidation('#cd-form-ResetPass')) {
            $(".preloader").css("display", "flex");
            $.ajax({
                type: 'POST',
                url: "/User/SendResetPasswordEmail",
                data: { p_Email: $('#ResetEmail').val() },
                dataType: 'json',
                success: function (result) {
                    $(".preloader").css("display", "none");
                    if (result.Success) {
                        ShowResult(result.Message);
                        setTimeout(function () {
                            window.location.href = result.RedirectURL;
                        }, 5000);
                    }
                    else {
                        ShowResult(result.Message);
                    }
                }
            });
        }
        else {
            fn_ShowValidationErrors();
        }
    });

    $('.btnContact').on('click', function () {
        if (fn_FormValidation('#contact-form')) {
            var SerializedObj = $('#contact-form').serialize();
            $.ajax({
                type: 'POST',
                url: "/Home/ContactMessage",
                data: SerializedObj,
                dataType: 'json',
                success: function (result) {
                    if (result.Success) {
                        ShowResult(result.Message);
                        fn_FormReset('#contact-form');
                    }
                    else {
                        ShowResult(result.Message);
                    }
                }
            });
        }
    });

    //credits https://css-tricks.com/snippets/jquery/move-cursor-to-end-of-textarea-or-input/
    jQuery.fn.putCursorAtEnd = function () {
        return this.each(function () {
            // If this function exists...
            if (this.setSelectionRange) {
                // ... then use it (Doesn't work in IE)
                // Double the length because Opera is inconsistent about whether a carriage return is one character or two. Sigh.
                var len = $(this).val().length * 2;
                this.setSelectionRange(len, len);
            } else {
                // ... otherwise replace the contents with itself
                // (Doesn't work in Google Chrome)
                $(this).val($(this).val());
            }
        });
    }
});

function InitCountryDropdown() {
    $('.CountryList').select2();
}
