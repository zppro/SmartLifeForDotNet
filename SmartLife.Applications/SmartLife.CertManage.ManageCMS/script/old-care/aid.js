function openTel(telType, tel, msg) {
    //记录日志
    if (!tel) {
        alertError(msg + "无效");
        return;
    }
    if (top.pageData) {
        if (top.execMode == "ForSeat" || top.execMode == "CS") {
            //客户端回拨
            var success = window.external.InvokeTransferPhone(telType, tel, msg);
            if (success) {
                if (recordLog(pageData.CallServiceId, 1, "坐席呼叫[" + telType + ":" + tel + ":" + msg + "]")) {

                }
            }
        }
        else {

            if (recordLog(pageData.CallServiceId, 1, "坐席呼叫[" + telType + ":" + tel + ":" + msg + "]")) {
                alert("Web:" + tel);
            }
        }
    }
}

function closeTel(telType, tel, msg) {
    if (top.execMode == "ForSeat" || top.execMode == "CS") {
        var success = window.external.InvokeRemovePhone(telType, tel, msg);
        if (success) {
            if (recordLog(pageData.CallServiceId, 1, "坐席移除[" + telType + ":" + tel + ":" + msg + "]")) {

            }
        }
    }
}

function doSearchOldManAsFriend(k) {
    alert(k);
}
function openChatRoom(chatRoom) {
    alert(chatRoom);
}
function openAnchor(anchor) {
    alert(anchor);
}
function openUrl(url) {
    alert(url);
}
//创建和初始化地图函数：
function initMap(id, longitude, latitude, callback) {
    createMap(id, longitude, latitude); //创建地图
    setMapEvent(callback); //设置地图事件
    addMapControl(); //向地图添加控件
    addContextMenu(); //添加右键功能菜单坐标
}

//创建地图函数：
function createMap(id, longitude, latitude) {

    var map = new BMap.Map(id); //在百度地图容器中创建一个地图
    var point = new BMap.Point(longitude, latitude); //定义一个中心点坐标
    map.centerAndZoom(point, 12); //设定地图的中心点和坐标并将地图显示在地图容器中
    //右上角，默认地图控件,开放卫星三维地图。
    map.addControl(new BMap.MapTypeControl({ anchor: BMAP_ANCHOR_TOP_RIGHT }));
    //map.setCurrentCity("杭州");   //有3D图，需要设置城市

    //初始化中心点
    var marker = new BMap.Marker(point);
    map.addOverlay(marker);
    marker.setAnimation(BMAP_ANIMATION_BOUNCE); //跳动的动画

    window.map = map; //将map变量存储在全局
}

//地图事件设置函数：
function setMapEvent(callback) {
    map.enableScrollWheelZoom(); //启用地图滚轮放大缩小
    map.enableKeyboard(); //启用键盘上下左右键移动地图

    //地图初始化完成
    if (callback) {
        var fn = function () {
            map.removeEventListener("tilesloaded", fn);
            if (_.isFunction(callback)) {
                callback.apply(this, []);
            }
        };
        map.addEventListener("tilesloaded", fn);
    }
}

//地图控件添加函数：
function addMapControl() {
    //向地图中添加缩放控件
    var ctrl_nav = new BMap.NavigationControl({ anchor: BMAP_ANCHOR_TOP_LEFT, type: BMAP_NAVIGATION_CONTROL_LARGE });
    map.addControl(ctrl_nav);
    //向地图中添加比例尺控件
    var ctrl_sca = new BMap.ScaleControl({ anchor: BMAP_ANCHOR_BOTTOM_LEFT });
    map.addControl(ctrl_sca);
}

//添加右键功能菜单坐标
function addContextMenu() {
    var menu = new BMap.ContextMenu();
    var txtMenuItem = [{ text: '在此添加标注', callback: function (p) {
        addPointMarker(p);
    }
    }];

    for (var i = 0; i < txtMenuItem.length; i++) {
        menu.addItem(new BMap.MenuItem(txtMenuItem[i].text, txtMenuItem[i].callback, 100));
    }

    map.addContextMenu(menu);
}

