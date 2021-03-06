﻿<%@ Page Title="" Language="C#" MasterPageFile="~/blank.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="OliveFramework.WebForm1" %>

<%@ MasterType VirtualPath="~/blank.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    <div class="col-sm-6">
        <section class="panel">
            <header class="panel-heading">
                <span class="label label-primary">Basic Table</span>
                <span class="tools pull-right">
                    <a href="javascript:;" class="fa fa-chevron-down"></a>
                    <a href="javascript:;" class="fa fa-times"></a>
                </span>
            </header>
            <table id="session" class="table">
                <thead>
                    <tr>
                        <th field="sid">Session ID</th>
                        <th field="aid">Account ID</th>
                        <th field="Token">Token</th>
                        <th field="LastActive">Last active</th>
                        <th field="ValidDay">Valid day</th>
                        <th field="CreateDay">Create day</th>
                    </tr>
                </thead>
            </table>
        </section>
    </div>

    <script type="text/javascript">
        function loadTableData(selector, dataUri) {
            var jqxhr = $.ajax(
                {
                    method: "GET",
                    url: dataUri,
                    dataType: "json"
                });

            jqxhr.done(function (data) {
                log("response", "done");
                log("response", data.success);
                if (data.success == true) {
                    var tableData = data.message;
                    var col = findTableHeader(selector);
                    var html = "<tbody>";


                    html += "</tbody>";
                    $(html).appendTo(selector);
                } else {

                }
            });

        }

        function findTableHeader(table)
        {
            var col = [];
            $(table).children("thead").each(function () {
                $(this).find("tr").each(function () {
                    $(this).find("th").each(function () {
                        var th = $(this);
                        col.push(th.attr("field"));
                    });
                });
            });

            return col;
        }

        function fillTable(tableData)
        {

        }
    </script>

    <div class="col-sm-6">
        <section class="panel">
            <header class="panel-heading">
                <span class="label label-primary">Striped Table</span>
                <span class="tools pull-right">
                    <a href="javascript:;" class="fa fa-chevron-down"></a>
                    <a href="javascript:;" class="fa fa-times"></a>
                </span>
            </header>
            <table class="table table-striped">
                <thead>
                    <tr>
                        <th>#</th>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Username</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td class="auto-style1">1</td>
                        <td class="auto-style1">Parth</td>
                        <td class="auto-style1">Jani</td>
                        <td class="auto-style1">@parth</td>
                    </tr>
                    <tr>
                        <td>2</td>
                        <td>Chintan</td>
                        <td>Pandya</td>
                        <td>@beyonder</td>
                    </tr>
                    <tr>
                        <td>3</td>
                        <td>Pruthvi</td>
                        <td>Bardolia</td>
                        <td>@Pruthvi</td>
                    </tr>
                </tbody>
            </table>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Javascript" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            log("Document ready", "Done");
            loadTableData("#session", "http://localhost:64974/method/GetSessionList.ashx?token=E8D738F8312DCAEB6F34618D0BC7F49F");
        });

    </script>
</asp:Content>
