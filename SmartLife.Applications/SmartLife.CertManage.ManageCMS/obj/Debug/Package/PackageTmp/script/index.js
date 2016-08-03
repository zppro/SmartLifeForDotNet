$(function () {
    $.ajaxf.install('script/flash4ajax/AJAX.swf');

    /*
    $('.session-out-mask').click(function () {
    $('.session-out-mask').fadeOut(800);
    $('.session-out-mask-content').fadeOut(800);
    });
    */

    window.models = {};
    window.isOutterSystemOfMainCenter = false;
    window.isFullScreen = false;
    window.tabCC = null;
    window.isSwitchUser = false;
    //区域设置   
    $('body').layout('panel', 'west').panel({ onResize: function (width, height) {
        $.cookie("west-width", parseInt(width));
        $('#applications-in-platform-c').layout('panel', 'east').panel({ width: $('body').width() - width + splitX });
        $('#logined-panel').width(width - splitX);
    }
    });

    //检测是否登录 
    gToken = $.cookie("token");
    gExtId = $.cookie("ExtId");
    gExtCode = $.cookie("ExtCode");
    gIPCCDial = $.cookie("IPCCDial");
    gIPCCRelayType = $.cookie("IPCCRelayType");
    gIPCCRelayPrefix = $.cookie("IPCCRelayPrefix");

    if (gExtId) {
        gIsCCSeat = true;
    }
    else {
        gIsCCSeat = false;
        InitCSLoadTime = 0;
    }
    if (!gToken) {
        //不存在token
        /*
        alertInfo("无法找到token。请重新登录后继续访问！", function () {

        });
        */

        if (getQueryString('mode') == 'loginbox') {
            onSessionOut();
        }
        else {
            Redirect(login_Url);
        }
    }
    else {
        //存在token，继续检测服务端session
        getTo(checkIsLoggedIn_InvokeUrl, null, function (result) {
            if (!result.ret.LoggedIn) {
                if (gToken) {
                    onSessionOut();
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
                gUserId = result.ret.UserId;
                gUserCode = result.ret.UserCode;
                gUserName = result.ret.UserName;
                gUserInfo = { isSuperAdmin: false, isMerchantUser: false, isAgencyUser: false, isCCSeat: gIsCCSeat };
                if (!gIsCCSeat) {
                    //检查是否是商家机构用户
                    getTo(baseCMSInvokeUrl + '/Pub/UserService/CheckCurrentUserCatalog', null, function (r1) {
                        gUserInfo.isSuperAdmin = r1.ret.isSuperAdmin;
                        gUserInfo.isMerchantUser = r1.ret.isMerchantUser;
                        gUserInfo.isAgencyUser = r1.ret.isAgencyUser;
                    }, { async: false });
                }

                if (gUserInfo.isMerchantUser || gUserInfo.isSuperAdmin) {
                    //查找映射的StationId
                    getTo(baseCMSInvokeUrl + '/Pub/UserService/GetMappingMerhants', null, function (r2) {
                        gUserInfo.MappingMerchants = r2.rows;
                    }, null, { isSuperAdmin: gUserInfo.isSuperAdmin });
                }

                if (gUserInfo.isAgencyUser || gUserInfo.isSuperAdmin) {
                    //查找映射的StationId
                    getTo(baseCMSInvokeUrl + '/Pub/UserService/GetMappingAgencys', null, function (r2) {
                        gUserInfo.MappingAgencys = r2.rows;
                    }, null, { isSuperAdmin: gUserInfo.isSuperAdmin });
                }

                var loginedPanels = '<div id="logined-user">' + gUserName + '</div><div id="logined-action"><a id="btnSettings" ><span class="login-icon-settings">&nbsp;</span>设置</a>|<a id=\"btnLogout\"><span class="login-icon-exit">&nbsp;</span>退出</a></div>';
                $("#logined-panel").html(loginedPanels);

                //获取当前节点区域全称
                var switchhtml = '<a onclick="switchArea()"><span class="nicon-R1C9 nicon-black"></span>切换</a>';
                if (window.showFullAreaPathFlag == 1) {
                    getTo(ajaxData_InvokeUrl + "/GetAreaFullName/" + window.appDeployArea.id, null, function (result) {
                        $('#CurrentArea').html(result + switchhtml);
                    });
                }
                else {
                    getTo(ajaxData_InvokeUrl + "/GetAreaName/" + window.appDeployArea.id, null, function (result) {
                        $('#CurrentArea').html(result + switchhtml);
                    });
                }


                $("#btnLogout").bind('click', function () {
                    alertConfirm('您确定要退出系统吗？', function (r) {
                        if (r) {
                            BS_Logout(top.execMode);
                        }
                    });
                });
                $("#btnSettings").bind('click', function (e) {
                    $('#mm-settings').menu('show', {
                        left: e.pageX,
                        top: e.pageY + 10
                    });
                });

                //客户端延时加载
                setTimeout(function () {
                    if (top.execMode == "CS") {
                        top.external.SetAreaId((top.objectId == '*' ? '00000' : top.appDeployArea.id));
                        top.external.SetCallCenterId(gCallCenterId);
                        if (gDialBackFlag) {
                            top.external.SetDialBackFlag(gDialBackFlag);
                        }
                        top.external.SetSeat(gUserId, gUserCode, gUserName);
                        if (gIsCCSeat) {
                            top.external.SetExt(gExtId, gExtCode, gIPCCDial, gIPCCRelayType, gIPCCRelayPrefix);
                        }
                        top.external.ArrageStatusBar();
                    }
                    else if (top.execMode == "ForSeat") {
                        //ForSeat 
                        top.external.SetAddresses(baseIPCCOfCityInvokeUrl, baseWeiXinOfServiceOnlineInovkeAddress, dialogOfProcessEmergency_RelativeUrl, dialogOfProcessFamilyCalls_RelativeUrl, dialogOfProcessCommonService_RelativeUrl, dialogOfProcessLife_RelativeUrl);
                        getTo(ajaxData_InvokeUrl + '/GetCCGroup/10009', null, function (result) {
                            var groups = _.map(result, function (item) {
                                var o = xml2json.parser(item, 'StringObjectDictionary');
                                return { Text: o.GroupName, Value: o.Callee };
                            });
                            top.external.SetTransferQueues($.toJSON(groups));
                        });
                        top.external.SetEnvironment(gUserId, gUserCode, gUserName, gExtId, gExtCode, gDialBackFlag, gCallCenter.StationId, gCallCenter.IP, gCallCenter.Port, gCallCenter.IPInner, gCallCenter.PortInner, gCallCenter.IPProxy, gCallCenter.PortProxy, (top.objectId == '*' ? '00000' : top.appDeployArea.id));
                        top.external.SetSeatFuncs($.toJSON([{ Text: window.SpecialMenus.CallServiceList.MenuTitle, Value: window.SpecialMenus.CallServiceList.MenuId }, { Text: window.SpecialMenus.CallbackServiceList.MenuTitle, Value: window.SpecialMenus.CallbackServiceList.MenuId }, { Text: window.SpecialMenus.RolledOutServiceList.MenuTitle, Value: window.SpecialMenus.RolledOutServiceList.MenuId}]));
                    }
                }, InitCSLoadTime);

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
                    var menuMore;
                    var currentAreaWidth = $('#CurrentArea').width();
                    var remainWidth = window.screen.width - currentAreaWidth - 5;

                    if (remainWidth < menus.length * 78) {
                        var canShowNum = Math.floor(remainWidth / 78) - 1;
                        menuMore = menus.splice(canShowNum, menus.length - canShowNum);
                        menus.push({ MenuId: 'Mores', MenuCode: 'Mores', PageUrl: null, Picture: 'menu78-认证管理-blue', Description: null, MenuName: '更多+' });
                    }

                    $("#platforms").append(_.map(menus, function (o) {
                        return $("<li MenuId=\"" + o.MenuId + "\" MenuCode=\"" + o.MenuCode + "\" PageUrl=\"" + isnull(o.PageUrl, "") + "\"><a offClass=\"" + o.Picture + "\" class=\"" + o.Picture + "\" title=\"" + isnull(o.Description, "") + "\"></a><span>" + o.MenuName + "</span></li>");
                    })).width(78 * menus.length); //.kwicks({ max: 52 * menus.length, spacing: 0, sticky: true, event: 'click' })

                    if (menuMore) {
                        $("#platforms-more").append(_.map(menuMore, function (o) {
                            return $("<li MenuId=\"" + o.MenuId + "\" MenuCode=\"" + o.MenuCode + "\" PageUrl=\"" + isnull(o.PageUrl, "") + "\"><a offClass=\"" + o.Picture + "\" class=\"" + o.Picture + "\" title=\"" + isnull(o.Description, "") + "\"></a><span>" + o.MenuName + "</span></li>");
                        })).bind('mouseleave', function () {
                            $(this).fadeOut(500);
                        });
                        $(".platforms li[MenuId='Mores']").bind('mouseenter', function () {
                            $('#platforms-more').fadeIn(800);
                        });
                    }

                    if (menus.length > 0) {
                        $(".platforms li[MenuId!='Mores']").bind('mouseenter', function () {
                            var $a = $(this).find('a');
                            if ($a.attr('class').indexOf('-on') == -1) {
                                $(this).find('span').addClass('on');
                            }
                        }).bind('mouseleave', function () {
                            var $a = $(this).find('a');
                            if ($a.attr('class').indexOf('-on') == -1) {
                                $(this).find('span').removeClass('on');
                            }
                        }).bind('click', function () {
                            //on-off 
                            var $a = $(this).find('a');
                            var $span = $(this).find('span');
                            var pageUrlOfPlatform = $(this).attr('PageUrl');
                            var menuId = $(this).attr('MenuId');
                            var menuCode = $(this).attr('MenuCode');

                            if (top.execMode == "CS" && menuCode == "B1800") {

                                //居家养老首页
                                openTab(menuId, menuCode, $span.text() + '首页', 'views/old-care/index.htm', "icon-max1", function () {
                                    maxTab(menuId);
                                }, true, false, false, true);
                                /*
                                //实时滚动页
                                var now = new Date();
                                openTab(maxTab + '_Scroll', '实时滚动播报', 'views/old-care/scroll.htm&stat-date=' + now.pattern('yyyy-MM-dd'), "icon-max1", function () {
                                maxTab(maxTab + '_Scroll');
                                }, true, false, true, true);
                                */

                                $('#tt').tabs('select', $span.text() + '首页');

                            }
                            else if (menuCode == "B1800") {

                            }
                            else if (menuCode == "B1200") {
                                /*
                                openTab(menuId, menuCode, $span.text() + '首页', 'views/cmu-manage/area-show.htm', "icon-max1", function () {
                                maxTab(menuId);
                                }, true, true, false, true);
                                */
                            }

                            _.each($(".platforms li a"), function (o) {
                                var onClass = $(o).attr('offClass') + '-on';
                                $(o).removeClass(onClass);
                            });
                            _.each($(".platforms li span"), function (o) {
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

                        if (toMenu) {
                            getTo(ajaxData_InvokeUrl + "/GetMenuByCode/" + toMenu, null, function (ret) {
                                openTab(ret.MenuId, ret.MenuCode, ret.MenuName, ret.PageUrl, null, null, ret.OpenInFrameFlag == 1, true, ret.SelectOnRefreshFlag == 1, ret.CanFullScreenFlag == 1);
                            });

                            var menuOfSystem = toMenu.substr(0, 4) + "0"; //菜单编码规则
                            var theSystem = _.find($("#platforms li"), function (o) { return $(o).attr('MenuCode') == menuOfSystem });
                            if (theSystem) {
                                //延时加载
                                setTimeout(function () {
                                    $(theSystem).trigger('click');
                                }, InitCSLoadTime);
                            }
                        }
                        else {
                            //座席客户端
                            if (top.execMode == "ForSeat") {
                                _.each(_.where(window.SpecialMenus, { SystemMenuCode: 'B1800' }), function (theSpecialMenu) {
                                    openTab(theSpecialMenu.MenuId, theSpecialMenu.MenuCode, theSpecialMenu.MenuTitle, theSpecialMenu.PageUrl, "icon-max1", function () {
                                        maxTab(window.SpecialMenus.CallbackServiceList.MenuId);
                                    }, true, false, false, false);
                                });
                                setTimeout(function () {
                                    maxTab(window.SpecialMenus.CallServiceList.MenuId);
                                }, 2000);
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
    if (top.execMode != "BS") {
        //解决客户端内嵌问题
        window.frames['tab_' + menuId].location.reload();
    }
}
function openTab(menuId, menuCode, menuTitle, pageUrl, iconCls, iconHandler, isInFrame, closable, selectOnRefresh, canFullScreen){

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

function switchArea() {
    openDialog('dlg-switch-area', 'views/dialogs/switch-area.htm', function (callback, dialogData) {
        callback();
    }, { title: '切换区域', width: 800, height: 600, padding: 5 });
}

function changeUserPassword() {
    openDialog('dlg-login-setting', 'views/dialogs/set-user-info.htm', function (callback, dialogData) {
        putTo(baseCMSInvokeUrl + '/Pub/UserService/UpdateValidatedUser/' + gUserId, $.toJSON(dialogData)).done(function (result) {
            if (result.Success) {
                alertInfo('修改成功！');
            }
            callback();
        });
    }, { dialogData: { UserId: gUserId,
        UserCode: gUserCode,
        UserName: gUserName
    }, title: '用户管理-修改密码', width: 320, height: 260, padding: 5, mergeDefault: true
    });
}
function switchUser() {
    window.isSwitchUser = true;
    onSessionOut();
}
function onSessionOut() {
    $('.session-out-mask').fadeIn(200);
    $('.session-out-mask-content').fadeIn(400);
    getHtml('views/dialogs/login-box.htm', function (str) {
        $('.session-out-mask-content').html(str);
        $.parser.parse('.session-out-mask-content');
    });
    
}

/*************** 与CS交互 基本操作 ******************/

function CS_GetQueueNoAndCallee() {
    var queueNoAndCallees = {};
    getTo(top.baseCMSInvokeUrl + '/Pub/UserService/GetQueueNoAndCallee/' + gUserId, null, function (result) {
        queueNoAndCallees = result;
    }, { async: false });
    return $.toJSON(queueNoAndCallees);
}

function BS_Logout(_execMode) {
    $.ajaxf.postJSON(LogOffUser_InvokeUrl, $.toJSON({ ApplicationIdFrom: gApplicationId, ApplicationIdTo: gInvokeApplicationId, Token: gToken, ObjectId: window.objectId, RunMode: window.runMode, Tag: redirectTagOfSignOut }), function (result1) {
        if (result1.Success) {
            if (result1.ret.RedirectUrl) {
                //清理动作1 - 如果是坐席解除绑定
                if (gIsCCSeat) {
                    postTo(window.CommonRemoveSeatExtBinding_InvokeUrl, null, null, { async: false });
                }
                if (_execMode == "CS") {
                    //CS登录清理
                    window.external.SetLogout();
                }
                else if (_execMode == "ForSeat") {
                    //ForClient登录清理  
                    window.external.SetLogout();
                }
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
/*************** End 与CS交互 基本操作 ******************/



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