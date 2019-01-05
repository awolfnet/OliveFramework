<%@ Page Title="" Language="C#" MasterPageFile="~/jeasyui.Master" AutoEventWireup="true" CodeBehind="MerchantManager.aspx.cs" Inherits="OliveFramework.MerchantManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>商户管理</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <!-- 全局变量 -->
    <script type="text/javascript">
        market = null;
        product = null;
    </script>


    <!-- 表格 -->
    <table id="dg" class="easyui-datagrid" title="商户列表"
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
                <th data-options="field:'merchant_name'">商户名称</th>
                <th data-options="field:'merchant_contact'">联系方式</th>
                <th data-options="field:'merchant_website'">网站</th>
                <th data-options="field:'merchant_description'">简介</th>
            </tr>
        </thead>
    </table>

    <!-- 工具栏 -->
    <div id="tb" style="padding: 2px 5px;">
        省份：<select class="easyui-combobox" id="province" name="province" style="width: 150px;" data-options="panelHeight:'auto'">
            <option>辽宁省</option>
            <option>吉林省</option>
            <option>黑龙江省</option>
        </select>
        |
        市场：<input class="easyui-combobox" id="market" name="market" style="width: 200px;" data-options="
            valueField:'market_id',
            textField:'market_name',
            panelHeight:'auto'
            ">
        <a href="javascript:void(0);" class="easyui-linkbutton" onclick="addMarket_OnClick();">添加市场</a>
        <a href="javascript:void(0);" class="easyui-linkbutton" iconcls="icon-remove" onclick="deleteMarket_OnClick();">删除市场</a>
        |
                规格：<input class="easyui-combobox" id="product" name="product" style="width: 100px;" data-options="
            valueField:'productid',
            textField:'name',
            panelHeight:'auto'
            ">

        <a href="javascript:void(0);" class="easyui-linkbutton" onclick="addProduct_OnClick();">添加规格</a>
        <a href="javascript:void(0);" class="easyui-linkbutton" iconcls="icon-remove" onclick="deleteProduct_OnClick();">删除规格</a>
        <br />

        <a href="javascript:void(0);" class="easyui-linkbutton" iconcls="icon-search" onclick="searchMerchant_OnClick();">查找商户</a>
        |
        <a href="javascript:void(0);" class="easyui-linkbutton" iconcls="icon-add" onclick="addMerchant_OnClick();">新建商户</a>
        |
        <a href="javascript:void(0);" class="easyui-linkbutton" iconcls="icon-edit" onclick="modifyMerchant_OnClick();">修改商户</a>
        |
        <a href="javascript:void(0);" class="easyui-linkbutton" iconcls="icon-remove" onclick="deleteMerchant_OnClick();">删除商户</a>

        <script type="text/javascript">
            function addMarket_OnClick() {
                $('#addMarketWindow').window('open');
            }
            function deleteMarket_OnClick() {
                marketId = $('#market').combobox('getValue');
                marketName = $('#market').combobox('getText');
                $.messager.confirm('删除市场', '即将删除"' + marketName + '"。<br/>请确保该市场下没有商户。<br/>是否确定？', function (r) {
                    if (r) {
                        deleteMarket(marketId);
                    }
                });
            }
            function addProduct_OnClick() {
                $('#addProductWindow').window('open');
            }
            function deleteProduct_OnClick() {
                productID = $('#product').combobox('getValue');
                productName = $('#product').combobox('getText');
                $.messager.confirm('删除规格', '即将删除"' + productName + '"。<br/>请确保该规格下没有商户。<br/>是否确定？', function (r) {
                    if (r) {
                        deleteProduct(productID);
                    }
                });
            }
            function searchMerchant_OnClick() {
                $('#searchMerchantWindow').window('open');
            }

            function addMerchant_OnClick() {
                $('#addMerchantWindow').window('open');
            }
            function modifyMerchant_OnClick() {
                var merchantSelected = getSelections();
                if (merchantSelected == null || merchantSelected.length <= 0) {
                    $.messager.alert('修改商户', '请选择一个商户', 'warning');
                    return;
                }
                $('#modifyMerchantWindow').window('open');
            }
            function deleteMerchant_OnClick() {
                var merchantSelected = getSelections();
                if (merchantSelected == null || merchantSelected.length <= 0) {
                    $.messager.alert('删除商户', '请至少选择一个商户', 'warning');
                    return;
                }

                showConformDeleteMerchantDialog(merchantSelected);

            }
        </script>

    </div>

    <!-- 确认删除商户对话框 -->
    <div id="dlgConfirmDeleteMerchant" class="easyui-dialog" title="确认删除商户" data-options="modal:true,closed:true,buttons:'#confirmDeleteMerchantPromptButtons'" style="width: 400px; height: 200px; padding: 10px">
        <br />
        <span id="confirmDeleteMerchantPrompt"></span>
        <br />
        <span>请输入删除原因：</span>
        <input type="text" id="confirmDeleteMerchantLog" class="easyui-textbox" data-options="max:10" style="width: 100%">

        <div id="confirmDeleteMerchantPromptButtons">
            <a href="#" class="easyui-linkbutton" onclick="btnComfirmDeleteMerchantOK_OnClick();">确认</a>
            <a href="#" class="easyui-linkbutton" onclick="btnComfirmDeleteMerchantCancel_OnClick();">取消</a>
        </div>

        <script type="text/javascript">
            var merchantDeleteSelected = null;

            function showConformDeleteMerchantDialog(merchantSelected) {
                merchantDeleteSelected = merchantSelected
                $('#confirmDeleteMerchantPrompt').html("确认删除" + merchantDeleteSelected.length + "个商户，是否确定？");
                $('#dlgConfirmDeleteMerchant').dialog('open');
            }

            function btnComfirmDeleteMerchantOK_OnClick() {
                var merchantSelected = getSelections();
                deleteLog = $("#confirmDeleteMerchantLog").textbox('getValue');

                if (deleteLog == null || deleteLog.length <= 0) {
                    $.messager.alert('删除商户', '必须输入删除原因', 'warning');
                    return;
                }

                deleteMerchant(merchantDeleteSelected);
                $('#dlgConfirmDeleteMerchant').dialog('close');
            }
            function btnComfirmDeleteMerchantCancel_OnClick() {
                $('#dlgConfirmDeleteMerchant').dialog('close');
            }
        </script>
    </div>

    <!-- 添加市场窗口 -->
    <div id="addMarketWindow" class="easyui-window" title="添加市场" style="width: 400px; height: 190px"
        data-options="modal:true,closed:true,collapsible:false,minimizable:false,maximizable:false,resizable:false,iconCls:'icon-search'">
        <div style="padding: 10px">
            <table width="100%" border="0">
                <tr>
                    <td width="35%" align="right">所属省份:</td>
                    <td width="65%">
                        <select class="easyui-combobox" id="addMarketProvince" name="addMarketProvince" style="width: 200px;" data-options="panelHeight:'auto'">
                            <option>辽宁省</option>
                            <option>吉林省</option>
                            <option>黑龙江省</option>
                        </select>

                    </td>
                </tr>
                <tr>
                    <td width="35%" align="right">市场名称:</td>
                    <td width="65%">
                        <input type="text" id="addMarketName" class="easyui-textbox" data-options="max:10" style="width: 200px"></td>
                </tr>

                <tr>
                    <td align="right">市场类别：</td>
                    <td>
                        <select class="easyui-combobox" id="addMarketType" name="addMarketType" style="width: 200px;" data-options="panelHeight:'auto'">
                            <option value="1">钢材</option>
                            <option value="2">石材</option>
                            <option value="3">木材</option>
                        </select>

                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td align="right">
                        <br />
                        <a id="btnAddMarketOk" href="javascript:void(0);" class="easyui-linkbutton" onclick="btnAddMarketOk_OnClick();" iconcls="icon-ok" style="width: 80px">确定</a>
                        <a id="btnAddMarketCancel" href="javascript:void(0);" class="easyui-linkbutton" onclick="btnAddMarketCancel_OnClick();" iconcls="icon-cancel" style="width: 80px">取消</a>
                    </td>
                </tr>
            </table>
        </div>

        <script type="text/javascript">
            function btnAddMarketOk_OnClick() {
                marketProvince = $("#addMarketProvince").combobox('getValue');
                marketName = $("#addMarketName").textbox('getValue');
                marketType = $("#addMarketType").combobox('getValue');
                addMarket(marketProvince, marketName, marketType);
                $('#addMarketWindow').window('close');
            }

            function btnAddMarketCancel_OnClick() {
                $('#addMarketWindow').window('close');
            }

        </script>
    </div>


    <!-- 添加规格窗口 -->
    <div id="addProductWindow" class="easyui-window" title="添加规格" style="width: 400px; height: 160px"
        data-options="modal:true,closed:true,collapsible:false,minimizable:false,maximizable:false,resizable:false,iconCls:'icon-search'">
        <div style="padding: 10px">
            <table width="100%" border="0">
                <tr>
                    <td width="35%" align="right">规格:</td>
                    <td width="65%">
                        <input type="text" id="addProductName" class="easyui-textbox" data-options="max:10" style="width: 200px"></td>
                </tr>

                <tr>
                    <td align="right">所属类别：</td>
                    <td>
                        <select class="easyui-combobox" id="addProductMarketType" name="addProductMarketType" style="width: 200px;" data-options="panelHeight:'auto'">
                            <option value="1">钢材</option>
                            <option value="2">石材</option>
                            <option value="3">木材</option>
                        </select>

                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td align="right">
                        <br />
                        <a id="btnAddProductOk" href="javascript:void(0);" class="easyui-linkbutton" onclick="btnAddProductOk_OnClick();" iconcls="icon-ok" style="width: 80px">确定</a>
                        <a id="btnAddProductCancel" href="javascript:void(0);" class="easyui-linkbutton" onclick="btnAddProductCancel_OnClick();" iconcls="icon-cancel" style="width: 80px">取消</a>
                    </td>
                </tr>
            </table>
        </div>

        <script type="text/javascript">
            function btnAddProductOk_OnClick() {
                productName = $("#addProductName").textbox('getValue');
                marketType = $("#addProductMarketType").combobox('getValue');
                addProduct(productName, marketType);
                $('#addProductWindow').window('close');
            }

            function btnAddProductCancel_OnClick() {
                $('#addProductWindow').window('close');
            }

        </script>
    </div>


    <!-- 查找商户窗口 -->
    <div id="searchMerchantWindow" class="easyui-window" title="查找" style="width: 400px; height: 190px"
        data-options="modal:true,closed:true,collapsible:false,minimizable:false,maximizable:false,resizable:false,iconCls:'icon-search'">
        <div style="padding: 10px">
            <table width="100%" border="0">
                <tr>
                    <td width="35%" align="right">商户名称:</td>
                    <td width="65%">
                        <input type="text" id="searchMerchantName" class="easyui-textbox" data-options="max:10" style="width: 150px"></td>
                </tr>
                <tr>
                    <td align="right">联系方式:</td>
                    <td>
                        <input type="text" id="searchMerchantContact" class="easyui-textbox" style="width: 150px"></td>
                </tr>
                <tr>
                    <td align="right">简介:</td>
                    <td>
                        <input type="text" id="searchMerchantDescrption" class="easyui-textbox" data-options="max:10" style="width: 150px"></td>
                </tr>
                <tr>
                    <td align="right">
                        <br />
                        <a id="btnSearchMerchantAll" href="javascript:void(0);" class="easyui-linkbutton" onclick="btnSearchMerchantAll_OnClick();" style="width: 80px">显示全部</a>
                    </td>
                    <td align="right">
                        <br />
                        <a id="btnSearchMerchantOk" href="javascript:void(0);" class="easyui-linkbutton" onclick="btnSearchMerchantOk_OnClick();" iconcls="icon-ok" style="width: 80px">确定</a>
                        <a id="btnSearchMerchantCancel" href="javascript:void(0);" class="easyui-linkbutton" onclick="btnSearchMerchantCancel_OnClick();" iconcls="icon-cancel" style="width: 80px">取消</a>
                    </td>
                </tr>
            </table>
        </div>

        <script type="text/javascript">
            function btnSearchMerchantAll_OnClick() {
                loadMerchant("", "", "", "", "");
                $('#searchMerchantWindow').window('close');
            }

            function btnSearchMerchantOk_OnClick() {
                merchantName = $("#searchMerchantName").textbox('getValue');
                merchantContact = $("#searchMerchantContact").textbox('getValue');
                merchantDescrption = $("#searchMerchantDescrption").textbox('getValue');

                loadMerchant(merchantName, merchantContact, merchantDescrption, "", "");
                $('#searchMerchantWindow').window('close');
            }

            function btnSearchMerchantCancel_OnClick() {
                $('#searchMerchantWindow').window('close');
            }

        </script>
    </div>


    <!-- 新建商户窗口 -->
    <div id="addMerchantWindow" class="easyui-window" title="新建商户" style="width: 400px; height: 460px"
        data-options="modal:true,closed:true,collapsible:false,minimizable:false,maximizable:false,resizable:false,iconCls:'icon-add'">
        <div style="padding: 10px">
            <table width="100%" border="0">
                <tr>
                    <td width="35%" align="right">省份:</td>
                    <td width="65%">
                        <input type="text" id="addMerchantProvince" class="easyui-textbox" style="width: 200px" data-options="readonly:true">
                    </td>
                </tr>
                <tr>
                    <td width="35%" align="right">市场:</td>
                    <td width="65%">
                        <input type="text" id="addMerchantMarket" class="easyui-textbox" style="width: 200px" data-options="readonly:true">
                    </td>
                </tr>
                <tr>
                    <td width="35%" align="right">商户类型:</td>
                    <td width="65%">
                        <input class="easyui-combobox" id="addMerchantType" style="width: 200px" data-options="
                        valueField:'mtypeid',
                        textField:'Description',
                        panelHeight:'auto'
                        "></td>
                </tr>
                <tr>
                    <td width="35%" align="right">商户名称:</td>
                    <td width="65%">
                        <input type="text" id="addMerchantName" class="easyui-textbox" data-options="max:10" style="width: 200px">

                    </td>
                </tr>

                <tr>
                    <td align="right">联系方式:</td>
                    <td>
                        <input type="text" id="addMerchantContact" class="easyui-textbox" style="width: 200px"></td>
                </tr>
                <tr>
                    <td width="35%" align="right">网站:</td>
                    <td width="65%">
                        <input type="text" id="addMerchantWebsite" class="easyui-textbox" data-options="max:10" style="width: 200px"></td>
                </tr>
                <tr>
                    <td align="right">简介:</td>
                    <td>
                        <input type="text" id="addMerchantDescrption" class="easyui-textbox" data-options="max:10" style="width: 200px"></td>
                </tr>
                <tr>
                    <td align="right">规格:</td>
                    <td>
                        <div class="easyui-datalist" id="addMerchantSell" style="width: 200px; height: 100px" data-options="
                        checkbox: true,
                        selectOnCheck: false,
                        onBeforeSelect: function(){return false;}
                        ">
                        </div>
                        <input class="easyui-combobox" id="addMerchantProduct" name="addMerchantProduct" style="width: 140px;" data-options="
                        valueField:'productid',
                        textField:'name',
                        panelHeight:'auto'
                        ">
                        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="btnAddMerchantAddProduct_OnClick();"></a>
                        <a href="#" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="btnAddMerchantRemoveProduct_OnClick();"></a>
                    </td>
                </tr>
                <tr>
                    <td align="right">所属账号:</td>
                    <td>
                        <input class="easyui-combobox" id="addMerchantOwner" name="addMerchantOwner" style="width: 200px;" data-options="
                        valueField:'aid',
                        textField:'Name',
                        panelHeight:'auto'
                        ">
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td align="right">
                        <br />
                        <a id="btnAddOk" href="javascript:void(0);" class="easyui-linkbutton" onclick="btnAddOk_OnClick();" iconcls="icon-ok" style="width: 80px">确定</a>
                        <a id="btnAddCancel" href="javascript:void(0);" class="easyui-linkbutton" onclick="btnAddCancel_OnClick();" iconcls="icon-cancel" style="width: 80px">取消</a>
                    </td>
                </tr>
            </table>
        </div>

        <script type="text/javascript">
            function addMerchantWindow_OnLoad() {
                marketProvince = $("#province").combobox('getText');
                marketName = $("#market").combobox('getText');
                marketType = market.market_typename;

                $('#addMerchantProvince').textbox('setValue', marketProvince);
                $('#addMerchantMarket').textbox('setValue', marketName);

                $('#addMerchantType').combobox({
                    onBeforeLoad: function () {
                        //showLoading("正在加载");
                    },
                    onLoadSuccess: function () {
                        //hideLoading();
                    },
                    onLoadError: function () {
                        //hideLoading();
                    },
                    onChange: function (newValue, oldValue) 
                    {
                        merchantTypeDescription=$("#addMerchantType").combobox('getText');
                        $('#addMerchantName').textbox('setValue', "【" + merchantTypeDescription + "】");
                    },
                    url: 'api/GetMerchantType.ashx'
                });

                $('#addMerchantProduct').combobox({
                    onBeforeLoad: function () {
                        //showLoading("正在加载");
                    },
                    onLoadSuccess: function () {
                        //hideLoading();
                    },
                    onLoadError: function () {
                        //hideLoading();
                    },
                    url: 'api/GetProductList.ashx?market_type=' + marketType
                });

                $('#addMerchantOwner').combobox({
                    onBeforeLoad: function () {
                        //showLoading("正在加载");
                    },
                    onLoadSuccess: function () {
                        //hideLoading();
                    },
                    onLoadError: function () {
                        //hideLoading();
                    },
                    url: 'api/GetAccountList.ashx'
                });
            }
            function btnAddMerchantAddProduct_OnClick() {
                productID = $('#addMerchantProduct').combobox('getValue');
                productName = $('#addMerchantProduct').combobox('getText');

                $('#addMerchantSell').datagrid('appendRow', {
                    id: productID,
                    text: productName,
                });
            }
            function btnAddMerchantRemoveProduct_OnClick() {
                var productList = $('#addMerchantSell').datalist('getChecked');
                for (var i = 0; i < productList.length; i++) {
                    rowIndex = $('#addMerchantSell').datalist('getRowIndex', productList[i]);
                    $('#addMerchantSell').datagrid('deleteRow', rowIndex);
                }
            }
            function btnAddOk_OnClick() {
                merchantName = $('#addMerchantName').textbox('getText');
                merchantType = $("#addMerchantType").combobox('getValue');
                merchantContact = $('#addMerchantContact').textbox('getText');
                merchantWebsite = $('#addMerchantWebsite').textbox('getText');
                merchantDescription = $('#addMerchantDescrption').textbox('getText');
                marketID = market.market_id;
                ownerID = $("#addMerchantOwner").combobox('getValue');
                productList = $('#addMerchantSell').datalist('getRows');


                if (merchantName.length == 0) {
                    $.messager.alert('新建商户', '商户名称不能为空', 'warning');
                    return;
                }

                if (merchantType.length == 0) {
                    $.messager.alert('新建商户', '商户类型不能为空', 'warning');
                    return;
                }

                if (merchantContact.length == 0) {
                    $.messager.alert('新建商户', '联系方式不能为空', 'warning');
                    return;
                }

                if (productList == null || productList <= 0) {
                    $.messager.alert('新建商户', '请至少添加一个产品规格', 'warning');
                    return;
                }
                /*
                if (ownerID.length == 0 || ownerID==0) {
                    $.messager.alert('新建商户', '请选择商户所属账号', 'warning');
                    return;
                }
                */

                jsonProductList = JSON.stringify(productList);
                var jqxhr = $.ajax({
                    type: "POST",
                    url: "api/AddMerchant.ashx",
                    data: { name: merchantName, type: merchantType, contact: merchantContact, website: merchantWebsite, market: marketID, description: merchantDescription, sell: jsonProductList, owner: ownerID },
                    dataType: "json"
                });

                jqxhr.done(function (data, textStatus, jqXHR) {
                    if (data.success == true) {
                        $.messager.alert('新建商户', '商户"' + merchantName + '"添加成功', 'info');
                        reloadDatagrid();
                    } else {
                        $.messager.alert('新建商户', '失败：<br />' + data.message.ErrorMessage, 'error');
                    }

                });

                jqxhr.fail(function (jqXHR, textStatus, errorThrown) {
                    $.messager.alert('新建商户', '遇到网络错误：<br />' + errorThrown, 'error');
                });

                jqxhr.always(function () {
                    //hideLoading();
                });

                $('#addMerchantWindow').window('close');
            }

            function btnAddCancel_OnClick() {
                $('#addMerchantWindow').window('close');
            }

        </script>
    </div>


    <!-- 修改商户窗口 -->
    <div id="modifyMerchantWindow" class="easyui-window" title="修改商户" style="width: 400px; height: 405px"
        data-options="modal:true,closed:true,collapsible:false,minimizable:false,maximizable:false,resizable:false,iconCls:'icon-search'">
        <div style="padding: 10px">
            <table width="100%" border="0">
                <tr>
                    <td align="right">商户ID:</td>
                    <td>
                        <input type="text" id="modifyMerchantID" class="easyui-textbox" style="width: 200px" data-options="readonly:true"></td>
                </tr>
                <tr>
                    <td align="right">所属账户:</td>
                    <td>

                        <input class="easyui-combobox" id="modifyMerchantOwner" name="addMerchantOwner" style="width: 200px;" data-options="
                        valueField:'aid',
                        textField:'Name',
                        panelHeight:'auto'
                        ">
                </tr>
                <tr>
                    <td width="35%" align="right">商户名称:</td>
                    <td width="65%">
                        <input type="text" id="modifyMerchantName" class="easyui-textbox" data-options="max:10" style="width: 200px"></td>
                </tr>
                <tr>
                    <td align="right">联系方式:</td>
                    <td>
                        <input type="text" id="modifyMerchantContact" class="easyui-textbox" style="width: 200px"></td>
                </tr>
                <tr>
                    <td width="35%" align="right">网站:</td>
                    <td width="65%">
                        <input type="text" id="modifyMerchantWebsite" class="easyui-textbox" data-options="max:10" style="width: 200px"></td>
                </tr>
                <tr>
                    <td align="right">简介:</td>
                    <td>
                        <input type="text" id="modifyMerchantDescrption" class="easyui-textbox" data-options="max:10" style="width: 200px"></td>
                </tr>
                <tr>
                    <td align="right">规格:</td>
                    <td>
                        <div class="easyui-datalist" id="modifyMerchantSell" style="width: 200px; height: 100px" data-options="
                        checkbox: true,
                        selectOnCheck: false,
                        onBeforeSelect: function(){return false;}
                        ">
                        </div>
                        <input class="easyui-combobox" id="modifyMerchantProduct" name="modifyMerchantProduct" style="width: 140px;" data-options="
                        valueField:'productid',
                        textField:'name',
                        panelHeight:'auto'
                        ">
                        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="btnModifyMerchantAddProduct_OnClick();"></a>
                        <a href="#" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="btnModifyMerchantRemoveProduct_OnClick();"></a>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td align="right">
                        <br />
                        <a id="btnModifyMerchantOk" href="javascript:void(0);" class="easyui-linkbutton" onclick="btnModifyMerchantOk_OnClick();" iconcls="icon-ok" style="width: 80px">确定</a>
                        <a id="btnModifyMerchantCancel" href="javascript:void(0);" class="easyui-linkbutton" onclick="btnModifyMerchantCancel_OnClick();" iconcls="icon-cancel" style="width: 80px">取消</a>
                    </td>
                </tr>
            </table>
        </div>

        <script type="text/javascript">
            function modifyMerchantWindow_OnLoad() {
                merchantSelected = getSelected();

                merchantID = merchantSelected.merchantid;
                merchantName = merchantSelected.merchant_name;
                merchantDescription = merchantSelected.merchant_description;
                merchantContact = merchantSelected.merchant_contact;
                merchantWebsite = merchantSelected.merchant_website;
                merchantOwnerID = merchantSelected.merchant_owner_id;
                debugger;
                marketType = market.market_typename;

                $('#modifyMerchantID').textbox('setValue', merchantID);
                $('#modifyMerchantName').textbox('setValue', merchantName);
                $('#modifyMerchantDescrption').textbox('setValue', merchantDescription);
                $('#modifyMerchantContact').textbox('setValue', merchantContact);
                $('#modifyMerchantWebsite').textbox('setValue', merchantWebsite);

                $('#modifyMerchantProduct').combobox({
                    onBeforeLoad: function () {
                        //showLoading("正在加载");
                    },
                    onLoadSuccess: function () {
                        //hideLoading();
                    },
                    onLoadError: function () {
                        //hideLoading();
                    },
                    url: 'api/GetProductList.ashx?market_type=' + marketType
                });

                $('#modifyMerchantOwner').combobox({
                    onBeforeLoad: function () {
                        //showLoading("正在加载");
                    },
                    onLoadSuccess: function () {
                        //hideLoading();
                        $('#modifyMerchantOwner').combobox('select', merchantOwnerID);
                    },
                    onLoadError: function () {
                        //hideLoading();
                    },
                    url: 'api/GetAccountList.ashx'
                });

                $('#modifyMerchantSell').datalist({
                    url: 'api/GetMerchantSell.ashx?merchant_id=' + merchantID
                })

            }
            function btnModifyMerchantAddProduct_OnClick() {
                productID = $('#modifyMerchantProduct').combobox('getValue');
                productName = $('#modifyMerchantProduct').combobox('getText');

                $('#modifyMerchantSell').datagrid('appendRow', {
                    id: productID,
                    text: productName,
                });
            }
            function btnModifyMerchantRemoveProduct_OnClick() {
                var productList = $('#modifyMerchantSell').datalist('getChecked');
                for (var i = 0; i < productList.length; i++) {
                    rowIndex = $('#modifyMerchantSell').datalist('getRowIndex', productList[i]);
                    $('#modifyMerchantSell').datagrid('deleteRow', rowIndex);
                }
            }

            function btnModifyMerchantOk_OnClick() {
                merchantID = $('#modifyMerchantID').textbox('getText');
                merchantName = $('#modifyMerchantName').textbox('getText');
                merchantContact = $('#modifyMerchantContact').textbox('getText');
                merchantWebsite = $('#modifyMerchantWebsite').textbox('getText');
                merchantDescription = $('#modifyMerchantDescrption').textbox('getText');
                marketID = market.market_id;
                ownerID = $("#modifyMerchantOwner").combobox('getValue');
                productList = $('#modifyMerchantSell').datalist('getRows');


                if (merchantName.length == 0) {
                    $.messager.alert('修改商户', '商户名称不能为空', 'warning');
                    return;
                }

                if (merchantContact.length == 0) {
                    $.messager.alert('修改商户', '联系方式不能为空', 'warning');
                    return;
                }

                if (productList == null || productList <= 0) {
                    $.messager.alert('修改商户', '请至少添加一个产品规格', 'warning');
                    return;
                }

                if (ownerID.length == 0 || ownerID == 0) {
                    $.messager.alert('修改商户', '请选择商户所属账号', 'warning');
                    return;
                }


                jsonProductList = JSON.stringify(productList);
                var jqxhr = $.ajax({
                    type: "POST",
                    url: "api/ModifyMerchant.ashx",
                    data: { merchant_id: merchantID, name: merchantName, contact: merchantContact, website: merchantWebsite, market: marketID, description: merchantDescription, sell: jsonProductList, owner: ownerID },
                    dataType: "json"
                });

                jqxhr.done(function (data, textStatus, jqXHR) {
                    if (data.success == true) {
                        $.messager.alert('修改商户', '商户"' + merchantName + '"修改成功', 'info');
                        reloadDatagrid();
                    } else {
                        $.messager.alert('修改商户', '失败：<br />' + data.message.ErrorMessage, 'error');
                    }

                });

                jqxhr.fail(function (jqXHR, textStatus, errorThrown) {
                    $.messager.alert('修改商户', '遇到网络错误：<br />' + errorThrown, 'error');
                });

                jqxhr.always(function () {
                    //hideLoading();
                });

                $('#modifyMerchantWindow').window('close');
            }

            function btnModifyMerchantCancel_OnClick() {
                $('#modifyMerchantWindow').window('close');
            }

        </script>
    </div>


    <script type="text/javascript">
        $(document).ready(function () {
            $('#addMerchantWindow').window({
                onBeforeOpen: function () {
                    addMerchantWindow_OnLoad();
                }
            });

            $('#modifyMerchantWindow').window({
                onBeforeOpen: function () {
                    modifyMerchantWindow_OnLoad();
                }
            });

            $('#province').combobox({
                onBeforeLoad: function () {
                    //showLoading("正在加载");
                },
                onLoadSuccess: function () {
                    //hideLoading();
                },
                onLoadError: function () {
                    //hideLoading();
                },
                onSelect: function (data) {
                    province = data.text;
                    loadMarket(province);
                }
            });
        });


        function loadMarket(province) {
            $('#market').combobox({
                onBeforeLoad: function () {
                    //showLoading("正在加载");
                },
                onLoadSuccess: function () {
                    //hideLoading();
                },
                onLoadError: function () {
                    //hideLoading();
                },
                onSelect: function (data) {
                    //order_status = rec.statusid;
                    market = data;
                    marketType = market.market_typename;
                    loadProduct(marketType);
                },
                method: 'get',
                url: 'api/GetMarketList.ashx?location=' + province
            });
        }

        function loadProduct(marketType) {
            $('#product').combobox({
                onBeforeLoad: function () {
                    //showLoading("正在加载");
                },
                onLoadSuccess: function () {
                    //hideLoading();
                },
                onLoadError: function () {
                    //hideLoading();
                },
                onSelect: function (data) {
                    //order_status = rec.statusid;
                    //marketType = data.market_typename
                    product = data;
                    loadMerchant("", "", "", market.market_name, product.name);
                },
                method: 'get',
                url: 'api/GetProductList.ashx?market_type=' + marketType
            });
        }

        function loadMerchant(merchantName, merchantContact, merchantDescription, marketName, productName) {
            queryString = "?merchant_name=" + merchantName +
                          "&merchant_contact=" + merchantContact +
                          "&merchant_description=" + merchantDescription +
                          "&product_name=" + productName +
                          "&market_name=" + marketName;

            url = 'api/GetMerchantList.ashx' + queryString;
            $("#dg").datagrid({ url: url });
        }

        function addMarket(marketProvince, marketName, marketType) {
            var jqxhr = $.ajax({
                type: "POST",
                url: "api/AddMarket.ashx",
                data: { market_province: marketProvince, market_name: marketName, market_type: marketType },
                dataType: "json"
            });

            jqxhr.done(function (data, textStatus, jqXHR) {
                if (data.success == true) {
                    $.messager.alert('添加市场', '市场"' + marketName + '"添加成功', 'info');
                    $('#market').combobox('reload');
                } else {
                    $.messager.alert('添加市场', '失败：<br />' + data.message.ErrorMessage, 'error');
                }

            });

            jqxhr.fail(function (jqXHR, textStatus, errorThrown) {
                $.messager.alert('添加市场', '遇到网络错误：<br />' + errorThrown, 'error');
            });

            jqxhr.always(function () {
                //hideLoading();
            });
        }

        function addProduct(productName, marketType) {
            var jqxhr = $.ajax({
                type: "POST",
                url: "api/AddProduct.ashx",
                data: { product: productName, market_type: marketType },
                dataType: "json"
            });

            jqxhr.done(function (data, textStatus, jqXHR) {
                if (data.success == true) {
                    $.messager.alert('添加规格', '规格"' + productName + '"添加成功', 'info');
                    $('#market').combobox('reload');
                } else {
                    $.messager.alert('添加规格', '失败：<br />' + data.message.ErrorMessage, 'error');
                }

            });

            jqxhr.fail(function (jqXHR, textStatus, errorThrown) {
                $.messager.alert('添加规格', '遇到网络错误：<br />' + errorThrown, 'error');
            });

            jqxhr.always(function () {
                //hideLoading();
            });
        }

        function deleteMarket(MarketID) {
            $.ajax({
                type: "GET",
                url: "api/DeleteMarket.ashx",
                data: { market_id: MarketID }
            }).done(function (data) {
                if (data.success == true) {
                    $.messager.alert('删除市场', '删除市场成功');
                    $('#market').combobox('reload');
                } else {
                    $.messager.alert('删除市场', '删除市场失败。<br />' + data.message.ErrorMessage, 'error');
                }
            }).fail(function (errMsg) {
                $.messager.alert('删除商户', '删除规格时发生错误：<br/>' + errMsg, 'error');
            }).always(function () {
                //hideLoading();
            });
        }

        function deleteMerchant(merchantSelected) {
            var json = JSON.stringify(merchantSelected);

            //showLoading("正在删除安装单");
            $.ajax({
                type: "POST",
                url: "api/DeleteMerchant.ashx",
                data: { merchant_list: json },
                dataType: "json"
            }).done(function (data) {
                if (data.success == true) {
                    $.messager.alert('删除商户', '删除商户成功');
                    reloadDatagrid();
                } else {
                    $.messager.alert('删除商户', '删除商户失败。<br />' + data.message.ErrorMessage, 'error');
                }
            }).fail(function (errMsg) {
                $.messager.alert('删除商户', '删除商户时发生错误：<br/>' + errMsg, 'error');
            }).always(function () {
                //hideLoading();
            });
        }

        function deleteProduct(productID) {
            $.ajax({
                type: "GET",
                url: "api/DeleteProduct.ashx",
                data: { product_id: productID }
            }).done(function (data) {
                if (data.success == true) {
                    $.messager.alert('删除规格', '删除规格成功');
                    $('#product').combobox('reload');
                } else {
                    $.messager.alert('删除规格', '删除规格失败。<br />' + data.message.ErrorMessage, 'error');
                }
            }).fail(function (errMsg) {
                $.messager.alert('删除商户', '删除规格时发生错误：<br/>' + errMsg, 'error');
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
                ss.push(row.merchantid);
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
