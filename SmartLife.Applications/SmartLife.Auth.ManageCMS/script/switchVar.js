//运行模式
window.runMode = 1;

window.theHost = window.location.host; 
if (runMode == 1) {
    //开发模式 
    window.baseCertInvokeUrl = "/SmartLife.Auth.CertServices";
    window.baseUrl = "/SmartLife.Auth.ManageCMS";
    window.baseCMSInvokeUrl = "/SmartLife.Auth.ManageServices";
    theHost = window.location.host + baseCMSInvokeUrl;
}
else {
    //window.baseCertInvokeUrl = "http://auth.lifeblue.cn:120"; //有域名
    //window.baseUrl = "http://auth.lifeblue.cn:12"; //有域名
    window.baseCertInvokeUrl = "http://115.236.175.109:120"; //无域名
    window.baseUrl = "http://115.236.175.109:12"; //无域名
    window.baseCMSInvokeUrl = "/manageservices";
}

//页面地址集合
window.login_Url = baseUrl + "/login.htm";
window.index_Url = baseUrl + "/index.htm";

window.redirectTagOfSignIn = "SignIn";
window.redirectTagOfSignOut = "SignOut";
window.redirectTagOfIndex = "IndexPage";
window.redirectTagOfLogin = "LoginPage";


window.redirectTagOfAutoLoginToGovIndex = "IndexPage-Gov-Client";
window.redirectTagOfAutoLoginToMerIndex = "IndexPage-Mer-Client";
window.redirectTagOfAutoLoginToPamIndex = "IndexPage-Pam-Client";