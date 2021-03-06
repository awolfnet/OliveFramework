﻿<%@ Page Title="" Language="C#" MasterPageFile="~/jeasyui.Master" AutoEventWireup="true" CodeBehind="CargoManager.aspx.cs" Inherits="OliveFramework.CargoManager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>车辆管理</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <!-- 全局变量 -->
    <script type="text/javascript">
        market = null;
        product = null;
    </script>


    <!-- 表格 -->
    <table id="dg" class="easyui-datagrid" title="车辆列表"
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
                <th data-options="field:'cargo_plate'">牌照</th>
                <th data-options="field:'cargo_driver'">司机</th>
                <th data-options="field:'driver_licence'">驾驶证</th>
                <th data-options="field:'driver_contact'">联系方式</th>
                <th data-options="field:'cargo_description'">车辆描述</th>
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

        <br />

        <a href="javascript:void(0);" class="easyui-linkbutton" iconcls="icon-search" onclick="searchCargo_OnClick();">查找车辆</a>
        |
        <a href="javascript:void(0);" class="easyui-linkbutton" iconcls="icon-add" onclick="addCargo_OnClick();">新建车辆</a>
        |
        <a href="javascript:void(0);" class="easyui-linkbutton" iconcls="icon-edit" onclick="modifyCargo_OnClick();">修改车辆</a>
        |
        <a href="javascript:void(0);" class="easyui-linkbutton" iconcls="icon-remove" onclick="deleteCargo_OnClick();">删除车辆</a>

        <script type="text/javascript">
            function searchCargo_OnClick() {
                $('#searchCargoWindow').window('open');
            }

            function addCargo_OnClick() {
                $('#addCargoWindow').window('open');
            }
            function modifyCargo_OnClick() {
                var cargoSelected = getSelections();
                if (cargoSelected == null || cargoSelected.length <= 0) {
                    $.messager.alert('修改车辆', '请选择一个车辆', 'warning');
                    return;
                }
                $('#modifyCargoWindow').window('open');
            }
            function deleteCargo_OnClick() {
                var cargoSelected = getSelections();
                if (cargoSelected == null || cargoSelected.length <= 0) {
                    $.messager.alert('删除车辆', '请至少选择一个车辆', 'warning');
                    return;
                }

                $.messager.confirm('删除车辆', '即将删除' + cargoSelected.length + '个车辆，是否确定？', function (r) {
                    if (r) {
                        deleteCargo(cargoSelected);
                    }
                });
            }
        </script>

    </div>

    <!-- 查找车辆窗口 -->
    <div id="searchCargoWindow" class="easyui-window" title="查找" style="width: 400px; height: 245px"
        data-options="modal:true,closed:true,collapsible:false,minimizable:false,maximizable:false,resizable:false,iconCls:'icon-search'">
        <div style="padding: 10px">
            <table width="100%" border="0">
                <tr>
                    <td width="35%" align="right">车辆牌照:</td>
                    <td width="65%">
                        <input type="text" id="searchCargoPlate" class="easyui-textbox" data-options="max:10" style="width: 200px"></td>
                </tr>
                                <tr>
                    <td align="right">司机姓名:</td>
                    <td>
                        <input type="text" id="searchCargoDriver" class="easyui-textbox" style="width: 200px"></td>
                </tr>
                                <tr>
                    <td align="right">驾驶证:</td>
                    <td>
                        <input type="text" id="searchCargoLicence" class="easyui-textbox" style="width: 200px"></td>
                </tr>
                <tr>
                    <td align="right">联系方式:</td>
                    <td>
                        <input type="text" id="searchCargoContact" class="easyui-textbox" style="width: 200px"></td>
                </tr>
                <tr>
                    <td align="right">车辆描述:</td>
                    <td>
                        <input type="text" id="searchCargoDescrption" class="easyui-textbox" data-options="max:10" style="width: 200px"></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td align="right">
                        <br />
                        <a id="btnSearchCargoOk" href="javascript:void(0);" class="easyui-linkbutton" onclick="btnSearchCargoOk_OnClick();" iconcls="icon-ok" style="width: 80px">确定</a>
                        <a id="btnSearchCargoCancel" href="javascript:void(0);" class="easyui-linkbutton" onclick="btnSearchCargoCancel_OnClick();" iconcls="icon-cancel" style="width: 80px">取消</a>
                    </td>
                </tr>
            </table>
        </div>

        <script type="text/javascript">
            function btnSearchCargoOk_OnClick() {
                cargoPlate = $('#searchCargoPlate').textbox('getText');
                cargoDriver = $('#searchCargoDriver').textbox('getText');
                cargoLicence = $('#searchCargoLicence').textbox('getText');
                cargoContact = $('#searchCargoContact').textbox('getText');
                cargoDescription = $('#searchCargoDescrption').textbox('getText');

                loadCargo(cargoPlate, cargoDriver, cargoLicence, cargoContact, cargoDescription, market.market_name);

                $('#searchCargoWindow').window('close');
            }

            function btnSearchCargoCancel_OnClick() {
                $('#searchCargoWindow').window('close');
            }

        </script>
    </div>


    <!-- 新建车辆窗口 -->
    <div id="addCargoWindow" class="easyui-window" title="新建车辆" style="width: 400px; height: 330px"
        data-options="modal:true,closed:true,collapsible:false,minimizable:false,maximizable:false,resizable:false,iconCls:'icon-add'">
        <div style="padding: 10px">
            <table width="100%" border="0">
                <tr>
                    <td width="35%" align="right">省份:</td>
                    <td width="65%">
                        <input type="text" id="addCargoProvince" class="easyui-textbox" style="width: 200px" data-options="readonly:true"></td>
                    </td>
                </tr>
                <tr>
                    <td width="35%" align="right">市场:</td>
                    <td width="65%">
                        <input type="text" id="addCargoMarket" class="easyui-textbox" style="width: 200px" data-options="readonly:true"></td>
                    </td>
                </tr>
                <tr>
                    <td width="35%" align="right">车辆牌照:</td>
                    <td width="65%">
                        <input type="text" id="addCargoPlate" class="easyui-textbox" data-options="max:10" style="width: 200px"></td>
                </tr>
                <tr>
                    <td align="right">司机姓名:</td>
                    <td>
                        <input type="text" id="addCargoDriver" class="easyui-textbox" style="width: 200px"></td>
                </tr>
                <tr>
                    <td align="right">驾驶证:</td>
                    <td>
                        <input type="text" id="addCargoLicence" class="easyui-textbox" style="width: 200px"></td>
                </tr>
                <tr>
                    <td align="right">联系方式:</td>
                    <td>
                        <input type="text" id="addCargoContact" class="easyui-textbox" style="width: 200px"></td>
                </tr>
                <tr>
                    <td align="right">车辆描述:</td>
                    <td>
                        <input type="text" id="addCargoDescrption" class="easyui-textbox" data-options="max:10" style="width: 200px"></td>
                </tr>
                <tr>
                    <td align="right">所属账号:</td>
                    <td>
                        <input class="easyui-combobox" id="addCargoOwner" name="addCargoOwner" style="width: 200px;" data-options="
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
            function addCargoWindow_OnLoad() {
                marketProvince = $("#province").combobox('getText');
                marketName = $("#market").combobox('getText');
                marketType = market.market_typename;

                $('#addCargoProvince').textbox('setValue', marketProvince);
                $('#addCargoMarket').textbox('setValue', marketName);

                $('#addCargoOwner').combobox({
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
            function btnAddOk_OnClick() {
                cargoPlate = $('#addCargoPlate').textbox('getText');
                cargoDriver = $('#addCargoDriver').textbox('getText');
                cargoLicence = $('#addCargoLicence').textbox('getText');
                cargoContact = $('#addCargoContact').textbox('getText');
                cargoDescription = $('#addCargoDescrption').textbox('getText');
                cargoOwnerID = $("#addCargoOwner").combobox('getValue');


                if (cargoPlate.length == 0) {
                    $.messager.alert('新建车辆', '车辆牌照不能为空', 'warning');
                    return;
                }

                if (cargoDriver.length == 0) {
                    $.messager.alert('新建车辆', '司机姓名不能为空', 'warning');
                    return;
                }

                if (cargoLicence.length == 0) {
                    $.messager.alert('新建车辆', '驾驶证不能为空', 'warning');
                    return;
                }

                if (cargoContact.length == 0) {
                    $.messager.alert('新建车辆', '联系方式不能为空', 'warning');
                    return;
                }

                
                //if (cargoOwnerID.length == 0 || cargoOwnerID == 0) {
                //    $.messager.alert('新建车辆', '请选择车辆所属账号', 'warning');
                //    return;
                //}
                

                marketID = market.market_id;
                var jqxhr = $.ajax({
                    type: "POST",
                    url: "api/AddCargo.ashx",
                    data: { cargo_plate: cargoPlate, cargo_driver: cargoDriver, cargo_licence: cargoLicence, cargo_contact: cargoContact, cargo_description: cargoDescription, cargo_owner: cargoOwnerID, market: marketID },
                    dataType: "json"
                });

                jqxhr.done(function (data, textStatus, jqXHR) {
                    if (data.success == true) {
                        $.messager.alert('新建车辆', '车辆"' + cargoPlate + '"添加成功', 'info');
                        reloadDatagrid();
                    } else {
                        $.messager.alert('新建车辆', '失败：<br />' + data.message.ErrorMessage, 'error');
                    }

                });

                jqxhr.fail(function (jqXHR, textStatus, errorThrown) {
                    $.messager.alert('新建车辆', '遇到网络错误：<br />' + errorThrown, 'error');
                });

                jqxhr.always(function () {
                    //hideLoading();
                });

                $('#addCargoWindow').window('close');
            }

            function btnAddCancel_OnClick() {
                $('#addCargoWindow').window('close');
            }

        </script>
    </div>


    <!-- 修改车辆窗口 -->
    <div id="modifyCargoWindow" class="easyui-window" title="修改车辆" style="width: 400px; height: 305px"
        data-options="modal:true,closed:true,collapsible:false,minimizable:false,maximizable:false,resizable:false,iconCls:'icon-search'">
        <div style="padding: 10px">
            <table width="100%" border="0">
                <tr>
                    <td align="right">车辆ID:</td>
                    <td>
                        <input type="text" id="modifyCargoID" class="easyui-textbox" style="width: 200px" data-options="readonly:true"></td>
                </tr>
                <tr>
                    <td align="right">所属账户:</td>
                    <td>

                        <input class="easyui-combobox" id="modifyCargoOwner" name="addCargoOwner" style="width: 200px;" data-options="
                        valueField:'aid',
                        textField:'Name',
                        panelHeight:'auto'
                        ">
                </tr>
                <tr>
                    <td width="35%" align="right">车辆牌照:</td>
                    <td width="65%">
                        <input type="text" id="modifyCargoPlate" class="easyui-textbox" data-options="max:10" style="width: 200px"></td>
                </tr>
                <tr>
                    <td width="35%" align="right">司机姓名:</td>
                    <td width="65%">
                        <input type="text" id="modifyCargoDriver" class="easyui-textbox" data-options="max:10" style="width: 200px"></td>
                </tr>
                <tr>
                    <td width="35%" align="right">驾驶证:</td>
                    <td width="65%">
                        <input type="text" id="modifyCargoLicence" class="easyui-textbox" data-options="max:10" style="width: 200px"></td>
                </tr>
                <tr>
                    <td align="right">联系方式:</td>
                    <td>
                        <input type="text" id="modifyCargoContact" class="easyui-textbox" style="width: 200px"></td>
                </tr>
                <tr>
                    <td align="right">车辆描述:</td>
                    <td>
                        <input type="text" id="modifyCargoDescrption" class="easyui-textbox" data-options="max:10" style="width: 200px"></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td align="right">
                        <br />
                        <a id="btnModifyCargoOk" href="javascript:void(0);" class="easyui-linkbutton" onclick="btnModifyCargoOk_OnClick();" iconcls="icon-ok" style="width: 80px">确定</a>
                        <a id="btnModifyCargoCancel" href="javascript:void(0);" class="easyui-linkbutton" onclick="btnModifyCargoCancel_OnClick();" iconcls="icon-cancel" style="width: 80px">取消</a>
                    </td>
                </tr>
            </table>
        </div>

        <script type="text/javascript">
            function modifyCargoWindow_OnLoad() {
                cargoSelected = getSelected();

                cargoID = cargoSelected.cargo_id;
                cargoOwnerID = cargoSelected.cargo_owner_id;
                cargoPlate = cargoSelected.cargo_plate;
                cargoDriver = cargoSelected.cargo_driver;
                cargoLicence = cargoSelected.driver_licence;
                cargoDescription = cargoSelected.cargo_description;
                cargoContact = cargoSelected.driver_contact;

                marketType = market.market_typename;

                $('#modifyCargoID').textbox('setValue', cargoID);
                $('#modifyCargoPlate').textbox('setValue', cargoPlate);
                $('#modifyCargoDriver').textbox('setValue', cargoDriver);
                $('#modifyCargoLicence').textbox('setValue', cargoLicence);
                $('#modifyCargoContact').textbox('setValue', cargoContact);
                $('#modifyCargoDescrption').textbox('setValue', cargoDescription);

                $('#modifyCargoOwner').combobox({
                    onBeforeLoad: function () {
                        //showLoading("正在加载");
                    },
                    onLoadSuccess: function () {
                        //hideLoading();
                        $('#modifyCargoOwner').combobox('select', cargoOwnerID);
                    },
                    onLoadError: function () {
                        //hideLoading();
                    },
                    url: 'api/GetAccountList.ashx'
                });

            }

            function btnModifyCargoOk_OnClick() {

            }

            function btnModifyCargoCancel_OnClick() {
                $('#modifyCargoWindow').window('close');
            }

        </script>
    </div>


    <script type="text/javascript">
        
        $(document).ready(function () {
            $('#addCargoWindow').window({
                onBeforeOpen: function () {
                    addCargoWindow_OnLoad();
                }
            });

            $('#modifyCargoWindow').window({
                onBeforeOpen: function () {
                    modifyCargoWindow_OnLoad();
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
                    loadCargo("", "", "", "", "", data.market_name);
                },
                method: 'get',
                url: 'api/GetMarketList.ashx?location=' + province
            });
        }

        function loadCargo(cargoPlate, cargoDriver, cargoLicence, cargoContact, cargoDescription,marketName) {
            queryString = "?cargo_plate=" + cargoPlate +
                          "&cargo_driver=" + cargoDriver +
                          "&cargo_licence="+cargoLicence+
                          "&cargo_contact=" + cargoContact +
                          "&cargo_description=" + cargoDescription +
                          "&market_name=" + marketName;

            url = 'api/GetCargoList.ashx' + queryString;
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

        function deleteCargo(cargoSelected) {
            var json = JSON.stringify(cargoSelected);

            //showLoading("正在删除安装单");
            $.ajax({
                type: "POST",
                url: "api/DeleteCargo.ashx",
                data: { cargo_list: json },
                dataType: "json"
            }).done(function (data) {
                if (data.success == true) {
                    $.messager.alert('删除车辆', '删除车辆成功');
                    reloadDatagrid();
                } else {
                    $.messager.alert('删除车辆', '删除车辆失败。<br />' + data.message.ErrorMessage, 'error');
                }
            }).fail(function (errMsg) {
                $.messager.alert('删除车辆', '删除车辆时发生错误：<br/>' + errMsg, 'error');
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
                ss.push(row.cargo_id);
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
