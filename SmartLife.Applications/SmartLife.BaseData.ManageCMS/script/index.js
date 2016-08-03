$(function () {

    window.models = {};
    window.isOutterSystemOfMainCenter = false;
    window.isFullScreen = false;
    window.tabCC = null;
    //区域设置   
    $('body').layout('panel', 'west').panel({ onResize: function (width, height) {
        $.cookie("west-width", parseInt(width));
        $('#applications-in-platform-c').layout('panel', 'east').panel({ width: $('body').width() - width + splitX });
        $('#logined-panel').width(width - splitX);
    }
    });

    //检测是否登录 
    gToken = $.cookie("token");
    if (window.gLogonMode == '0') {
        $.ajaxf.install('script/flash4ajax/AJAX.swf');
    }
    var needCheckToken = window.gLogonMode == '0';

    if (needCheckToken && !gToken) {
        //不存在token
        Redirect(login_Url);
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
                var loginedPanels = '<div id="logined-user">' + gUserName + '</div><div id="logined-action"><a id="btnSettings" ><span class="login-icon-settings">&nbsp;</span>设置</a>|<a id=\"btnLogout\"><span class="login-icon-exit">&nbsp;</span>退出</a></div>';
                $("#logined-panel").html(loginedPanels);

                $("#btnSettings").bind('click', function (e) {
                    $('#mm-settings').menu('show', {
                        left: e.pageX,
                        top: e.pageY + 10
                    });
                });
                $("#btnLogout").bind('click', function () {
                    alertConfirm('您确定要退出系统吗？', function (r) {
                        if (r) {
                            if (window.gLogonMode == '0') {
                                Flash_Logout(top.execMode);
                            }
                            else {
                                Local_Logout();
                            }
                        }
                    });
                });

                $('#tt').tabs({
                    onSelect: function (title, index) {
                        var tab = $(this).tabs('getTab', index);
                        if (tab.data('menuId')) {
                            window.currentMenuId = tab.data('menuId');
                            if (tab.data('selectOnRefresh') == true && document.frames['tab_' + window.currentMenuId]) {
                                document.frames['tab_' + window.currentMenuId].location.reload();
                            }
                        }
                        if (tab.data('menuCode')) {
                            window.currentMenuCode = tab.data('menuCode');
                        }
                    }
                });

                loadMenu(function (menus) {

                    //载入平台级
                    $("#platforms").append(_.map(menus, function (o) {
                        return $("<li MenuId=\"" + o.MenuId + "\" MenuCode=\"" + o.MenuCode + "\" PageUrl=\"" + isnull(o.PageUrl, "") + "\"><a offClass=\"" + o.Picture + "\" class=\"" + o.Picture + "\" title=\"" + isnull(o.Description, "") + "\"></a><span>" + o.MenuName + "</span></li>");
                    })); //.kwicks({ max: 52 * menus.length, spacing: 0, sticky: true, event: 'click' })

                    if (menus.length > 0) {
                        $("#platforms li").bind('click', function () {
                            //on-off
                            var $a = $(this).find('a');
                            var $span = $(this).find('span');
                            var pageUrlOfPlatform = $(this).attr('PageUrl');
                            var menuId = $(this).attr('MenuId');
                            var menuCode = $(this).attr('MenuCode');
                            if (menuCode == "B1200") {
                                //openTab(menuId, menuCode, '欢迎页', 'views/eab-manage/welcome.htm', true, false, false, false);
                            }


                            _.each($("#platforms li a"), function (o) {
                                var onClass = $(o).attr('offClass') + '-on';
                                $(o).removeClass(onClass);
                            });
                            _.each($("#platforms li span"), function (o) {
                                $(o).removeClass('on');
                            });
                            $a.addClass($a.attr('offClass') + '-on');
                            $span.addClass('on');


                            $('.ext-system').hide();
                            if (pageUrlOfPlatform) {
                                $("#applications-in-platform").html("");

                                if (!isOutterSystemOfMainCenter) {
                                    var expandWidth = $('body').layout('panel', 'center').panel('options').width + parseInt($.cookie("west-width"));
                                    $('body').layout('panel', 'center').panel('resize', { width: expandWidth, left: 0 });
                                    $('body').layout('panel', 'west').panel('collapse');
                                    //$('body').layout('panel', 'south').panel('collapse');
                                    $('body').layout('panel', 'center').panel({ fit: false });
                                    $('#tt').hide();
                                    $('#logined-panel').hide();
                                    //是否存在指定的iframe  

                                    isOutterSystemOfMainCenter = true;
                                }

                                if ($('#ext_' + menuCode).size() == 0) {
                                    var html = '<iframe id="ext_' + menuCode + '" name="ext_' + menuCode + '" class="ext-system" scrolling="auto" frameborder="0"  src="' + pageUrlOfPlatform + '" style="position:absolute;left:0px;top:0px;width:' + $('#tt').tabs('options').width + 'px;height:' + $('#tt').tabs('options').height + 'px;"></iframe>';
                                    $(html).appendTo('#main-center');
                                }
                                else {
                                    $('#ext_' + menuCode).show();
                                }
                            }
                            else {
                                if (isOutterSystemOfMainCenter) {
                                    var collapseWidth = $('body').layout('panel', 'center').panel('options').width - parseInt($.cookie("west-width"));
                                    $('body').layout('panel', 'center').panel('resize', { width: collapseWidth, left: parseInt($.cookie("west-width")) });
                                    $('body').layout('panel', 'west').panel('expand');
                                    //$('body').layout('panel', 'south').panel('expand');
                                    $('body').layout('panel', 'center').panel({ fit: true });
                                    $('#tt').show();
                                    $('#logined-panel').show();
                                    isOutterSystemOfMainCenter = false;
                                }

                                loadMenu(function (menus2) {
                                    //载入应用级
                                    $("#applications-in-platform").html(_.map(menus2, function (o2) {
                                        return "<a href=\"javascript:void(0);\" MenuId=\"" + o2.MenuId + "\" MenuCode=\"" + o2.MenuCode + "\" title=\"" + isnull(o2.Description, "") + "\">" + o2.MenuName + "</a>";
                                    }).join(""));

                                    if (menus2.length > 0) {
                                        $("#applications-in-platform a").bind('click', function () {
                                            //on-off
                                            _.each($("#applications-in-platform a"), function (o) {
                                                $(o).removeClass('on');
                                            });
                                            $(this).addClass('on');
                                            var menu2Name = $(this).text();

                                            loadMenuAsCTE(function (menus3) {
                                                if (menus3.length > 0) {

                                                    var nodes = _.map(menus3, function (o3) {
                                                        return { id: o3.MenuId, pId: o3.ParentId, name: o3.MenuName, code: o3.MenuCode, iconSkin: "menu3", pageUrl: o3.PageUrl, endFlag: o3.EndFlag, openInFrameFlag: o3.OpenInFrameFlag, selectOnRefreshFlag: o3.SelectOnRefreshFlag, canFullScreenFlag: o3.CanFullScreenFlag };
                                                    });
                                                    var treeObj = $("#navigations");
                                                    zTreeNavigationSetting.callback.onClick = function (event, treeId, treeNode) {
                                                        //alert(treeNode.code + '|' + treeNode.name + '|' + treeNode.pageUrl);
                                                        var iconCls = treeNode.canFullScreenFlag == 1 ? 'icon-max1' : '';
                                                        var iconHandler = treeNode.canFullScreenFlag == 1 ? function () { maxTab(treeNode.id) } : null;
                                                        openTab(treeNode.id, treeNode.code, treeNode.name, treeNode.pageUrl, iconCls, iconHandler, treeNode.openInFrameFlag == 1, true, treeNode.selectOnRefreshFlag == 1, treeNode.canFullScreenFlag == 1);
                                                    };

                                                    zTreeNavigationSetting.callback.beforeClick = function (treeId, treeNode) {
                                                        if (!treeNode.endFlag) {
                                                            var zTree = $.fn.zTree.getZTreeObj(treeId);
                                                            zTree.expandNode(treeNode);
                                                            return false;
                                                        }
                                                        return true;
                                                    };
                                                    $.fn.zTree.init(treeObj, zTreeNavigationSetting, nodes);
                                                    treeObj.prepend('<div class="navigation-head-bg">' + menu2Name + '</div>');
                                                    $.fn.zTree.getZTreeObj("navigations").expandAll(true);
                                                    /**/

                                                }
                                                else {
                                                    $("#navigations").empty();
                                                }
                                            }, $(this).attr("MenuId"), false, false, true);
                                        }).first().trigger('click');
                                    }
                                    else {
                                        $("#applications-in-platform").empty();
                                        $("#navigations").empty();
                                    } //end menu2s if
                                }, $(this).attr("MenuId"));
                            }

                        });
                        var platformItemsSize = $("#platforms li").size();
                        if (platformItemsSize > 1) {
                            $("#platforms li").first().trigger('click');
                        }
                        else {
                            $("#platforms li").hide();
                            if (platformItemsSize == 1) {
                                $("#platforms li").first().trigger('click');
                            } 
                        }

                    }

                }); //loadmenu
            }
        });
    }

});

