window.ServiceUrl = baseCMSInvokeUrl + "/Pub/CmsService/";
window.appDeployArea = { id: 99999, code: 120300 };
//window.Cms_UserCode = { "addr": "浙江省杭州市", "zip": "310000", "right": "蓝谷养老服务网" };

$(function () {
    //网站用户区域代码
    getTo(ServiceUrl + "GetParameterValue/Sys_CopyrightInfo", null, function (result) {
         if (result.ret.Value) { 
            window.Cms_UserCode = eval('(' + result.ret.Value + ')');
        }
        else {
            window.Cms_UserCode ={ "addr": "浙江省杭州市", "zip": "310000", "right": "蓝谷科技" }; //默认值
        }
    }, { async: false });

    //同步部署区域信息
    getTo(ServiceUrl + "GetParameterValue/Sys_AppDeployArea", null, function (result) {
        if (result.ret.Value) { 
            window.appDeployArea = eval('(' + result.ret.Value + ')');
        }
        else {
            alert("无效的部署节点！");
        }
    }, { async: false });
});


function ndateFormatterX(val, datefmt) {
    var date = null;
    if (val == null || val == "") return "";
    //if (val.indexOf("Date") < 0) return val;
    if (val.indexOf("Date") < 0) {
        date = new Date(val);
    } else {
        var time = eval('new ' + val.replace(/\//g, ''));
        date = new Date();
        date.setTime(time);
    }
    return date.pattern(datefmt);
}

function exchangeTiltle(ArtcileTiltle,length) {
    var newTitle = "";
    var _length = 18;
    if (length) {
        _length = length;
    }
    if (ArtcileTiltle) {
        if (ArtcileTiltle.length > _length) {
            newTitle = ArtcileTiltle.substr(0, _length) + "...";
        } else {
            newTitle = ArtcileTiltle;
        }
    }
    return newTitle;
}


function loadHeadFoot(){
    getHtml("head.htm", function (result_html) {
        $(".header").html(result_html);
    }, null);

    getHtml("foot.htm", function (result_html) {
        $(".footer").html(result_html);
    }, null);
}

/****************************百度地图api*******************************/
//创建和初始化地图函数：
function initMap(id, longitude, latitude) {
    //alert(id + ":" + longitude + ":" + latitude);
    createMap(id, longitude, latitude); //创建地图
    setMapEvent(); //设置地图事件
    addMapControl(); //向地图添加控件
    //addContextMenu(); //添加右键功能菜单坐标
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
function setMapEvent() {
    map.enableScrollWheelZoom(); //启用地图滚轮放大缩小
    map.enableKeyboard(); //启用键盘上下左右键移动地图
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