//添加坐标标记
function addAnimationMarker(point) {
    var marker = new BMap.Marker(point);
    map.addOverlay(marker);
    marker.setAnimation(BMAP_ANIMATION_BOUNCE); //跳动的动画
}

//添加坐标标记
function addPointMarker(point) {
    var marker = new BMap.Marker(point);
    //var px = map.pointToPixel(p);
    map.addOverlay(marker);
}

//设置缩放级别
function setViewport(point_arr) {
    map.setViewport(point_arr);
}

//获取两点之间的距离
function getDistanceaByPoints(start_point, end_point) {
    return map.getDistance(start_point, end_point);
}

//添加自定义标注点
function addCustomMarker(point, row) {
    var key;
    if (!isNaN(row)) {
        key = String.fromCharCode(65 + row);
    }
    else {
        key = row;
    }
    var icon = new BMap.Icon("../../images/map/" + key + ".png", new BMap.Size(26, 25),  //图片大小
                {  //绘制标注点
                anchor: new BMap.Size(12, 25),  // 指定定位位置
                imageOffset: new BMap.Size(0, 0), // 设置图片偏移
                imageSize: new BMap.Size(26, 25)    //图片显示的尺寸
            }
            );
    var marker = new BMap.Marker(point, { icon: icon });
    map.addOverlay(marker);
}
//添加返回自定义标注点
function retCustomMarker(point, row) {
    var key;
    if (!isNaN(row)) {
        key = String.fromCharCode(65 + row);
    }
    else {
        key = row;
    }
    var imgurl = "../../images/map/" + key + ".png"; ;
    var icon = new BMap.Icon(imgurl, new BMap.Size(26, 25),  //图片大小
                {  //绘制标注点
                anchor: new BMap.Size(12, 25),  // 指定定位位置
                imageOffset: new BMap.Size(0, 0), // 设置图片偏移
                imageSize: new BMap.Size(26, 25)    //图片显示的尺寸
            });

    var marker = new BMap.Marker(point, { icon: icon });
    map.addOverlay(marker);
    return marker;
}

//添加坐标标记
function retPointMarker(point) {
    var marker = new BMap.Marker(point);
    map.addOverlay(marker);

    return marker;
}

//添加标记点的连接线
function addPointLine(point_arr, bshow) {
    if (point_arr.length > 0) {
        var polyline = new BMap.Polyline(point_arr, { strokeColor: "blue", strokeWeight: 5, strokeOpacity: 0.4, fillOpacity: 0.4, strokeStyle: 'solid' });
        map.addOverlay(polyline);
        //默认显示 
        if (!bshow) polyline.hide();
    }
}

//返回标记点的连接线
function retPointLine(point_arr, bshow) {
    var retline = null;
    if (point_arr.length > 0) {
        var polyline = new BMap.Polyline(point_arr, { strokeColor: "blue", strokeWeight: 5, strokeOpacity: 0.4, fillOpacity: 0.4, strokeStyle: 'solid' });
        map.addOverlay(polyline);
        //默认显示 
        if (!bshow) polyline.hide();
        retline = polyline;
    }
    return retline;
}

//画圆
function addPointCircle(cen_point, radius) {
    var circle = new BMap.Circle(cen_point, radius, { strokeColor: "red", strokeWeight: 1, strokeOpacity: 0.3, fillColor: "red", fillOpacity: 0.3, strokeStyle: 'solid' }); //
    map.addOverlay(circle);
}

//添加标记点的文本说明
function createPointLabel(mak, point, txt, bshow) {
    var opts = {
        position: point,    // 指定文本标注所在的地理位置
        offset: new BMap.Size(-55, -30)    //设置文本偏移量
    }
    var label = new BMap.Label(txt, opts);  // 创建文本标注对象
    label.setStyle({
        color: "red",
        fontSize: "12px",
        height: "20px",
        lineHeight: "20px"
    });
    //是否初始化时隐藏
    if (bshow) {
        label.hide();
    }
    mak.setLabel(label);
    mak.addEventListener("mouseover", function (e) {
        e.currentTarget.getLabel().show();
    });
    mak.addEventListener("mouseout", function (e) {
        e.currentTarget.getLabel().hide();
    });
}

