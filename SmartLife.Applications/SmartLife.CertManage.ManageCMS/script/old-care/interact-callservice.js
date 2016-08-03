function getCallService(jsonstr) {
    var callService = {};
    var paramObj = eval('(' + jsonstr + ")");
    var queryUrl = top.callservice_InvokeUrl + '/FindByClient/';
    if (paramObj.Queue && paramObj.Queue.substring(paramObj.Queue.length - 1, paramObj.Queue.length) == '2') {
        queryUrl = top.callservice_InvokeUrl + '/FindByClient2/';
    }
    //过滤0开头的呼叫号码
    var caller = paramObj.Caller;
    if (caller.substring(0, 1)=='0') {
        caller = caller.substring(1);
    }
    getTo(queryUrl + caller + "," + paramObj.Callee + "," + paramObj.Queue, null, function (result) {
        if (result.instance) {
            callService = result.instance;
        }
    }, { async: false });
    return $.toJSON(callService);
}

function getCallService2(callServiceId) {
    var callService = {};
    getTo(top.callservice_InvokeUrl + '/' + callServiceId, null, function (result) {
        if (result.instance) {
            callService = result.instance;
        }
    }, { async: false });
    return $.toJSON(callService);
}

function getCallServiceByUuid(uuid) {
    var callService = {};
    getTo(top.callservice_InvokeUrl + '/FindByUuidOfIPCC/' + uuid, null, function (result) {
        if (result.instance) {
            callService = result.instance;
        }
    }, { async: false });
    return $.toJSON(callService);
}

function saveCallService(callServiceId, jsonstr, logContent) {
    var success = false;
    putTo(top.callservice_InvokeUrl + '/' + callServiceId, jsonstr, function (result) {
        success = result.Success;
    }, { async: false });

    if (success) {
        success = recordLog(callServiceId, 0, logContent);
    }
    return success;
}

function beginProcessCallService(callServiceId, jsonstr, logContent) {
    var success = false;
    putTo(top.callservice_InvokeUrl + '/' + callServiceId, jsonstr, function (result) {
        success = result.Success;
    }, { async: false });

    if (success) {
        success = recordLog(callServiceId, 0, logContent);
    }
    return success;
}

//保存回拨的UUID
function saveNoLogCallService(callServiceId, jsonstr) {
    var success = false;
    putTo(top.callservice_InvokeUrl + '/' + callServiceId, jsonstr, function (result) {
        success = result.Success;
    }, { async: false });

    return success;
}


function addServiceVoice(callServiceId, voiceType, Address, logContent) {
    var success = false;
    postTo(top.serviceVoiceService_InvokeUrl + '/', $.toJSON({ CallServiceId: callServiceId, VoiceType: voiceType, Address: Address }), function (result) {
        success = result.Success;
    }, { async: false });

    if (success && logContent) {
        success = recordLog(callServiceId, 0, logContent);
    }
    return success;
}

function finishServiceVoice(callServiceId, voiceType, ipAddress, logContent) {
    if (logContent) {
        logContent += ' <a href="javascript:void(0)" class="button-red-1-small" onclick="openServiceVoice(\'' + callServiceId + '\',\'' + voiceType + '\',\'' + ipAddress + '\');">录音</a>';
    }
    var success = recordLog(callServiceId, 0, logContent);
    return success;
}

function telConnected(callServiceId, telInfo, needRemove) {
    var logContent = telInfo + '已接通';
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
function telDisConnected(callServiceId,telInfo) {
    var success = recordLog(callServiceId, 0, telInfo + "已离开会话");
    return success;
}

function recordLog(callServiceId, logType, logContent) {
    var success = false;
    postTo(top.serviceTrackLog_InvokeUrl + '/', $.toJSON({ LogType: logType, LogContent: logContent, StationId: top.gCallCenterId, CallServiceId: callServiceId }), function (result) {
        success = result.Success;
    }, { async: false });
    return success;
}

function getWorkOrders(listContainerId, containerId, callServiceId,workOrderInfoUrl,splitChar) {
    getTo(top.baseCMSInvokeUrl + '/Oca/CallService/GetServiceWorkOrderInProcess/' + callServiceId, null, function (ret) {
        var htmls = [];
        var rows = _.map(ret.rows, function (o) {
            return xml2json.parser(o, 'StringObjectDictionary');
        });
        _.each(rows, function (o, index) {
            //htmls.push(((index > 0 && index % 2 == 0) ? '<br/>' : '') + '<a id="' + o.WorkOrderId + '" class="workorder-link">' + o.WorkOrderNo + '' + (o.DoStatus == 9 ? '【作废】' : '') + '</a>');
            htmls.push((index > 0 ? '<br/>' : '') + '<a id="' + o.WorkOrderId + '" class="workorder-link">' + o.WorkOrderNo + '' + (o.DoStatus == 9 ? '【作废】' : '') + '</a>');
        });

        if (htmls.length > 0) {
            $('#' + listContainerId).html(htmls.join((splitChar || ";")));

            $('.workorder-link').unbind('click').bind('click', function () {
                var workOrderId = $(this).attr('id');

                getHtml(workOrderInfoUrl, function (str) {
                    $("#" + containerId).html(str);
                    $.parser.parse('#btn-workorder');
                    $("#the-workorder").dialog({
                        inline: false,
                        onClose: function () {
                            $(this).dialog('destroy');
                        },
                        onBeforeOpen: function () {
                            //$('#fm-life-service-dispatch').form('clear');
                            loadWorkOrderContent(workOrderId);
                        },
                        modal: true
                    }).dialog('open').dialog('setTitle', '工单对话框');
                });
            });
        }
        else {
            $('#' + listContainerId).html("");
        }
    });
}
  
function changeHoldRetreiveCall(status) { 
    if ($('#btnHoldRetreive').size() == 0) {
        return;
    }
    if (status == 0) {
        $('#btnHoldRetreive').text('恢复'); 
    }
    else if (status == 1) {
        $('#btnHoldRetreive').text('呼叫保持'); 
    }
}

function takeOverCallServiceOfCallBack(callServiceId, extNo) {
    var callService = {};
    postTo(top.callservice_InvokeUrl + '/TakeOverCallServiceOfCallBack', $.toJSON({ CallServiceId: callServiceId, ServiceExtNo: extNo }), function (result) {
        if (result.instance) {
            callService = result.instance;
        }
    }, { async: false });
    
    return $.toJSON(callService);
}

function getDialBackCallNos(oldManId) {
    var oldManConfigInfo = {};
    getTo(top.baseCMSInvokeUrl + '/Oca/OldManConfigInfoService/' + oldManId, null, function (ret) {
        if (ret.instance) {
            oldManConfigInfo = ret.instance;
        }
    }, { async: false });
    return $.toJSON(oldManConfigInfo);
}