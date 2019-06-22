//front end logic check
function front_end_check()
{
    var result = true;
    if (user.value == "")
    {
        user_error.innerHTML = "Your username is empty";
        result = false;
    }
    else
        user_error.innerHTML = "";

    if (pwd.value == "")
    {
        pwd_error.innerHTML = "Your password is empty";
        result = false;
    }
    else
        pwd_error.innerHTML = "";
    
    return result;
}

//submit POST function, if correct then redirect
function submit_user()
{
    if (front_end_check())
    {
        var data = {"username":user.value,
                    "password":pwd.value,
                    };
        $.post("/users/login",data, function(data){
            if (data == "0")
            {
                sessionStorage.setItem("simplewebauthorized","99asd1a29asd9");
                sessionStorage.setItem("simplewebuser",user.value);
                location.replace("/Views/index.html");
            }
            else
            if (data == "1")
            {
                user_error.innerHTML = "You have entered wrong username";
            }
            else
            if (data == "2")
            {
                pwd_error.innerHTML = "You have entered wrong password";
            }
        });
    }
}

 $(document).ajaxStart(function(){
  $(".wait").css("display", "block");
});

$(document).ajaxComplete(function(){
  $(".wait").css("display", "none");
});