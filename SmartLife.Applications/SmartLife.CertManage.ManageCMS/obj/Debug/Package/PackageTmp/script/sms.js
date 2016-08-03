var locateFlag = 1;

function getModel() {
    return top.getModel();
}
function getJQO(parms) {
    return top.getJQO(parms);
}

function openGps() {
    setingGps();
}
function closeGps() {
    setingGps();
    locateFlag = 0;
}

function setingGps(row) {
    var theModel = getModel();
    var gridIdKey = "gridId";
    var pkIdKey = "pkId";
    var baseModelUrlKey = "baseModelUrl";
    var pkKey = "pk";
    var formIdKey = "formId";
    var dialogIdKey = "dialogId";
    var dialogOptionKey = "dialogOption";
    var entityNameKey = "entityName";

    var isSys = getJQO('#' + theModel[pkIdKey]).attr("type") == 'sys';
    if (!row) {
        row = getJQO('#' + theModel[gridIdKey]).datagrid('getSelected');
    }
    if (row) {
        var url = theModel[baseModelUrlKey] + row[theModel[pkKey]];

        getTo(url).done(function (ret) {
            getJQO('#' + theModel[formIdKey]).form('clear');
            getJQO('#' + theModel[formIdKey]).form('load', ret.instance);

            getJQO('#' + theModel[dialogIdKey]).dialog(_.extend({
                onClose: function () {
                    theModel.uiMode = 'list';
                },
                modal: true
            }, theModel[dialogOptionKey])).dialog('open').dialog('setTitle', '编辑' + theModel[entityNameKey]);

        });
        theModel.uiMode = 'edit';

    }
    else {
        alertInfo('请选中要配置的记录信息！');
    }
}



function savegps(row) {
    var theModel = getModel();
    var pkIdKey = "pkId";
    var pkKey = "pk";
    var formIdKey = "formId";
    var dialogIdKey = "dialogId";
    var gridIdKey = "gridId";
    var baseModelUrlKey = "baseModelUrl";
    var defaultModelKey = "defaultModel";

    var isSys = getJQO('#' + theModel[pkIdKey]).attr("type") == 'sys';
    if (!row) {
        row = getJQO('#' + theModel[gridIdKey]).datagrid('getSelected');
    }
    if (row) {
        if (row.CallNo) {
            var smsData = {};
            smsData.Mobile = row.CallNo;
            smsData.SendContent = locateFlag ? '002' : '003';
            smsData.SourceCatalog = (locateFlag ? '开启' : '关闭') + '围栏报警';
            smsData.SourceId = row.OldManId;
            smsData.SendCatalog = 1;

            var ret = SendSms(smsData);
            if (ret) {
                var formData = getJQO('#' + theModel[formIdKey]).serializeObject();

                formData.PK = getJQO('#' + theModel[pkIdKey]).val() == "" ? theModel.getPKValueWhenAdd() : getJQO('#' + theModel[pkIdKey]).val();
                formData.CallNo = row.CallNo;
                formData.LocateFlag = locateFlag;
                formData.isCreate = false;

                saveTo(theModel[baseModelUrlKey], $.extend(theModel[defaultModelKey] || {}, formData), function () {
                    getJQO('#' + theModel[dialogIdKey]).dialog('close');
                    getJQO('#' + theModel[gridIdKey]).datagrid('reload');

                    theModel.uiMode = 'list';
                    alertInfo((locateFlag ? '开启' : '关闭') + 'GPS定位成功！');
                    locateFlag = 1;
                });
            }
        }
        else {
            alertInfo('请先配置呼叫号码！');
        }
    }
    else {
        alertInfo('请选中要操作的设备号！');
    }
}

function query2() {
    var theModel = getModel();
    var gridFilterIdKey = "gridFilterId";
    var dlgFilterIdKey = "dlgFilterId";
    var fmFilterIdKey = "fmFilterId";

    var arrSearchData = getJQO('#' + theModel[fmFilterIdKey]).serializeArray();
    var formDataArray = _.map(_.filter(arrSearchData, function (o) {
        if (o.value) {
            return o.name.indexOf('_Start') == -1 && o.name.indexOf('_End') == -1;
        }
    }), function (o) {
        return { key: o.name, value: o.value };
    });

    var instanceParm = _.map(_.filter(arrSearchData, function (o) {
        if (o.value) {
            return o.name.indexOf('_Start') > -1 || o.name.indexOf('_End') > -1;
        }
    }), function (o) {
        return { key: o.name, value: o.value };
    });

    var queryParamsArray = getJQO('#' + theModel[gridFilterIdKey]).datagrid('options').queryParams;
    _.each(instanceParm, function (o) {
        if (o.value) {
            queryParamsArray.filterFields.push(o);
        }
    });

    _.extend(queryParamsArray, { fuzzyFields: formDataArray });
    getJQO('#' + theModel[gridFilterIdKey]).datagrid('load', queryParamsArray);
}

function close_filter() {
    var theModel = getModel();
    var dlgFilterIdKey = "dlgFilterId";
    $('#' + theModel[dlgFilterIdKey]).dialog('close');
}


function SendRtlsSms(row) {
    var smsData = {};
    smsData.Mobile = row.CallNo;
    smsData.SendContent = '004';
    smsData.SourceCatalog = '获取实时定位';
    smsData.SourceId = row.OldManId;
    smsData.SendCatalog = 0;

    return SendSms(smsData);
}

