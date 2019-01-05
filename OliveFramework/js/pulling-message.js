function pullingMessage(userToken) {
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