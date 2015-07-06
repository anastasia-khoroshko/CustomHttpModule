function EmailRegex(email) {
    var re = /^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$/;
    return email.match(re);
}
$(document).ready(function () {
    $('#btnlogin').click(function (event) {
        event.preventDefault();
        $('#modalLogin').css('display', 'block');
    })
    $('.close').click(function () {
        $('#modalLogin').css('display', 'none');
    })
    $('#btnlog').click(function () {
        var email = $('#email1').val();
        var password = $('#password1').val();
        if (EmailRegex(email)) {
            $.ajax(
               {
                   type: "POST",
                   url: "/ServiceLogic/Handlers/LoginHandler.ashx",
                   data: 'email=' + email + '&password=' + password,
                   contentType: 'application/x-www-form-urlencoded',
                   success: function () {
                       $('#modalLogin').css('display', 'none');
                       $('#userName').append(email);
                       $('#btnlogoff').css('display', 'block');
                       $('#btnlogin').css('display', 'none');
                   },
                   error: function () {
                       $("#error-message").html("Incorrect email or password!");
                       $('#info-message').html("");
                   }
               })
        }
        else
            $("#error-message").append("Incorrect email!");
    })
    $('#btn-register').click(function () {
        $('#modalLogin').css('display', 'none');
        $('#modalRegister').css('display', 'block');
    })

    $('#btnSave').click(function () {
        var email = $('#email2').val();
        var password = $('#password2').val();
        var confirmPassword=$('#password3').val();
        if (EmailRegex(email) && password==confirmPassword) {
            $.ajax(
                {
                    type: "POST",
                    url: "/ServiceLogic/Handlers/RegisterHandler.ashx",
                    data: 'email=' + email + '&password=' + password,
                    contentType: 'application/x-www-form-urlencoded',
                    success: function () {
                        $('#modalLogin').css('display', 'block');
                        $('#modalRegister').css('display', 'none');
                        $("#info-message").append("Account create successfully. Enter your data to sign in!");
                    },
                    error: function () {
                        $("#error-message1").html("Incorrect email or password!");
                    }
                })
        }
        else $("#error-message1").html("Incorrect email or password!");
    })

    $('#btnlogoff').click(function () {
        $.ajax(
           {
               type: "POST",
               data: null,
               url: "/ServiceLogic/Handlers/LogOffHandler.ashx",
               success: function () {
                   location.reload();
               }
           })
    })
})