// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Modal popup function
$(function () {
    var PlaceHolder = $('#PlaceHolder');
    $('button[data-toggle="ajax-modal"]').click(function (event){

        var url = $(this).data(url);
        $.get(url).done(function (data) {
            PlaceHolder.html(data);
            PlaceHolder.find('.modal').modal('show')
        })
    })

    PlaceHolder.on('click', '[data-save="modal"]', function (event) {
        event.preventDefault();
        var actionUrl = 'Upload';
        var sendData = new FormData();
        var fileUpload = $("#ImageData").get(0);
        var files = fileUpload.files;
        console.log(sendData);

        for (var i = 0; i < files.length; i++) {
            sendData.append('Files', files[i]);
        }

        $("input[type='text'").each(function (x, y) {
            sendData.append($(y).attr("name"), $(y).val());
        });

        $("textarea").each(function (x, y) {
            sendData.append($(y).attr("name"), $(y).val());
        });

       
        $.ajax({
            url: actionUrl,
            type: 'POST',
            enctype: 'multipart/form-data',
            data: sendData,
            processData: false,
            contentType: false,
            success: function (response) {
         
                PlaceHolder.find('.modal').modal('hide')
                window.location.reload();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                window.alert("Upload Unsucssefull")
                
            }
        });
     
    })
})

function fileCheck()
{
   
  
}

//Login functionn

$(function ()
{
    $("#signIn").click(function (event) {
        event.preventDefault()

        var actionUrl = 'Login/Login';
        var formData = new FormData();

        formData.append("username", $("#username").val());
        formData.append("password", $("#password").val());

        console.log(formData)

        $.ajax({
            url: actionUrl,
            enctype: 'multipart/form-data',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            
            success: function (response) {
              //  console.log(response)
               location.href = '/Home/Index';
            },
            error: function (jqXHR, textStatus, errorThrown) {
               console.error(textStatus, errorThrown);

            }
        });

    })



})






