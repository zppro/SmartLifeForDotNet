function rechargeAccount(entityName, row) {
    if (!row) {
        row = $('#' + models[currentMenuCode].gridId).datagrid('getSelected');
    }
    if (row) {
        $('#' + models[currentMenuCode].dialogId).dialog({
            onClose: function () {
            },
            onBeforeOpen: function () {
                $('#OldManName').text(row['OldManName']);
                $('#Gender').text(row['Gender'] == 'M' ? "男" : "女");
                $('#ContactPhone').text(row['ContactPhone']);
                $('#Address').text(row['Address']);
            },
            modal: true
        }).dialog('open').dialog('setTitle', '充值' + entityName);
    }
    else {
        alertInfo('请选中要充值的' + entityName + '！'); 
    }
}