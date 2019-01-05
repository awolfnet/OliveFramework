<%@ Page Title="" Language="C#" MasterPageFile="~/jeasyui.Master" AutoEventWireup="true" CodeBehind="SlideAdView.aspx.cs" Inherits="OliveFramework.SlideAdView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        * {
            margin: 0px;
            padding: 0px;
            font-size: 12px;
        }

        a {
            text-decoration: none;
            font-size: 12px;
        }

            a:link {
                color: #383455;
                font-size: 12px;
            }

            a:hover {
                color: #ff0000;
                font-size: 12px;
            }

            a:visited {
                color: #383455;
                font-size: 12px;
            }

        img {
            border: none;
        }

        .hl_main5_content {
            width: 898px;
            height: 155px;
            border-top: none;
            margin-left: 1px;
            margin: 100px auto;
        }

        .hl_main5_content1 {
            width: 838px;
            margin-top: 5px;
            overflow: hidden;
            float: left;
            margin-left: 15px;
        }

            .hl_main5_content1 ul {
                width: 1600px;
                overflow: hidden;
            }

                .hl_main5_content1 ul li {
                    float: left;
                    width: 200px;
                    display: inline;
                    border: 1px #FF0000 solid;
                    margin-right: 10px;
                }

                    .hl_main5_content1 ul li img {
                        width: 200px;
                    }

        .hl_scrool_leftbtn {
            width: 14px;
            height: 38px;
            background: #ccc url(hl_scroll_left.jpg) no-repeat;
            float: left;
            margin-top: 50px;
            cursor: pointer;
        }

        .hl_scrool_rightbtn {
            width: 14px;
            height: 38px;
            background: #ccc url(hl_scroll_right.jpg) no-repeat;
            float: right;
            margin-top: 50px;
            cursor: pointer;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        var flag = "left";
        function DY_scroll(wraper, prev, next, img, speed, or) {
            var wraper = $(wraper);
            var prev = $(prev);
            var next = $(next);
            var img = $(img).find('ul');
            var w = img.find('li').outerWidth(true);
            var s = speed;
            next.click(function () {
                img.animate({ 'margin-left': -w }, function () {
                    img.find('li').eq(0).appendTo(img);
                    img.css({ 'margin-left': 0 });
                });
                flag = "left";
            });
            prev.click(function () {
                img.find('li:last').prependTo(img);
                img.css({ 'margin-left': -w });
                img.animate({ 'margin-left': 0 });
                flag = "right";
            });
            if (or == true) {
                ad = setInterval(function () { flag == "left" ? next.click() : prev.click() }, s * 1000);
                wraper.hover(function () { clearInterval(ad); }, function () { ad = setInterval(function () { flag == "left" ? next.click() : prev.click() }, s * 1000); });
            }
        }
        DY_scroll('.hl_main5_content', '.hl_scrool_leftbtn', '.hl_scrool_rightbtn', '.hl_main5_content1', 3, true); // true为自动播放，不加此参数或false就默认不自动 
    </script>
    <div style="margin: 0 auto; width: 950px;">
        支持自动播放的开启与关闭,同时支持左右箭头的点击播放。主要是修改DY_scroll()里面的参数,第一个参数hl_main5_content是最外层的div的class，其次是左边按纽,右边按纽,包含图片的div,时间(控制速度的,值越小越快),是否自动播放。
    </div>


    <div class="hl_main5_content">
        <div class="hl_scrool_leftbtn"></div>
        <div class="hl_scrool_rightbtn"></div>
        <div class="hl_main5_content1">
            <ul>
                <li><a href="#">
                    <img src="upload/ad/01.jpg" style="height: 200px" /></a></li>
                <li><a href="#">
                    <img src="upload/ad/02.jpg" style="height: 200px" /></a></li>
                <li><a href="#">
                    <img src="upload/ad/03.jpg" style="height: 200px" /></a></li>
                <li><a href="#">
                    <img src="upload/ad/04.jpg " style="height: 200px" /></a></li>
            </ul>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FormContent" runat="server">
</asp:Content>
