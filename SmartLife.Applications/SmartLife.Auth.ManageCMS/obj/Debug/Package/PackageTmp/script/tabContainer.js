

$(function () {
    //$.ajaxf.install('script/flash4ajax/AJAX.swf');
    window.models = {};
    gToken = $.cookie("token");
    var needCheckToken = window.gLogonMode == '0';
    if (needCheckToken && !gToken) {
        //不存在token
        Redirect(login_Url, true);
    }
    else {
        //存在token，继续检测服务端session
        getTo(checkIsLoggedIn_InvokeUrl, null, function (result) {
            if (!result.ret.LoggedIn) {
                Redirect(login_Url, true);
            }
            else {
                //已经登录  
                var pageUrl = getTabPage(getQueryString('pageUrl'));
                window.currentMenuId = getQueryString('menuId');
                window.currentMenuCode = getQueryString('menuCode');
                window.currentMenuTitle = getQueryString('menuTitle');
                var canFullScreen = getQueryString('canFullScreen');
                var additional = '';
                if (canFullScreen) {
                    additional = '<a id="btnNormalize" href="javascript:void(0);" class="easyui-linkbutton" style="display:none;position:absolute;top:2px;right:5px;" data-options="iconCls:\'icon-max1\',plain:true" onclick="top.normalizeTab()" ></a>';
                }
                if (pageUrl) {

                    getTo(ajaxData_InvokeUrl + '/Behavior/' + currentMenuId).done(function (behaviors) {
                        getHtml(pageUrl, function (str) {
                            $('#tabContent').html(str + additional);
                            $.parser.parse('#tabContent');
                            if (behaviors && behaviors.length > 0) {
                                _.each(behaviors, function (o) {
                                    var behavior = xml2json.parser(o, 'StringObjectDictionary');
                                    if (behavior.PermitFlag === 'True') {
                                        $('[behaviorCode="' + behavior.BehaviorCode + '"]', '#tabContent').show();
                                    }
                                    else {
                                        $('[behaviorCode="' + behavior.BehaviorCode + '"]', '#tabContent').hide();
                                    }
                                });

                            }

                            if (canFullScreen) {
                                if (top.isFullScreen) {
                                    $('#btnNormalize').show();
                                }
                            }
                        });
                    });
                }
            }
        });
    }
});

/**************** 被oldcare中的replace.js替换 ********************/
function closeTab(menuTitle) {
    top._closeTab(menuTitle);
}
function getTabPage(url) {
    return '../../' + url;
}

function getUrlRootFromOutside(urlRoot) {
    return urlRoot;
}

/**************** 重载site.js,index.js 里的方法为了 in frame or not in frame********************/
function autosize(id, offset) {
    if (!offset) {
        offset = 0;
    }
    $("#" + id).height($("#" + id).parent().parent().parent().height() + offset);
}

function getTabWidth() {
    return $('#tabContent').width();
}

function createSearchBox(options) {
    top._createSearchBox(options, true);
}

function createSearchBoxItem(options) {
    top._createSearchBox(options, false);
}

function add() {
    _add(true);
}
function addItem() {
    _add(false);
}
function _add(isPrimary) {
    top._add(isPrimary);
}
function edit(row) {
    _edit(true, row);
}
function editItem(row) {
    _edit(false, row);
}
function _edit(isPrimary, row) {
    top._edit(isPrimary, row);
}
function remove() {
    _remove(true);
}
function removeItem() {
    _remove(false);
}
function _remove(isPrimary) {
    top.__remove(isPrimary, false);
}

function nullify() {
    _nullify(true);
}
function nullifyItem() {
    _nullify(false);
}
function _nullify(isPrimary) {
    top.__remove(isPrimary, true, false);
}

function stop() {
    _stop(true);
}
function stopItem() {
    _stop(false);
}
function _stop(isPrimary) {
    top.__stop(isPrimary, true);
}
function restart() {
    _restart(true);
}
function restartItem() {
    _restart(false);
}
function _restart(isPrimary) {
    top.__stop(isPrimary, false);
}
function save() {
    _save(true);
}
function saveItem() {
    _save(false);
}
function _save(isPrimary) {
    top._save(isPrimary);
}
function cancel() {
    _cancel(true);
}
function cancelItem() {
    _cancel(false);
}
function _cancel(isPrimary) {
    top._cancel(isPrimary);
}

function exec() {
    top._exec(); 
}

function query() {
    top._query();
}

function clear() {
    top._clear();
}
function quit() {
    top._quit();
}
function exportScript() {
    top._exportScript(true);
}

function openDialog(dialogId, href, done, options) {
    top.openDialog(dialogId, href, done, options);
}

function addRow(gridId) {
    top.addRow(gridId);
}
function editRow(gridId, index) {
    top.editRow(gridId, index);
}
function nullifyRow(gridId) {
    top.nullifyRow(gridId);
}
function saveRow(gridId) {
    top.saveRow(gridId);
}