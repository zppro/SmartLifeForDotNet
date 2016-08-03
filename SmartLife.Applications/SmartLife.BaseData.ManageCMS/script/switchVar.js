//运行模式
window.runMode = 1;

window.theHost = window.location.host; 
if (runMode == 1) {
    //开发模式 
    window.baseCertInvokeUrl = "/SmartLife.BaseData.CertServices";
    window.baseUrl = "/SmartLife.BaseData.ManageCMS";
    window.baseCMSInvokeUrl = "/SmartLife.BaseData.ManageServices";
    theHost = window.location.host + baseCMSInvokeUrl;
}
else {
    window.baseCertInvokeUrl = "http://1203.basedata.lifeblue.cn:12031";
    window.baseUrl = "http://1203.basedata.lifeblue.cn:12030";
    window.baseCMSInvokeUrl = "/manageservices";
}

//页面地址集合
window.login_Url = baseUrl + "/login.htm";
window.index_Url = baseUrl + "/index.htm";

window.redirectTagOfIndex = "IndexPage";
window.redirectTagOfLogin = "LoginPage";