﻿<%@ Page Title="" Language="C#" MasterPageFile="~/jeasyui.Master" AutoEventWireup="true" CodeBehind="QRCodeShare.aspx.cs" Inherits="OliveFramework.QRCodeShare" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 280px;
            height: 280px;
        }

        body {
            margin: 0;
            padding: 0;
            height:100%;
            width:100%;
        }
        .style2 {
      position: relative;
      top: 8%;
      transform: translateY(8%);
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

    <div style="text-align: center">
        <div class="style2">
        <img alt="" class="auto-style1" src="img/ic_launcher.png" style="width: 40%; height: 40%" /><br />
            <br />
        <img alt="同城建材" class="auto-style1" src="download/download.png" style="height: 80%; width: 80%" />
            <h1>024-88785320</h1>
            </div>
        
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FormContent" runat="server">
</asp:Content>
