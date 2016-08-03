$(function () {

    if (!window.isCDR) {
        //初始化服务  
        postTo(Initilize_InvokeUrl, $.toJSON({ Host: window.theHost, ApplicationIdFrom: gApplicationId, ApplicationIdTo: gInvokeApplicationId, RunMode: window.runMode }), function (result) {
            if (result.Success) {
                window.objectId = result.ret.ObjectId;
            }
        }, { async: false });

        /****参数
        getTo(parameterData_InvokeUrl + "/Sys_RunMode", null, function (result) {
        window.runMode = result.ret.Value;
        });
        ****/
        //技术支持及信息
        getTo(parameterData_InvokeUrl + "/Sys_TechSupport", null, function (result) {
            window.techSupport = result.ret.Value;
            $("#aTechSupport").text(techSupport);
        });
        getTo(parameterData_InvokeUrl + "/Sys_TechSupportInfo", null, function (result) {
            window.techSupportInfo = eval('(' + result.ret.Value + ')');
            $("#aTechSupport").attr('href', techSupportInfo.site);
        });
        //微信开通标志
        getTo(parameterData_InvokeUrl + "/Sys_WeiXinFlag", null, function (result) {
            window.WeiXinFlag = result.ret.Value;
        }, { async: false });
        //ajaxData
        getTo(ajaxData_InvokeUrl + "/ApplicationAliasName/" + gApplicationId, null, function (result) {
            window.gApplicationAliasName = result.ret.Value;
            document.title = gApplicationAliasName;
            $("#ApplicationAliasName").text(gApplicationAliasName);
        });

        //显示完整区域路径
        getTo(parameterData_InvokeUrl + "/Sys_ShowFullAreaPathFlag", null, function (result) {
            window.showFullAreaPathFlag = result.ret.Value;
        }, { async: false });

        //同步部署区域信息
        getTo(parameterData_InvokeUrl + "/Sys_AppDeployArea", null, function (result) {
            window.appDeployArea = eval('(' + result.ret.Value + ')');
            if (window.objectId != window.appDeployArea.code) {
                alert("无效的部署节点！");
            }
        }, { async: false });

        /* old-ipcc */
        getTo(ajaxData_InvokeUrl + "/GetServiceStation?StationType=00004&AreaId=" + window.appDeployArea.id, null, function (result) {
            window.gCallCenterId = _.first(result).StationId;
        }, { async: false });

        /** ecomm ipcc ***/
        getTo(ajaxData_InvokeUrl + "/GetCallCenter", null, function (result) {
            window.gCallCenter = result;
        }, { async: false });

        //是否用分机号进行回拨
        getTo(parameterData_InvokeUrl + "/Oca_DialBackFlag", null, function (result) {
            window.gDialBackFlag = result.ret.Value;
        }, { async: false });
    }
});