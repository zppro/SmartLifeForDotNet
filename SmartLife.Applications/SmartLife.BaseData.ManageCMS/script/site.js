$(function () {
    //ajax设置
    $.ajaxSetup({
        cache: false,
        contentType: "application/json; charset=utf-8"
    });
    //ajax settings 
    $("#loading").bind("ajaxSend", function () {
        $(this).appendTo('body').show();
    }).bind("ajaxComplete ajaxStop", function () {
        $(this).hide();
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
            if (jhr.status == 403) {
                postTo(top.Initilize_InvokeUrl, $.toJSON({ Host: top.theHost, ApplicationIdFrom: top.gApplicationId, ApplicationIdTo: top.gInvokeApplicationId, RunMode: top.runMode }), function (result) {
                    if (result.Success) {
                        $.ajax(ajaxOptions);
                    }
                });
            }
            else if (jhr.status == 409) {
                alertError(msg, function () {
                    if (top.execMode == "CS") {
                        top.external.Redirect(login_Url);
                    }
                    else {
                        top.document.location.href = login_Url;
                    }
                });
            }
            else {
                alertError(msg);
            }
        }
    });


});

function Redirect(url, fromTab) {
    
    if (fromTab) {
        if (parent.execMode == "CS") {
            parent.external.Redirect(url);
        }
        else {
            parent.document.location.href = url;
        }
    }
    else {
        if (window.execMode == "CS") {
            window.external.Redirect(url);
        }
        else {
            document.location.href = url;
        }
    }
}

function validateUI() {
    $("textarea[maxlength]").bind('input propertychange', function () {
        var maxLength = $(this).attr('maxlength');
        if ($(this).val().length > maxLength) {
            $(this).val($(this).val().substring(0, maxLength));
        }
    });
}

function easyuiLoader(param, success, error) {
    var that = $(this);
    var opts = that.datagrid("options");
    if (!opts.url) {
        return false;
    }
    
    var cache = that.data().datagrid.cache;
    if (!cache) {
        $.ajax({
            type: opts.method,
            url: opts.url,
            data: $.toJSON(param),
            dataType: "json",
            success: function (data) {
                //that.data().datagrid['cache'] = data; 
                success(data);

            },
            error: function () {
                error.apply(this, arguments);
            }
        });
    } else {
        success(cache);
    }
}

function easyuiLoaderForStringObjectDictionary(param, success, error) {
    var that = $(this);
    var opts = that.datagrid("options");
    if (!opts.url) {
        return false;
    }

    var cache = that.data().datagrid.cache;
    if (!cache) {
        $.ajax({
            type: opts.method,
            url: opts.url,
            data: $.toJSON(param),
            dataType: "json",
            success: function (data) {
                //that.data().datagrid['cache'] = data;
               // xml2json.parser(data.rows[0], 'StringObjectDictionary');
                data.rows = _.map(data.rows, function (o) {
                    //var jo = xml2json.parser(o, 'StringObjectDictionary');
                    return xml2json.parser(o, 'StringObjectDictionary');
                });
                success(data);
            },
            error: function () {
                error.apply(this, arguments);
            }
        });
    } else {
        success(cache);
    }
}

function easyuigrid_settings(settings) {
    return _.extend({}, window.easyUIDataGridDefaultSetting, settings);
}

function easyuigrid_diFormatter(val, row, idx) {
    var self = this;
    if (val == null || val == "") return "";
    var newVal = _.find(this.vals, function (o) { return o[self.valueField || "ItemId"] == val });
    if (newVal) { 
        return newVal[this.textField || "ItemName"];
    }
}

//把秒转化为基本的日期格式
function easyuigrid_TimeSpanFormatter(val, row, idx) {
    if (val == null || val == "") return "";
    var dd, hh, mm, ss; //天、时、分、秒
    
    dd = Math.round(val / 86400 + 0.5) - 1;
    hh = Math.round((val - dd * 86400) / 3600 + 0.5) - 1;
    mm = Math.round((val - dd * 86400 - hh * 3600) / 60 + 0.5) - 1;
    ss = Math.round(val - dd * 86400 - hh * 3600 - mm * 60);
    
    var strtip = "";
    if (dd > 0)
        strtip = strtip + dd + '天';
    if (hh > 0)
        strtip = strtip + hh + '小时';
    if (mm > 0) {
        strtip = strtip + mm + '分钟';
        if (dd > 0)
            return strtip;
    }
    
    if (ss > 0)
        strtip = strtip + ss + '秒';
    return strtip;
}