//添加标记点的非隐藏的文本说明
function createPointLabel2(mak, point, options, txt) {
    var dSettings = {
        offsetWidth: 0,
        offsetHeight: 0,
        labelStyle:{
            color: "red",
            backgroundColor: "#fff",
            fontSize: "12px",
            height: "20px",
            lineHeight: "20px"
        }
    };
    if (options) {
         _.extend(dSettings, options);
    }
    var opts = {
        position: point,    // 指定文本标注所在的地理位置
        offset: new BMap.Size(dSettings.offsetWidth, dSettings.offsetHeight)    //设置文本偏移量
    }
    var label = new BMap.Label(txt, opts);  // 创建文本标注对象
    label.setStyle(dSettings.labelStyle);

    mak.setLabel(label);
}

//添加标记点的信息窗口
function createPointInfoWindow(mak, point, txt) {
    var opts = {
        width: 200,     // 信息窗口宽度
        height: 160,     // 信息窗口高度
        enableMessage: false//设置允许信息窗发送短息
    }
    var infoWindow = new BMap.InfoWindow(txt, opts);  // 创建信息窗口对象

    mak.addEventListener("click", function (e) {
//        if (e.currentTarget.getLabel()) {
//            e.currentTarget.getLabel().hide();
//        }
        this.openInfoWindow(infoWindow);
        //        //图片加载完毕重绘infowindow
        //        document.getElementById('imgDemo').onload = function () {
        //            infoWindow.redraw();   //防止在网速较慢，图片未加载时，生成的信息框高度比图片的总高度小，导致图片部分被隐藏
        //        }
    });
}

function setZoomCenter(row) {
    var latlng = row.LatLng.toString().split(",");
    var point = null;
    if (latlng.length > 1) {
        point = new BMap.Point(latlng[0], latlng[1]);
        map.centerAndZoom(point, 20);
    }
    var len = map.getOverlays().length;
    if (len > 0 && point != null) {
        var mrk = null;
        var pnt = null;
        for (var i = 0; i < len; i++) {
            mrk = map.getOverlays()[i];
            if (mrk.toString().indexOf("Marker") >= 0 && mrk.getLabel()) {
                mrk.getLabel().hide();
                if (mrk.getPosition().equals(point)) {
                    mrk.getLabel().show();
                }
            }
        }
    }
}
//生成逆地址解析变量
function getGeo() {
    return new BMap.Geocoder();
}
//逆地址解析
function parsePointToAddress(BmpGeo, opptions) {
    var dSettings = {
        BmpPt: null,
        BmpMarker: null,
        BmpLabel: ''
    };
    _.extend(dSettings, opptions || {});
    if (dSettings) {
        BmpGeo.getLocation(dSettings.BmpPt, function (rs) {
            var addComp = rs.addressComponents;
            var strLabel = dSettings.BmpLabel + " ： " + addComp.street + addComp.streetNumber;
            createPointLabel.apply(this, [dSettings.BmpMarker, dSettings.BmpPt, strLabel, false]);
        });
    }
}


//编辑处理页面的服务类型
function editServiceTypeSelect(url, data, obj) {
    if (data) {
        $(obj).empty();
        var html = [];
        _.each(data, function (value, name) {
            var servicesTypeSr;
            switch (name) {
                case "Emergency":
                    servicesTypeSr = "<option value='" + value + "'>紧急救助</option>";
                    break;
                case "FamilyCalls":
                    servicesTypeSr = "<option value='" + value + "'>亲人通话</option>";
                    break;
                case "Infotainment":
                    servicesTypeSr = "<option value='" + value + "'>公共服务</option>";
                    break;
                case "Life":
                    servicesTypeSr = "<option value='" + value + "'>生活服务</option>";
                    break;
                default:
                    break;
            }
            html.push(servicesTypeSr);
        });
        if (html.length > 0) {
            $(obj).append(html.join(""));
            _.each(data, function (value) {
                if (url.indexOf(value) > 0) $(obj).val(value);
            });
        }
    }
}