function loadApplications(platformId) {

    _.each($("#applications-in-platform a"), function (o) {
        if ($(o).attr("platformId") == platformId) {
            $(o).show();
        }
        else {
            $(o).hide();
        }
    });
    $("#applications-in-platform").show();
}

function loadMenuByApp(fn, applicationId, parentId) {
    var url = ajaxData_InvokeUrl + "/AppMenu/" + applicationId + "," + (parentId || "null");
    getTo(url, null, function (result) { 
        if (fn) {
            fn.call(this, result.rows);
        }
    });
}

function loadMenu(fn, parentId) {
    var url = ajaxData_InvokeUrl + "/Menu/" +  (parentId || "null");
    getTo(url, null, function (result) {
        if (fn) {
            fn.call(this, result.rows);
        }
    });
}

function loadMenuAsCTE(fn, menuId, isAncestor, isInCludeSelf, isPermit) {
    var url = ajaxData_InvokeUrl + "/MenuAsCTE/" + (menuId || "null") + ',' + (isAncestor ? "1" : "0") + ',' + (isInCludeSelf ? "1" : "0") + ',' + (isPermit ? "1" : "0");
    getTo(url, null, function (result) {
        if (fn) {
            fn.call(this, result.rows);
        }
    });
}
function normalizeTab() {
    if (tabCC) {
        isFullScreen = false;
        $('#tab_' + currentMenuId).toggleClass('menuIdFrameMax', false).appendTo(tabCC);
    }
}
function maxTab(menuId) {
    isFullScreen = true;
    tabCC = $('#tab_' + menuId).parent();
    $('#tab_' + menuId).toggleClass('menuIdFrameMax', true).appendTo('body');
    if (top.execMode == "CS") {
        //解决客户端内嵌问题
        window.frames['tab_' + menuId].location.reload();
    }
}
function openTab(menuId,menuCode, menuTitle, pageUrl, iconCls, iconHandler, isInFrame, closable, selectOnRefresh, canFullScreen) {

    if (!$('#tt').tabs('exists', menuTitle)) {
        window.currentMenuId = menuId;
        window.currentMenuCode = menuCode;
        var tabUrl = (pageUrl || 'views/shared/empty.htm');
        var tabOption = {
            title: menuTitle,
            height: "auto",
            closable: closable,
            style: { padding: "8px" }
        };

        if (iconCls) {
            _.extend(tabOption, { tools: [{
                iconCls: iconCls,
                handler: iconHandler 
            }]
            });
        }

        if (isInFrame) {
            //tabOption.content = '<iframe name="tab_' + menuCode + '" scrolling="auto" framebordbackground: 'white', position: 'absolute', top: 0, left: 0, right: 0, bottom: 0, width: "100%", height: "100%", "z-index": 99999er="0"  src="views/shared/service-process-panel.htm?menuCode=' + menuCode + '" style="width:100%;height:100%;"></iframe>';
            //
            tabOption.content = '<iframe id="tab_' + menuId + '" name="tab_' + menuId + '" scrolling="auto" frameborder="0" class="menuIdFrameNormal" style="background:white;width:100%;height:100%;"></iframe>';

        }
        else {

            tabOption.href = tabUrl;
        }
        $('#tt').tabs('add', tabOption);
        if (isInFrame) {
            formatTabUrlInFrame(tabUrl, menuId, menuCode, canFullScreen);
        }
        var tab = $('#tt').tabs('getSelected');
        tab.data('menuId', currentMenuId);
        tab.data('menuCode', currentMenuCode);
        tab.data('selectOnRefresh', selectOnRefresh);
    }
    else {
        $('#tt').tabs('select', menuTitle);
    }
}

