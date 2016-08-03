    function uploadBaseData(entityName, formData) {
    var uploaderUrl;
    
    if (entityName == "老人基本资料") {
        uploaderUrl = baseCMSInvokeUrl + '/Oca/_OldCareOldManBaseDataUploadify.ashx';
    }
    else if (entityName == "政府机构资料" || entityName == "社区机构资料" || entityName == "商家机构资料") {
        uploaderUrl = baseCMSInvokeUrl + '/Pub/_ServiceStationUploadify.ashx';
    }
    else if (entityName == "邻里互助人员资料") {
        uploaderUrl = baseCMSInvokeUrl + '/Oca/_MutualAidPersonUploadify.ashx';
    }
    else if (entityName == "居民资料") {
        uploaderUrl = baseCMSInvokeUrl + '/Oca/_FamilyMemberUploadify.ashx';
    }
    else if (entityName == "家庭摄像头资料") {
        uploaderUrl = baseCMSInvokeUrl + '/Oca/_FamilyCameraUploadify.ashx';
    }
    else if (entityName == "辖区基本资料") {
        uploaderUrl = baseCMSInvokeUrl + '/Pub/_AreaBaseDataUploadify.ashx';
    } 
    else if (entityName == "居民基本资料") {
        uploaderUrl = baseCMSInvokeUrl + '/Bas/_ResidentBaseUploadify.ashx';
    }
    $('#' + models[currentMenuCode].dialogId).dialog({
        onClose: function () {
        },
        onBeforeOpen: function () {
            $('#aTemplate').attr('href', baseUrl + '/templates/old-care/' + entityName + '.xls');
            $('#file_upload').uploadify({
                'formData': formData,
                'multi': false,
                'buttonText': "上传" + entityName,
                'fileTypeDesc': 'xls 文件',
                'fileTypeExts': '*.xls',
                'swf': '../../script/uploadify/uploadify.v3.2.swf',
                'uploader': uploaderUrl
            });
        },
        modal: true
    }).dialog('open').dialog('setTitle', '上传' + entityName);
}
//加载不同页面
function uploadBaseData2(entityName, formData) {
    var uploaderUrl;
    if (entityName == "居民基本资料") {
        uploaderUrl = baseCMSInvokeUrl + '/Oca/_ResidentBaseUploadify.ashx';
    }
    $('#' + models[currentMenuCode].dialogUploadId).dialog({
        onClose: function () {
        },
        onBeforeOpen: function () {
            $('#aTemplate').attr('href', baseUrl + '/templates/old-care/' + entityName + '.xls');
            $('#file_upload').uploadify({
                'debug': true,
                'formData': formData,
                'multi': false,
                'buttonText': "上传" + entityName,
                'fileTypeDesc': 'xls 文件',
                'fileTypeExts': '*.xls',
                'swf': '../../script/uploadify/uploadify.v3.2.swf',
                'uploader': uploaderUrl,
                //需要重写的事件
                //'overrideEvents': ['onDialogClose', 'onSelectError', 'onUploadError'],
                'onSelect': function (file) {
                    //                    alert("文件名：" + file.name + "\r\n" + "文件大小：" + file.size + "\r\n" + "文件类型：" + file.type);
                },
                //文件选择错误时触发
                'onUploadError': function (file, errorCode, errorMsg, errorString) {
                    $('#' + file.id).find('.data').html(' - 文件格式错误 ' + errorString);
                    //                    alert('文件 ' + file.name + ' 上传失败的原因: ' + errorString + "错误代码:" + errorCode + "错误信息:" + errorMsg);
                },
                //文件队列上传完毕触发
                'onQueueComplete': function () {
                    //                    if (this.settings.onQueueComplete) this.settings.onQueueComplete.call(this, "ok");
                },
                'onUploadSuccess': function (file, data, response) {
                    if (data != "1" && models[currentMenuCode].uploadSuccessDone) {
                        models[currentMenuCode].uploadSuccessDone(data);
                    }
                },
                'onFallback': function () {
                    alert("您未安装FLASH控件，无法上传图片！请安装FLASH控件后再试。");
                }
            });
        },
        modal: true
    }).dialog('open').dialog('setTitle', '上传' + entityName);
}

function closeUpload(breload) {
    $('#' + models[currentMenuCode].dialogId).dialog('close');
    $('#' + models[currentMenuCode].gridId).datagrid('reload');
}

function closeUpload2(breload) {
    $('#' + models[currentMenuCode].dialogUploadId).dialog('close');
    if (!breload) {
        $('#' + models[currentMenuCode].gridId).datagrid('reload');
    }
}

