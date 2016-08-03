window.gApplicationId = "BP004"; //智慧生活.城市.管理CMS
window.gInvokeApplicationId = "IP012"; //智慧生活.城市.管理服务接口
/****************************************************************************************************************/
/**************************************************** 地址 ******************************************************/
/****************************************************************************************************************/ 

//单点登录API 地址集合
window.AuthenticateUserFlash_InvokeUrl = baseCertInvokeUrl + "/v1/PC/AuthenticateUser";
window.LogOffUserFlash_InvokeUrl = baseCertInvokeUrl + "/v1/PC/LogOffUser";
window.AuthenticateUserSSO_InvokeUrl = baseCMSInvokeUrl + "/Share/Login/SSO";
window.AuthenticateUserLocal_InvokeUrl = baseCMSInvokeUrl + "/Share/Login/_Logon";
window.LogOffUserLocal_InvokeUrl = baseCMSInvokeUrl + "/Share/Login/_Logout";

window.AuthenticateContactFlash_InvokeUrl = baseCertInvokeUrl + "/v1/PC/AuthenticateContactByPC";
window.LogOffContactFlash_InvokeUrl = baseCertInvokeUrl + "/v1/PC/LogOffContactByPC";
window.AuthenticateContactLocal_InvokeUrl = baseCMSInvokeUrl + "/Share/Login/_LogonContact";
window.LogOffContactLocal_InvokeUrl = baseCMSInvokeUrl + "/Share/Login/_LogoutContact";

//CMS API 地址集合
window.Initilize_InvokeUrl = baseCMSInvokeUrl + "/Share/Init/Initilize"; 
window.parameterData_InvokeUrl = baseCMSInvokeUrl + "/Share/ParameterData"; 
window.ajaxData_InvokeUrl = baseCMSInvokeUrl + "/Share/AjaxData";
window.dictionaryDataByName_InvokeUrl = baseCMSInvokeUrl + "/Share/DictionaryDataService/GetDictionaryItemByItemName/";
window.dictionaryDataByLevel_InvokeUrl = baseCMSInvokeUrl + "/Share/DictionaryDataService/GetDictionaryItemByLevels/";
window.dictionaryDataByParentId_InvokeUrl = baseCMSInvokeUrl + "/Share/DictionaryDataService/GetDictionaryItemByParentId/";
window.treeDataOfGet_InvokeUrl = baseCMSInvokeUrl + "/Share/TreeDataService/getTreeData/";
window.treeDataOfGet_InvokeUrl2 = baseCMSInvokeUrl + "/Share/TreeDataService/getTreeData2/";
window.treeDataOfPost_InvokeUrl = baseCMSInvokeUrl + "/Share/TreeDataService/fetchTreeData";
window.checkIsLoggedIn_InvokeUrl = baseCMSInvokeUrl + "/Share/Login/IsLoggedIn";

window.checkIsContactLoggedIn_InvokeUrl = baseCMSInvokeUrl + "/Share/Login/IsContactLoggedIn";
window.msdbJob_InvokeUrl = baseCMSInvokeUrl + "/Share/MsdbJobService";
window.MsSQLScript_InvokeUrl = baseCMSInvokeUrl + "/Share/MsSQLScriptService";
window.log_InvokeUrl = baseCMSInvokeUrl + "/Share/LogService";
/****************************************************************************************************************/
/****************************************************************************************************************/
/****************************************************************************************************************/


//默认消息对话框
window.defaultMessageInfoDialogId = "_dialogMessageInfo";
window.defaultMessageErrorDialogId = "_dialogMessageError";
window.defaultMessageConfirmDialogId = "_dialogMessageConfirm";

window.execMode = "BS";
//token
window.gToken = null;

//常量
window.contants = {
    guidAutoGenerate: "A0000000-0000-0000-0000-000000000000",
    GuidAsGroup_SysAdmin: "00000000-0000-0000-0000-000000000001",
    GuidAsGroup_SysOper: "00000000-0000-0000-0000-000000000002", 
    charAutoGenerate: "自动生成",
    idAutoGenerate: -1,
    defaultGridId: "grid",
    defaultGridPagerId: "pager"
};

//共享选项
window.shareOptions = {
    YesOrNo: [{ ItemId: 0, ItemName: "否" }, { ItemId: 1, ItemName: "是"}]
};

//ztree
window.zTreeNavigationSetting = {
    view: {
        showLine: false,
        showIcon: true,
        selectedMulti: false,
        dblClickExpand: false,
        addDiyDom: function (treeId, treeNode) {
            var spaceWidth = 1;
            var switchObj = $("#" + treeNode.tId + "_switch"),
                icoObj = $("#" + treeNode.tId + "_ico");
            switchObj.remove();
            icoObj.before(switchObj);

            if (treeNode.level > 0) {
                var spaceStr = "<span style='display: inline-block;width:" + (spaceWidth * treeNode.level) + "px'></span>";
                switchObj.before(spaceStr);
            }
        }
    },
    data: {
        simpleData: {
            enable: true
        }
    },
    callback: {}
};

window.easyUIDataGridDefaultSetting = {
    pageSize: 18,
    pageList: [10, 15, 18]
};

