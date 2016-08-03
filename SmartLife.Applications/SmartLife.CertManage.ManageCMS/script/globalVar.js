window.gApplicationId = "BP001"; //智慧生活.认证管理平台.管理CMS 
window.gInvokeApplicationId = "IP002"; //智慧生活.认证管理平台.管理服务接口 
/****************************************************************************************************************/
/**************************************************** 地址 ******************************************************/
/****************************************************************************************************************/
//单点登录API 地址集合
window.AuthenticateUser_InvokeUrl = baseCertInvokeUrl + "/v1/PC/AuthenticateUser";
window.AuthenticateUserSSO_InvokeUrl = baseCMSInvokeUrl + "/Share/Login/SSO";
window.LogOffUser_InvokeUrl = baseCertInvokeUrl + "/v1/PC/LogOffUser";

//CMS API 地址集合
window.Initilize_InvokeUrl = baseCMSInvokeUrl + "/Share/Init/Initilize";
window.CommonAddSeatExtBinding_InvokeUrl = baseCMSInvokeUrl + "/Share/Common/AddSeatExtBinding";
window.CommonRemoveSeatExtBinding_InvokeUrl = baseCMSInvokeUrl + "/Share/Common/RemoveSeatExtBinding";
window.parameterData_InvokeUrl = baseCMSInvokeUrl + "/Share/ParameterData";
window.ipcc_InvokeUrl = baseCMSInvokeUrl + "/Share/IPCC";
window.ajaxData_InvokeUrl = baseCMSInvokeUrl + "/Share/AjaxData";
window.dictionaryDataByName_InvokeUrl = baseCMSInvokeUrl + "/Share/DictionaryDataService/GetDictionaryItemByItemName/";
window.dictionaryDataByLevel_InvokeUrl = baseCMSInvokeUrl + "/Share/DictionaryDataService/GetDictionaryItemByLevels/";
window.dictionaryDataByParentId_InvokeUrl = baseCMSInvokeUrl + "/Share/DictionaryDataService/GetDictionaryItemByParentId/";
window.treeDataOfGet_InvokeUrl = baseCMSInvokeUrl + "/Share/TreeDataService/getTreeData/";
window.treeDataOfGet_InvokeUrl2 = baseCMSInvokeUrl + "/Share/TreeDataService/getTreeData2/";
window.treeDataOfPost_InvokeUrl = baseCMSInvokeUrl + "/Share/TreeDataService/fetchTreeData";
window.MsSQLScript_InvokeUrl = baseCMSInvokeUrl + "/Share/MsSQLScriptService"; 
window.attachmentListTableAndIdAndTag_InvokeUrl = baseCMSInvokeUrl + "/Share/AttachmentService/ListTableAndIdAndTag/";
window.attachmentListTableAndId_InvokeUrl = baseCMSInvokeUrl + "/Share/AttachmentService/ListTableAndId/";
window.attachmentListTable_InvokeUrl = baseCMSInvokeUrl + "/Share/AttachmentService/ListTable/";
window.attachmentAddBatch_InvokeUrl = baseCMSInvokeUrl + "/Share/AttachmentService/AddBatch";
window.attachmentAddBatchAfterDeleteTableAndIdAndTag_InvokeUrl = baseCMSInvokeUrl + "/Share/AttachmentService/AddBatchAfterDeleteTableAndIdAndTag/";
window.attachmentAddBatchAfterDeleteTableAndId_InvokeUrl = baseCMSInvokeUrl + "/Share/AttachmentService/AddBatchAfterDeleteTableAndId/";
window.attachmentAddBatchAfterDeleteTable_InvokeUrl = baseCMSInvokeUrl + "/Share/AttachmentService/AddBatchAfterDeleteTable/";
window.attachmentDeleteBatch_InvokeUrl = baseCMSInvokeUrl + "/Share/AttachmentService/DeleteBatch/";
window.attachmentDeleteBatchWithSaveNames_InvokeUrl = baseCMSInvokeUrl + "/Share/AttachmentService/DeleteBatchWithSaveNames/";
window.checkIsLoggedIn_InvokeUrl = baseCMSInvokeUrl + "/Share/Login/IsLoggedIn";
window.login_InvokeUrl = baseCMSInvokeUrl + "/Share/Login/Logon";
window.loginOfCompany_InvokeUrl = baseCMSInvokeUrl + "/AppShare/Login/LogonOfCompany";
window.logout_InvokeUrl = baseCMSInvokeUrl + "/Share/Login/Logout";
window.clearUploadedFiles_InvokeUrl = baseCMSInvokeUrl + "/Share/CommonService/ClearUploadedFiles";
window.registrar_InvokeUrl = baseCMSInvokeUrl + "/Share/RegistrarService";
//uploadify地址
window.uploadify_InvokeUrl = baseCMSInvokeUrl + "/Share/uploadify.ashx";
//上传文件地址
window.baseContentInvokeUrl = "http://" + window.location.hostname + ":17999/";