function formatTabUrlInFrame(tabUrl, menuId, menuCode, canFullScreen) {
    var newUrl = 'views/shared/tabContainer.htm?pageUrl=' + tabUrl + '&menuId=' + menuId + '&menuCode=' + menuCode;
    if (canFullScreen) {
        newUrl += '&canFullScreen=' + canFullScreen;
    }
    $('#tab_' + menuId).attr('src', newUrl);
}

function getTreeContainerHeight() {
    if ($.browser.msie) {
        return getContainerHeight() + 36;
    }
    else {
        return getContainerHeight() + 42;
    }
    
}
function getContentBoxHeight() {
    var top_h = 70;
    return $("#content-box").height() - top_h;
}
function getContainerHeight() {
    if ($.browser.msie) {
        return getContentBoxHeight() - $(".toolbar").height() - 5;
    }
    else {
        return getContentBoxHeight() - $(".toolbar").height();
    }
}
function getContainerWidth() {
    return $("#content-box").width() - 10;
}
//before use backbone.js
function openMenu(x,params) {
    $(".form").remove();
    $('#content-box').load('Views/' + x);
    if (params) {
        $('#content-box').data("params", params);
    }
}
function getPageParams() {
    return $('#content-box').data("params")
}

function changeUserPassword() {
    //修改密码
    openDialog('dlg-login-setting', 'views/dialogs/change-user-password.htm', function (callback, dialogData) {
        putTo(baseCMSInvokeUrl + '/Pub/UserService/ChangeUserPassword/' + gUserId, $.toJSON(dialogData)).done(function (result) {
            if (result.Success) {
                alertInfo('修改成功！');
            }
            callback();
        });
    }, { dialogData: { UserId: gUserId,
        UserCode: gUserCode,
        UserName: gUserName
    }, title: '修改密码对话框', width: 320, height: 260, padding: 5
    });
} 
function Local_Logout() {
    postTo(LogOffUserLocal_InvokeUrl, $.toJSON({ ApplicationIdFrom: gApplicationId, ApplicationIdTo: gInvokeApplicationId, Tag: redirectTagOfLogin }), function (result) {
        if (result.Success) {
            var url = replaceWithKeys(result.ret.RedirectUrl, { cmsSiteRoot: baseUrl });
            Redirect(url);
        }
    });
}
function Flash_Logout(_execMode) {
    $.ajaxf.postJSON(LogOffUserFlash_InvokeUrl, $.toJSON({ ApplicationIdFrom: gApplicationId, ApplicationIdTo: gInvokeApplicationId, Token: gToken }), function (result1) {
        if (result1.Success) {
            if (result1.ret.RedirectUrl) {
                //alert(result1.ret.RedirectUrl);
                //清理动作1 - 如果是坐席解除绑定 
                var url = replaceWithKeys(result1.ret.RedirectUrl, { cmsInterfaceRoot: baseCMSInvokeUrl, tag: redirectTagOfLogin });
                //alert("url:" + url);
                getTo(url, null, function (result2) {
                    if (result2.Success) {
                        var url2 = replaceWithKeys(result2.ret.RedirectUrl, { cmsSiteRoot: baseUrl });
                        Redirect(url2);
                    }
                    else {
                        alertError(result2.ErrorMessage);
                    }
                });
            }
        }
    });
} 