function switchAidServiceType(url, pageData, obj) {
    if (pageData) {
        var selectTypeObj = $(obj).find("option:selected");
        var selectText = $(selectTypeObj).text();
        var selectType = selectText == "紧急救助" ? "0" : (selectText == "亲人通话" ? "1" : (selectText == "生活服务" ? "3" : "2"));

        if (url.indexOf(selectTypeObj.val()) < 0) {
            var initText;
            $(obj).find("option").each(function () {
                if (url.indexOf($(this).val()) > 0) initText = $(this).text();
            });
            //新URL
            url = $(selectTypeObj).val();

            var oldContent = pageData.Content;
            var oldQueueNo = pageData.ServiceQueueNo;
            var oldServiceQueueId = pageData.ServiceQueueId;

            var newContent = oldContent.replace(initText, selectText);
            var newQueueNo = oldQueueNo.substring(0, 5) + selectType;
            var newGroupType = oldServiceQueueId.substring(0, 4) + selectType;
            var newQueueId = oldServiceQueueId.replace(oldQueueNo, newQueueNo);
            newQueueId = newQueueId.replace(oldServiceQueueId.substring(0, 5), newGroupType);

            var objarray = { Content: newContent, ServiceQueueId: newQueueId, ServiceQueueNo: newQueueNo, ServiceQueueIdOld: oldServiceQueueId, ServiceQueueNoOld: oldQueueNo };
            putTo(baseCMSInvokeUrl + '/Oca/CallService/' + pageData.CallServiceId, $.toJSON(objarray), function (ret) {
                if (ret.Success) {
                    OpenNewWinAsCallService(url, pageData.CallServiceId);
                }
            });
        }
    }
}

//历史服务记录切换
function switchInfoServiceType(pageData, obj) {
    if (pageData) {
        var selectTypeObj = $(obj).find("option:selected");

        var initType = pageData.ServiceQueueNo.substring(5, pageData.ServiceQueueNo.length);
        var switchQueueType = pageData.ServiceQueueNo.substring(0, 5) + selectTypeObj.val();
        var initText;
        $(obj).find("option").each(function () { if ($(this).val() == initType) initText = $(this).text(); });
        if (initType != selectTypeObj.val()) {
            var url = getServiceUrl(switchQueueType);

            var oldContent = pageData.Content;
            var oldQueueNo = pageData.ServiceQueueNo;
            var oldServiceQueueId = pageData.ServiceQueueId;

            var newContent = oldContent.replace(initText, selectTypeObj.text());
            var newQueueNo = oldQueueNo.substring(0, 5) + selectTypeObj.val();
            var newQueueId = oldServiceQueueId.replace(oldQueueNo, newQueueNo);
            newQueueId = newQueueId.replace(oldServiceQueueId.substring(0, 5), (oldServiceQueueId.substring(0, 4) + selectTypeObj.val()));

            var updateobj = { ServiceQueueId: newQueueId, ServiceQueueNo: newQueueNo, ServiceQueueIdOld: oldServiceQueueId, ServiceQueueNoOld: oldQueueNo, Content: newContent };
            putTo(baseCMSInvokeUrl + '/Oca/CallService/' + pageData.CallServiceId, $.toJSON(updateobj), function (ret) {
                if (ret.Success) {
                    OpenNewWinAsCallService(url, pageData.CallServiceId, 'info');
                }
            });
        }
    }
}

