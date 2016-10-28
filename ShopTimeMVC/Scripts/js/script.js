// yadwinder Pal Singh
// Gurjit Singh Ghangura


$(document).ready(function () {
       
    // add item to cart
    $('.item .add-to-cart').on('click', function () {
        var id = $(this).attr('data-id');
        var url = '../ajax.php?id=' + id + '&action=add';
        $.ajax({
            type: 'GET',
            url: url,
            success: function (data) {
                $('.shop-badge .badge').text(data);
            }
        }).done(function () {
            toastr.options = {
                'positionClass': 'toast-bottom-right'
            }, toastr.info('Successfully added to cart!');
        });
    });
    
    
    // clear cart
    $('.btn-clear-cart').click(function () {
        var url = '../ajax.php?action=clearcart';
        $.ajax({
            type: 'GET',
            url: url
        }).done(function () {
            window.location.reload();
        });
    });
    
    
    // update quantity and sub total
    $('.quantity-field').change(function () {
        var id = $(this).attr('data-id');
        var quantity = $(this).val();
        var parentElem = $(this).parent().parent();
        var url = '../ajax.php?id=' + id + '&quantity=' + quantity + '&action=update';
        $.ajax({
            type: 'GET',
            url: url,
            success: function (data) {
                data = JSON.parse(data);
                parentElem.find('.shop-red').html('$' + data.price);
                $('.total-result-in').html('$ ' + data.subTotal);
            }
        }).done(function () {
            toastr.info('Successfully added to cart!');
        });
    });
    
    // increment quantity
    $('.quantity-button.add').click(function () {
        if($(this).prev('input').val() < 5) {
            $(this).prev('input').val(parseInt($(this).prev('input').val()) + 1);
            $(this).prev('input').trigger('change');
        }
        else{
            toastr.info('You have reached the limit');
        }
    });
    
    // decrement quantity
    $('.quantity-button.subtract').click(function () {
        if($(this).next('input').val() > 1) {
            $(this).next('input').val(parseInt($(this).next('input').val()) - 1);
            $(this).next('input').trigger('change');
        }
    });
    
    // remove item from cart
    $('.shopping-cart .close').click(function () {
        var id = $(this).attr('data-id');
        var url = '../ajax.php?id=' + id + '&action=delete';
        $.ajax({
            type: 'GET',
            url: url
        }).done(function () {
            window.location.reload();
        });
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
            
            // invoice for user 
            $('.invoice-info #name').append($('#first-name').val() + ' ' + $('#last-name').val());
            $('.invoice-info #email').append($('#email').val());
            $('.invoice-info #phone').append($('#phone').val());
            $('.invoice-info #address').append($('#address').val());
            $('.invoice-info #city').append($('#city').val());
            $('.invoice-info #province').append($('#province').val());
            $('.invoice-info #postal-code').append($('#postal-code').val());
            
            var subTotal = parseFloat($('#sub-total').attr('data-total')).toFixed(2);
            
            var tax;
            if ($('#province').val() == 'Alberta') {
                tax = (subTotal * 0.05).toFixed(2);
            }
            else if ($('#province').val() == 'British Columbia') {
                tax = (subTotal * 0.05).toFixed(2);
            }
            else if ($('#province').val() == 'Manitoba') {
                tax = (subTotal * 0.05).toFixed(2);
            }
            else if ($('#province').val() == 'New Brunswick') {
                tax = (subTotal * 0.13).toFixed(2);
            }
            else if ($('#province').val() == 'Newfoundland And Labrador') {
                tax = (subTotal * 0.13).toFixed(2);
            }
            else if ($('#province').val() == 'Nova Scotia') {
                tax = (subTotal * 0.15).toFixed(2);
            }
            else if ($('#province').val() == 'Ontario') {
                tax = (subTotal * 0.13).toFixed(2);
            }
            else if ($('#province').val() == 'Prince Edward Island') {
                tax = (subTotal * 0.14).toFixed(2);
            }
            else if ($('#province').val() == 'Quebec') {
                tax = (subTotal * 0.05).toFixed(2);
            }
            else if ($('#province').val() == 'Saskatchewan') {
                tax = (subTotal * 0.05).toFixed(2);
            }
            else if ($('#province').val() == 'Yukon') {
                tax = (subTotal * 0.05).toFixed(2);
            }
            
            $('#tax').append('$ ' + tax);
            
            var shipping = 0,
                deliveryDay = 0;
            
            if (subTotal > 0 && subTotal < 25) {
                shipping = 3.00;
                deliveryDay = 1;
            }
            else if (subTotal > 25.01 && subTotal < 50) {
                shipping = 4.00;
                deliveryDay = 3;
            }
            else if (subTotal > 50.01 && subTotal < 75) {
                shipping = 5.00;
                deliveryDay = 1;
            }
            else if (subTotal > 75) {
                shipping = 6.00;
                deliveryDay = 4;
            }
            
            var date = new Date();
            
            date.setDate(date.getDate() + deliveryDay);
            
            $('#del-date').append(date.getFullYear() + '/' + (date.getMonth() < 10 ? '0' : '') + date.getMonth() + '/' + (date.getDay() < 10 ? '0' : '') + date.getDay());
            
            $('#shipping').append('$ ' + parseFloat(shipping).toFixed(2));
            var total = parseFloat(tax) + parseFloat(subTotal) + parseFloat(shipping);
            $('#total').append('$ ' + total.toFixed(2));
            $('.billing-section').addClass('hidden');
            $('.thank-you').removeClass('hidden');
            
            endShop();
        }
        return false;
    });
});

function endShop(){
    var url = '../ajax.php?action=clearcart';
    $.ajax({
        type: 'GET',
        url: url
    }).done(function () {
        $('.shop-badge .badge').text('');
    });
}