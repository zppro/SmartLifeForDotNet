

$(function () {
    //$.ajaxf.install('script/flash4ajax/AJAX.swf');
    window.models = {};
    gToken = $.cookie("token");
    if (!gToken) {
        alert(gToken);
        //不存在token
        /*
        alertInfo("无法找到token。请重新登录后继续访问！", function () {
        Redirect(login_Url, true);
        });
        */
        Redirect(login_Url, true);
    }
    else {
        //存在token，继续检测服务端session
        
        getTo(checkIsLoggedIn_InvokeUrl, null, function (result) {
            if (!result.ret.LoggedIn) {
                if (gToken) {
                    top.onSessionOut();
                }
                else {
                    Redirect(login_Url);
                } 
                /*
                alertInfo("您长时间没有登录。请重新登录后继续访问！", function () {
                Redirect(login_Url);
                });
                */
            }
            else {
                //已经登录  
                var pageUrl = getTabPage(getQueryString('pageUrl'));
                
                window.currentMenuId = getQueryString('menuId');
                window.currentMenuCode = getQueryString('menuCode');
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
                                        $('a[behaviorCode="' + behavior.BehaviorCode + '"]', '#tabContent').show();
                                    }
                                    else {
                                        $('a[behaviorCode="' + behavior.BehaviorCode + '"]', '#tabContent').hide();
                                        var $menuItem = $('div[behaviorCode="' + behavior.BehaviorCode + '"]');
                                        if ($menuItem.size() > 0) {
                                            var $menu = $menuItem.parent();
                                            $menu.find('div.menu-item:visible').size();
                                            $menu.menu('removeItem', $menuItem[0]);
                                            if ($menu.find('div.menu-item:visible').size() == 0) {

                                                $menu.menu('destroy');

                                                var $menuButtons = $('.easyui-menubutton', '#tabContent');
                                                if ($menuButtons.size() > 0) {
                                                    $menuButtons.menubutton('destroy');
                                                }
                                            }
                                        }
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
    top.__remove(isPrimary, true);
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

function exec(isRough) {
    top._exec(isRough); 
}

function query() {
    top._query();
}

function resetfm(isRough) {
    top._resetfm(isRough);
}
function quit() {
    top._quit();
}

function openDialog(dialogId, href, done, options) {
    top.openDialog(dialogId, href, done, options);
}

function exportScript() {
    top._exportScript(true);
}

function exportData(options, data) {
    top._exportData(options, data);
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

function getServiceUrl(serviceQueueNo) {
    var parms = serviceQueueNo.substring(5, serviceQueueNo.length);
    var retUrl;
    switch (parms) {
        case "0":
            retUrl = window.dialogOfInfoEmergency_RelativeUrl;
            break;
        case "1":
            retUrl = window.dialogOfInfoFamilyCalls_RelativeUrl;
            break;
        case "2":
            retUrl = window.dialogOfInfoCommonService_RelativeUrl;
            break;
        case "3":
            retUrl = window.dialogOfInfoLife_RelativeUrl;
            break;
        default:
            break;
    }
    return retUrl;
}

///页面内置dialog
function openDialog2(dialogId, href, done, options) {
    var settings = $.extend({ title: '', width: 480, height: 360, padding: 2 }, options);
    var _buttons = [];
    if (settings.buttons && settings.buttons.length > 0) {
        for (var o in settings.buttons) {
            _buttons.push(settings.buttons[o]);
        }
    }
    if (settings.mergeDefault) {
        _buttons.push({
            text: '确定',
            iconCls: 'icon-ok',
            handler: function () {
                if (dialogBeforeSubmit()) {
                    if (done) {
                        done.apply(this, [function () { $('#' + dialogId).dialog('close'); }, dialogClose(dialogId)]);
                    }
                }
            }
        });
        _buttons.push({
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () { $('#' + dialogId).dialog('close'); }
        });
    }

    $('<div id="' + dialogId + '" style="width: ' + settings.width + 'px; height: ' + settings.height + 'px;padding:' + settings.padding + 'px;"></div>').appendTo('body')
    .dialog({
        onClose: function () {
            $(this).dialog('destroy');
        },
        onLoad: function () {
            dialogOpen(dialogId, settings.dialogData);
        },
        buttons: _buttons,
        title: settings.title,
        modal: true,
        href: href
        //content: '<div class="easyui-layout" data-options="fit:true"><div data-options="region:\'north\',border:false" style="height: 36px;"><span id="selectedArea" style="font-weight:bold;color:blue;"></span></div><div data-options="region:\'center\',border:false"><ul id="tree-SetArea"></ul></div></div>'
    }).dialog('open');
}

function initPrint() {
    $('<OBJECT  ID="jatoolsPrinter" CLASSID="CLSID:B43D3361-D075-4BE2-87FE-057188254255" codebase="../../script/jatoolsPrinter/jatoolsPrinter.cab#version=8,6,1,0"></OBJECT>').appendTo($(body));
}
function print() {
    var myDoc = {
        documents: document,
        pagePrefix: 'prt-',
        /*
        要打印的div 对象在本文档中，控件将从本文档中的 id 为 'page1' 的div对象，
        作为首页打印id 为'page2'的作为第二页打印            */
        copyrights: '杰创软件拥有版权  www.jatools.com' // 版权声明,必须   
    };
    jatoolsPrinter.print(myDoc, false); // 直接打印，不弹出打印机设置对话框 
}

//初始化占位符
function placeholder() {
    var JPlaceHolder = {
        //检测
        _check: function () {
            return 'placeholder' in document.createElement('input');
        },
        //初始化
        init: function () {
            if (!this._check()) {
                this.fix();
            }
        },
        //修复
        fix: function () {
            jQuery(':input[placeholder]').each(function (index, element) {
                var self = $(this), txt = self.attr('placeholder');
                self.wrap($('<div></div>').css({ position: 'relative', zoom: '1', border: 'none', background: 'none', padding: 'none', margin: 'none' }));
                var pos = self.position(), h = self.outerHeight(true), paddingleft = self.css('padding-left');
                var holder = $('<span></span>').text(txt).css({ position: 'absolute', left: pos.left, top: pos.top + 3, height: h, lienHeight: h, paddingLeft: paddingleft, color: '#888' }).appendTo(self.parent());
                self.focusin(function (e) {
                    holder.hide();
                }).focusout(function (e) {
                    if (!self.val()) {
                        holder.show();
                    }
                });
                holder.click(function (e) {
                    holder.hide();
                    self.focus();
                });
            });
        }
    };
    //执行
    jQuery(function () {
        JPlaceHolder.init();
    });
}