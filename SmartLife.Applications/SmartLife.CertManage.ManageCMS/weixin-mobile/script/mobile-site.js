$(function () {
    //ajax设置
    $.ajaxSetup({
        cache: false,
        contentType: "application/json; charset=utf-8"
    });

    $("body").bind("ajaxSuccess", function (e, jhr, ajaxOptions) {
        if (ajaxOptions.dataType == "json") {
            if (jhr.responseText) {
                var result = $.evalJSON(jhr.responseText);
                if (result.Success != undefined) {
                    if (!result.Success) {
                        alertError(result.ErrorMessage);
                    }
                }
            }
        }
    }).bind("ajaxError", function (e, jhr, ajaxOptions) {
        if (ajaxOptions.dataType == "json") {
            var msg = "状态码：" + jhr.status + " 状态信息：" + jhr.statusText;
            if (jhr.responseText) {
                msg += "<br/><br/>详细信息:" + jhr.responseText;
            }
            alert(msg);
        }
    });

});

function getTo(url, data, success, options, headers) {
    var defaults = {
        type: 'GET',
        url: url,
        data: data,
        success: success,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    };
    var settings = $.extend(defaults, options || {});
    headers = _.extend(headers || {}, { 'device-type': 'mobile' });
    if (settings.beforeSend) {
        var fn_BeforeSend = settings.beforeSend;
        settings.beforeSend = function (jqXHR, s) {
            fn_BeforeSend(jqXHR, s);
            for (var key in headers) {
                jqXHR.setRequestHeader(key, headers[key]);
            }
        }
    }
    else {
        settings.beforeSend = function (jqXHR, s) {
            for (var key in headers) {
                jqXHR.setRequestHeader(key, headers[key]);
            }
        }
    }
    return $.ajax(settings);
}



//[{url1,data2,success3,options4},{url1,data2,success3,options4}]
function getAll(urls, d, f, headers) {
    var getTos = [];
    $.each(urls, function (i, o) {
        getTos.push(getTo(o, null, null, null, headers));
    });

    $.when.apply($, getTos).done(function () {
        var newArgs = [];
        if (_.isArray(urls) && urls.length > 1) {
            $.each(arguments, function (i, r) {
                newArgs.push(r[0]);
            });
        }
        else {
            newArgs.push(arguments[0]);
        }
        d.apply(this, newArgs);
    }).fail(f);
}
function postTo(url, data, success, options, headers) {
    var defaults = {
        type: 'POST',
        url: url,
        data: data,
        success: success,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    };
    var settings = $.extend(defaults, options || {});
    headers = _.extend(headers || {}, { 'device-type': 'mobile' });
    if (headers) {
        if (settings.beforeSend) {
            var fn_BeforeSend = settings.beforeSend;
            settings.beforeSend = function (jqXHR, s) {
                fn_BeforeSend(jqXHR, s);
                for (var key in headers) {
                    jqXHR.setRequestHeader(key, headers[key]);
                }
            }
        }
        else {
            settings.beforeSend = function (jqXHR, s) {
                for (var key in headers) {
                    jqXHR.setRequestHeader(key, headers[key]);
                }
            }
        }
    }
    return $.ajax(settings);
}

//[[url1,data1],[url2,data2]]
function postAll(urlAndDatas, d, f, headers) {
    var postTos = [];
    $.each(urlAndDatas, function (i, o) {
        postTos.push(postTo(o[0], o[1], null, null, headers));
    });

    $.when.apply($, postTos).done(function () {
        var newArgs = [];
        if (_.isArray(urlAndDatas) && urlAndDatas.length > 1) {
            $.each(arguments, function (i, r) {
                newArgs.push(r[0]);
            });
        }
        else {
            newArgs.push(arguments[0]);
        }
        d.apply(this, newArgs);
    }).fail(f);
}
function putTo(url, data, success, options, headers) {
    var defaults = {
        type: 'PUT',
        url: url,
        data: data,
        success: success,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    };
    var settings = $.extend(defaults, options || {});
    headers = _.extend(headers || {}, { 'device-type': 'mobile' });
    if (headers) {
        if (settings.beforeSend) {
            var fn_BeforeSend = settings.beforeSend;
            settings.beforeSend = function (jqXHR, s) {
                fn_BeforeSend(jqXHR, s);
                for (var key in headers) {
                    jqXHR.setRequestHeader(key, headers[key]);
                }
            }
        }
        else {
            settings.beforeSend = function (jqXHR, s) {
                for (var key in headers) {
                    jqXHR.setRequestHeader(key, headers[key]);
                }
            }
        }
    }
    return $.ajax(settings);
}
function deleteTo(url, success, options, headers) {
    var defaults = {
        type: 'DELETE',
        url: url,
        success: success,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    };
    var settings = $.extend(defaults, options || {});
    headers = _.extend(headers || {}, { 'device-type': 'mobile' });
    if (headers) {
        if (settings.beforeSend) {
            var fn_BeforeSend = settings.beforeSend;
            settings.beforeSend = function (jqXHR, s) {
                fn_BeforeSend(jqXHR, s);
                for (var key in headers) {
                    jqXHR.setRequestHeader(key, headers[key]);
                }
            }
        }
        else {
            settings.beforeSend = function (jqXHR, s) {
                for (var key in headers) {
                    jqXHR.setRequestHeader(key, headers[key]);
                }
            }
        }
    }
    return $.ajax(settings);
}

function deleteTo2(url, selected, done, headers) {
    var ajax = deleteTo(url + 'DeleteSelected/' + selected, null, null, headers);
    if (done) {
        ajax.done(function (ret) {
            if (ret.Success) {
                done.call(this, ret);
            }
        });
    }
    return ajax;
}

function getHtml(url, success, options) {
    var defaults = {
        type: 'GET',
        url: url,
        success: success,
        contentType: 'application/x-www-form-urlencoded',
        dataType: "html"
    };

    var settings = $.extend(defaults, options || {});
    return $.ajax(settings);
}
function loadScripts(scripts, callback) {
    $.getScript(scripts.shift(), scripts.length
       ? loadScripts.bind(null, scripts, callback)
       : callback
   );
}
function getAllScript(scripts, d, async, f) {
    if (async) {
        var getscripts = [];
        $.each(scripts, function (i, o) {
            getscripts.push($.ajax({
                url: o,
                dataType: "script",
                async: false
            }));
        });
        $.when.apply($, getscripts).then(function () {
            var newArgs = [];
            if (_.isArray(scripts) && scripts.length > 1) {
                $.each(arguments, function (i, r) {
                    newArgs.push(r[0]);
                });
            }
            else {
                newArgs.push(arguments[0]);
            }
            d.apply(this, newArgs);
        }).fail(f);
    }
    else {
        loadScripts(scripts, d);
    }
}

function loadfiles(files, d, f) {
    var css = _.where(files, { type: "css" });
    var js = _.where(files, { type: "js" });

    var cssHtmls = _.map(css, function (o) {
        return '<link rel="stylesheet" type="text/css" href="' + o.url + '" />';
    }).join('');
    $("head").append(cssHtmls);
    getAllScript(_.map(js, function (o) {
        return o.url;
    }), d, true, f);
}
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = decodeURI(window.location.search).substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}