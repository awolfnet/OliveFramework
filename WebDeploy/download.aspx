﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="download.aspx.cs" Inherits="OliveFramework.download" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>下载</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <link rel="stylesheet" type="text/css" href="jeasyui/themes/metro/easyui.css">
    <link rel="stylesheet" type="text/css" href="jeasyui/themes/mobile.css">
    <link rel="stylesheet" type="text/css" href="jeasyui/themes/icon.css">
    <script type="text/javascript" src="jeasyui/jquery.min.js"></script>
    <script type="text/javascript" src="jeasyui/jquery.easyui.min.js"></script>
    <script type="text/javascript" src="jeasyui/jquery.easyui.mobile.js"></script>
</head>
<body>

    <div class="easyui-navpanel">
        <header>
            <div class="m-toolbar">
                <div class="m-title">同城建材</div>
            </div>
        </header>
        <div style="text-align: center">
            <img alt="" class="auto-style1" src="img/ic_launcher.png" style="width: 100%; height: 100%" /><br />
            <a href="https://app.awolf.net/download/app-release.apk" class="easyui-linkbutton" style="width: 30%; height: 40px;display:none"><span style="font-size: 16px">下载</span></a>
            <a href="http://app.awolf.net:60000/download/app-release.apk" class="easyui-linkbutton" style="width: 30%; height: 40px"><span style="font-size: 16px">下载</span></a>
            <a href="http://download.awolf.net/app-release.apk" class="easyui-linkbutton" style="width: 30%; height: 40px;display:none"><span style="font-size: 16px">下载</span></a>
            <br />
            <br />
            如果下载无效<br />
            请点右上角并选择<strong> 使用浏览器 </strong>打开
        </div>
    </div>


    <form id="form1" runat="server">
        <div style="text-align: center">
        </div>
    </form>
</body>
</html>
