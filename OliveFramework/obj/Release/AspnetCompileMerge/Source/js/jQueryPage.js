


function setBarVisable(bar, visable)
{
    var barName = "#header_" + bar + "_bar";
    log("Bar:", barName);
    if(visable)
    {
        $(barName).show();
    }else
    {
        $(barName).hide();
    }
}



