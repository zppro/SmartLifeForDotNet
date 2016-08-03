//运行模式
window.runMode = 1;
if (top.external.SwitchClientRunMode) {

    runMode = top.external.SwitchClientRunMode();
}
window.theHost = window.location.host;
//window.X2JS = new X2JS();
if (runMode == 1) {
    //开发模式 
    window.baseIPCCOfCityInvokeUrl = 'http://ipcc.ngrok.com';
    //window.baseCertInvokeUrl = "/SmartLife.CertManage.CertServices";原始版本

    window.baseUrl = "http://localhost/SmartLife.CertManage.ManageCMS";
    window.baseContentUrl = "http://localhost/SmartLife.CertManage.ContentServices_120300";
    window.baseCMSInvokeUrl = "http://localhost/SmartLife.CertManage.ManageServices";
    window.baseMerchantInvokeUrl = "http://localhost/SmartLife.CertManage.MerchantServices";
    theHost = baseCMSInvokeUrl;
    window.baseWeiXinOfServiceOnlineInovkeAddress = "http://localhost/SmartLife.WeiXinCloud.OldCare"; //"http://localhost:8077";
}
else if (runMode == 2) {
    //内网模式
    window.baseIPCCOfCityInvokeUrl = 'http://115.236.175.109:16903';
    //window.baseCertInvokeUrl = "/SmartLife.CertManage.CertServices";
    window.baseUrl = "/SmartLife.CertManage.ManageCMS";
    window.baseContentUrl = "/SmartLife.CertManage.ContentServices";
    window.baseCMSInvokeUrl = "/SmartLife.CertManage.ManageServices";
    window.baseMerchantInvokeUrl = "/SmartLife.CertManage.MerchantServices";
    theHost = "http://192.168.1.68" + baseCMSInvokeUrl;
    //window.baseUrl = "http://192.168.1.11:17000"; //蓝谷演示测试
    window.baseWeiXinOfServiceOnlineInovkeAddress = "http://wx.lifeblue.com.cn/120300";
}
else {
    window.baseIPCCOfCityInvokeUrl = 'http://115.236.175.109:16903';
    window.baseUrl = "http://115.236.175.110:17000"; //蓝谷演示测试
    //window.baseUrl = "http://115.236.175.110:17101"; //上城区-移动
    //window.baseUrl = "http://115.236.175.110:17102"; //下城区-移动
    //window.baseUrl = "http://115.236.175.110:17103"; //江干区-移动
    //window.baseUrl = "http://115.236.175.110:17104"; //拱墅区-移动
    //window.baseUrl = "http://115.236.175.110:17105"; //西湖区-移动
    //window.baseUrl = "http://115.236.175.110:17106"; //滨江区-蓝谷
    //window.baseUrl = "http://115.236.175.110:17003"; //南京三魁
    //window.baseUrl = "http://115.236.175.110:17002"; //杭州东忠
    //window.baseUrl = "http://115.236.175.110:17001"; //第一居委会
    //window.baseUrl = "http://115.236.175.110:17004"; //江干演示
    window.baseContentUrl = "/contentservices";
    window.baseCMSInvokeUrl = "/manageservices";
    window.baseMerchantInvokeUrl = "/merchantservices";
    window.baseWeiXinOfServiceOnlineInovkeAddress = "/weixinservices";
}
window.baseIPCCOfCityInvokeUrl = 'http://115.236.175.109:16903';
if (runMode == 2) {
    //内网模式
    window.baseCertInvokeUrl = "http://115.236.175.109:16000"; //蓝谷演示测试
}
else {
    //window.baseCertInvokeUrl = "http://115.236.175.109:16000"; //蓝谷演示测试 非单点认证 -远程
    //加入认证平台
    //window.baseCertInvokeUrl = "/SmartLife.Auth.CertServices"; //单点认证 -本地
    window.baseCertInvokeUrl = "http://115.236.175.109:120"; //无域名 -远程
}
//window.baseCertInvokeUrl = "http://115.236.175.109:16101"; //上城区-移动
//window.baseCertInvokeUrl = "http://115.236.175.109:16102"; //下城区-移动
//window.baseCertInvokeUrl = "http://115.236.175.109:16103"; //江干区-移动
//window.baseCertInvokeUrl = "http://115.236.175.109:16104"; //拱墅区-移动
//window.baseCertInvokeUrl = "http://115.236.175.109:16105"; //西湖区-移动
//window.baseCertInvokeUrl = "http://115.236.175.109:16106"; //滨江区-蓝谷
//window.baseCertInvokeUrl = "http://115.236.175.109:16003"; //南京三魁
//window.baseCertInvokeUrl = "http://115.236.175.109:16002"; //杭州东忠
//window.baseCertInvokeUrl = "http://115.236.175.109:16001"; //第一居委会
//window.baseCertInvokeUrl = "http://115.236.175.109:16004"; //江干演示
 
window.baseCertInvokeUrl = "http://121.40.154.222:120"; //无域名 -远程

//页面地址集合
window.login_Url = baseUrl + "/login.htm";
//window.login_Url = baseUrl+"/login_mobile.htm"; //上城区-移动
//window.login_Url = baseUrl+"/login_mobile.htm"; //下城区-移动
//window.login_Url = baseUrl+"/login_mobile.htm"; //江干区-移动
//window.login_Url = baseUrl+"/login_mobile.htm"; //拱墅区-移动
//window.login_Url = baseUrl+"/login_mobile.htm"; //西湖区-联通
//window.login_Url = baseUrl+"/login.htm"; //滨江区-蓝谷
window.index_Url = baseUrl + "/index.htm";
window.loginOfCompany_Url = baseUrl + "/loginOfCompany.html";
window.indexOfCompany_Url = baseUrl + "/indexOfCompany.html";

window.redirectTagOfSignIn = "SignIn";
window.redirectTagOfSignOut = "SignOut";
window.redirectTagOfIndex = "IndexPage";
window.redirectTagOfLogin = "LoginPage";

window.redirectTagOfAutoLoginToGovIndex = "IndexPage-Gov-Client";
window.redirectTagOfAutoLoginToMerIndex = "IndexPage-Mer-Client";
window.redirectTagOfAutoLoginToPamIndex = "IndexPage-Pam-Client";

window.baseInfoNode = "SmartLife1206"; //市级节点
window.authInfoNode = "SmartLife12"; //省级节点