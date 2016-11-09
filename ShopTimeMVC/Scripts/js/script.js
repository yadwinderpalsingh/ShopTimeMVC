// Yadwinder Pal Singh
// Gurjit Singh Ghangura


$(document).ready(function () {

    // add item to cart
    $('.item .add-to-cart').on('click', function () {
        debugger;
        var recordToAdd = $(this).attr('data-id');

        if (recordToAdd != '') {

            // Perform the ajax post
            $.post("/addtocart", { "id": recordToAdd },
                function (data) {
                    // Update the page elements
                    if (data.CartCount > 0) {
                        $('.shop-badge .badge').text(data.CartCount);
                        $('.bottom-right').notify({
                            message: { text: data.Message }
                        }).show();
                    }
                }
            );
        }
    });


    // clear cart
    $('.btn-clear-cart').click(function () {
        $.post("/clearcart", 
            function (data) {
                window.location.href = data;
            }
        );
    });


    // update quantity and sub total
    $('.quantity-field').change(function () {
        var id = $(this).attr('data-id');
        var record = $(this).attr('data-recordId');
        var quantity = $(this).val();
        var parentElem = $(this).parent().parent();
        $.post("/updatecart", { "productId": id, "recordId": record, "count": quantity },
            function (data) {
                window.location.href = data;
            }
        );
    });

    // increment quantity
    $('.quantity-button.add').click(function () {
        if ($(this).prev('input').val() < 5) {
            $(this).prev('input').val(parseInt($(this).prev('input').val()) + 1);
            $(this).prev('input').trigger('change');
        }
        else {
            $('.bottom-right').notify({
                message: { text: 'You have reached the limit' }
            }).show();
        }
    });

    // decrement quantity
    $('.quantity-button.subtract').click(function () {
        if ($(this).next('input').val() > 1) {
            $(this).next('input').val(parseInt($(this).next('input').val()) - 1);
            $(this).next('input').trigger('change');
        }
    });

    // remove item from cart
    $(".shopping-cart .close").click(function () {
        // Get the id from the link
        var recordToDelete = $(this).attr("data-id");

        if (recordToDelete != '') {

            // Perform the ajax post
            $.post("/remove", { "id": recordToDelete },
                function (data) {
                    window.location.href = data;
                }
            );
        }
    });

    // billing information 


    var hasError = false;

    $('.checkout').click(function () {
        $('#contact-form').submit();
    });

    $('#contact-form .form-control').each(function () {
        if ($.trim($(this).val()) == '') {
            $(this).removeClass('input-filled');
        }
        else {
            $(this).addClass('input-filled');
        }
    });

    $('#contact-form .form-control').on('blur', function () {
        if ($.trim($(this).val()) == '') {
            $(this).removeClass('input-filled');
        }
        else {
            $(this).addClass('input-filled');
        }
    });

    $('#contact-form .form-control').on('focus', function () {
        $(this).parent('.controls').find('.error-message').fadeOut(300);
    });

    $('#contact-form').submit(function () {
        hasError = false;
        if ($('#contact-form').hasClass('clicked')) {
            return false;
        }
        $('#contact-form').addClass('clicked');
        $('#contact-form .error-message,#contact-form .contact-form-message').remove();
        $('.requiredField').each(function () {
            if ($.trim($(this).val()) == '') {
                var errorText = $(this).data('error-empty');
                $(this).next('label').append('<span class="error-message" style="display:none;">' + errorText + '.</span>').find('.error-message').fadeIn('fast');
                hasError = true;
            }
            if ($(this).attr('name') === 'email') {
                var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,6})?$/;
                if (!emailReg.test($.trim($(this).val()))) {
                    var invalidEmail = $(this).data('error-invalid');
                    $(this).next('label').append('<span class="error-message" style="display:none;">' + invalidEmail + '.</span>').find('.error-message').fadeIn('fast');
                    hasError = true;
                }
            }
            if ($(this).attr('name') === 'phone' && $.trim($(this).val()) != '') {
                var phoneReg = /^\d{3}-\d{3}-\d{4}$/;
                if (!phoneReg.test($.trim($(this).val()))) {
                    var invalidPhone = $(this).data('error-invalid');
                    $(this).next('label').append('<span class="error-message" style="display:none;">' + invalidPhone + '.</span>').find('.error-message').fadeIn('fast');
                    hasError = true;
                }
            }
            if ($(this).attr('name') === 'postalCode' && $.trim($(this).val()) != '') {
                var webReg = /^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$/;
                if (!webReg.test($.trim($(this).val()))) {
                    var invalidWeb = $(this).data('error-invalid');
                    $(this).next('label').append('<span class="error-message" style="display:none;">' + invalidWeb + '.</span>').find('.error-message').fadeIn('fast');
                    hasError = true;
                }
            }
        });
        if (hasError) {
            $('#contact-form').removeClass('clicked');
        }
        else {
            debugger;
            $.post("/checkout", $('#contact-form').serialize(), function (data) {
                window.location.href = data;
            });
        }
        return false;
    });
});