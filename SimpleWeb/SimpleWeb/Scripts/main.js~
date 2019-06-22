//call at the start to check if user is login or not
function load_session_storage()
{
    var data = sessionStorage.getItem("simplewebauthorized");
    if (data != "99asd1a29asd9")
    {
        alert("Please log in to your account first");
        location.replace("/Views/login.html");
    }
    else
    {
        load_pages();
    }
}

//load all list function
function load_pages()
{
    $.get("/users/", function(counted){
            var j;
            while (page_numbers.firstChild)
                {
                    page_numbers.removeChild(page_numbers.firstChild);
                }
            for (j = 1; j<=counted;j++)
            {
                var btn = document.createElement("BUTTON");
                btn.innerHTML = j;
                btn.setAttribute("onclick","get_page_info('/users/',"+j+");");
                page_numbers.appendChild(btn);
            }
            get_page_info("/users/",1);
        });
}

//get 10 item from url with page number
function get_page_info(url,page)
{
    $.get(url+page, function(data){
                var i;
                while (table_body.firstChild)
                {
                    table_body.removeChild(table_body.firstChild);
                }
                for (i = 0;i<data.length;i++)
                {
                    var row = table_body.insertRow();
                    var cellu = row.insertCell();
                    var cellf = row.insertCell();
                    var celll = row.insertCell();
                    var celle = row.insertCell();
                    cellu.innerHTML = data[i]["username"];
                    cellf.innerHTML = data[i]["firstname"];
                    celll.innerHTML = data[i]["lastname"];
                    celle.innerHTML = data[i]["email"];
                }
            });
 }
 
 //searching function
 function search()
 {if (search_box.value == ""){load_pages();}
 else
    $.get("/users/search/"+search_box.value, function(counted){
            var j;
            while (page_numbers.firstChild)
                {
                    page_numbers.removeChild(page_numbers.firstChild);
                }
            for (j = 1; j<=counted;j++)
            {
                var btn = document.createElement("BUTTON");
                btn.innerHTML = j;
                btn.setAttribute("onclick","get_page_info('/users/search/"+search_box.value+"/',"+j+");");
                page_numbers.appendChild(btn);
            }
            get_page_info("/users/search/"+search_box.value+"/",1);
        });
 }
 
 //logout, clear storage session
 function logout()
 {
    sessionStorage.setItem("simplewebauthorized","");
    location.replace("/Views/login.html");
 }
 
 $(document).ajaxStart(function(){
  $(".wait").css("display", "block");
});

$(document).ajaxComplete(function(){
  $(".wait").css("display", "none");
});