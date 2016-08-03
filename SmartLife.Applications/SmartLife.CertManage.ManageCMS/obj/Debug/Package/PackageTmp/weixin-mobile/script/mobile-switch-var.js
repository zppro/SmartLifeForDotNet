window.runMode = 1;
if (runMode == 1) {
    window.baseUrl = "http://192.168.18.100/SmartLife.CertManage.ManageCMS/weixin-mobile/";
    window.baseCMSInvokeUrl = "http://192.168.18.100/SmartLife.CertManage.ManageServices";
    window.baseWeiXinInvokeUrl = 'http://192.168.18.100/SmartLife.WeiXinCloud.OldCare';
}
else {
    window.baseUrl = "http://115.236.175.110:17000/weixin-mobile/";
    window.baseCMSInvokeUrl = "/120300/manageservices";
    window.baseWeiXinInvokeUrl = '/120300/weixinservices';
}