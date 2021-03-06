﻿<%@ Page Title="" Language="C#" MasterPageFile="~/jeasyui.Master" AutoEventWireup="true" CodeBehind="AdManager.aspx.cs" Inherits="OliveFramework.AdManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>广告管理</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <!-- 全局变量 -->
    <script type="text/javascript">

    </script>


    <!-- 表格 -->
    <table id="dg" class="easyui-datagrid" title="广告列表"
        data-options="rownumbers:true,
                      singleSelect:false,
                      selectOnCheck:true,
                      checkOnSelect:true,
                      fit:true,
                      fitColumns:true,
                      pagination:true,
                      pageSize:20,
                      idField:'order_id'"
        toolbar="#tb">
        <thead>
            <tr>
                <th data-options="field:'ck',checkbox:true"></th>
                <th data-options="field:'imgsrc'">广告图片</th>
                <th data-options="field:'imglink'">广告链接</th>
                <th data-options="field:'pagelevel'">展示位置</th>
            </tr>
        </thead>
    </table>

    <!-- 工具栏 -->
    <div id="tb" style="padding: 2px 5px;">
        <a href="javascript:void(0);" class="easyui-linkbutton" iconcls="icon-add" onclick="addAd_OnClick();">添加广告</a>
        |
        <a href="javascript:void(0);" class="easyui-linkbutton" iconcls="icon-remove" onclick="deleteAd_OnClick();">删除广告</a>

        <script type="text/javascript">
            function addAd_OnClick() {
                $('#addAdWindow').window('open');
            }
            function deleteAd_OnClick() {
                var adSelected = getSelections();
                if (adSelected == null || adSelected.length <= 0) {
                    $.messager.alert('删除广告', '请至少选择一个广告', 'warning');
                    return;
                }

                $.messager.confirm('删除广告', '即将删除' + adSelected.length + '个广告，是否确定？', function (r) {
                    if (r) {
                        deleteAd(adSelected);
                    }
                });
            }
        </script>

    </div>

    <!-- 添加广告窗口 -->
    <div id="addAdWindow" class="easyui-window" title="查找" style="width: 470px; height: 210px"
        data-options="modal:true,closed:true,collapsible:false,minimizable:false,maximizable:false,resizable:false,iconCls:'icon-search'">
        <div style="padding: 10px">
            <table width="100%" border="0">
                <tr>
                    <td width="20%" align="right">图片</td>
                    <td width="80%">
                        <form id="frmImageUpload" method="post" enctype="multipart/form-data" action="method/FileUpload.ashx">
                            <input id="imageFile" name="imageFile" class="easyui-filebox" style="width: 260px" data-options="prompt:'选择文件...'" />
                            <a href="javascript:void(0);" class="easyui-linkbutton" style="width: 40px" onclick="uploadImageFile_OnClick();">上传</a>
                            <br />
                            <span id="imageUploaded" style="width: 100%">图片文件</span>
                        </form>
                </tr>
                <tr>
                    <td align="right">网址</td>
                    <td>
                        <input type="text" id="addAdImageLink" class="easyui-textbox" style="width: 300px"></td>
                </tr>
                <tr>
                    <td align="right">位置</td>
                    <td>
                        <select class="easyui-combobox" id="addAdPagelevel" name="pagelevel" style="width: 300px;" data-options="panelHeight:'auto'">
                            <option value="0">首页</option>
                            <option value="1">二级页</option>
                            <option value="2">三级页</option>
                            <option value="10">首页(下)</option>
                            <option value="11">二级页(下)</option>
                            <option value="12">三级页(下)</option>
                        </select>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td align="right">
                        <br />
                        <a id="btnAddAdOk" href="javascript:void(0);" class="easyui-linkbutton" onclick="btnAddAdOk_OnClick();" iconcls="icon-ok" style="width: 80px">确定</a>
                        <a id="btnAddAdCancel" href="javascript:void(0);" class="easyui-linkbutton" onclick="btnAdCancel_OnClick();" iconcls="icon-cancel" style="width: 80px">取消</a>
                    </td>
                </tr>
            </table>
        </div>

        <script type="text/javascript">
            function addAdWindow_OnLoad() {

            }
            function uploadImageFile_OnClick() {
                var uploadfile = $('#imageFile').textbox('getText');
                if (IsNullOrWhiteSpace(uploadfile)) {
                    $.messager.alert('图片上传', '请选择一个图片文件！', 'error');
                }
                //closeImportOrderWindow();
                //showLoading("正在上传附件" + uploadfile);

                $.ajax({
                    url: 'method/FileUpload.ashx',
                    type: 'POST',
                    cache: false,
                    data: new FormData($('#frmImageUpload')[0]),
                    processData: false,
                    contentType: false,
                    dataType: "json"
                }).done(function (data) {
                    if (data.success == true) {
                        var attachment = data.message;
                        $('#imageUploaded').html(attachment);
                        $.messager.alert('图片上传', '上传成功！', 'info');
                    } else {
                        $.messager.alert('图片上传', '上传文件失败。<br />' + data.message.ErrorMessage, 'error');
                        hideLoading();
                    }
                }).fail(function (data) {
                    //hideLoading();
                }).always(function () {

                });
            }

            function btnAddAdOk_OnClick() {
                adFile = $('#imageUploaded').html();
                adLink = $('#addAdImageLink').textbox('getText');
                adPagelevel = $('#addAdPagelevel').textbox('getValue');

                if (IsNullOrWhiteSpace(adFile)) {
                    $.messager.alert('图片上传', '请先上传图片文件！', 'error');
                }

                //showLoading("正在删除安装单");
                $.ajax({
                    type: "POST",
                    url: "api/AddAd.ashx",
                    data: { img_src: adFile, img_link:adLink, page_level:adPagelevel },
                    dataType: "json"
                }).done(function (data) {
                    if (data.success == true) {
                        $.messager.alert('添加广告', '添加广告成功');
                        reloadDatagrid();
                    } else {
                        $.messager.alert('添加广告', '添加广告失败。<br />' + data.message.ErrorMessage, 'error');
                    }
                }).fail(function (errMsg) {
                    $.messager.alert('添加广告', '添加广告时发生错误：<br/>' + errMsg, 'error');
                }).always(function () {
                    //hideLoading();
                });
            }

            function btnAdCancel_OnClick() {
                $('#addAdWindow').window('close');
            }

        </script>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#addAdWindow').window({
                onBeforeOpen: function () {
                    addAdWindow_OnLoad();
                }
            });

            loadAd();
        });

        function loadAd() {
            url = 'api/GetAdList.ashx';
            $("#dg").datagrid({ url: url });
        }

        function deleteAd(adSelected) {
            var json = JSON.stringify(adSelected);

            //showLoading("正在删除安装单");
            $.ajax({
                type: "POST",
                url: "api/DeleteAd.ashx",
                data: { ad_list: json },
                dataType: "json"
            }).done(function (data) {
                if (data.success == true) {
                    $.messager.alert('删除广告', '删除广告成功');
                    reloadDatagrid();
                } else {
                    $.messager.alert('删除广告', '删除广告失败。<br />' + data.message.ErrorMessage, 'error');
                }
            }).fail(function (errMsg) {
                $.messager.alert('删除广告', '删除广告时发生错误：<br/>' + errMsg, 'error');
            }).always(function () {
                //hideLoading();
            });
        }

        function getSelected() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                return row;
            }
            return null;
        }

        function getSelections() {
            var ss = [];
            var rows = $('#dg').datagrid('getSelections');
            for (var i = 0; i < rows.length; i++) {
                var row = rows[i];
                ss.push(row.adid);
            }
            return ss;
        }

        function clearDatagrid() {
            $('#dg').datagrid('loadData', { total: 0, rows: [] });
        }
        function reloadDatagrid() {
            $("#dg").datagrid("clearSelections");
            $('#dg').datagrid('reload');
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FormContent" runat="server">
</asp:Content>
