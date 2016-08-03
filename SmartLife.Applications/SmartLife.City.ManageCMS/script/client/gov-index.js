var clientWidth;
var clientHeight;
var pageParams = {};
var queryParams;
window.dialogUrlPrefix = '';
window.currentMenuId = undefined;
$(function () {
    window.models = {};
    gToken = $.cookie("token");
    if (window.gLogonMode == '0') {
        $.ajaxf.install('../../script/flash4ajax/AJAX.swf');
    }
    var needCheckToken = window.gLogonMode == '0';
    if (needCheckToken && !gToken) {
        Redirect(login_Url, true);
    }
    else {
        //存在token，继续检测服务端session 
        getTo(checkIsLoggedIn_InvokeUrl, null, function (result) {
            if (!result.ret.LoggedIn) {
                Redirect(login_Url);
            }
            else {
                //已经登录 
                gUserId = result.ret.UserId;
                gUserCode = result.ret.UserCode;
                gUserName = result.ret.UserName;

                dialogUrlPrefix = (window.execMode == 'ForGov' ? '/' : '');
                gUserInfo = { isSuperAdmin: false, isMerchantUser: false };
                var pageUrl = getQueryString('pageUrl');
                if (pageUrl) {
                    loadPage(pageUrl, getQueryString('pageParamsJsonStr'), getQueryString('target'), getQueryString('winName'));
                }
            }
        });
    }
});
function setClientSize(width, height) {
    clientWidth = parseFloat(width);
    clientHeight = parseFloat(height);
}

function getTabWidth() {
    return clientWidth - 20;
}
function switchPage(pageUrl, pageParamsJsonStr, target, winName) {
    if (target) {
        loadPage(pageUrl, pageParamsJsonStr, target, winName);
    }
    else {
        window.location.href = 'index.htm?target=' + isnull(target, '') + '&winName=' + escape(isnull(winName, '')) + '&pageParamsJsonStr=' + escape(isnull(pageParamsJsonStr, '')) + '&pageUrl=' + pageUrl;
    }
}
function loadPage(pageUrl, pageParamsJsonStr, target, winName) {
    if (target) {
        if (pageParamsJsonStr) {
            var splitX = getQuerySplit(pageUrl);
            var objQueryStrings = eval('(' + pageParamsJsonStr + ')');
            for (var k in objQueryStrings) {
                splitX = getQuerySplit(pageUrl);
                pageUrl += splitX + k + '=' + objQueryStrings[k];
            }
        }
        if (target == '_blank') {
            if (pageUrl.indexOf('~') == 0) {
                pageUrl = window.baseUrl + pageUrl.substr(1);
            }
            else if (pageUrl.indexOf('http://') == -1) {
                //内部系统
                pageUrl = window.baseUrl + '/' + pageUrl;
                pageUrl += getQuerySplit(pageUrl) + 'token=' + gToken;
            }

            var ret = window.external.IECheck();
            if (ret) {
                window.open(pageUrl, winName, 'scrollbars=1,top=0,left=0,width=' + screen.width + ', height=' + screen.height);
            }
            else {
                window.external.OpenIE(pageUrl);
            }
        }
    }
    else {
        var loadingBox = new ajaxLoader($('#clientContainer'), { classOveride: 'blue-loader' });
        getHtml(pageUrl, function (str) {

            if (pageParamsJsonStr) {
                pageParams = eval('(' + pageParamsJsonStr + ')');
            }
            var queryParts = pageUrl;
            if (queryParts.indexOf('?') != -1) {
                queryParts = queryParts.substr(queryParts.indexOf('?') + 1);
            }
            var arrQueryParams = queryParts.split('&');
            queryParams = {};
            for (var i = 0; i < arrQueryParams.length; i++) {
                var key = arrQueryParams[i].split('=')[0];
                var val = arrQueryParams[i].split('=')[1];
                queryParams[key] = val;
            }
            $('#clientContainer').html(str);

            $.parser.parse('#clientContainer');
            if (loadingBox) loadingBox.remove();
        });
    }
}