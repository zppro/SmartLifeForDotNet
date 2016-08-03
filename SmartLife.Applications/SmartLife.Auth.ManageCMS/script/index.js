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
                        if (tab.data('menuTitle')) {
                            window.currentMenuTitle = tab.data('menuTitle');
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
                                                        //var iconCls = treeNode.canFullScreenFlag == 1 ? 'icon-max1' : '';
                                                        //var iconHandler = treeNode.canFullScreenFlag == 1 ? function () { maxTab(treeNode.id) } : null;
                                                        openTab(treeNode.id, treeNode.code, treeNode.name, treeNode.pageUrl, null, null, treeNode.openInFrameFlag == 1, true, treeNode.selectOnRefreshFlag == 1, treeNode.canFullScreenFlag == 1);
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

                        var toMenu = getQueryString('menu');

                        if (!toMenu) {
                            toMenu = 'X1102';
                        }
                        else {
                            getTo(ajaxData_InvokeUrl + "/GetMenuByCode/" + toMenu, null, function (ret) {
                                openTab(ret.MenuId, ret.MenuCode, ret.MenuName, ret.PageUrl, null, null, ret.OpenInFrameFlag == 1, true, ret.SelectOnRefreshFlag == 1, ret.CanFullScreenFlag == 1);
                            });
                        }

                        var menuOfSystem = toMenu.substr(0, 4) + "0"; //菜单编码规则
                        var theSystem = _.find($("#platforms li"), function (o) { return $(o).attr('MenuCode') == menuOfSystem });
                        if (theSystem) {
                            $(theSystem).trigger('click');
                        }
                    }

                }); //loadmenu
            }
        });
    }

}); 
  
function loadMenu(fn, parentId) {
    var url = ajaxData_InvokeUrl + "/Menu/" + (parentId || "null");
    getTo(url, null, function (result) {
        if (fn) {
            result.rows[{ "ApplicationId": "BG011", "CanFullScreenFlag": 0, "CodeClass": null, "Description": null, "EndFlag": 0, "Id": 2, "Levels": 1, "MenuCode": "C12", "MenuId": "30002", "MenuName": "人力资源管理平台", "OpenInFrameFlag": 0, "OrderNo": 200, "PageUrl": "", "ParentId": "*    ", "Picture": "menu78-hr-blue", "SelectOnRefreshFlag": 0, "Status": 1 }, { "ApplicationId": "BG011", "CanFullScreenFlag": 0, "CodeClass": null, "Description": null, "EndFlag": 0, "Id": 1, "Levels": 1, "MenuCode": "C11", "MenuId": "30001", "MenuName": "认证管理平台", "OpenInFrameFlag": 0, "OrderNo": 900, "PageUrl": "", "ParentId": "*    ", "Picture": "menu78-sys-blue", "SelectOnRefreshFlag": 0, "Status": 1}];
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
function openTab(menuId, menuCode, menuTitle, pageUrl, iconCls, iconHandler, isInFrame, closable, selectOnRefresh, canFullScreen) {

    if (!$('#tt').tabs('exists', menuTitle)) {
        window.currentMenuId = menuId;
        window.currentMenuCode = menuCode;
        window.currentMenuTitle = menuTitle;
        var tabUrl = (pageUrl || 'views/shared/empty.htm');
        var tabOption = {
            title: menuTitle,
            height: "auto",
            closable: closable,
            style: { padding: "8px" }
        };

        if (!iconCls && canFullScreen) {
            iconCls = 'icon-max1';
        }
        if (!iconHandler && canFullScreen) {
            iconHandler = function () { maxTab(menuId) };
        }

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
            formatTabUrlInFrame(tabUrl, menuId, menuCode, menuTitle, canFullScreen);
        }
        var tab = $('#tt').tabs('getSelected');
        tab.data('menuId', currentMenuId);
        tab.data('menuCode', currentMenuCode);
        tab.data('menuTitle', currentMenuTitle);
        tab.data('selectOnRefresh', selectOnRefresh);
    }
    else {
        $('#tt').tabs('select', menuTitle);
    }
}

function _closeTab(menuTitle) {
    $('#tt').tabs('close', menuTitle);
}

function closeTab(menuTitle) {
    _closeTab(menuTitle);
}

function formatTabUrlInFrame(tabUrl, menuId, menuCode, menuTitle, canFullScreen) {
    var newUrl = 'views/shared/tabContainer.htm?pageUrl=' + tabUrl + '&menuId=' + menuId + '&menuCode=' + menuCode + '&menuTitle=' + escape(menuTitle);
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
function openMenu(x, params) {
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
    $.ajaxf.postJSON(LogOffUserFlash_InvokeUrl, $.toJSON({ ApplicationIdFrom: gApplicationId, ApplicationIdTo: gInvokeApplicationId, Token: gToken, ObjectId: window.objectId, Tag: redirectTagOfSignOut }), function (result1) {
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