//EntityDefination实体定义相关
window.readHotelControlOfEntityDefination = baseCMSInvokeUrl + "/Pub/CompanyService/ReadSettingsOfHotelControl/";
window.saveHotelControlOfEntityDefination = baseCMSInvokeUrl + "/Pub/CompanyService/SaveSettingsOfHotelControl/";
window.readCompanyControlOfAirShowOfEntityDefination = baseCMSInvokeUrl + "/Pub/CompanyService/ReadSettingsOfCompanyControlOfAirShow/";
window.saveCompanyControlOfAirShowOfEntityDefination = baseCMSInvokeUrl + "/Pub/CompanyService/SaveSettingsOfCompanyControlOfAirShow/";

//多语言
window.getMultiLanguageStorageInfo_InvokeUrl = baseCMSInvokeUrl + "/Share/MultiLanguageStorageService/GetMultiLanguageStorageInfo/";

//居家养老地址
window.callservice_InvokeUrl = baseCMSInvokeUrl + "/Oca/CallService";
window.serviceTrackLog_InvokeUrl = baseCMSInvokeUrl + "/Oca/ServiceTrackLogService";
window.serviceVoiceService_InvokeUrl = baseCMSInvokeUrl + "/Oca/VoiceService";

window.dialogOfProcessEmergency_RelativeUrl = '/views/shared/tabContainer.htm?pageUrl=views/old-care/emergency-aid.htm&menuCode=B180101';
window.dialogOfProcessFamilyCalls_RelativeUrl = '/views/shared/tabContainer.htm?pageUrl=views/old-care/family-calls.htm&menuCode=B180201';
window.dialogOfProcessCommonService_RelativeUrl = '/views/shared/tabContainer.htm?pageUrl=views/old-care/infotainment-aid.htm&menuCode=B180301';
window.dialogOfProcessLife_RelativeUrl = '/views/shared/tabContainer.htm?pageUrl=views/old-care/life-aid.htm&menuCode=B180498';

window.dialogOfInfoEmergency_RelativeUrl = '/views/shared/tabContainer.htm?pageUrl=views/old-care/emergency-aid-info.htm';
window.dialogOfInfoFamilyCalls_RelativeUrl = '/views/shared/tabContainer.htm?pageUrl=views/old-care/family-calls-info.htm';
window.dialogOfInfoCommonService_RelativeUrl = '/views/shared/tabContainer.htm?pageUrl=views/old-care/infotainment-aid-info.htm';
window.dialogOfInfoLife_RelativeUrl = '/views/shared/tabContainer.htm?pageUrl=views/old-care/life-aid-info.htm';