function saveUpload() {
    postTo(models[currentMenuCode].baseModelUrl + "SynUploadBaseInfo", null, function (ret) {
        if (ret.Success) {
            alertInfo("上传成功！");
            $('#' + models[currentMenuCode].dialogUploadId).dialog('close');

            $('#' + models[currentMenuCode].gridId).datagrid('reload');
        }
    }, null, models[currentMenuCode].headers);
    
}

//加载不同页面
function uploadImg(entityName, formData) {
    uploaderUrl = baseCMSInvokeUrl + '/AppShare/uploadify.ashx';
    var settings = { folderPath: top.objectId };
    _.extend(settings, formData.ImageData);


    $('#' + models[currentMenuCode].dialogUploadId).dialog({
        onClose: function () {
        },
        onBeforeOpen: function () {
            if (formData.onBeforeDialogOpen) {
                if (_.isFunction(formData.onBeforeDialogOpen)) {
                    formData.onBeforeDialogOpen.apply(this, []);
                }
            }
            $('#file_upload').uploadify({
                'formData': settings,
                //'auto':false,  //禁止自动上传
                'multi': true,
                //'folder': 'Image',  //? 后台获取不到值
                'buttonText': "上传" + entityName,
                'fileTypeDesc': '支持的格式：',
                'fileTypeExts': '*.png;*.jpg;*.gif',
                'swf': '../../script/uploadify/uploadify.v3.2.swf',
                'uploader': uploaderUrl,
                'cancelImg': '../../css/uploadify/cancel.png',
                'onQueueComplete': function () {
                    if (formData.onBeforeDialogOpen) {
                        if (_.isFunction(formData.onBeforeDialogOpen)) {
                            formData.onBeforeDialogOpen.apply(this, []);
                        }
                    }
                }//,
//                'onUploadSuccess': function () {
//                    alert("23");
//                }
            });
        },
        modal: true
    }).dialog('open').dialog('setTitle', '上传' + entityName);
}

function uploadfile(entityName, formData) {
    uploaderUrl = baseCMSInvokeUrl + '/AppShare/uploadify.ashx';
    var settings = { folderPath: top.objectId };
    _.extend(settings, formData.ImageData);


    $('#' + models[currentMenuCode].dialogUploadId).dialog({
        onClose: function () {
        },
        onBeforeOpen: function () {
            if (formData.onBeforeDialogOpen) {
                if (_.isFunction(formData.onBeforeDialogOpen)) {
                    formData.onBeforeDialogOpen.apply(this, []);
                }
            }
            $('#file_upload').uploadify({
                'formData': settings,
                //'auto':false,  //禁止自动上传
                'multi': true,
                //'folder': 'Image',  //? 后台获取不到值
                'buttonText': "上传" + entityName,
                'fileTypeDesc': '支持的格式：',
//                'fileTypeExts': '*.png;*.jpg;*.gif',
                'swf': '../../script/uploadify/uploadify.v3.2.swf',
                'uploader': uploaderUrl,
                'cancelImg': '../../css/uploadify/cancel.png',
                'onQueueComplete': function () {
                    if (formData.onBeforeDialogOpen) {
                        if (_.isFunction(formData.onBeforeDialogOpen)) {
                            formData.onBeforeDialogOpen.apply(this, []);
                        }
                    }
                } 
            });
        },
        modal: true
    }).dialog('open').dialog('setTitle', '上传' + entityName);
}

function uploadAttachment(dialogId, inputId, text, postData, events) {
    var uploadUrl = baseCMSInvokeUrl + '/AppShare/uploadify.ashx';
    $('#' + dialogId).dialog({
        onClose: function () {
        },
        onBeforeOpen: function () {
            if (events.onBeforeDialogOpen) {
                events.onBeforeDialogOpen.apply(this, []);
            }

            $('#' + inputId).uploadify({
                'formData': postData,
                'multi': false,
                'buttonText': text,
                'fileTypeDesc': window.fileTypeDesc,
                'fileTypeExts': window.fileTypeExts,
                'swf': '../../script/uploadify/uploadify.v3.2.swf',
                'uploader': uploadUrl,
                'cancelImg': 'http://115.236.175.109:5833/share/css/uploadify/cancel.png',
                'onQueueComplete': function () {
                    if (events.onQueueComplete) {
                        events.onQueueComplete.apply(this, []);
                    }
                },
                'onUploadSuccess': events.onUploadSuccess,
                'onUploadComplete': function (file) { $('#' + dialogId).dialog('close'); }
            });
        },
        modal: true
    }).dialog('open').dialog('setTitle', text + '对话框');
}