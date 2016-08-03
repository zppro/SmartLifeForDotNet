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
        if (currentMenuId && window.frames['tab_' + currentMenuId]) {
            return window.frames['tab_' + currentMenuId].$(selector);
        }
    }
    return $(selector);
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
                theModel.onBeforeDialogOpen.apply(this, [row]);
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

        getTo(url, null, null, null, theModel.headers).done(function (ret) {

            getJQO('#' + theModel[formIdKey]).form('clear');
            getJQO('#' + theModel[formIdKey]).form('load', ret.instance);

            var dateboxs = getJQO('#' + theModel[formIdKey] + ' .easyui-datebox');
            if (dateboxs.size() > 0) {
                dateboxs.datebox('setValue', ndateFormatter(dateboxs.datebox('getValue'), 'yyyy-MM-dd'));
            }
            /*
            if (dateboxs.size() > 1) {
                 
            _.each(dateboxs, function (o) {
            //alert($(o).attr('id'));
            $('#'+$(o).attr('id')).datebox('setValue', ndateFormatter($('#'+$(o).attr('id')).datebox('getValue'), 'yyyy-MM-dd'));
            });
                
            for (var i = 0; i < dateboxs.length; i++) {
            dateboxs[i].datebox('setValue', ndateFormatter(dateboxs[i].datebox('getValue'), 'yyyy-MM-dd'));
                
            }
            else if (dateboxs.size() == 1) {
            dateboxs.datebox('setValue', ndateFormatter(dateboxs.datebox('getValue'), 'yyyy-MM-dd'));
            }
            */

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
    __remove(true, true);
}
function nullifyItem() {
    __remove(false, true);
}

function remove() {
    __remove(true, false);
}
function removeItem() {
    __remove(false, false);
}
function __remove(isPrimary, isNullify) {
    var theModel = getModel();
    var gridIdKey = isPrimary ? "gridId" : "gridIdOfItem";
    var pkKey = isPrimary ? "pk" : "pkOfItem";
    var pkIdKey = isPrimary ? "pkId" : "pkIdOfItem";
    var baseModelUrlKey = isPrimary ? "baseModelUrl" : "baseModelUrlOfItem";
    var gridIdKey = isPrimary ? "gridId" : "gridIdOfItem";
    var actionName = isNullify ? "作废" : "删除";
    var unCheckSystemKey = isPrimary ? "unCheckSystem" : "unCheckSystemOfItem";
    var isSys = getJQO('#' + theModel[pkIdKey]).attr("type") == 'sys';
    var rows = getJQO('#' + theModel[gridIdKey]).datagrid('getChecked');
    var action = 'delete';
    if (isNullify) {
        action = "nullify";
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
                    nullifyTo2(theModel[baseModelUrlKey], selected, fn, theModel.headers);
                }
                else {
                    deleteTo2(theModel[baseModelUrlKey], selected, fn, theModel.headers);
                }
            }
        });
    }
    else {
        alertInfo('请选中要' + actionName + '的记录！');
    }
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
    var rows = getJQO('#' + theModel[gridIdKey]).datagrid('getChecked');
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
                    stopTo2(theModel[baseModelUrlKey], selected, fn, theModel.headers);
                }
                else {
                    restartTo2(theModel[baseModelUrlKey], selected, fn, theModel.headers);
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
        }, theModel.headers);
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

    if (theModel.onBeforeSearchDialogOpen) {
        if (_.isFunction(theModel.onBeforeSearchDialogOpen)) {
            theModel.onBeforeSearchDialogOpen.apply(this, []);
        }
    }
    getJQO('#' + theModel[searchIdKey]).dialog({
        inline: false,
        onClose: function () {
            theModel.uiMode = 'list';
        },
        modal: true
    }).dialog('open').dialog('setTitle', '查询-' + theModel[entityNameKey]);
    getJQO('#' + theModel[formIdKey]).form('clear');

    if (theModel.onAfterSearchDialogOpen) {
        if (_.isFunction(theModel.onAfterSearchDialogOpen)) {
            theModel.onAfterSearchDialogOpen.apply(this, []);
        }
    }
}
function _exec(isRough) {
    var theModel = getModel();
    var gridIdKey = "gridId";
    var searchIdKey = "dlgSearchId";
    var formIdKey = "fmSearchId";   //精确查询
    var formRoughIdKey = "fmSearchRoughId";     //模糊查询

    //清空信息
    var $restForm = getJQO('#' + (isRough ? theModel[formIdKey] : theModel[formRoughIdKey]));
    if ($restForm.length) {
        getJQO('#' + (isRough ? theModel[formIdKey] : theModel[formRoughIdKey]))[0].reset();
    }

    var searchData = getJQO('#' + theModel[formIdKey]).serializeObject();
    var searchRoughData = getJQO('#' + theModel[formRoughIdKey]).serializeObject();
    //提交查询前
    if (theModel.changeSearchFormDataBeforeSubmit) {
        theModel.changeSearchFormDataBeforeSubmit(searchRoughData);
    }
    //合并查询条件
    var formData = {};
    if (isRough) {
        $.extend(formData, searchData, searchRoughData);
    }
    else {
        $.extend(formData, searchRoughData, searchData);
    }
    //修改models的默认值
    var queryParamsArray = getJQO('#' + theModel[gridIdKey]).datagrid('options').queryParams;
    for (var instance in queryParamsArray.instance) {
        if (_.has(formData, instance)) {
            queryParamsArray.instance[instance] = formData[instance];
            formData[instance] = undefined;
        }
    }

    //清除前一次查询的匹配条件
    for (var o in formData) {
        if (queryParamsArray.fuzzyFields && _.isArray(queryParamsArray.fuzzyFields)) {
            for (var i = 0; i < queryParamsArray.fuzzyFields.length; i++) {
                if (queryParamsArray.fuzzyFields[i].key == o) {
                    queryParamsArray.fuzzyFields.splice(i, 1);
                    i--;
                }
            }
        }
        if (queryParamsArray.filterFields && _.isArray(queryParamsArray.filterFields)) {
            for (var i = 0; i < queryParamsArray.filterFields.length; i++) {
                if (queryParamsArray.filterFields[i].key == o) {
                    queryParamsArray.filterFields.splice(i, 1);
                    i--;
                }
            }
        }
    }

    //添加当前查询条件
    var filterFields = queryParamsArray.filterFields || [];
    var fuzzyFields = queryParamsArray.fuzzyFields || [];
    for (var o in formData) {
        if (formData[o]) {
            var dataItem = { key: o, value: formData[o] };
            if (o.indexOf('_Start') == -1 && o.indexOf('_End') == -1) {
                fuzzyFields.push(dataItem);
            }
            else {
                filterFields.push(dataItem);
            }
        }
    }

    queryParamsArray.fuzzyFields = fuzzyFields;
    queryParamsArray.filterFields = filterFields;
    //模糊查询
    if (isRough) {
        queryParamsArray.fuzzyFieldOP = "OR";
    }
    else {
        delete queryParamsArray["fuzzyFieldOP"];
        //queryParamsArray.fuzzyFieldOP = undefined;
    }

    getJQO('#' + theModel[gridIdKey]).datagrid('load', queryParamsArray);
    getJQO('#' + theModel[searchIdKey]).dialog('close');
}

