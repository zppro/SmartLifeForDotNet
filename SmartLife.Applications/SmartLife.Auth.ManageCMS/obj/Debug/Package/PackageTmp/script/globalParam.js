$(function () {

    if (!window.isCDR) {
        //初始化服务 
        postTo(Initilize_InvokeUrl, $.toJSON({ Host: window.theHost, ApplicationIdFrom: gApplicationId, ApplicationIdTo: gInvokeApplicationId, RunMode: window.runMode }), function (result) {
            if (result.Success) {
                window.objectId = result.ret.ObjectId;
            }
        }, { async: false });

        //技术支持及信息
        getTo(parameterData_InvokeUrl + "/Sys_TechSupport", null, function (result) {
            window.techSupport = result.ret.Value;
            $("#aTechSupport").text(techSupport);
        });
        getTo(parameterData_InvokeUrl + "/Sys_TechSupportInfo", null, function (result) {
            window.techSupportInfo = eval('(' + result.ret.Value + ')');
            $("#aTechSupport").attr('href', techSupportInfo.site);
        });

        //版权及信息
        getTo(parameterData_InvokeUrl + "/Sys_Copyright", null, function (result) {
            window.copyright = result.ret.Value;
            $("#aCopyright").text(copyright);
        });
        getTo(parameterData_InvokeUrl + "/Sys_CopyrightInfo", null, function (result) {
            window.copyright = eval('(' + result.ret.Value + ')');
            $("#aCopyright").attr('href', copyright.site);
        });
        //ajaxData
        getTo(ajaxData_InvokeUrl + "/ApplicationAliasName/" + gApplicationId, null, function (result) {
            window.gApplicationAliasName = result.ret.Value;
            document.title = gApplicationAliasName;
            $("#ApplicationAliasName").text(gApplicationAliasName);
        });

        //登录模式
        getTo(parameterData_InvokeUrl + "/Sys_LogonMode", null, function (result) {
            window.gLogonMode = result.ret.Value;
        }, { async: false });

        //同步部署区域信息
        getTo(parameterData_InvokeUrl + "/Sys_AppDeployArea", null, function (result) {
            window.appDeployArea = eval('(' + result.ret.Value + ')');
            if (window.objectId != window.appDeployArea.code) {
                alert("无效的部署节点！");
            }
        }, { async: false });


    }
});