/*************** easyUI 基本操作 ******************/
function getModel() { 
    if (!models[currentMenuCode]) {
        return window.frames['tab_' + currentMenuId].models[currentMenuCode];
    }
    return models[currentMenuCode];
}
function setModel(key, val) { 
    if (!models[currentMenuCode]) {
        window.frames['tab_' + currentMenuId].models[currentMenuCode][key] = val;
    }
    models[currentMenuCode][key] = val; 
}
function getJQO(selector, fromTab) {
    if ($(selector).size() == 0) {
        if (window.frames['tab_' + currentMenuId]) {
            return window.frames['tab_' + currentMenuId].$(selector);
        }
    }
    return $(selector);
}

function createSearchBox(options) {
    _createSearchBox(options, true);
}
function createSearchBoxItem(options) {
    _createSearchBox(options, false);
}
function _createSearchBox(options, isPrimary) {
    var theModel = getModel();
    var gridIdKey = isPrimary ? "gridId" : "gridIdOfItem";
    var searchBoxIdKey = isPrimary ? "searchBoxId" : "searchBoxIdOfItem";
    var menuIdKey = isPrimary ? "menuId" : "menuIdOfItem";
    var fields = getJQO('#' + theModel[gridIdKey]).datagrid('getColumnFields');
    options = options || {};
    var schFieldOptions = _.map(_.filter(fields, function (o) {
        return getJQO('#' + theModel[gridIdKey]).datagrid('getColumnOption', o).sch === true;
    }), function (o) { return getJQO('#' + theModel[gridIdKey]).datagrid('getColumnOption', o) });
    var muit = '<div name="default">默认</div>' + _.map(schFieldOptions, function (o) {
        return "<div name='" + o.field + "'>" + o.title + "</div>";
    }).join("");
    getJQO('#' + theModel[menuIdKey]).html(getJQO('#' + theModel[menuIdKey]).html() + muit);
    getJQO('#' + theModel[searchBoxIdKey]).searchbox({
        width: (options.width || 200),
        searcher: (options.searcher || function (keyword, type) {
            var fuzzyFields;
            if (type == "default") {
                fuzzyFields = _.map(schFieldOptions, function (o) {
                    return { key: o.field, value: keyword };
                });
                fuzzyFields.push({ key: 'InputCode1', value: keyword });
            }
            else {
                fuzzyFields = _.map(_.filter(schFieldOptions, function (o) {
                    return o.field === type;
                }), function (o) {
                    return { key: o.field, value: keyword };
                });
            }
            getJQO('#' + theModel[gridIdKey]).datagrid('load', _.extend(getJQO('#' + theModel[gridIdKey]).datagrid('options').queryParams, {
                fuzzyFields: fuzzyFields
            }));
        }),
        menu: '#' + theModel[menuIdKey],
        prompt: options.prompt || '请输入查询内容'
    });
}

