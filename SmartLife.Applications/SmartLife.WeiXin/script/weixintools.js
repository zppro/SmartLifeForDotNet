//获取url中的参数
function GetRequest() {
    var url = location.search; //获取url中"?"符后的字串
    var theRequest = new Object();
    if (url.indexOf("?") != -1) {
        var str = url.substr(1);
        strs = str.split("&");
        for (var i = 0; i < strs.length; i++) {
            theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
        }
    }
    return theRequest;
}
//ajax 删除
function deleteTo(url, success, options) {
    var defaults = {
        type: 'DELETE',
        url: url,
        success: success,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    };
    var settings = $.extend(defaults, options || {});
    return $.ajax(settings);
}

//进度条 默认不带样式
function defProgress() {
    $.mobile.loadingMessageTextVisible = false;
    $.mobile.showPageLoadingMsg();
}

//进度条 带样式
function themeProgress(theme) {
    $.mobile.loadingMessageTextVisible = true;
    $.mobile.loadingMessageTheme = theme;
    $.mobile.showPageLoadingMsg();
}

//进度条 带样式 带文字
function textProgress(theme, text) {
    $.mobile.loadingMessageTextVisible = true;
    $.mobile.showPageLoadingMsg(theme, text);
}

//隐藏滚动条 只显示文字
function noProgress(theme, text) {
    $.mobile.loadingMessageTextVisible = true;
    $.mobile.showPageLoadingMsg(theme, text, true);
}

//关闭进度条
function closeProgress() {
    $.mobile.hidePageLoadingMsg();
}

//0自动填充左补齐位数
function pad(num, n) {
    var len = num.toString().length;
    while (len < n) {
        num = "0" + num;
        len++;
    }
    return num;
}

//身份证号合法性验证 
//支持15位和18位身份证号
//支持地址编码、出生日期、校验位验证
function IdentityCodeValid(code) {
    var city = { 11: "北京", 12: "天津", 13: "河北", 14: "山西", 15: "内蒙古", 21: "辽宁", 22: "吉林", 23: "黑龙江 ", 31: "上海", 32: "江苏", 33: "浙江", 34: "安徽", 35: "福建", 36: "江西", 37: "山东", 41: "河南", 42: "湖北 ", 43: "湖南", 44: "广东", 45: "广西", 46: "海南", 50: "重庆", 51: "四川", 52: "贵州", 53: "云南", 54: "西藏 ", 61: "陕西", 62: "甘肃", 63: "青海", 64: "宁夏", 65: "新疆", 71: "台湾", 81: "香港", 82: "澳门", 91: "国外 " };
    var tip = "";
    var pass = true;

    if (!code || !/^(\d{6})(18|19|20)?(\d{2})([01]\d)([0123]\d)(\d{3})(\d|X|x)?$/i.test(code)) {
        tip = "身份证号格式错误";
        pass = false;
    }

    else if (!city[code.substr(0, 2)]) {
        tip = "身份证地址编码错误";
        pass = false;
    }
    else {
        //18位身份证需要验证最后一位校验位
        if (code.length == 18) {
            code = code.split('');
            //∑(ai×Wi)(mod 11)
            //加权因子
            var factor = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2];
            //校验位
            var parity = [1, 0, 'X', 9, 8, 7, 6, 5, 4, 3, 2];
            var parity2 = [1, 0, 'x', 9, 8, 7, 6, 5, 4, 3, 2];
            var sum = 0;
            var ai = 0;
            var wi = 0;
            for (var i = 0; i < 17; i++) {
                ai = code[i];
                wi = factor[i];
                sum += ai * wi;
            }
            var last = parity[sum % 11];
            if ( (parity[sum % 11] != code[17]) && (parity2[sum % 11] != code[17]) ) {
                tip = "身份证最后一位错误";
                pass = false;
            }
        }
    }
    if (!pass) alert(tip);
    return pass;
}

//验证手机号码(检验13,14,15,18开头的手机号！)  
function check_telephone(phoneNO) {
    var reg = /^[1][358]\d{9}$/;
    if (phoneNO == "" || !reg.test(phoneNO)) {
        alert("手机号码格式输入错误！");
        return false;
    } else {
        return true;
    }
}  