function fetchServiceTrackLog(containerId, totalRows) {
    if (window.pageData) {

        getTo(serviceTrackLog_InvokeUrl + '/Query?parms=' + $.toJSON({ CallServiceId: pageData.CallServiceId }), null, function (result) {
            var htmls = [];
            if (result.rows.length > 0) {

                _.each(result.rows, function (row) {
                    var strLogContent = row.LogContent;
                    var strLogFileType = row.LogFileType;
                    var strContent;
                    if (strLogContent) {
                        strContent = strLogContent;
                        if (strContent.indexOf('[') != -1) {
                            var re = /\[(\d+):(.+):(.+)\]/;
                            re.test(strContent);
                            var type = parseInt(RegExp.$1);
                            if (type == 0) {
                                //紧急电话
                                strContent = strContent.replace(re, '<a href="javascript:void(0)" class="button-red-1-small" >$3</a>');
                            }
                            else if (type == 1) {
                                //社区电话
                                strContent = strContent.replace(re, '<a href="javascript:void(0)" class="button-yellow-1-small" >$3</a>');
                            }
                            else if (type == 2) {
                                //亲人电话
                                strContent = strContent.replace(re, '<a href="javascript:void(0)" class="button-blue-1-small" >$3</a>');
                            }
                            else if (type == 11) {
                                //生活服务
                                strContent = strContent.replace(re, '<a href="javascript:void(0)" class="button-red-2-small" >$3</a>');
                            }
                            else if (type == 98) {
                                //老人
                                strContent = strContent.replace(re, '<a href="javascript:void(0)" class="button-red-2-small" >$3</a>');
                            }
                            else if (type == 99) {
                                //坐席
                                strContent = strContent.replace(re, '$3');
                            }
                            //alert(RegExp.$1 + '|' + RegExp.$2 + '|' + RegExp.$3 + '|' + RegExp.$4);
                        }
                    }
                    else {

                    }

                    var fontClass = "";
                    if (row.LogType == 0) {
                        //系统
                        fontClass = "TrackLogRed";
                    }
                    else if (row.LogType == 1) {
                        //座席
                        fontClass = "TrackLogBlack";
                    }
                    else if (row.LogType == 2) {
                        //亲人
                        fontClass = "TrackLogBlue";
                    }
                    else if (row.LogType == 3) {
                        //商家
                        fontClass = "TrackLogGreen";
                    }
                    htmls.push("<div class=\"TrackLog " + fontClass + "\">" + ndateFormatter(row.CheckInTime, 'yyyy-MM-dd HH:mm:ss') + "&nbsp;&nbsp;" + strContent + "</div>");
                });
            }

            if (htmls.length > 0) {
                if (totalRows) {
                    for (var i = totalRows - htmls.length; i > 0; i--) {
                        htmls.push("<div class=\"TrackLog\">……………………………………………………………………………………</div>");
                    }
                }
                $('#' + containerId).html(htmls.join(""));
            }
        });
    }
}

function fetchServiceArchiveHistory(archiveId, totalRows) {
    if (window.pageData) {
        var queryPamams = { OldManId: window.pageData.OldManId, DoStatus: 3, DictionaryId: '11013', OrderByClause: 'CheckInTime desc' };
        getTo(baseCMSInvokeUrl + '/Oca/CallService/LoadLatestCallService?parms=' + $.toJSON(queryPamams), null, function (ret) {
            if (ret.rows && ret.rows.length > 0) {
                var rows = _.map(ret.rows, function (o) {
                    return xml2json.parser(o, 'StringObjectDictionary');
                });
                var htm = [];
                htm.push("<table width='100%'><tr>");
                htm.push(_.map(rows, function (item) {
                    var serviceQueueNo = item.ServiceQueueNo.toString();
                    var strFlag = right(serviceQueueNo, 1);
                    var strColor = strFlag == "0" ? "#ed1b24" : (strFlag == "1" ? "#00adef" : strFlag == "3" ? "#9bd3ae" : "");
                    return "<td style='text-align:center;background-color:" + strColor + ";'><a class='iconArchive' href='javascript:void(0)' CallServiceId='" + item.CallServiceId + "'  title='" + item.GroupName + ":" + item.ItemName + "' >"+ndateFormatter(item.CheckInTime, 'yyyy-mm-dd HH:mm:ss') +"</br>"+ left(item.GroupName, 4) + " : " + item.ItemName +"</a></td>";
                }).join(" "));
                htm.push("</tr></table>");

                $('#' + archiveId).html(htm.join(""));
                $('.iconArchive').css({ 'color': '#000', 'display': 'block','line-height':'18px' }).unbind('click').bind('click', function () {
                    var callServiceId = $(this).attr('CallServiceId');
                    getHtml(baseUrl + '/views/dialogs/get-history-service-process-content.htm', function (str) {
                        $("#service-history-block").html(str);
                        $.parser.parse('#btn-history-content');
                        $("#the-history-content").dialog({
                            inline: false,
                            onClose: function () {
                                $(this).dialog('destroy');
                            },
                            onBeforeOpen: function () {
                                dialogOpen(null, { CallServiceId: callServiceId });
                            },
                            modal: true
                        }).dialog('open').dialog('setTitle', '查看-服务历史日志');
                    });
                });
            }
        });
    }
}