function add() {
    _add(true);
}
function addItem() {
    _add(false);
}
function _add(isPrimary) {
    var theModel = getModel();
    var dialogIdKey = isPrimary ? "dialogId" : "dialogIdOfItem";
    var dialogOptionKey = isPrimary ? "dialogOption" : "dialogOptionOfItem";
    var entityNameKey = isPrimary ? "entityName" : "entityNameOfItem";
    var formIdKey = isPrimary ? "formId" : "formIdOfItem";
    var pkIdKey = isPrimary ? "pkId" : "pkIdOfItem";
    var disabledKey = isPrimary ? "disabled" : "disabledOfItem";
    var defaultModelWhenAddKey = isPrimary ? "defaultModelWhenAdd" : "defaultModelWhenAddOfItem";
    var isSys = getJQO('#' + theModel[pkIdKey]).attr("type") == 'sys';

    if (theModel.onBeforeDialogOpen) {
        if (_.isFunction(theModel.onBeforeDialogOpen)) {
            theModel.onBeforeDialogOpen.apply(this, []);
        }
    }
    //隐藏反作废按钮
    getJQO('#btnRedoStatus').hide();
    if (isPrimary && theModel.containerIdOfItem) {
        getJQO('#' + theModel.containerIdOfItem).hide();
    }
    getJQO('#' + theModel[dialogIdKey]).dialog(_.extend({
        inline: false,
        onClose: function () {
            if (isPrimary) {
                theModel.uiMode = 'list';
            }
            else {
                theModel.uiModeOfItem = 'list';
            }
        },
        modal: true
    }, theModel[dialogOptionKey])).dialog('open').dialog('setTitle', '新增' + theModel[entityNameKey]);
    getJQO('#' + theModel[formIdKey]).form('clear');

    if (theModel[defaultModelWhenAddKey]) {
        getJQO('#' + theModel[formIdKey]).form('load', theModel[defaultModelWhenAddKey]);
    }
    
    if (theModel.disabled) {
        getJQO('#' + theModel[pkIdKey]).attr("disabled", _.any(theModel[disabledKey], function (o) { return o == 'add' }));
    }

    if (theModel.getPKValueWhenAdd) {
        getJQO('#' + theModel[pkIdKey]).val(theModel.getPKValueWhenAdd(isPrimary));
    }
    else {
        getJQO('#' + theModel[pkIdKey]).val("");
    }
   
    if (isPrimary) {
        theModel.uiMode = 'add'; 
    }
    else {
        theModel.uiModeOfItem = 'add';
    }
    if (theModel.onAfterDialogOpen) {
        if (_.isFunction(theModel.onAfterDialogOpen)) {
            theModel.onAfterDialogOpen.apply(this, []);
        }
    }
}
function edit(row) {
    _edit(true, row);
}
function editItem(row) {
    _edit(false, row);
}
function _edit(isPrimary, row) {
    var theModel = getModel();
    var gridIdKey = isPrimary ? "gridId" : "gridIdOfItem";
    var pkIdKey = isPrimary ? "pkId" : "pkIdOfItem";
    var baseModelUrlKey = isPrimary ? "baseModelUrl" : "baseModelUrlOfItem";
    var pkKey = isPrimary ? "pk" : "pkOfItem";
    var disabledKey = isPrimary ? "disabled" : "disabledOfItem";
    var formIdKey = isPrimary ? "formId" : "formIdOfItem";
    var dialogIdKey = isPrimary ? "dialogId" : "dialogIdOfItem";
    var dialogOptionKey = isPrimary ? "dialogOption" : "dialogOptionOfItem";
    var entityNameKey = isPrimary ? "entityName" : "entityNameOfItem";
    var unCheckSystemKey = isPrimary ? "unCheckSystem" : "unCheckSystemOfItem";

    var isSys = getJQO('#' + theModel[pkIdKey]).attr("type") == 'sys';
    if (!row) {
        row = getJQO('#' + theModel[gridIdKey]).datagrid('getSelected');
    }
    if (row) {
        /*
        if (isSys) {
        $('#' + theModel[pkIdKey]).attr("disabled", true);
        }
        */ 
        if (!theModel[unCheckSystemKey] && row.SystemFlag && row.SystemFlag == 1) {
            alertInfo('选中记录是系统记录，无法修改！');
            return;
        }
        if (theModel.onBeforeDialogOpen) {
            if (_.isFunction(theModel.onBeforeDialogOpen)) {
                theModel.onBeforeDialogOpen.apply(this, []);
            }
        }
        var url;
        if (theModel.baseEditModelUrl) {
            if (_.isFunction(theModel.baseEditModelUrl)) {
                url = theModel.baseEditModelUrl();
            }
            else {
                url = theModel.baseEditModelUrl;
            }
        }
        else {
            url = theModel[baseModelUrlKey];
        }

        url = url + row[theModel[pkKey]];
        if (isSys && !isPrimary && theModel.containerIdOfItem) {
            //子实体
            url = theModel[baseModelUrlKey] + getJQO('#' + theModel.pkId).val() + ',' + row[theModel[pkKey]];
        }
        getTo(url).done(function (ret) {
            getJQO('#' + theModel[formIdKey]).form('clear');
            getJQO('#' + theModel[formIdKey]).form('load', ret.instance);
            var dateboxs = getJQO('#' + theModel[formIdKey] + ' .easyui-datebox');
            if (dateboxs.size() > 1) {
                _.each(dateboxs, function (o) {
                    o.datebox('setValue', ndateFormatter(o.datebox('getValue'), 'yyyy-MM-dd'));
                });
            }
            else if (dateboxs.size() == 1) {
                dateboxs.datebox('setValue', ndateFormatter(dateboxs.datebox('getValue'), 'yyyy-MM-dd'));
            }
            if (ret.instance && ret.instance.Status != undefined && ret.instance.Status == 0) {
                getJQO('#btnRedoStatus').show();
            }
            else {
                getJQO('#btnRedoStatus').hide();
            }
            if (isPrimary && theModel.containerIdOfItem) {
                //父实体
                getJQO('#' + theModel.containerIdOfItem).show();
                getJQO('#' + theModel.gridIdOfItem).datagrid({
                    url: theModel.baseModelUrlOfItem + 'ListForEasyUIgrid',
                    queryParams: {
                        sort: 'OrderNo',
                        order: 'asc',
                        instance: {
                            TreeId: getJQO('#' + theModel.pkId).val(),
                            Status: theModel.defaultModelOfItem.Status
                        }
                    }
                });
            }

            getJQO('#' + theModel[dialogIdKey]).dialog(_.extend({
                onClose: function () {
                    if (isPrimary) {
                        theModel.uiMode = 'list';
                    }
                    else {
                        theModel.uiModeOfItem = 'list';
                    }
                },
                modal: true
            }, theModel[dialogOptionKey])).dialog('open').dialog('setTitle', '编辑' + theModel[entityNameKey]);
            if (theModel.disabled) {
                getJQO('#' + theModel[pkIdKey]).attr("disabled", _.any(theModel[disabledKey], function (o) { return o == 'edit' }));
            }

            if (theModel.onAfterDialogOpen) {
                if (_.isFunction(theModel.onAfterDialogOpen)) {
                    theModel.onAfterDialogOpen.apply(this, [row]);
                }
            }
        });
        if (isPrimary) {
            theModel.uiMode = 'edit'; 
        }
        else {
            theModel.uiModeOfItem = 'edit';
        }
        
    }
    else {
        alertInfo('请选中要编辑的记录！');
    }
}



