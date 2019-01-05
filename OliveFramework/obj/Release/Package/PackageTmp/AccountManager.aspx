<%@ Page Title="" Language="C#" MasterPageFile="~/jeasyui.Master" AutoEventWireup="true" CodeBehind="AccountManager.aspx.cs" Inherits="OliveFramework.AccountManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>账号管理</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <!-- 全局变量 -->
    <script type="text/javascript">

    </script>


    <!-- 表格 -->
    <table id="dg" class="easyui-datagrid" title="账号列表"
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
                <th data-options="field:'Name'">账号（手机号）</th>
                <th data-options="field:'Mail'">邮箱</th>
                <th data-options="field:'Nickname'">姓名</th>
                <th data-options="field:'MemberValid'">会员有效期</th>
            </tr>
        </thead>
    </table>

    <!-- 工具栏 -->
    <div id="tb" style="padding: 2px 5px;">
        <a href="javascript:void(0);" class="easyui-linkbutton" iconcls="icon-search" onclick="searchAccount_OnClick();">查找账号</a>
        |
        <a href="javascript:void(0);" class="easyui-linkbutton" iconcls="icon-add" onclick="addAccount_OnClick();">添加账号</a>
        |
        <a href="javascript:void(0);" class="easyui-linkbutton" iconcls="icon-edit" onclick="modifyAccount_OnClick();">修改账号</a>
        |
        <a href="javascript:void(0);" class="easyui-linkbutton" iconcls="icon-remove" onclick="deleteAccount_OnClick();">删除账号</a>
                |
        <a href="javascript:void(0);" class="easyui-linkbutton" onclick="pushMessage_OnClick();">消息推送</a>
        <script type="text/javascript">
            function searchAccount_OnClick() {
                $('#searchAccountWindow').window('open');
            }

            function addAccount_OnClick() {
                $('#addAccountWindow').window('open');
            }
            function modifyAccount_OnClick() {
                var accountSelected = getSelections();
                if (accountSelected == null || accountSelected.length <= 0) {
                    $.messager.alert('修改账号', '请选择一个账号', 'warning');
                    return;
                }
                $('#modifyAccountWindow').window('open');
            }
            function deleteAccount_OnClick() {
                var accountSelected = getSelections();
                if (accountSelected == null || accountSelected.length <= 0) {
                    $.messager.alert('删除账号', '请至少选择一个账号', 'warning');
                    return;
                }

                $.messager.confirm('删除账号', '即将账号' + accountSelected.length + '个账号，是否确定？', function (r) {
                    if (r) {
                        deleteAccount(accountSelected);
                    }
                });
            }

            function pushMessage_OnClick() {
                $('#pushMessageWindow').window('open');
            }
        </script>

    </div>


    <!-- 查找账号窗口 -->
    <div id="searchAccountWindow" class="easyui-window" title="查找" style="width: 400px; height: 190px"
        data-options="modal:false,closed:true,collapsible:false,minimizable:false,maximizable:false,resizable:false,iconCls:'icon-search'">
        <div style="padding: 10px">
            <table width="100%" border="0">
                <tr>
                    <td width="35%" align="right">账号（手机号）:</td>
                    <td width="65%">
                        <input type="text" id="searchAccountName" class="easyui-textbox" data-options="max:10" style="width: 200px"></td>
                </tr>
                <tr>
                    <td align="right">姓名:</td>
                    <td>
                        <input type="text" id="searchAccountNickname" class="easyui-textbox" style="width: 200px"></td>
                </tr>
                <tr>
                    <td align="right">邮箱:</td>
                    <td>
                        <input type="text" id="searchAccountMail" class="easyui-textbox" style="width: 200px"></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td align="right">
                        <br />
                        <a id="btnSearchAccountOk" href="javascript:void(0);" class="easyui-linkbutton" onclick="btnSearchAccountOk_OnClick();" iconcls="icon-ok" style="width: 80px">确定</a>
                        <a id="btnSearchAccountCancel" href="javascript:void(0);" class="easyui-linkbutton" onclick="btnSearchAccountCancel_OnClick();" iconcls="icon-cancel" style="width: 80px">关闭</a>
                    </td>
                </tr>
            </table>
        </div>

        <script type="text/javascript">
            function btnSearchAccountOk_OnClick() {
                accountName = $('#searchAccountName').textbox('getText');
                accountMail = $('#searchAccountMail').textbox('getText');
                accountNickname = $('#searchAccountNickname').textbox('getText');

                loadAccount(accountName, accountMail, accountNickname);
                $('#searchAccountWindow').window('close');
            }

            function btnSearchAccountCancel_OnClick() {
                $('#searchAccountWindow').window('close');
            }

        </script>
    </div>

    <!-- 添加账号窗口 -->
    <div id="addAccountWindow" class="easyui-window" title="查找" style="width: 400px; height: auto;"
        data-options="modal:true,closed:true,collapsible:false,minimizable:false,maximizable:false,resizable:false,iconCls:'icon-search'">
        <div style="padding: 10px">
            <table width="100%" border="0">
                <tr>
                    <td width="35%" align="right">账号（手机号）:</td>
                    <td width="65%">
                        <input type="text" id="addAccountName" class="easyui-textbox" data-options="max:11" style="width: 200px"></td>
                </tr>
                <tr>
                    <td align="right">姓名:</td>
                    <td>
                        <input type="text" id="addAccountNickname" class="easyui-textbox" style="width: 200px"></td>
                </tr>
                <tr>
                    <td align="right">密码:</td>
                    <td>
                        <input type="text" id="addAccountPassword" class="easyui-textbox" style="width: 200px"></td>
                </tr>
                <tr>
                    <td align="right">邮箱:</td>
                    <td>
                        <input type="text" id="addAccountMail" class="easyui-textbox" style="width: 200px"></td>
                </tr>
                                <tr>
                    <td align="right">会员有效期:</td>
                    <td>
                        
                                    <input class="easyui-datebox" id="addMemberValid" style="width:200px;">
                </tr>

                
                <tr>
                    <td>&nbsp;</td>
                    <td align="right">
                        <br />
                        <a id="btnAddAccountOk" href="javascript:void(0);" class="easyui-linkbutton" onclick="btnAddAccountOk_OnClick();" iconcls="icon-ok" style="width: 80px">确定</a>
                        <a id="btnAddAccountCancel" href="javascript:void(0);" class="easyui-linkbutton" onclick="btnAddAccountCancel_OnClick();" iconcls="icon-cancel" style="width: 80px">取消</a>
                    </td>
                </tr>
            </table>
        </div>

        <script type="text/javascript">
            function addAccountWindow_OnLoad()
            {

            }
            function btnAddAccountOk_OnClick() {
                accountName=$('#addAccountName').textbox('getText');
                accountPassword=$('#addAccountPassword').textbox('getText');
                accountMail=$('#addAccountMail').textbox('getText');
                accountNickname=$('#addAccountNickname').textbox('getText');
                
                /*
                var accountModel = new Map();
                accountModel.set("account_name", accountName);
                accountModel.set("account_password", accountPassword);
                accountModel.set("account_mail", accountMail);
                accountModel.set("account_nickname", accountNickname);
                */

                json = '{"account_name":"' + accountName + '","account_password":"' + accountPassword + '","account_mail":"' + accountMail + '","account_nickname":"' + accountNickname + '"}';

                //showLoading("正在删除安装单");
                $.ajax({
                    type: "POST",
                    url: "api/AddAccount.ashx",
                    data: { model: json },
                    dataType: "json"
                }).done(function (data) {
                    if (data.success == true) {
                        $.messager.alert('添加账号', '添加账号成功');
                        reloadDatagrid();
                        $('#addAccountWindow').window('close');
                    } else {
                        $.messager.alert('添加账号', '添加账号失败。<br />' + data.message.ErrorMessage, 'error');
                    }
                }).fail(function (errMsg) {
                    $.messager.alert('添加账号', '添加账号时发生错误：<br/>' + errMsg, 'error');
                }).always(function () {
                    //hideLoading();
                });
            }

            function btnAddAccountCancel_OnClick() {
                $('#addAccountWindow').window('close');
            }

        </script>
    </div>

    <!-- 修改账号窗口 -->
    <div id="modifyAccountWindow" class="easyui-window" title="查找" style="width: 400px; height: auto;"
        data-options="modal:true,closed:true,collapsible:false,minimizable:false,maximizable:false,resizable:false,iconCls:'icon-search'">
        <div style="padding: 10px">
            <table width="100%" border="0">
                <tr>
                    <td width="35%" align="right">账号（手机号）:</td>
                    <td width="65%">
                        <input type="text" id="modifyAccountName" class="easyui-textbox" data-options="max:11,readonly:true" style="width: 200px"></td>
                </tr>
                <tr>
                    <td align="right">姓名:</td>
                    <td>
                        <input type="text" id="modifyAccountNickname" class="easyui-textbox" style="width: 200px"></td>
                </tr>
                <tr>
                    <td align="right">密码:</td>
                    <td>
                        <input type="text" id="modifyAccountPassword" class="easyui-textbox" style="width: 200px"></td>
                </tr>
                <tr>
                    <td align="right">邮箱:</td>
                    <td>
                        <input type="text" id="modifyAccountMail" class="easyui-textbox" data-options="readonly:true" style="width: 200px"></td>
                </tr>

                <tr>
                    <td align="right">会员有效期:</td>
                    <td>
                        <input class="easyui-datebox" id="modifyMemberValid" style="width:200px;">
                    </td>
                </tr>

                
                <tr>
                    <td>&nbsp;</td>
                    <td align="right">
                        <br />
                        <a id="btnModifyCargoOk" href="javascript:void(0);" class="easyui-linkbutton" onclick="btnModifyAccountOk_OnClick();" iconcls="icon-ok" style="width: 80px">确定</a>
                        <a id="btnModifyhCargoCancel" href="javascript:void(0);" class="easyui-linkbutton" onclick="btnModifyAccountCancel_OnClick();" iconcls="icon-cancel" style="width: 80px">取消</a>
                    </td>
                </tr>
            </table>
        </div>

        <script type="text/javascript">
            function modifyAccountWindow_OnLoad() {
                accountSelected = getSelected();


                accountName = accountSelected.Name;
                accountPassword = ""
                accountMail = accountSelected.Mail;
                accountNickname = accountSelected.Nickname;
                accountMemberValid = accountSelected.MemberValid;

                $('#modifyAccountName').textbox('setValue', accountName);
                $('#modifyAccountPassword').textbox('setValue', accountPassword);
                $('#modifyAccountMail').textbox('setValue', accountMail);
                $('#modifyAccountNickname').textbox('setValue', accountNickname);
                $('#modifyMemberValid').datebox('setValue', accountMemberValid);

            }
            function btnModifyAccountOk_OnClick() {
                accountName = $('#modifyAccountName').textbox('getText');
                accountPassword = $('#modifyAccountPassword').textbox('getText');
                accountMail = $('#modifyAccountMail').textbox('getText');
                accountNickname = $('#modifyAccountNickname').textbox('getText');
                accountMemberValid = $('#modifyMemberValid').datebox('getValue');

                json = '{"account_name":"' + accountName + '","account_password":"' + accountPassword + '","account_mail":"' + accountMail + '","account_nickname":"' + accountNickname + '","account_membervalid":"' + accountMemberValid + '"}';

                //showLoading("正在删除安装单");
                $.ajax({
                    type: "POST",
                    url: "api/ModifyAccount.ashx",
                    data: { model: json },
                    dataType: "json"
                }).done(function (data) {
                    if (data.success == true) {
                        $.messager.alert('修改账号', '修改账号成功');
                        reloadDatagrid();
                        $('#modifyAccountWindow').window('close');
                    } else {
                        $.messager.alert('修改账号', '修改账号失败。<br />' + data.message.ErrorMessage, 'error');
                    }
                }).fail(function (errMsg) {
                    $.messager.alert('修改账号', '修改账号时发生错误：<br/>' + errMsg, 'error');
                }).always(function () {
                    //hideLoading();
                });
            }

            function btnModifyAccountCancel_OnClick() {
                $('#modifyAccountWindow').window('close');
            }

        </script>
    </div>


    
    <!-- 消息推送窗口 -->
    <div id="pushMessageWindow" class="easyui-window" title="消息推送"
                 data-options="modal:true,closed:true,collapsible:false,minimizable:false,maximizable:false,resizable:false">
        <div style="padding: 10px">
            <table>
                <tr>
                    <td>消息标题</td>
                    <td>
                        <input type="text" id="pushMessageTitle" class="easyui-textbox" data-options="max:10" style="width: 400px" value="新消息"></td>
                </tr>
                <tr>
                    <td>消息:</td>
                    <td>
                        <input type="text" id="pushMessageContent" class="easyui-textbox" multiline="true" style="width: 400px;height:120px"></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <div style="text-align:right">
                        <br />
                        <a id="btnPushMessageOk" href="javascript:void(0);" class="easyui-linkbutton" onclick="btnPushMessageOk_OnClick();" iconcls="icon-ok" style="width: 80px">确定</a>
                        <a id="btnPushMessageCancel" href="javascript:void(0);" class="easyui-linkbutton" onclick="btnPushMessageCancel_OnClick();" iconcls="icon-cancel" style="width: 80px">取消</a>
                            </div>
                    </td>
                </tr>
            </table>
        </div>

        <script type="text/javascript">
            function btnPushMessageOk_OnClick() {

                messageTitle = $('#pushMessageTitle').textbox('getText');
                messageContent = $('#pushMessageContent').textbox('getText');


                json = '{"Title":"' + messageTitle + '","Content":"' + messageContent + '","Type":1,"Style":3}';

                //showLoading("正在删除安装单");
                $.ajax({
                    type: "POST",
                    url: "api/PushMessage.ashx",
                    data: { model: json },
                    dataType: "json"
                }).done(function (data) {
                    if (data.success == true) {
                        $.messager.alert('消息推送', '消息推送成功');
                        $('#pushMessageWindow').window('close');
                    } else {
                        $.messager.alert('消息推送', '消息推送失败。<br />' + data.message.ErrorMessage, 'error');
                    }
                }).fail(function (errMsg) {
                    $.messager.alert('消息推送', '消息推送时发生错误：<br/>' + errMsg, 'error');
                }).always(function () {
                    //hideLoading();
                });


                
            }

            function btnPushMessageCancel_OnClick() {
                $('#pushMessageWindow').window('close');
            }

        </script>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#addAccountWindow').window({
                onBeforeOpen: function () {
                    addAccountWindow_OnLoad();
                }
            });

            $('#modifyAccountWindow').window({
                onBeforeOpen: function () {
                    modifyAccountWindow_OnLoad();
                }
            });

            loadAccount("", "", "");
        });

        function loadAccount(accountName,accountMail,accountNickname) {
            queryString = "?account_name=" + accountName +
                          "&account_mail=" + accountMail +
                          "&account_nickname=" + accountNickname;

            url = 'api/GetUserinfoList.ashx' + queryString;
            $("#dg").datagrid({ url: url });
        }
        
        function deleteAccount(accountSelected) {
            var json = JSON.stringify(accountSelected);

            //showLoading("正在删除安装单");
            $.ajax({
                type: "POST",
                url: "api/DeleteAccount.ashx",
                data: { account_list: json },
                dataType: "json"
            }).done(function (data) {
                if (data.success == true) {
                    $.messager.alert('删除账号', '删除账号成功');
                    reloadDatagrid();
                } else {
                    $.messager.alert('删除账号', '删除账号失败。<br />' + data.message.ErrorMessage, 'error');
                }
            }).fail(function (errMsg) {
                $.messager.alert('删除账号', '删除账号时发生错误：<br/>' + errMsg, 'error');
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
                ss.push(row.aid);
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
