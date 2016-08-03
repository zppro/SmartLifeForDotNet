function setExecModeForSeat() {
    window.execMode = "ForSeat";
    ensureGlobalParams();
}

function ensureGlobalParams() {
    if (!top.gCallCenter) {
        /** ecomm ipcc ***/
        getTo(ajaxData_InvokeUrl + "/GetCallCenter", null, function (result) {
            window.gCallCenter = result;
        }, { async: false });
    }
}

function getCallCenterIP() {
    return window.gCallCenter.IP;
}

function LogoutForSeat() {
    if (window.execMode == "ForSeat") {
        BS_Logout(window.execMode);
    }
}

function OpenCallServiceWinForSeat(callServiceId) {
    getTo(callservice_InvokeUrl + '/' + callServiceId, null, function (ret) {
        var url = top.getServiceUrl(ret.instance.ServiceQueueNo);
        top.external.OpenNewWinAsProcessing(url, $.toJSON(ret.instance));
    });
}

function OpenCallServiceInfoWinForSeat(callServiceId) {
    getTo(callservice_InvokeUrl + '/' + callServiceId, null, function (ret) {
        var url = top.getServiceUrl(ret.instance.ServiceQueueNo);
        top.external.OpenNewWinAsInfo(url, $.toJSON(ret.instance));
    });
}

function getServiceObjectPhoneNos(callServiceId) {
    var phoneNos = []; 
    getTo(top.callservice_InvokeUrl + '/GetOldManPhoneNos/' + callServiceId, null, function (ret) {
        if (ret.Success) {
            phoneNos = ret.rows;
        }
    }, { async: false });
    return $.toJSON(phoneNos);
}

function dialBackForCallService(callServiceId, extNo) {
    var callService = {};
    postTo(top.callservice_InvokeUrl + '/DialBackForCallService', $.toJSON({ CallServiceId: callServiceId, ServiceExtNo: extNo }), function (result) {
        if (result.instance) {
            callService = result.instance;
        }
    }, { async: false });

    return $.toJSON(callService);
}

function dialBackForCallService(callServiceId, extNo, batchCallServiceIds) {
    var callService = {};
    postTo(top.callservice_InvokeUrl + '/DialBackForCallService', $.toJSON({ CallServiceId: callServiceId, ServiceExtNo: extNo, BatchCallServiceIds: batchCallServiceIds }), function (result) {
        if (result.instance) {
            callService = result.instance;
        }
    }, { async: false });

    return $.toJSON(callService);
}

function conferenceCreate(callServiceId) {
    var success = recordLog(callServiceId, 0, "开始多方通话");
    return success;
}
function conferenceRemove(callServiceId) {
    var success = recordLog(callServiceId, 0, "结束多方通话");
    return success;
} 
function conferenceMemberAdd(callServiceId, telInfo, needRemove) {
    var logContent = telInfo + '加入多方通话';
    if (needRemove) {
        var re = /\[(\d+):(.+):(.+)\]/;
        re.test(telInfo);
        var telType = parseInt(RegExp.$1);
        var tel = RegExp.$2;
        var msg = RegExp.$3; ;

        logContent += '<a href="javascript:void(0)" class="button-red-1-small" onclick="closeTel(' + telType + ',\'' + tel + '\',\'' + msg + '\');">移除</a>';
    }
    var success = recordLog(callServiceId, 0, logContent);
    return success;
}


function conferenceMemberRemove(callServiceId, telInfo) {
    var success = recordLog(callServiceId, 0, telInfo + "已离开会话");
    return success;
}

function controlConferenceMemberSuccess(callServiceId, telInfo, extNo, controlType) {
    var controlTypeName;
    
    if (controlType == "mute") {
        controlTypeName = '禁麦';
    }
    else if (controlType == "unmute") {
        controlTypeName = '取消禁麦';
    }
    else if (controlType == "deaf") {
        controlTypeName = '静音';
    }
    else if (controlType == "undeaf") {
        controlTypeName = '取消静音';
    } 
    if (controlTypeName) { 
        recordLog(callServiceId, 0, '座席<' + extNo + '>对' + telInfo + '设置' + controlTypeName);
    }
}

function updateCallSeconds(callServiceId, extNo, callSeconds) {
    var success = false;
    putTo(top.callservice_InvokeUrl + '/' + callServiceId, $.toJSON({ CallSeconds: callSeconds }), function (result) {
        success = result.Success;
    }, { async: false });

    if (success) {
        success = recordLog(callServiceId, 0, '座席<' + extNo + '>挂断,通话时长约' + easyuigrid_TimeSpanFormatter(callSeconds));
    }
    return success;
}

function finishServiceVoiceInEComm(callServiceId, voiceType, address, logContent) {
    if (logContent) {
        logContent += ' <a href="javascript:void(0)" class="button-red-1-small" onclick="openServiceVoice2InEComm(' + voiceType + ',\'' + address + '\');">录音</a>';
    }
    var success = recordLog(callServiceId, 0, logContent);
    return success;
}

function openServiceVoice2InEComm(voiceType, address) {
    var url;
    if (voiceType == 2) {
        //新IPCC
        var ip;
        var port;
        if (window.runMode == 0) {
            ip = top.gCallCenter.IP;
            port = top.gCallCenter.Port;
        }
        else if (window.runMode == 1) {
            ip = top.gCallCenter.IPProxy;
            port = top.gCallCenter.PortProxy;
        }
        else if (window.runMode == 2) {
            ip = top.gCallCenter.IPInner;
            port = top.gCallCenter.PortInner;
        }
        url = replaceWithKeys(top.strTemplateOfVoiceType2, { IP: ip, Port: port, Address: address });
    }
    else {
        //旧
    }

    if (url) { top.external.OpenWinAsServiceVoice(url); }
}

function reProcessCallService(callServiceId, extNo) {
    var success = recordLog(callServiceId, 0, '座席<' + extNo + '>重新服务');
    return success;
}
function switchFunc(menuId) { 
    if (menuId && menuId != window.currentMenuId) {
        normalizeTab();
        //居家养老-回访处理 
        var theSpecialMenu = window.SpecialMenus[menuId];
        openTab(theSpecialMenu.MenuId, theSpecialMenu.MenuCode, theSpecialMenu.MenuTitle, theSpecialMenu.PageUrl, "icon-max1", function () {
            maxTab(window.SpecialMenus.CallbackServiceList.MenuId);
        }, true, false, false, false);
        maxTab(theSpecialMenu.MenuId);
    }
}