function nullify() {
    __remove(true, true, false);
}
function nullifyItem() {
    __remove(false, true, false);
}
function redoNullify() {
    __remove(true, true, true);
}
function redoNullifyItem() {
    __remove(false, true, true);
}
function remove() {
    __remove(true, false);
}
function removeItem() {
    __remove(false, false);
}
function __remove(isPrimary, isNullify,isRedo) {
    var theModel = getModel();
    var gridIdKey = isPrimary ? "gridId" : "gridIdOfItem";
    var pkKey = isPrimary ? "pk" : "pkOfItem";
    var pkIdKey = isPrimary ? "pkId" : "pkIdOfItem";
    var baseModelUrlKey = isPrimary ? "baseModelUrl" : "baseModelUrlOfItem";
    var gridIdKey = isPrimary ? "gridId" : "gridIdOfItem";
    var actionName = isNullify ? "作废" : "删除";
    var unCheckSystemKey = isPrimary ? "unCheckSystem" : "unCheckSystemOfItem";
    var isSys = getJQO('#' + theModel[pkIdKey]).attr("type") == 'sys';
    var rows = getJQO('#' + theModel[gridIdKey]).datagrid('getSelections');
    var action = 'delete';
    if (isNullify) {
        action = "nullify";
        if (isRedo) {
            action = "redoNullify";
        }
    }
    
    if (rows.length > 0) {
        alertConfirm('确定要' + actionName + '选中记录吗？', function (r) {
            if (r) {
                if (!theModel[unCheckSystemKey] && _.some(rows, function (o) {
                    return o.SystemFlag && o.SystemFlag == 1;
                })) {
                    alertInfo('选中记录中包含系统记录，无法' + actionName + '！');
                    return;
                }
                var pks = _.map(rows, function (o) {
                    return o[theModel[pkKey]];
                });
                var selected = pks.join('|');

                var url
                if (theModel.baseEditModelUrl) {
                    if (_.isFunction(theModel.baseEditModelUrl)) {
                        selected = (theModel.baseEditModelUrl() + pks.join('|')).replace(theModel[baseModelUrlKey], '');
                    }
                    else {
                        selected = (theModel.baseEditModelUrl + pks.join('|')).replace(theModel[baseModelUrlKey], '');
                    }
                }
                else {
                    if (isSys && !isPrimary && theModel.containerIdOfItem) {
                        selected = $('#' + theModel.pkId).val() + ',' + pks.join('|');
                    }
                }
                var fn = function () {
                    getJQO('#' + theModel[gridIdKey]).datagrid('reload');
                    if (isPrimary) {
                        theModel.uiMode = 'list';
                    }
                    else {
                        theModel.uiModeOfItem = 'list';
                    }
                    if (theModel.actionDone) {
                        theModel.actionDone.apply(this, [action, pks]);
                    }
                    alertInfo(actionName + '成功！');
                };

                if (isNullify) {
                    nullifyTo2(theModel[baseModelUrlKey], selected, isRedo, fn);
                }
                else {
                    deleteTo2(theModel[baseModelUrlKey], selected, fn);
                }
            }
        });
    }
    else {
        alertInfo('请选中要' + actionName + '的记录！');
    }
}
function redoNullifyCurrent() {
    __redoNullifyCurrent(true);
}
function redoNullifyCurrentItem() {
    __redoNullifyCurrent(false);
}
function __redoNullifyCurrent(isPrimary) {
    var theModel = getModel();
    var pkIdKey = isPrimary ? "pkId" : "pkIdOfItem";
    var baseModelUrlKey = isPrimary ? "baseModelUrl" : "baseModelUrlOfItem";
    var gridIdKey = isPrimary ? "gridId" : "gridIdOfItem";
    var dialogIdKey = isPrimary ? "dialogId" : "dialogIdOfItem";
    alertConfirm('确定要反作废选中记录吗？', function (r) {
        if (r) {
            nullifyTo2(theModel[baseModelUrlKey], getJQO('#' + theModel[pkIdKey]).val(), true, function () {
                getJQO('#' + theModel[dialogIdKey]).dialog('close');
                getJQO('#' + theModel[gridIdKey]).datagrid('reload');
                if (isPrimary) {
                    theModel.uiMode = 'list';
                }
                else {
                    theModel.uiModeOfItem = 'list';
                }
                alertInfo('反作废成功！');
            });
        }
    });
}

function stop() {
    __stop(true, true);
}
function stopItem() {
    __stop(false, true);
}