//IPCC template
window.strTemplateOfVoiceType0 = 'http://[IP]:[Port]/manager/recordfile.php?uniqueid=[Address]';
window.strTemplateOfVoiceType1 = 'http://[IP]:[Port]/manager/conf_recordfile.php?recordfile=[Address]';
window.strTemplateOfVoiceType2 = 'http://[IP]:[Port]/atstar/index.php/cdr-cdrplay/uuidplay?uuid=[Address]';
window.strTemplateOfVoiceType0ForInner = 'http://[IPInner]:[PortInner]/manager/recordfile.php?uniqueid=[Address]';
window.strTemplateOfVoiceType1ForInner = 'http://[IPInner]:[PortInner]/manager/conf_recordfile.php?recordfile=[Address]';
window.strTemplateOfVoiceType2ForInner = 'http://[IPInner]:[PortInner]/atstar/index.php/cdr-cdrplay/uuidplay?uuid=[Address]';

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
window.dialogUrlPrefix = '';
//常量
window.contants = {
    guidAutoGenerate: "A0000000-0000-0000-0000-000000000000",
    GuidAsGroup_SysAdmin: "00000000-0000-0000-0000-000000000001",
    GuidAsGroup_SysOper: "00000000-0000-0000-0000-000000000002",
    GuidAsGroup_GovernmentAdmin: '00001000-0001-0000-0000-000000000001',
    GuidAsGroup_GovernmentOper: '00001000-0001-0000-0000-000000000002',
    GuidAsGroup_MerchantAdmin: "00001000-0002-0000-0000-000000000001", //商家管理员
    GuidAsGroup_MerchantOper: "00001000-0002-0000-0000-000000000002", //商家操作员
    GuidAsGroup_MerchantServeMan: "00001000-0002-0000-0000-000000000003", //商家服务人员
    GuidAsGroup_PensionAgencyAdmin: "00001000-0006-0000-0000-000000000001",//养老机构管理员
    GuidAsGroup_PensionAgencyOper: "00001000-0006-0000-0000-000000000002", //养老机构操作员
    GuidAsGroup_PensionAgencyServeMan: "00001000-0006-0000-0000-000000000003", //养老机构服务人员
    GuidAsGroup_TeamAdmin: "00001000-0003-0000-0000-000000000001",
    GuidAsGroup_TeamOper: "00001000-0003-0000-0000-000000000002",
    GuidAsGroup_OrganizationAdmin: "00001000-0004-0000-0000-000000000001",
    GuidAsGroup_OrganizationOper: "00001000-0004-0000-0000-000000000002",
    GuidAsGroup_CommunityAdmin: "00001000-0005-0000-0000-000000000001",
    GuidAsGroup_CommunityOper: "00001000-0005-0000-0000-000000000002", 
    UserTypeAsMerchant: "00003",
    charAutoGenerate: "自动生成",
    idAutoGenerate: -1,
    defaultGridId: "grid",
    defaultGridPagerId: "pager"
};

window.SpecialMenus = {
    RolledOutServiceList: { SystemMenuCode: 'B1800', MenuId: 'RolledOutServiceList', MenuCode: 'RolledOutServiceList', MenuTitle: '居家养老-转出处理', PageUrl: 'views/seat-client/rolledout-service-list.htm' },
    CallbackServiceList: { SystemMenuCode: 'B1800', MenuId: 'CallbackServiceList', MenuCode: 'CallbackServiceList', MenuTitle: '居家养老-回访处理', PageUrl: 'views/seat-client/callback-service-list.htm' },
    CallServiceList: { SystemMenuCode: 'B1800', MenuId: 'CallServiceList', MenuCode: 'CallServiceList', MenuTitle: '居家养老-话务处理', PageUrl: 'views/seat-client/call-service-list.htm' }
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
    pageList: [10, 15, 18],

    rownumbers: true,
    singleSelect: false,
    checkOnSelect: true,
    selectOnCheck: true,
    method: 'POST'
};

/***************************************/
/*暂时为解决网速慢
*而不能获取到客户端设置的top.execMode
*导致的无法正常使用客户端
*在此设置一个延时加载时间变量
*/
window.InitCSLoadTime = 3000;   //客户端加载延迟时间
/***************************************/