function openServiceVoice(callServiceId, voiceType, ipAddress) {
    getTo(baseCMSInvokeUrl + '/Oca/CallService/GetServiceVoice/' + callServiceId, null, function (ret) {
        if (ret.Success) {
            var url;
            _.map(ret.rows, function (o) {
                var strDomain;
                if (o.VoiceType != voiceType) return true;
                if (o.IPInner.toString().indexOf('192.168') > 0) {
                    strDomain = o.IPInner + ':' + o.PortInner;
                }
                else {
                    strDomain = o.IP + ':' + o.Port;
                }
                if (strDomain) url = 'http://' + strDomain + (o.VoiceType == 0 ? '/manager/recordfile.php?uniqueid=' : "/manager/conf_recordfile.php?recordfile=") + o.Address;
            });
            if (url) window.open(url, "_blank");
        }
    });
}

//获取街道和社区信息(无权限)
function GetStreetAndCommunityInArea(parentId, levels) {
    var arr = [];
    if (parentId != null) {
        getTo(ajaxData_InvokeUrl + "/GetStreetAndCommunityInArea/" + parentId, null, function (result) {
            //根据levels选择不同层的数据
            for (var i = 0; i < result.length; i++) {
                if (result[i].Levels == levels) {
                    arr.push(result[i]);
                }
            }
        }, { async: false });
    }
    return arr;
}

//获取街道和社区信息(有权限)
function GetStreetAndCommunityInAreaAuthority(parentId, userId, levels) {
    var arr = [];
    if (userId && parentId && parentId != '' && userId != '') {
        getTo(baseCMSInvokeUrl + '/Pub/UserService/GetStreetAndCommunityInAreaAuthority/' + parentId + ',' + userId, null, function (result) {
            //根据levels选择不同层的数据
            for (var i = 0; i < result.length; i++) {
                if (result[i].Levels == levels) {
                    arr.push(result[i]);
                }
            }
        }, { async: false });
    }
    return arr;
}

//业务流程
function openFlowDialog(options, buttons) {
    openDialog(options.dialogId, options.dialogPage, options.beforeFlowDialogClose
    , { dialogData: options, title: options.title, width: 1000, height: 600, padding: 5,  buttons: buttons });
}

//取历史记录
function getCirculationHistory(flowModels) {
    var rows;
    var params = { BIZ_ID: flowModels.bizid, TableName: flowModels.tableName, OrderByClause: flowModels.OrderByClause, FlowName: flowModels.flowName };
    getTo(baseCMSInvokeUrl + "/Share/Circulation/QueryFlow?parms=" + $.toJSON(params), null, function (result) {
        if (result.rows) {
            rows = _.map(result.rows, function (o) {
                return xml2json.parser(o, 'StringObjectDictionary');
            });
        }
    }, { async: false }, flowModels.headers);
    return rows;
}

//呼叫手动输入号码
function TakeOtherTel(txtOtherTelNoId) {
    if ($("#" + txtOtherTelNoId).size() > 0) {
        var tel = $("#" + txtOtherTelNoId).val();
        //var testtel = /(^[0-9]{3,4}\-[0-9]{7,8}$)|(^[0-9]{7,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)|(13\d{9}$)|(15[0135-9]\d{8}$)|(18[267]\d{8}$)/;
        var teleReg = /^((0\d{2,3})-)(\d{7,8})$/;
        var mobileReg = /^0{0,1}1[358]\d{9}$/;
        if (teleReg.test(tel) || mobileReg.test(tel)) {
            openTel(2, tel, tel);
        }
        else {
            alertInfo("呼叫号码格式不正确!");
        }
    }
}