function __stop(isPrimary, isStop) {
    var theModel = getModel();
    var gridIdKey = isPrimary ? "gridId" : "gridIdOfItem";
    var pkKey = isPrimary ? "pk" : "pkOfItem";
    var pkIdKey = isPrimary ? "pkId" : "pkIdOfItem";
    var baseModelUrlKey = isPrimary ? "baseModelUrl" : "baseModelUrlOfItem";
    var gridIdKey = isPrimary ? "gridId" : "gridIdOfItem";
    var actionName = isStop ? "停用" : "重新启用";
    var isSys = getJQO('#' + theModel[pkIdKey]).attr("type") == 'sys';
    var rows = getJQO('#' + theModel[gridIdKey]).datagrid('getSelections');
    if (rows.length > 0) {
        alertConfirm('确定要' + actionName + '选中记录吗？', function (r) {
            if (r) {

                var pks = _.map(rows, function (o) {
                    return o[theModel[pkKey]];
                });
                var selected = pks.join('|');

                var url
                if (theModel.baseEditModelUrl) {
                    if (_.isFunction(theModel.baseEditModelUrl)) {
                        selected = (theModel.baseEditModelUrl() + pks.join('|')).replace(theModel[baseModelUrlKey], '');
                    }
                    else {
                        selected = (theModel.baseEditModelUrl + pks.join('|')).replace(theModel[baseModelUrlKey], '');
                    }
                }
                else {
                    if (isSys && !isPrimary && theModel.containerIdOfItem) {
                        selected = $('#' + theModel.pkId).val() + ',' + pks.join('|');
                    }
                }
                var fn = function () {
                    getJQO('#' + theModel[gridIdKey]).datagrid('reload');
                    if (isPrimary) {
                        theModel.uiMode = 'list';
                    }
                    else {
                        theModel.uiModeOfItem = 'list';
                    }
                    if (theModel.actionDone) {
                        theModel.actionDone();
                    }
                    alertInfo(actionName + '成功！');
                };
                if (isStop) {
                    stopTo2(theModel[baseModelUrlKey], selected, fn);
                }
                else {
                    restartTo2(theModel[baseModelUrlKey], selected, fn);
                }

            }
        });
    }
    else {
        alertInfo('请选中要' + actionName + '的记录！');
    }
}

function restart() { 
    __stop(true, false);
}
function restartItem() {
    __stop(false, false);
}

function save() {
    _save(true);
}
function saveItem() {
    _save(false);
}
function _save(isPrimary) {
    var theModel = getModel();
    var formIdKey = isPrimary ? "formId" : "formIdOfItem";
    var pkIdKey = isPrimary ? "pkId" : "pkIdOfItem";
    var pkKey = isPrimary ? "pk" : "pkOfItem";
    var baseModelUrlKey = isPrimary ? "baseModelUrl" : "baseModelUrlOfItem";
    var defaultModelKey = isPrimary ? "defaultModel" : "defaultModelOfItem";
    var dialogIdKey = isPrimary ? "dialogId" : "dialogIdOfItem";
    var gridIdKey = isPrimary ? "gridId" : "gridIdOfItem";
    var action;
    var isSys = getJQO('#' + theModel[pkIdKey]).attr("type") == 'sys';
    if (getJQO('#' + theModel[formIdKey]).form('validate')) {
        if (theModel.customValidate) {
            if (!theModel.customValidate()) {
                return;
            }
        }
        var formData = getJQO('#' + theModel[formIdKey]).serializeObject();
        if (isSys) {
            formData[theModel[pkKey]] = getJQO('#' + theModel[pkIdKey]).val();
        }
        if (!isPrimary) {
            formData[theModel.pk] = getJQO('#' + theModel.pkId).val();
        }

        formData.PK = getJQO('#' + theModel[pkIdKey]).val() == "" ? theModel.getPKValueWhenAdd(isPrimary) : getJQO('#' + theModel[pkIdKey]).val();
        var isCreate;
        if (isPrimary) {
            isCreate = theModel.uiMode == 'add';
        }
        else { 
            isCreate = theModel.uiModeOfItem == 'add';
        }
        formData.isCreate = isCreate;
        action = isCreate ? "insert" : "update";

        var url;
        if (!isCreate && theModel.baseEditModelUrl) {
            if (_.isFunction(theModel.baseEditModelUrl)) {
                url = theModel.baseEditModelUrl();
            }
            else {
                url = theModel.baseEditModelUrl;
            }
        }
        else {
            var url = theModel[baseModelUrlKey];
            if (!isCreate && isSys && !isPrimary && theModel.containerIdOfItem) {
                //子实体
                url = theModel[baseModelUrlKey] + getJQO('#' + theModel.pkId).val() + ',';
            }
        }
        var dateboxs = getJQO('#' + theModel[formIdKey] + ' .easyui-datebox');

        if (dateboxs.size() > 1) {
            _.each(dateboxs, function (o) {
                formData[o.attr('comboname')] = toJsonDate(o.datebox('getValue'));
            });
        }
        else if (dateboxs.size() == 1) {
            formData[dateboxs.attr('comboname')] = toJsonDate(dateboxs.datebox('getValue'));
        }

        if (theModel.changeFormDataBeforeSubmit) {
            theModel.changeFormDataBeforeSubmit(formData);
        }
        saveTo(url, $.extend(theModel[defaultModelKey] || {}, formData), function (ret) {
            getJQO('#' + theModel[dialogIdKey]).dialog('close');
            getJQO('#' + theModel[gridIdKey]).datagrid(isCreate ? 'load' : 'reload');

            if (isPrimary) {
                theModel.uiMode = 'list';
            }
            else {
                theModel.uiModeOfItem = 'list';
            }
            if (theModel.actionDone) { 
                theModel.actionDone.apply(this, [action, ret]);
            }
            alertInfo((isCreate ? '新增' : '编辑') + '成功！');
        });
    }
}


function cancel() {
    _cancel(true);
}
function cancelItem() {
    _cancel(false);
}
function _cancel(isPrimary) {
    var theModel = getModel();
    var dialogIdKey = isPrimary ? "dialogId" : "dialogIdOfItem";
    getJQO('#' + theModel[dialogIdKey]).dialog('close');
}