function easyuigrid_dateFormatter(val, row, idx) {
    if (val == null || val == "") return "";
    if (val.indexOf("Date") < 0) return val;
    //var time = cellval.replace(/\/Date\(([0-9]*)\)\//, '$1');
    var time = eval('new ' + val.replace(/\//g, ''));

    var date = new Date();
    date.setTime(time);
    return date.pattern(this.datefmt || "yyyy-MM-dd");
} 

function easyuigrid_ageFormatter(val, row, idx) {
    if (val == null || val == "") return "";
    if (val.indexOf("Date") < 0) return val;
    //var time = cellval.replace(/\/Date\(([0-9]*)\)\//, '$1');
    var time = eval('new ' + val.replace(/\//g, ''));
    var date = new Date();
    date.setTime(time); 
    return DateDiff('y', date, new Date());
}
function easyuigrid_age2Formatter(val, row, idx) {
    if (val == null || val == "") return "";
    var sIndex = val.indexOf(' ');
    var date = new Date(val.substring(0, sIndex)); 
    return DateDiff('y', date, new Date());
}
function easyuigrid_boolFormatter(val, row, idx) {
    return val == "1" ? "是" : "否";
}

function easyuigrid_bool2Formatter(val, row, idx) {
    var item = _.find(this.vals.split('~t'), function (o) {
        return o.split(':')[1] == val;
    });
    if (item) {
        return item.split(':')[0];
    }
    return "";
}

function easyuigrid_genderFormatter(val, row, idx) {
    if (val == null || val == "") return "";
    return val == "F" ? "女" : (val == "M" ? "男" : "--");
}

function easyuigrid_ajaxFormatter(val, row, idx) {
   
    var _self = this;
    if (!this.vals) { 
        if (_.isFunction(this.url)) {
            //this.url.apply(this, [row]);
            this.vals = this.url.apply(this, [row]);
            //this.vals = this.url();
        }
        else {
            getTo(this.url, this.data, function (result) {
                _self.vals = result;
            }, { async: false });
        }
        
        //alert(_.find(this.vals, function (o) { return o[_self.valueField] == val }));
    }
    
    /*
    var newVal = _.find(this.vals, function (o) { return o[_self.valueField || "ItemId"] == val });
    if (newVal) {
        return newVal[this.textField || "ItemName"];
    }
    */ 
    var arrNames = [];
    if (val) {

        var arrVals = ('' + val).split(',');
        var caseSensitive = true;
        if (this.caseSensitive == undefined) {
            caseSensitive = true;
        }
        else {
            caseSensitive = this.caseSensitive;
        }

        for (var i = 0; i < arrVals.length; i++) {

            var newVal = _.find(this.vals, function (o) {
                if (caseSensitive) {
                    
                    return o[_self.valueField || "ItemId"] == arrVals[i];
                }
                else {

                    return o[_self.valueField || "ItemId"].toLowerCase() == arrVals[i].toLowerCase();
                }
            });
            if (newVal) { 
                arrNames.push(newVal[this.textField || "ItemName"]);
            }
        }
    }
    
    return arrNames.join();
}

function easyuigrid_ajaxFormatter2(val, row, idx) {
    var ret;
    var _self = this;
    if (!this[this.field + '_vals']) {
        this[this.field + '_vals'] = [];
    }
    var vals = this[this.field + '_vals'];
    var newVal = _.find(vals, function (o) {
        return o[_self.valueField || "ItemId"] == val;
    });

    if (newVal) {
        ret = newVal[this.textField || "ItemName"];
    }

    if (!ret) {
        if (_.isFunction(this.url)) {
            //this.url.apply(this, [row]);
            var retVal = this.url.apply(this, [row]);
            if (_.isArray(retVal)) {
                vals.union(retVal);
            }
            else {
                vals.push(retVal);
            } 
        }
        else {
            getTo(this.url, this.data, function (result) {
                if (_.isArray(result)) {
                    vals.union(result);
                }
                else {
                    vals.push(result);
                } 
            }, { async: false });
        }
    }

    newVal = _.find(vals, function (o) {
        return o[_self.valueField || "ItemId"] == val;
    });

    if (newVal) {
        ret = newVal[this.textField || "ItemName"];
    } 
    return ret;
}

function easyuigrid_linkFormatter(val, row, idx) {
    if (_.isFunction(this.fn)) {
        return this.fn.apply(this, [row]);
    }
    else {
        return this.fn;
    }
}
 
function easyuiDataGrid() {

}

function getDIs(dictIds, d, f) {
    /*
        $.when($.ajax("/page1.php"), $.ajax("/page2.php")).done(function (a1, a2) {
            //a1 and a2 are arguments resolved for the 
            //page1 and page2 ajax requests, respectively
            var jqXHR = a1[2]; // arguments are [ "success", statusText, jqXHR ]
            if (/Whip It/.test(jqXHR.responseText)) {
                alert("First page has 'Whip It' somewhere.");
            }
        });
    */
    var getTos = [];
    $.each(dictIds, function (i, o) {
        getTos.push(getTo(window.ajaxData_InvokeUrl + '/GetDictionaryItem/' + o));
    });

    $.when.apply($, getTos).done(function () {
        var newArgs = []; 
        $.each(arguments, function (i, r) {
            newArgs.push(r[0]);
        });
        d.apply(this, newArgs);
    }).fail(f);
}
function initForm() {
    
    initFormLike();
    initEnterToTab();
}

function autosize(id, delta) {
    $("#" + id).height($("#" + id).parent().height() + (delta || 0));
}

function getTreeData(treeCode, orderByClause, treeParams, d) {
    var strJsonValueOfTreeParam = treeParams || "";
    var url = window.treeDataOfPost_InvokeUrl;
    var _treeParam = { TreeCode: treeCode };
    if (orderByClause) {
        _treeParam.OrderByClause = orderByClause;
    }
    if (strJsonValueOfTreeParam) {
        _treeParam.TreeParams = strJsonValueOfTreeParam;
    }
    postTo(url, $.toJSON(_treeParam), function (nodes) {
        if (d) {
            d.apply(this, [nodes]);
        }
    });
}

function buildTree(type, treeContainerId, treeSettings, nodes, localAync, nameField) {
    var setting = treeSettings || {};
    var url = window.treeDataOfPost_InvokeUrl;
    var treeObj;
    var isAync = _.isString(nodes);
    if (type == "zTree") {
        setting.data = {
            simpleData: {
                enable: true
            }
        };
        treeObj = $.fn.zTree.init($('#' + treeContainerId), setting, nodes);
    }
    else if (type == "easyUITree") {
        if (isAync) {
            setting.url = nodes;
            setting.method = "get";
            setting.loadFilter = function (data, parent) {
                _.each(data, function (o) {
                    if (o.attributes) {
                        if (o.attributes && _.isString(o.attributes)) {
                            o.attributes = eval('(' + o.attributes + ')');
                        }
                    }
                });
                return data;
            };
        }
        else {
            if (localAync) {
                setting.localAync = localAync;
                setting.nodes = nodes;
                if (nameField) {
                    setting.nameField = nameField;
                }
                var sons = _.where(setting.nodes, { pId: '_' });
                var rets = [];
                if (sons.length > 0) {
                    _.each(sons, function (o) {
                        if (o.attributes && _.isString(o.attributes)) {
                            o.attributes = eval('(' + o.attributes + ')');

                        }
                        if (nameField) {
                            o.text = o.attributes[nameField];
                        }
                        else {
                            o.text = o.name;
                        }
                        //o.attributes.isParent = o.isParent;
                        o.state = 'open';
                        if (o.isParent) {
                            o.state = 'closed';
                            o.children = __getTreeNode(setting, o, nameField);
                        }
                        rets.push(o);
                    });
                }
                setting.data = rets;
                setting.onBeforeExpand = function (node) {
                    if (node.attributes.IsParent) {
                        var target = node.target;
                        if ($(this).tree('getChildren', target).length == 0) {
                            $(this).tree("append", { parent: target, data: __getTreeNode(setting, node, nameField) });
                        }
                    }
                }
            }
            else {
                setting.data = __getTreeNode2(nodes, '_');
            }
        }
        treeObj = $('#' + treeContainerId).tree(setting);
    }
    else if (type == "easyUIComboTree") {
        //[id:'',text:'',children:[{id:'',text:''}]]
        if (isAync) {
            setting.url = nodes;
            setting.method = "get";
            setting.loadFilter = function (data, parent) {
                _.each(data, function (o) {
                    if (o.attributes && _.isString(o.attributes)) {
                        o.attributes = eval('(' + o.attributes + ')');
                    }
                });
                return data;
            };
        }
        else {
            if (localAync) {
                setting.localAync = localAync;
                setting.nodes = nodes;
                if (nameField) {
                    setting.nameField = nameField;
                } 
                var sons = _.where(setting.nodes, { pId: '_' });
                var rets = [];
                if (sons.length > 0) {
                    _.each(sons, function (o) {
                        if (o.attributes && _.isString(o.attributes)) {
                            o.attributes = eval('(' + o.attributes + ')');

                        }
                        if (nameField) {
                            o.text = o.attributes[nameField];
                        }
                        else {
                            o.text = o.name;
                        }
                        //o.attributes.isParent = o.isParent;
                        o.state = 'open';
                        if (o.isParent) {
                            o.state = 'closed';
                            o.children = __getTreeNode(setting, o, nameField);
                        }
                        rets.push(o);
                    });
                }
                setting.data = rets;
                setting.onBeforeExpand = function (node) {
                    if (node.attributes.IsParent) {
                        var target = node.target;
                        if ($(this).tree('getChildren', target).length == 0) {
                            $(this).tree("append", { parent: target, data: __getTreeNode(setting, node, nameField) });
                        }
                    }
                }
            }
            else { 
                if (treeSettings.needRoot) {
                    setting.data = [{ id: '_', text: treeSettings.rootName || '无', state: 'open', children: __getTreeNode2(nodes, '_')}]; // __getTreeNode(nodes, '_');
                }
                else {
                    setting.data = __getTreeNode2(nodes, '_');
                }
            }
        }
        treeObj = $('#' + treeContainerId).combotree(setting).combotree('tree');
    }
    
    return treeObj;
}

function initTree(type, treeContainerId, treeSettings, treeData, selectedOrCheckedNodes, d, localAync, nameField) {
    var isCheckedTree = false; //用checkbox来决定是否多选
    if (type == "zTree") {
        isCheckedTree = treeSettings.check.enable;
    }
    else {
        isCheckedTree = treeSettings.checkbox;
    }

    if (_.isFunction(treeData)) {
        treeData(function (nodes) {
            var treeObj = buildTree(type, treeContainerId, treeSettings, nodes, localAync, nameField);
            if (selectedOrCheckedNodes == 0) {
                if (type == "zTree") {
                    treeObj.expandNode(nodes[selectedOrCheckedNodes], true, false, true);
                    treeObj.selectNode(nodes[selectedOrCheckedNodes]);
                }
                else {
                    var node = treeObj.tree('find', nodes[selectedOrCheckedNodes].id);
                    treeObj.tree('expandTo', node.target);
                    treeObj.tree('select', node.target);
                }
            }
            else if (selectedOrCheckedNodes) {

            }

            if (d) {
                d.apply(this, [treeObj]);
            }
        });
    }
    else {
        var isAync = _.isString(treeData); //异步URL
        var treeObj = buildTree(type, treeContainerId, treeSettings, treeData);

        if (d) {
            d.apply(this, [treeObj]);
        }
    }
}

function refreshTree(type, treeContainerId, treeCode, orderByClause, treeParams) {

    getTreeData(treeCode, orderByClause, treeParams, function (nodes) {
        if (type == "easyUITree") {
            var treeObj = $('#' + treeContainerId);
            var setting = treeObj.tree('options');
            if (setting.localAync) {
                var selectedNodeOld = treeObj.tree('getSelected');
                var selectedNodeId;
                if (selectedNodeOld) {
                    selectedNodeId = selectedNodeOld.id;
                }
                buildTree(type, treeContainerId, setting, nodes, setting.localAync, setting.nameField);
                treeObj = $('#' + treeContainerId);
                if (selectedNodeId) {
                    var node = treeObj.tree('find', selectedNodeId);
                    if (node) {  
                        treeObj.tree('expandTo', node.target);
                        treeObj.tree('select', node.target);
                    }
                    else {
                        //寻找祖先

                        getTo(baseCMSInvokeUrl + '/Adb/DirectoryService/GetAncestorDirectories/' + $('#CurrentBook').combobox('getValue') + ',' + selectedNodeId).done(function (ret) {
                            _.each(ret, function (o) {
                                node = treeObj.tree('find', o.DirectoryId);
                                if (node) {
                                    treeObj.tree('expand', node.target);
                                }
                            });
                            treeObj.tree('select', node.target);
                        });
                    }
                }
                else {
                    treeObj.tree('expand', treeObj.tree('getRoot').target);
                }
            }
            else {
                var selectedNodeOld = treeObj.tree('getSelected');
                treeObj.tree('loadData', __getTreeNode2(nodes, '_'));
                if (selectedNodeOld) {
                    var node = treeObj.tree('find', selectedNodeOld.id);
                    treeObj.tree('expandTo', node.target);
                    treeObj.tree('select', node.target);
                }
                else {
                    treeObj.tree('expand', treeObj.tree('getRoot').target)
                }
            }
        }
        else if (type == "easyUIComboTree") {
            var combotreeObj = $('#' + treeContainerId);
            var setting = combotreeObj.combotree('options');
            if (setting.localAync) {
                var selectedNodeOld = combotreeObj.combotree('tree').tree('getSelected');
                var selectedNodeId;
                if (selectedNodeOld) {
                    selectedNodeId = selectedNodeOld.id;
                }
                buildTree(type, treeContainerId, setting, nodes, setting.localAync, setting.nameField);
                combotreeObj = $('#' + treeContainerId);
                if (selectedNodeId) {
                    var node = combotreeObj.combotree('tree').tree('find', selectedNodeId);
                    if (node) {
                        combotreeObj.combotree('tree').tree('expandTo', node.target);
                        combotreeObj.combotree('tree').tree('select', node.target);
                    }
                    else {
                        //寻找祖先
                        getTo(baseCMSInvokeUrl + '/Adb/DirectoryService/GetAncestorDirectories/' + $('#CurrentBook').combobox('getValue') + ',' + selectedNodeId).done(function (ret) {
                            _.each(ret, function (o) {
                                node = combotreeObj.combotree('tree').tree('find', o.DirectoryId);
                                if (node) {
                                    combotreeObj.combotree('tree').tree('expand', node.target);
                                }
                            });
                            combotreeObj.combotree('tree').tree('select', node.target);
                        });
                    }
                }
                else {
                    combotreeObj.combotree('tree').tree('expand', treeObj.tree('getRoot').target);
                }
            }
            else {
                combotreeObj.combotree('loadData', __getTreeNode2(nodes, '_'));
            }
        }
        else if (type == "zTree") {
            var treeObj = $.fn.zTree.getZTreeObj(treeContainerId);
            $.fn.zTree.init($('#' + treeContainerId), treeObj.setting, nodes);
            var selectedNodes = treeObj.getSelectedNodes();
            if (selectedNodes) {
                _.each(selectedNodes, function (node) { treeObj.expandNode(node, true, false, true); });
            }
        }
    });
}

function __getTreeNode(setting, parentNode, nameField) {
    var sons = _.where(setting.nodes, { pId: parentNode.id });
    _.each(sons, function (o) {
        if (o.attributes && _.isString(o.attributes)) {
            o.attributes = eval('(' + o.attributes + ')');

        }
        if (nameField) {
            o.text = o.attributes[nameField];
        }
        else {
            o.text = o.name;
        }
        //o.attributes.isParent = o.isParent;
        if (o.isParent) {
            o.state = 'closed'; 
        }
        else {
            o.state = 'open';
        } 
    }); 
    return sons;
}

function __getTreeNode2(nodes, parentId) {
    var rets = _.where(nodes, { pId: parentId });
    if (rets.length == 0 && parentId == '_' && nodes.length > 0) {
        return __getTreeNode2(nodes, nodes[0].pId);
    }
    var sons = _.reject(nodes, function (o) { return o.pId == parentId });
    if (rets.length > 0) {
        rets = _.map(rets, function (o) {
            var attributes = null;
            if (o.attributes && _.isString(o.attributes)) {
                attributes = eval('(' + o.attributes + ')');
            }
            var children = __getTreeNode2(sons, o.id);
            var state = 'open';
            if (o.isParent) {
                state = 'closed';
            }
            return { id: o.id, text: o.name, checked: o.checked, state: state, attributes: attributes, children: children };
        });
    }
    return rets;
}
  
function initEnterToTab() {
    _initEnterToTab(".form-field:visible,input[type='password']");
}
function _initEnterToTab(el){
    var $els = $(el).not(".not-enter-to-tab");

    $els.each(function (index, o) {
        if (o.tagName == "DIV") {
            if (!o.tabindex) {
                o.tabindex = 0;
                $(o).attr("tabindex", 0); //修复div 不能绑定keydown事件
            }
        }
    })
    .unbind('keydown').bind('keydown', function (e) {
        var key = e.which;
        if (key == 13) {
            e.preventDefault();
            var nextIndex = $els.index(this) + 1;
            var $el = $els.eq(nextIndex);
            $els.eq(nextIndex).focus();
        }
    });
}
function initFormLike() {
    _initFormLike(".form");
}
function _initFormLike(el) {
    var indexSN = 0;
    $(el).find(":input").each(function (index, o) {
        var $o = $(o);
        if (!$o.attr("name")) {
            if (!$o.attr("id")) {
                if ($.browser.msie) {
                    //ie
                    $o.attr("name", $o.attr("id"));
                }
                else {
                    $o.attr("name", "INPUT_" + indexSN++);
                }
            }
            else {
                $o.attr("name", $o.attr("id"));
            }
        }
    });
}
function bindSubmit(el, eldata, func) { 
    var $el = $(el);
    var $eldata = $(eldata).find(":input");
    $el.bind("click keydown", function (e) {
        var cansubmit = false;
        if (e.type == "click") {
            cansubmit = true;
        }
        else if (e.type == "keydown") {
            var key = e.which;
            if (key == 13) {
                e.preventDefault();
                cansubmit = true;
            }
        }
        
        if (cansubmit) {
            if ($.browser.msie) {
                //检测是否有使用placeholder
                $eldata.each(function (index, o) {
                    var $o = $(o);
                    if ($o.attr("placeholder")) {
                        if ($o.val() == $o.attr("placeholder")) {
                            $o.val("");
                        }
                    }
                });
            }
            //x.serializeArray() , x.serializeObject, $.param(x); 
            func.apply(this, [$eldata.serializeObject()]);
            if ($.browser.msie) {
                $eldata.each(function (index, o) {
                    var $o = $(o);
                    if ($o.attr("placeholder") && $o.attr("type") != "password") {
                        if ($o.val() == "") {
                            $o.val($o.attr("placeholder"));
                        }
                    }
                });
            }
        }
    });
}

//fn 带一个是否成功的参数
function bindSubmitForm(el, form, url, data, fn) {
    $(el).unbind('click').bind('click', function () {
        beginProgress();
        $(form).form('submit', {
            url: url,
            onSubmit: function () {
                //检测form
                var isValid = $(this).form('validate');
                if (!isValid) {
                    endProgress(); // hide progress bar while the form is invalid
                }
                return isValid; // return false will stop the form submission
            },
            onBeforeLoad: function (param) {
                $.extend(param, data);
            },
            success: function (json) {
                endProgress();
                if (fn) {
                    fn.call(this, $.evalJSON(json));
                }
            },
            onLoadError: function () { 
                endProgress();
            }
        });
    });
}

function initGridForListView(view, gridOptions, actionOptions, pageOptions) {
   
    var gridId = view.el.attr("id");
    var pagerId = view.el.next().attr("id");
    var gridSettings = $.extend({
        url: "",
        shrinkToFit: false,
        autowidth: true,
        //datatype:"local",
        datatype: function (postdata) {
            var _shouldFechData = true;
            if (view.shouldFechData) {
                if (_.isFunction(view.shouldFechData)) {
                    _shouldFechData = view.shouldFechData();
                }
                else {
                    _shouldFechData = view.shouldFechData;
                }
            } 
            if (_shouldFechData) {
                var schData = view.appendSchData(postdata);
                //alert($.toJSON(schData));
                view.model.fetchJqgridCollection(schData, function (jsonData) {
                    view.el[0].addJSONData(jsonData);
                });
            }
        },
        height: getContainerHeight(),
        multiselect: true,
        pager: "#" + pagerId,
        viewrecords: true,
        jsonReader: {
            repeatitems: false,
            id: "0"
        },
        ondblClickRow: function (rowid) {
            var newrowid = rowid; //处理多主键
            if (view.belongView && view.belongView.model) {
                newrowid = view.belongView.model.id + "," + newrowid
            }
            if (view.detailView) {
                view.detailView.load(newrowid, {
                    buttons: {
                        "提交": function () {
                            view.detailView.save(function () {
                                if (view.detailView.afterSaveSuccess) {
                                    view.detailView.afterSaveSuccess.apply(view.detailView, [view.detailView.el[0]]);
                                }
                                else {
                                    view.el.trigger("reloadGrid");
                                    $(this).dialog("close");
                                }
                            });
                        },
                        "取消": function () {
                            $(this).dialog("close");
                        }
                    }
                });
            }
        }
    }, gridOptions || {});
    var pageSettings = $.extend({
        edit: false, add: false, del: false, search: false
    }, pageOptions || {});
    var actionSettings = $.extend({
        sch: true, add: true, edit: true, del: true,
        searchid: "btnSch", addid: "btnAdd", editid: "btnEdit", delid: "btnDel",
        schshowmode: "icon", addshowmode: "icon", editshowmode: "icon", delshowmode: "icon",
        schtoolbar: true, addtoolbar: true, edittoolbar: true, deltoolbar: true,
        schcaption: "查询", addcaption: "添加", editcaption: "编辑", delcaption: "删除",
        schtitle: "按查询条件过滤记录", addtitle: "添加新记录", edittitle: "编辑所选记录", deltitle: "删除所选记录",
        schbuttonicon: "ui-icon-search", addbuttonicon: "ui-icon-plus", editbuttonicon: "ui-icon-pencil", delbuttonicon: "ui-icon-trash",
        schfunc: function () { view.loadSchDialog(); },
        addfunc: function () {
            if (view.detailView) {
                view.detailView.add({
                    buttons: {
                        "提交": function () {
                            view.detailView.save(function () {
                                if (view.detailView.afterSaveSuccess) {
                                    view.el.trigger("reloadGrid");
                                    view.detailView.afterSaveSuccess.apply(view.detailView, [view.detailView.el[0]]);
                                }
                                else {
                                    $(this).dialog("close");
                                }
                            });
                        },
                        "取消": function () {
                            $(this).dialog("close");
                        }
                    }
                });
            }
        },
        editfunc: function () {
            var id = view.el.getGridParam("selrow");
            if (!id) {
                showError("请选择一条记录");
                return;
            }
            if (view.detailView) {
                view.detailView.load(id, {
                    buttons: {
                        "提交": function () {
                            view.detailView.save(function () {
                                if (view.detailView.afterSaveSuccess) {
                                    view.detailView.afterSaveSuccess.apply(view.detailView, [view.detailView.el[0]]);
                                }
                                else {
                                    view.el.trigger("reloadGrid");
                                    $(this).dialog("close");
                                }
                            });
                        },
                        "取消": function () {
                            $(this).dialog("close");
                        }
                    }
                });
            }
        },
        delfunc: function () {
            var ids = view.el.getGridParam("selarrrow");
            if (ids.length == 0) {
                showError("请选择至少一条记录");
                return;
            }

            showConfirm("当前选中记录将被删除，确定吗？", function (ret) {
                if (ret == "ok") {
                    if (view.detailView) {
                        view.detailView.removeSelected(ids, function () { view.el.trigger("reloadGrid"); });
                    }
                }
                $(this).dialog("close");
            });
        },
        schposition: "last", addposition: "last", editposition: "last", delposition: "last",
        schcursor: "pointer", addcursor: "pointer", editcursor: "pointer", delcursor: "pointer",
        buttons: []
    }, actionOptions || {});
    //alert($.toJSON(gridSettings)); 
    var $grid = $("#" + gridId).jqGrid(gridSettings).navGrid("#" + pagerId, pageSettings);

    var $toolbar = view.toolbar; 

    if ($toolbar && (pageSettings.refresh == undefined || pageSettings.refresh == true)) {
        $("<button id=\"btnRefresh\" title=\"" + (pageSettings.refreshtitle || "刷新表格") + "\">" + (pageSettings.refreshtext || "刷新") + "</button>").appendTo($toolbar).button({ icons: { primary: pageSettings.refreshicon || "ui-icon-refresh"} }).unbind().bind("click", function () { $grid.trigger("reloadGrid"); });
    }
    if (actionSettings.sch) {
        var schbuttonSettings = { caption: actionSettings.schcaption, title: actionSettings.schtitle, buttonicon: actionSettings.schbuttonicon, onClickButton: function () { actionSettings.schfunc.apply(view, arguments); }, position: actionSettings.schposition, cursor: actionSettings.schcursor };
        $.extend(schbuttonSettings, { id: gridId + "_" + actionSettings.schid });
        if (actionSettings.schshowmode == "icon") {
            $.extend(schbuttonSettings, { caption: "" });
        }
        $grid.navButtonAdd('#' + pagerId, schbuttonSettings);
        if ($toolbar && actionSettings.schtoolbar) {
            $("<button id=\"" + actionSettings.schid + "\" title=\"" + actionSettings.schtitle + "\">" + actionSettings.schcaption + "</button>").appendTo($toolbar).button({ icons: { primary: actionSettings.schbuttonicon} }).unbind().bind("click", function () { actionSettings.schfunc.apply(view, arguments); });
        }
    }

    //检测group1 split
    var group1_split = (pageSettings.refresh == undefined || pageSettings.refresh == true || actionSettings.sch);
    var group1_split_toolbar = $toolbar && (pageSettings.refresh == undefined || pageSettings.refresh == true || (actionSettings.sch && actionSettings.schtoolbar));
    if (group1_split) {
        $grid.navSeparatorAdd('#' + pagerId);
    }
    if (group1_split_toolbar) {
        $("<div class=\"split\">&nbsp;</div>").appendTo($toolbar);
    }

    if (actionSettings.add) {
        var addbuttonSettings = { caption: actionSettings.addcaption, title: actionSettings.addtitle, buttonicon: actionSettings.addbuttonicon, onClickButton: function () { actionSettings.addfunc.apply(view, arguments); }, position: actionSettings.addposition, cursor: actionSettings.addcursor };
        $.extend(addbuttonSettings, { id: gridId + "_" + actionSettings.addid });
        if (actionSettings.addshowmode == "icon") {
            $.extend(addbuttonSettings, { caption: "" });
        }
        $grid.navButtonAdd('#' + pagerId, addbuttonSettings);
        if ($toolbar && actionSettings.addtoolbar) {
            $("<button id=\"" + actionSettings.addid + "\" title=\"" + actionSettings.addtitle + "\">" + actionSettings.addcaption + "</button>").appendTo($toolbar).button({ icons: { primary: actionSettings.addbuttonicon} }).unbind().bind("click", function () { actionSettings.addfunc.apply(view, arguments); });
        }
    }
    if (actionSettings.edit) {
        var editbuttonSettings = { caption: actionSettings.editcaption, title: actionSettings.edittitle, buttonicon: actionSettings.editbuttonicon, onClickButton: function () { actionSettings.editfunc.apply(view, arguments); }, position: actionSettings.editposition, cursor: actionSettings.editcursor };
        $.extend(editbuttonSettings, { id: gridId + "_" + actionSettings.editid });
        if (actionSettings.editshowmode == "icon") {
            $.extend(editbuttonSettings, { caption: "" });
        }
        $grid.navButtonAdd('#' + pagerId, editbuttonSettings);
        if ($toolbar && actionSettings.edittoolbar) {
            $("<button id=\"" + actionSettings.editid + "\" title=\"" + actionSettings.edittitle + "\">" + actionSettings.editcaption + "</button>").appendTo($toolbar).button({ icons: { primary: actionSettings.editbuttonicon} }).unbind().bind("click", function () { actionSettings.editfunc.apply(view, arguments); });
        }
    }
    if (actionSettings.del) {
        var delbuttonSettings = { caption: actionSettings.delcaption, title: actionSettings.deltitle, buttonicon: actionSettings.delbuttonicon, onClickButton: function () { actionSettings.delfunc.apply(view, arguments); }, position: actionSettings.delposition, cursor: actionSettings.delcursor };
        $.extend(delbuttonSettings, { id: gridId + "_" + actionSettings.delid });
        if (actionSettings.delshowmode == "icon") {
            $.extend(delbuttonSettings, { caption: "" });
        }
        $grid.navButtonAdd('#' + pagerId, delbuttonSettings);
        if ($toolbar && actionSettings.deltoolbar) {
            $("<button id=\"" + actionSettings.delid + "\" title=\"" + actionSettings.deltitle + "\">" + actionSettings.delcaption + "</button>").appendTo($toolbar).button({ icons: { primary: actionSettings.delbuttonicon} }).unbind().bind("click", function () { actionSettings.delfunc.apply(view, arguments); });
        }
    }
    if (actionSettings.buttons && actionSettings.buttons.length) {
        $grid.navSeparatorAdd('#' + pagerId);
        if ($toolbar && _.any(actionSettings.buttons, function (o) { return o.appendtoolbar; })) {
            $("<div class=\"split\">&nbsp;</div>").appendTo($toolbar);
        }
        $.each(actionSettings.buttons, function (i, button) {
            var buttonClick = button.onClickButton;
            $.extend(button, { id: gridId + "_" + (button.id || ("btnCustom_" + (i + 1))), title: button.title, caption: (button.caption || "newButton"), onClickButton: function () { buttonClick.apply(view, arguments); } });
            $grid.navButtonAdd('#' + pagerId, button);
            if ($toolbar && button.appendtoolbar) {
                $("<button id=\"" + (button.id || ("btnCustom_" + (i + 1))) + "\" title=\"" + (button.title || "") + "\">" + (button.caption || "") + "</button>").appendTo($toolbar).button({ icons: { primary: (button.buttonicon || "ui-icon-newwin")} }).unbind().bind("click", button.onClickButton);
            }
        });
      
    }
    return $grid;
}
 
function initTreeForTreeView(view, treeCode, treeOptions, async, treeParam) {
    var setting = {};

    if (async) {
        setting.async = {
            enable: true,
            type: "get",
            contentType: "application/json",
            url: function (treeId, treeNode) {
                var strJsonValueOfTreeParam = "null";
                if (treeParam) {
                    if (_.isFunction(treeParam)) {
                        strJsonValueOfTreeParam = treeParam();
                    }
                    else {
                        strJsonValueOfTreeParam = treeParam;
                    }
                }
                if (treeNode) {
                    return window.treeDataOfGet_InvokeUrl + treeCode + "," + treeNode.id + ",null," + strJsonValueOfTreeParam;
                }
                else {
                    return window.treeDataOfGet_InvokeUrl + treeCode + ",_,null," + strJsonValueOfTreeParam;
                }
            }
        };
        _.extend(setting, treeOptions || {});
        $.fn.zTree.init(view.zTreeEl(), setting);
    }
    else {
        var url = window.treeDataOfPost_InvokeUrl;
        setting.data = {
            simpleData: {
                enable: true
            }
        };
        _.extend(setting, treeOptions || {});

        var strJsonValueOfTreeParam = null;
        if (treeParam) {
            if (_.isFunction(treeParam)) {
                strJsonValueOfTreeParam = treeParam();
            }
            else {
                strJsonValueOfTreeParam = treeParam;
            }
        }
        
        var _treeParam = { TreeCode: treeCode }; 
        if (strJsonValueOfTreeParam) {
            _treeParam.TreeParams = strJsonValueOfTreeParam;
        }
        postTo(url, $.toJSON(_treeParam), function (zNodes) {
            $.fn.zTree.init(view.zTreeEl(), setting, zNodes);
            if (treeOptions && treeOptions.callback && treeOptions.callback.myOnLoad) {
                treeOptions.callback.myOnLoad.apply(view, [view.zTreeObj()]);
            }
        });
    }
}

function initGrid(gridOptions, actionOptions, gridId, pageOptions, pagerId) {

    if (!gridId) {
        gridId = window.contants.defaultGridId;
    }
    if (!pagerId) {
        pagerId = window.contants.defaultGridPagerId;
    }
    var gridSettings = $.extend({
        url: "",
        autowidth: true,
        dataType: "local",
        height: getContainerHeight(),
        multiselect: true,
        pager: "#" + pagerId,
        viewrecords: true,
        jsonReader: {
            repeatitems: false,
            id: "0"
        }
    }, gridOptions || {});
    var pageSettings = $.extend({
        edit: false, add: false, del: false, search: false
    }, pageOptions || {});

    var actionSettings = $.extend({
        sch: true, add: true, edit: true, del: true,
        searchid: "btnSch", addid: "btnAdd", editid: "btnEdit", delid: "btnDel",
        schshowmode: "icon", addshowmode: "icon", editshowmode: "icon", delshowmode: "icon",
        schtoolbar: true, addtoolbar: true, edittoolbar: true, deltoolbar: true,
        schcaption: "查询", addcaption: "添加", editcaption: "编辑", delcaption: "删除",
        schtitle: "按查询条件过滤记录", addtitle: "添加新记录", edittitle: "编辑所选记录", deltitle: "删除所选记录",
        schbuttonicon: "ui-icon-search", addbuttonicon: "ui-icon-plus", editbuttonicon: "ui-icon-pencil", delbuttonicon: "ui-icon-trash",
        schfunc: function () { alert("sch clicked"); }, addfunc: function () { alert("add clicked") }, editfunc: function () { alert("edit clicked"); }, delfunc: function () { alert("del clicked"); },
        schposition: "last", addposition: "last", editposition: "last", delposition: "last",
        schcursor: "pointer", addcursor: "pointer", editcursor: "pointer", delcursor: "pointer",
        buttons: []
    }, actionOptions || {});

    var $grid = $("#" + gridId).jqGrid(gridSettings).navGrid("#" + pagerId, pageSettings);
    var $toolbar = $("#" + (actionSettings.toolbarid || "toolbar"));
    if (pageSettings.refresh == undefined || pageSettings.refresh == true) {
        $("<button id=\"btnRefresh\" title=\"" + (pageSettings.refreshtitle || "刷新表格") + "\">" + (pageSettings.refreshtext || "刷新") + "</button>").appendTo($toolbar).button({ icons: { primary: pageSettings.refreshicon || "ui-icon-refresh"} }).unbind().bind("click", function () { $grid.trigger("reloadGrid"); });
    }
    if (actionSettings.sch) {
        var schbuttonSettings = { caption: actionSettings.schcaption, title: actionSettings.schtitle, buttonicon: actionSettings.schbuttonicon, onClickButton: actionSettings.schfunc, position: actionSettings.schposition, cursor: actionSettings.schcursor };
        $.extend(schbuttonSettings, { id: gridId + "_" + actionSettings.schid });
        if (actionSettings.schshowmode == "icon") {
            $.extend(schbuttonSettings, { caption: "" });
        }
        $grid.navButtonAdd('#' + pagerId, schbuttonSettings);
        if (actionSettings.schtoolbar) {
            $("<button id=\"" + actionSettings.schid + "\" title=\"" + actionSettings.schtitle + "\">" + actionSettings.schcaption + "</button>").appendTo($toolbar).button({ icons: { primary: actionSettings.schbuttonicon} }).unbind().bind("click", function () { actionSettings.schfunc.apply($grid[0], arguments); });
        }
    }

    //检测group1 split
    var group1_split = (pageSettings.refresh == undefined || pageSettings.refresh == true || actionSettings.sch);
    var group1_split_toolbar = (pageSettings.refresh == undefined || pageSettings.refresh == true || (actionSettings.sch && actionSettings.schtoolbar));
    if (group1_split) {
        $grid.navSeparatorAdd('#' + pagerId);
    }
    if (group1_split_toolbar) {
        $("<div class=\"split\">&nbsp;</div>").appendTo($toolbar);
    }

    if (actionSettings.add) {
        var addbuttonSettings = { caption: actionSettings.addcaption, title: actionSettings.addtitle, buttonicon: actionSettings.addbuttonicon, onClickButton: actionSettings.addfunc, position: actionSettings.addposition, cursor: actionSettings.addcursor };
        $.extend(addbuttonSettings, { id: gridId + "_" + actionSettings.addid });
        if (actionSettings.addshowmode == "icon") {
            $.extend(addbuttonSettings, { caption: "" });
        }
        $grid.navButtonAdd('#' + pagerId, addbuttonSettings);
        if (actionSettings.addtoolbar) {
            $("<button id=\"" + actionSettings.addid + "\" title=\"" + actionSettings.addtitle + "\">" + actionSettings.addcaption + "</button>").appendTo($toolbar).button({ icons: { primary: actionSettings.addbuttonicon} }).unbind().bind("click", function () { actionSettings.addfunc.apply($grid[0], arguments); });
        }
    }
    if (actionSettings.edit) {
        var editbuttonSettings = { caption: actionSettings.editcaption, title: actionSettings.edittitle, buttonicon: actionSettings.editbuttonicon, onClickButton: actionSettings.editfunc, position: actionSettings.editposition, cursor: actionSettings.editcursor };
        $.extend(editbuttonSettings, { id: gridId + "_" + actionSettings.editid });
        if (actionSettings.editshowmode == "icon") {
            $.extend(editbuttonSettings, { caption: "" });
        }
        $grid.navButtonAdd('#' + pagerId, editbuttonSettings);
        if (actionSettings.edittoolbar) {
            $("<button id=\"" + actionSettings.editid + "\" title=\"" + actionSettings.edittitle + "\">" + actionSettings.editcaption + "</button>").appendTo($toolbar).button({ icons: { primary: actionSettings.editbuttonicon} }).unbind().bind("click", function () { actionSettings.editfunc.apply($grid[0], arguments); });
        }
    }
    if (actionSettings.del) {
        var delbuttonSettings = { caption: actionSettings.delcaption, title: actionSettings.deltitle, buttonicon: actionSettings.delbuttonicon, onClickButton: actionSettings.delfunc, position: actionSettings.delposition, cursor: actionSettings.delcursor };
        $.extend(delbuttonSettings, { id: gridId + "_" + actionSettings.delid });
        if (actionSettings.delshowmode == "icon") {
            $.extend(delbuttonSettings, { caption: "" });
        }
        $grid.navButtonAdd('#' + pagerId, delbuttonSettings);
        if (actionSettings.deltoolbar) {
            $("<button id=\"" + actionSettings.delid + "\" title=\"" + actionSettings.deltitle + "\">" + actionSettings.delcaption + "</button>").appendTo($toolbar).button({ icons: { primary: actionSettings.delbuttonicon} }).unbind().bind("click", function () { actionSettings.delfunc.apply($grid[0], arguments); });
        }
    }
    if (actionSettings.buttons && actionSettings.buttons.length) {
        $grid.navSeparatorAdd('#' + pagerId);
        if (_.any(actionSettings.buttons, function (o) { return o.appendtoolbar; })) {
            $("<div class=\"split\">&nbsp;</div>").appendTo($toolbar);
        }
        $.each(actionSettings.buttons, function (i, button) {
            $.extend(button, { id: gridId + "_" + (button.id || ("btnCustom_" + (i + 1))) });
            $grid.navButtonAdd('#' + pagerId, button);
            if (button.appendtoolbar) {
                $("<button id=\"" + (button.id || ("btnCustom_" + (i + 1))) + "\" title=\"" + (button.title || "") + "\">" + (button.caption || "") + "</button>").appendTo($toolbar).button({ icons: { primary: (button.buttonicon || "ui-icon-newwin")} }).unbind().bind("click", function () { button.onClickButton.apply($grid[0], arguments); });
            }
        });
    }
    return $grid;
}

/***********************************以下工具库*****************************************/
function ndateFormatter(val, datefmt) {
    if (val == null || val == "") return "";
    if (val.indexOf("Date") < 0) return val;
    //var time = cellval.replace(/\/Date\(([0-9]*)\)\//, '$1');
    var time = eval('new ' + val.replace(/\//g, ''));

    var date = new Date();
    date.setTime(time);

    return date.pattern(datefmt);
}

function toJsonDate(date) {
    if (_.isDate(date)) {
        return '\/Date(' + date.getTime() + '+0800)\/';
    }
    return '\/Date(' + Date.parse(date) + '+0800)\/';
}

/**  
* 对Date的扩展，将 Date 转化为指定格式的String  
* 月(M)、日(d)、12小时(h)、24小时(H)、分(m)、秒(s)、周(E)、季度(q) 可以用 1-2 个占位符  
* 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字)  
* eg:  
* (new Date()).pattern("yyyy-MM-dd hh:mm:ss.S") ==> 2006-07-02 08:09:04.423  
* (new Date()).pattern("yyyy-MM-dd E HH:mm:ss") ==> 2009-03-10 二 20:09:04  
* (new Date()).pattern("yyyy-MM-dd EE hh:mm:ss") ==> 2009-03-10 周二 08:09:04  
* (new Date()).pattern("yyyy-MM-dd EEE hh:mm:ss") ==> 2009-03-10 星期二 08:09:04  
* (new Date()).pattern("yyyy-M-d h:m:s.S") ==> 2006-7-2 8:9:4.18  
*/
Date.prototype.toMSJSON = function () { var date = '\/Date(' + this.getTime() + ')\/'; alert(date); return date; };
Date.prototype.pattern = function (fmt) {
    if (fmt == undefined) { return ""; }
    var o = {
        "M+": this.getMonth() + 1, //月份   
        "d+": this.getDate(), //日   
        "h+": this.getHours() % 12 == 0 ? 12 : this.getHours() % 12, //小时   
        "H+": this.getHours(), //小时   
        "m+": this.getMinutes(), //分   
        "s+": this.getSeconds(), //秒   
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度   
        "S": this.getMilliseconds() //毫秒   
    };
    var week = {
        "0": "\u65e5",
        "1": "\u4e00",
        "2": "\u4e8c",
        "3": "\u4e09",
        "4": "\u56db",
        "5": "\u4e94",
        "6": "\u516d"
    };
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    if (/(E+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, ((RegExp.$1.length > 1) ? (RegExp.$1.length > 2 ? "\u661f\u671f" : "\u5468") : "") + week[this.getDay() + ""]);
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
}


function alertInfo(msg, fn, autoClose) {
    var _myWin = $.messager.alert('消息提示', msg, 'info').window({ onClose: function () {
        if (fn) {
            fn();
        }
    }
    });

    if (autoClose && _myWin) {
        var _delay = 2000;
        if (isNumber(autoClose) && autoClose !== true) {
            _delay = autoClose;
        }
        setTimeout(function () {
            _myWin.window('close');
        }, _delay);
    }
}

function alertError(msg, fn, autoClose) {
    var _myWin = $.messager.alert('错误提示', msg, 'error').window({ onClose: function () {
        if (fn) {
            fn();
        }
    }
    });
    if (autoClose && _myWin) {
        var _delay = 2000;
        if (isNumber(autoClose) && autoClose !== true) {
            _delay = autoClose;
        }
        setTimeout(function () {
            _myWin.window('close');
        }, _delay);
    }
}

function alertConfirm(msg, fn) {
    $.messager.confirm('确认对话框', msg, fn);
}

function beginProgress(msg) {
    $.messager.progress();
}

function endProgress() {
    $.messager.progress('close');
}

function showInfo(content, callback, options) {
    _showMessageDialog(content, callback, _.extend({ title: "消息提示", type: "info" }, (options || {})));
}
function showError(content, callback, options) {
    _showMessageDialog(content, callback, _.extend({ title: "错误提示", type: "error" }, (options || {})));
}
function showConfirm(content, callback, options) {
    _showMessageDialog(content, callback, _.extend({ title: "确认对话框", type: "confirm", dialogSettings: {
        resizable: false,
        modal: true,
        buttons: {
            "确定": function () {
                if (callback) {
                    callback.call(this, "ok");
                }
                else {
                    $(this).dialog("close");
                }
            },
            "取消": function () {
                if (callback) {
                    callback.call(this, "cancel");
                }
                else {
                    $(this).dialog("close");
                }
            }
        }
    }
    }, (options || {})));
}
function _showMessageDialog(content,callback, options) {
    //content, dialogId, title, type
    //需要页面载入已经调用过initMessageDialog

    var defaults = {
        dialogId: window.defaultMessageInfoDialogId,
        title: "",
        type: "info", //info error
        dialogSettings: {
            resizable: false,
            modal: true,
            buttons: {
                "确定": function () {
                    if (callback) {
                        callback.call(this);
                    }
                    else {
                        $(this).dialog("close");
                    }
                    //$(this).dialog("close");
                }
            }
        }
    };
    var settings = $.extend(true, defaults, options || {});
    var iconClass = "ui-icon-circle-check";
    switch (settings.type) {
        case "error":
            settings.dialogId = window.defaultMessageErrorDialogId;
            iconClass = "ui-icon-circle-close";
            break;
        case "confirm":
            settings.dialogId = window.defaultMessageConfirmDialogId;
            iconClass = "ui-icon-alert";
            break;
        default:
            break;
    }
    var dialogMessage = $("#" + settings.dialogId);
    if (dialogMessage.size() == 0) {
        dialogMessage = $("<div id=\"" + settings.dialogId + "\" ><span class=\"ui-icon " + iconClass + "\" style=\"float:left; margin:0 7px 50px 0;\"></span><div class=\"messageContent\"></div></div>");
        $("body").append(dialogMessage.hide());
    }
    if (content && content != "") {
        dialogMessage.find(".messageContent").html(content);
    }
    if (settings.title && settings.title != "") {
        dialogMessage.attr("title", settings.title);
    }
    dialogMessage.dialog(settings.dialogSettings);
} 
function getTo(url, data, success, options) {
    var defaults = {
        type: 'GET',
        url: url,
        data: data,
        success: success,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }; /// <reference path="../Views/Pub/Customer/list.html" />

    var settings = $.extend(defaults, options || {});
    return $.ajax(settings);
}

function getHtml(url, success, options) {
    var defaults = {
        type: 'GET',
        url: url,
        success: success,
        contentType: 'application/x-www-form-urlencoded',
        dataType: "html"
    }; /// <reference path="../Views/Pub/Customer/list.html" />

    var settings = $.extend(defaults, options || {});
    return $.ajax(settings);
}

//[{url1,data2,success3,options4},{url1,data2,success3,options4}]
function getAll(urls, d, f) {
    var getTos = [];
    $.each(urls, function (i, o) {
        getTos.push(getTo(o));
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

function postTo(url, data, success, options) {
    var defaults = {
        type: 'POST',
        url: url,
        data: data,
        success: success,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    };
    var settings = $.extend(defaults, options || {});
    return $.ajax(settings);
}
function putTo(url, data, success, options) {
    var defaults = {
        type: 'PUT',
        url: url,
        data: data,
        success: success,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    };
    var settings = $.extend(defaults, options || {});
    return $.ajax(settings);
}
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

function deleteTo2(url,selected,done) {
    var ajax = deleteTo(url + 'DeleteSelected/' + selected);
    if (done) {
        ajax.done(function (ret) {
            if (ret.Success) {
                done.call(this, ret);
            }
        });
    }
    return ajax;
}

function nullifyTo(url, success, options) {
    var defaults = {
        type: 'PUT',
        url: url,
        success: success,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    };
    var settings = $.extend(defaults, options || {});
    return $.ajax(settings);
}
function nullifyTo2(url, selected, isRedo, done) {
    var ajax = nullifyTo(url + 'NullifySelected/' + (isRedo ? '1' : "0") + ',' + selected);
    if (done) {
        ajax.done(function (ret) {
            if (ret.Success) {
                done.call(this, ret);
            }
        });
    }
    return ajax;
}

function stopTo(url, success, options) {
    var defaults = {
        type: 'PUT',
        url: url,
        success: success,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    };
    var settings = $.extend(defaults, options || {});
    return $.ajax(settings);
}
function stopTo2(url, selected, done) {
    var ajax = nullifyTo(url + 'StopSelected/' + selected);
    if (done) {
        ajax.done(function (ret) {
            if (ret.Success) {
                done.call(this, ret);
            }
        });
    }
    return ajax;
}

function restartTo(url, success, options) {
    var defaults = {
        type: 'PUT',
        url: url,
        success: success,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    };
    var settings = $.extend(defaults, options || {});
    return $.ajax(settings);
}
function restartTo2(url, selected, done) {
    var ajax = nullifyTo(url + 'RestartSelected/' + selected);
    if (done) {
        ajax.done(function (ret) {
            if (ret.Success) {
                done.call(this, ret);
            }
        });
    }
    return ajax;
}

function saveTo(url, data, done) { 
    var ajax;
    if (data.isCreate) {
        ajax = postTo(url, $.toJSON(data));
    }
    else {
        ajax = putTo(url + data.PK, $.toJSON(data));
    }
    if (done) {
        ajax.done(function (ret) {
            if (ret.Success) {
                done.call(this, ret.instance);
            }
        });
    } 
    return ajax;
}

var isNumber = function (value) {
    return _.isNumber(parseInt(value, 10));
};

var isDate = function (value) {
    if (_.isDate(value)) {
        return true;
    }
    try { $.datepicker.parseDate('yy-mm-dd', value);  return true; } catch (e) { return false; }
}

function isEmail(strEmail) {
    if (strEmail.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1) {
        return true;
    }
    else {
        return false;
    }
}

function isUrl(strUrl) {
    if (strUrl.search(/^([a-z]([a-z]|\d|\+|-|\.)*):(\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?((\[(|(v[\da-f]{1,}\.(([a-z]|\d|-|\.|_|~)|[!\$&'\(\)\*\+,;=]|:)+))\])|((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=])*)(:\d*)?)(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*|(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)|((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)|((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)){0})(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(\#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i) != -1) {
        return true;
    }
    else {
        return false;
    }
}

function newGuid() {
    var guid = "";
    for (var i = 1; i <= 32; i++) {
        var n = Math.floor(Math.random() * 16.0).toString(16);
        guid += n;
        if ((i == 8) || (i == 12) || (i == 16) || (i == 20))
            guid += "-";
    }
    return guid.toUpperCase();
}

function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

function getQueryStringInTab(name) {
    var pageUrl = getQueryString("pageUrl");
    var index = pageUrl.indexOf('?');
    if (index == -1) {
        return null;
    } 
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = pageUrl.substr(index + 1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

function autoAdapt(height1, height2, height3) { 
    if ($.browser.msie) {
        //ie
        return height1;
    }
    else if ($.browser.mozilla) {
        //mozila
        if (height2) {
            return height2;
        }
    }
    else {
        //webkit
        if (height3) {
            return height3;
        }
    }
    return height1;
}

function replaceWithKeys(s, kv) {
    for (var k in kv) {
        eval('s = s.replace(/\\[' + k + '\\]/gi, kv[k]);');
    }
    return s;
}
function isnull(s, d) {
    if (s == null) {
        return d;
    }
    return s;
}

function isempty(s, d) {
    if (s.length == 0) {
        return d;
    }
    return s;
}

function left(mainStr, lngLen) {
    if (lngLen > 0) { return mainStr.substring(0, lngLen) }
    else { return null }
}

function right(mainStr, lngLen) {
    // alert(mainStr.length) 
    if (mainStr.length - lngLen >= 0 && mainStr.length >= 0 && mainStr.length - lngLen <= mainStr.length) {
        return mainStr.substring(mainStr.length - lngLen, mainStr.length)
    }
    else { return null }
}
function mid(mainStr, starnum, endnum) {
    if (mainStr.length >= 0) {
        return mainStr.substr(starnum, endnum)
    } else { return null }
    //mainStr.length 
}


function DateDiff(interval, date1, date2) {
    var long = date2.getTime() - date1.getTime(); //相差毫秒
    switch (interval.toLowerCase()) {
        case "y": return parseInt(date2.getFullYear() - date1.getFullYear());
        case "m": return parseInt((date2.getFullYear() - date1.getFullYear()) * 12 + (date2.getMonth() - date1.getMonth()));
        case "d": return parseInt(long / 1000 / 60 / 60 / 24);
        case "w": return parseInt(long / 1000 / 60 / 60 / 24 / 7);
        case "h": return parseInt(long / 1000 / 60 / 60);
        case "n": return parseInt(long / 1000 / 60);
        case "s": return parseInt(long / 1000);
        case "l": return parseInt(long);
    }
}

function DateAdd(interval,number,date){
    switch (interval.toLowerCase()) {
        case "y": return new Date(date.setFullYear(date.getFullYear() + number));
        case "m": return new Date(date.setMonth(date.getMonth() + number));
        case "d": return new Date(date.setDate(date.getDate() + number));
        case "w": return new Date(date.setDate(date.getDate() + 7 * number));
        case "h": return new Date(date.setHours(date.getHours() + number));
        case "n": return new Date(date.setMinutes(date.getMinutes() + number));
        case "s": return new Date(date.setSeconds(date.getSeconds() + number));
        case "l": return new Date(date.setMilliseconds(date.getMilliseconds() + number));
    }
}

$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

if (window.Node) {

    Node.prototype.replaceNode = function ($target) {
        return this.parentNode.replaceChild($target, this);
    }
    Node.prototype.swapNode = function ($target) {
        var $targetParent = $target.parentNode;
        var $targetNextSibling = $target.nextSibling;
        var $thisNode = this.parentNode.replaceChild($target, this);
        $targetNextSibling ? $targetParent.insertBefore($thisNode, $targetNextSibling) : $targetParent.appendChild($thisNode);
        return this;
    }
}

function uploadExcel(uploaderUrl, dialogId, inputId, text, postData, onUploadSuccess, onUploadComplete) {
    $('#' + dialogId).dialog({
        onClose: function () {
        },
        onBeforeOpen: function () {
            $('#' + inputId).uploadify({
                'formData': postData,
                'multi': false,
                'buttonText': text,
                'fileTypeDesc': 'xls 文件',
                'fileTypeExts': '*.xls',
                'swf': '../../script/uploadify/uploadify.v3.2.swf',
                'uploader': uploaderUrl,
                'onUploadSuccess': onUploadSuccess,
                'onUploadComplete': function (file) { $('#' + dialogId).dialog('close'); }
            });
        },
        modal: true
    }).dialog('open').dialog('setTitle', text + '对话框');
}
function closeUpload(dialogId) {
    $('#' + dialogId).dialog('close');
}

function showLoading() {

    var $mask = $('.datagrid-mask');
    var $mask_msg = $('.datagrid-mask-msg');
    if ($mask.size() == 0) {
        $('<div class="datagrid-mask"></div><div class="datagrid-mask-msg"><div class="spinImage"></div></div>').appendTo('body');
        $mask = $('.datagrid-mask');
        $mask_msg = $('.datagrid-mask-msg');
    }
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

}

function hideLoading() {
    $('.datagrid-mask').hide();
    $('.datagrid-mask-msg').hide();
}

$.fn.scrollTo = function (target, options, callback) {
    if (typeof options == 'function' && arguments.length == 2) { callback = options; options = target; }
    var settings = $.extend({
        scrollTarget: target,
        offsetTop: 125,
        duration: 500,
        easing: 'swing'
    }, options);
    return this.each(function () {
        var scrollPane = $(this);
        var scrollTarget = (typeof settings.scrollTarget == "number") ? settings.scrollTarget : $(settings.scrollTarget);
        var scrollY = (typeof scrollTarget == "number") ? scrollTarget : scrollTarget.offset().top + scrollPane.scrollTop() - parseInt(settings.offsetTop);
        scrollPane.animate({ scrollTop: scrollY }, parseInt(settings.duration), settings.easing, function () {
            if (typeof callback == 'function') { callback.call(this); }
        });
    });
};