function SendBatchSms(content, log) {
    var theModel = getModel();
    var gridFilterIdKey = "gridFilterId";
    var fmFilterIdKey = "fmFilterId";
    var gridIdKey = "gridId";
    var dlgFilterIdKey = "dlgFilterId";

    var arrSearchData = getJQO('#' + theModel[fmFilterIdKey]).serializeArray();
    var formDataArray = _.map(_.filter(arrSearchData, function (o) {
        if (o.value) {
            return o.name.indexOf('_Start') == -1 && o.name.indexOf('_End') == -1;
        }
    }), function (o) {
        return { key: o.name, value: o.value };
    });

    var instanceParm = _.map(_.filter(arrSearchData, function (o) {
        if (o.value) {
            return o.name.indexOf('_Start') > -1 || o.name.indexOf('_End') > -1;
        }
    }), function (o) {
        return { key: o.name, value: o.value };
    });

    var queryParamsArray = getJQO('#' + theModel[gridFilterIdKey]).datagrid('options').queryParams;
    _.each(instanceParm, function (o) {
        if (o.value) {
            queryParamsArray.filterFields.push(o);
        }
    });

    var sendSmsArray = [{ "key": "SendContent", "value": content }, { "key": "SourceCatalog", "value": log }];
    _.each(sendSmsArray, function (o) {
        if (o.value) {
            var bfind = false;
            _.filter(queryParamsArray.filterFields, function (item) {
                if (o.key == item.key) {
                    item.value = o.value;
                    bfind = true;
                }
            });
            if (!bfind) {
                queryParamsArray.filterFields.push(o);
            }
        }
    });

    var options = getJQO('#' + theModel[gridFilterIdKey]).datagrid('getPager').data("pagination").options;
    _.extend(queryParamsArray, { rows: options.pageNumber, page: options.pageSize });
    _.extend(queryParamsArray, { fuzzyFields: formDataArray });

    postTo(baseCMSInvokeUrl + "/Pub/SmsSendService/BatchCreate", $.toJSON(queryParamsArray), function (result) {
        if (result.Success) {
            ret = result.Success;
        }
    }, { async: false });

    getJQO('#' + theModel[dlgFilterIdKey]).dialog('close');
    getJQO('#' + theModel[gridIdKey]).datagrid('reload');
}


function SendSms(options) {
    var ret;
    var defaults = {
        'SourceTable': 'Oca_OldManConfigInfo',
        'BatchNum': getRndBatchNo(),
        'SendCatalog': 0,
        'SendResult': 0
    };

    _.extend(defaults, options || {});

    postTo(baseCMSInvokeUrl + "/Pub/SmsSendService/", $.toJSON(defaults), function (result) {
        if (result.Success) {
            ret = result.Success;
        }
    }, { async: false });

    return ret;
}

function getRndBatchNo(number) {
    var defaultNo = 100000000;
    var rndseed = (new Date()).getTime();

    rndseed = (rndseed * 9301 + 49297) % 233280;
    var ret = rndseed / (233280.0);

    if (number) defaultNo = number;
    return Math.ceil(ret * defaultNo);
}

//短信发送状态
function easyuigrid_sendSmsStatus(val, row, index) {
    var ret;
    switch (val) 
    {
        case 0:
            ret = "排队中";
            break;
        case 1:
            ret = "发送成功";
            break;
        case 2:
            ret = "发送失败";
            break;
        default:
            ret = "未知状态"
            break;
    }

    return ret;
}

//短信返回状态
function easyuigrid_receivedSmsStatus(val, row, index) {
    var ret = "smsCode" + val.toString();
    if (row.Status == 0 && val == 0) {
        return "发送中...";
    }
    else {
        if (smsResult[ret]) {
            return smsResult[ret];
        }
    }
    return "错误异常";
}

var smsResult = {
        smsCode0 : "发送短信成功",
        smsCode1 :"提交参数不能为空",
        smsCode2 :"账号无效或未开户",
        smsCode3 :"账号密码错误",
        smsCode4 :"预约发送时间格式不正确",
        smsCode5 :"IP不合法",
        smsCode6 :"号码中含有无效号码或不在规定的号段",
        smsCode7 :"非法关键字",
        smsCode8 :"内容长度超过上限，最大402字符",
        smsCode9 :"接受号码过多，最大1000",
        smsCode10 :"未知错误10",
        smsCode11 :"提交速度太快",
        smsCode12 :"您尚未订购[普通短信业务]，暂不能发送该类信息",
        smsCode13 :"您的[普通短信业务]剩余数量发送不足，暂不能发送该类信息",
        smsCode14 :"流水号格式不正确",
        smsCode15 :"流水号重复",
        smsCode16 :"超出发送上限（操作员帐户当日发送上限）",
        smsCode17 :"余额不足",
        smsCode18 :"扣费不成功",
        smsCode19 :"未知错误19",
        smsCode20 :"发送失败",
        smsCode21 :"未知错误21",
        smsCode22 :"未知错误22",
        smsCode23 :"未知错误23",
        smsCode24 :"账户状态不正常",
        smsCode25 :"账户权限不足",
        smsCode26 :"需要人工审核",
        smsCode27 :"未知错误27",
        smsCode28 :"发送内容与模板不符",
        smsCode29 :"扩展号太长或不是数字&accnum=",
        smsCode30 :"未知错误30",
        smsCode31 :"未知错误31",
        smsCode32 :"同一号码相同内容发送次数太多"
}