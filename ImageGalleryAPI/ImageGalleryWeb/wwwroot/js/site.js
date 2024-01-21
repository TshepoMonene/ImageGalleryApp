// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

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
        var actionUrl = 'Home/Upload';
        var sendData = new FormData();
        var fileUpload = $("#ImageData").get(0);
        var files = fileUpload.files;

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
                console.log(response);
                PlaceHolder.find('.modal').modal('hide')
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error(textStatus, errorThrown);
                
            }
        });
     
    })
})

function fileCheck()
{
 

}