function _query() {
    var theModel = getModel();
    var searchIdKey = "dlgSearchId";
    var dialogOptionKey = "dialogOption";
    var entityNameKey = "entityName";
    var formIdKey = "fmSearchId";
    var pkIdKey = "pkId";
    var disabledKey = "disabled";
    var isSys = getJQO('#' + theModel[pkIdKey]).attr("type") == 'sys';

    if (theModel.onBeforeDialogOpen) {
        if (_.isFunction(theModel.onBeforeDialogOpen)) {
            theModel.onBeforeDialogOpen.apply(this, []);
        }
    }
//    getJQO('#' + theModel[searchIdKey]).dialog(_.extend({
//        inline: false,
//        onClose: function () {
//            theModel.uiMode = 'list';
//        },
//        modal: true
//    }, theModel[dialogOptionKey])).dialog('open').dialog('setTitle', '查询-' + theModel[entityNameKey]);
    getJQO('#' + theModel[searchIdKey]).dialog({
        inline: false,
        onClose: function () {
            theModel.uiMode = 'list';
        },
        modal: true
    }).dialog('open').dialog('setTitle', '查询-' + theModel[entityNameKey]);
    getJQO('#' + theModel[formIdKey]).form('clear');

    if (theModel.disabled) {
        getJQO('#' + theModel[pkIdKey]).attr("disabled", _.any(theModel[disabledKey], function (o) { return o == 'query' }));
    }
}
function _exec() {
    var theModel = getModel();
    var gridIdKey = "gridId";
    var searchIdKey = "dlgSearchId";
    var formIdKey = "fmSearchId";
    var arrSearchData = getJQO('#' + theModel[formIdKey]).serializeArray();
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
    
    var queryParamsArray = getJQO('#' + theModel[gridIdKey]).datagrid('options').queryParams;
    _.each(instanceParm, function (o) {
        if (o.value) {
            queryParamsArray.filterFields.push(o);
        }
    });

    _.extend(queryParamsArray, { fuzzyFields: formDataArray });
    getJQO('#' + theModel[gridIdKey]).datagrid('load',queryParamsArray );

    getJQO('#' + theModel[searchIdKey]).dialog('close');
}
function _clear() {
    var theModel = getModel();
    var formIdKey = "fmSearchId";
    getJQO('#' + theModel[formIdKey]).get(0).reset();
}
function _quit() {
    var theModel = getModel();
    var dialogIdKey = "dlgSearchId";
    getJQO('#' + theModel[dialogIdKey]).dialog('close');
}

///frame对应的js也有
function openDialog(dialogId, href, done, options) {
    var settings = $.extend({ title: '', width: 480, height: 360, padding: 2 }, options);
    $('<div id="' + dialogId + '" style="width: ' + settings.width + 'px; height: ' + settings.height + 'px;padding:' + settings.padding + 'px;"></div>').appendTo('body')
    .dialog({
        onClose: function () {
            $(this).dialog('destroy');
        },
        onLoad: function () {
            dialogOpen(dialogId, options.dialogData);
        },
        buttons: [{
            text: '确定',
            iconCls: 'icon-ok',
            handler: function () {
                if (dialogBeforeSubmit(dialogId)) {
                    var $mask = $('.datagrid-mask');
                    var $mask_msg = $('.datagrid-mask-msg');
                    $mask.css({
                        display: 'block',
                        'z-index': 9998, //最顶层，用户才能点到链接  
                        width: $(window).width(),
                        height: $(window).height(),
                        background: '#fff' //覆盖原来的样式  
                    }).appendTo(document.body);

                    $mask_msg.css({
                        display: 'block', //显示出来  
                        'z-index': 9999, //最顶层，用户才能点到链接   
                        width: 32,
                        height: 32,
                        background: '#fff', //覆盖原来的样式  
                        padding: '0px 0px 0px 0px', //覆盖原来的样式
                        border: 'none',
                        left: ($(window).width() - $mask_msg.outerWidth()) / 2,
                        top: ($(window).height() - $mask_msg.outerHeight()) / 2
                    });

                    $(window).resize(function () {
                        $mask.css({
                            width: $(window).width(),
                            height: $(window).height()
                        });
                        $mask_msg.css({
                            left: ($(window).width() - $mask_msg.outerWidth()) / 2,
                            top: ($(window).height() - $mask_msg.outerHeight()) / 2
                        });
                    }).resize();

                    if (done) {
                        done.apply(this, [function () {
                            $('#' + dialogId).dialog('close'); $mask.hide(); $mask_msg.hide();
                        }, dialogClose(dialogId)]);
                    }
                }
            }
        }, {
            text: '取消',
            iconCls: 'icon-cancel',
            handler: function () { $('#' + dialogId).dialog('close'); }
        }],
        title: settings.title,
        modal: true,
        href: href
        //content: '<div class="easyui-layout" data-options="fit:true"><div data-options="region:\'north\',border:false" style="height: 36px;"><span id="selectedArea" style="font-weight:bold;color:blue;"></span></div><div data-options="region:\'center\',border:false"><ul id="tree-SetArea"></ul></div></div>'
    }).dialog('open');
}

function addRow(gridId) {
    getJQO('#' + gridId).edatagrid('addRow');
}
function editRow(gridId,index) {
    getJQO('#' + gridId).edatagrid('editRow', index);
}
function nullifyRow(gridId) {
    getJQO('#' + gridId).edatagrid('destroyRow');
}
function saveRow(gridId) {
    getJQO('#' + gridId).edatagrid('saveRow');
}
/*************** ///easyUI 基本操作 ******************/