function _resetfm(isRough) {
    var theModel = getModel();
    var formIdKey = isRough ? "fmSearchRoughId" : "fmSearchId";
    //getJQO('#' + theModel[formIdKey]).form('clear');
    getJQO('#' + theModel[formIdKey])[0].reset();
}
function _quit() {
    var theModel = getModel();
    var dialogIdKey = "dlgSearchId";
    getJQO('#' + theModel[dialogIdKey]).dialog('close');
}

function exportScript() {
    _exportScript(true);
}

function _exportScript(isPrimary) {
    var theModel = getModel();
    var gridIdKey = isPrimary ? "gridId" : "gridIdOfItem";
    var exportScriptParams = theModel["exportScriptParams"];
    var rows;
    if (exportScriptParams.treeFilterFlag) {
        rows = [];

        var theTreeSelected = getJQO('#' + theModel["treeId"]).tree('getSelected');
        if (theTreeSelected) {
            var o = {};
            o[exportScriptParams.pkColumnName] = theTreeSelected.id;
            rows.push(o);
        }
    }
    else {
        rows = getJQO('#' + theModel[gridIdKey]).datagrid('getChecked');
    }
    if (!exportScriptParams) {
        alertError("请先设置导出脚本参数");
        return;
    }
    var url = window.MsSQLScript_InvokeUrl;
    if (exportScriptParams.tableItemNames) {
        url += "/getTableScriptWithItem/" + exportScriptParams.tableName + "," + exportScriptParams.tableItemNames;
    }
    else {
        url += "/getTableScript/" + exportScriptParams.tableName;
    }
    url += "," + exportScriptParams.pkColumnName;
    if (rows.length == 0) {
        url += ",none";
    }
    else {
        url += "," + _.map(rows, function (o) { return o[exportScriptParams.pkColumnName]; }).join("|");
    }

    openDialog('dlg-export-script', 'views/dialogs/export-script.htm', null, { dialogData: { url: url }, title: '导出脚本对话框', width: 800, height: 500, padding: 5, buttons: [{
        text: '复制',
        iconCls: 'icon-copy',
        handler: function () {

            if (window.clipboardData && window.clipboardData.setData) {
                window.clipboardData.setData('Text', $('#scriptContent').select().val()); //ie
                alertInfo("成功复制到剪贴板");
            } else {
                $('#scriptContent').select(); // 选中要复制的内容，给用户提供方便
                alertInfo("您的浏览器不支持此复制功能,请使用Ctrl+C或鼠标右键");
            }
        }
    }, {
        text: '关闭',
        iconCls: 'icon-cancel',
        handler: function () { $('#dlg-export-script').dialog('close'); }
    }]
    });
}

///frame对应的js也有
function openDialog(dialogId, href, done, options) {
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

function addRow(gridId) {
    getJQO('#' + gridId).edatagrid('addRow');
}
function editRow(gridId, index) {
    getJQO('#' + gridId).edatagrid('editRow', index);
}
function nullifyRow(gridId) {
    getJQO('#' + gridId).edatagrid('destroyRow');
}
function saveRow(gridId) {
    getJQO('#' + gridId).edatagrid('saveRow');
}
function exec(isRough) {
    _exec(isRough);
}
function query() {
    _query();
}

function resetfm(isRough) {
    _resetfm(isRough);
}
function quit() {
    _quit();
}
 
/*************** ///easyUI 基本操作 ******************/