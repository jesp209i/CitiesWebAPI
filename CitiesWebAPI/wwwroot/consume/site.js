$(document).ready(function () {
    $("a").click(function (event) {
        event.preventDefault();
        var webApiUrl = $(this).attr('href');
        var cityidbox = $('#cityid').val();
        var webApiUrlAddon;
        if ($("#poi").prop('checked')) {
            webApiUrlAddon = "?getPointOfInterest=true";
        } else {
            webApiUrlAddon = "?getPointOfInterest=false";
        }
        var doneurl;
        if (cityidbox > 0) {
            doneurl = webApiUrl + '/' + cityidbox + webApiUrlAddon;
        }
        else {
            doneurl = webApiUrl + webApiUrlAddon;
        }
        //console.log(doneurl + ' cityidbox:' + cityidbox);
        $.ajax({
            url: doneurl,
            type: 'GET',
            contentType: 'application/json',
            accept: 'application/json'
        }).then(function (response) {
            console.log(response);
            $("#main-content").html(JSON.stringify(response));
        });
    });

    $("#formularknap").click(function (event) {
        event.preventDefault();
        webApiUrl = $("#formular").attr('action');
        var cityname = $("#cityname").val();
        var citydescription = $("#citydescription").val();
        console.log(cityname + ' ' + citydescription);
        $.ajax({
            url: webApiUrl,
            type: 'POST',
            data: JSON.stringify({ name: cityname, description: citydescription }),
            contentType: 'application/json'
        }).then(function (response) {
            console.log(response);
            $("#main-content").html(JSON.stringify(response));
        });
    });



});
