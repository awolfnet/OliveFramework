function loadUserprofile(userToken) {
    var jqxhr = $.ajax(
            {
                method: "POST",
                url: "method/GetUserprofile.ashx",
                data: { token: userToken },
                dataType: "json"
            });

    jqxhr.done(function (data) {
        log("Response", data);
        if (data.success == true) {
            log("Nick", data.message.Nick);
            log("Avatar", data.message.Avatar);

            $('.username').text(data.message.Nick).show();
            $('#avatar').attr({ src: data.message.Avatar });


        } else {

        }
    });
}

function loadSystemConfig(userToken) {
    var jqxhr = $.ajax(
            {
                method: "POST",
                url: "method/GetSystemConfig.ashx",
                data: { token: userToken },
                dataType: "json"
            });

    jqxhr.done(function (data) {
        log("Response", data);
        if (data.success == true) {
            $(document).attr("title", data.message.WindowTitle);
        } else {

        }
    });
}

function pullMessage(userToken) {
    var jqxhr = $.ajax(
    {
        method: "POST",
        url: "method/PullMessage.ashx",
        data: { token: userToken },
        dataType: "json"
    });

    jqxhr.done(function (data) {
        log("Response", data);
        if (data.success == true) {


        } else {

        }
    });
}