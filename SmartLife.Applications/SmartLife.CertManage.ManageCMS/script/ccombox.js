/*
//wdw 2013-06-12
//新版本
//valfield :绑定的值
//textfield :绑定的文本
//data: 为本地json数据
//select:为默认选中值
//remote: 为远程数据
//prompttxt: 提示性文字
//cstyle: 下拉框根div样式
//defaultval: 为默认选中值
*/
(function ($) {
    $.fn.ccombox2 = function (options) {
        var defaults = {
            valfield: 'id',
            textfield: 'name',
            remote: function () { },
            data: null,
            prompttxt: '请选择',
            cstyle: '',
            defaultval: null
        }
        options = $.extend(defaults, options || {});

        return this.each(function () {
            $(this).hide();
            var currentid = $(this).attr("id");
            //模拟下拉框的id
            var id = 'divSelect' + currentid;

            //获取动态宽度
            var getwidth = $(this).width();
            var getheight = $(this).height();

            //插入模拟的下拉框
            var adddiv = '<div ><div id="' + id + '"><input type="text" id="a_' + id;
            adddiv += '" value="' + options.prompttxt + '"/></div><div id="b_' + id + '"></div></div>';
            $(this).after(adddiv);

            var $did = $("#" + id);
            var $aid = $('#a_' + id);
            var $bid = $('#b_' + id);
            //插入样式
            if (options.cstyle != '') {
                $did.parents("div").addClass(options.cstyle);
            }
            $did.addClass('am').css({ 'width': getwidth, 'height': getheight, 'line-height': getheight });
            $aid.addClass('box').css({ 'color': "#999", 'height': '100%' });

            //获取数据，以本地数据优先;
            var tmpdata = options.data;
            if (!tmpdata) {
                if (options.remote) {
                    tmpdata = options.remote();
                }
            }
            //获取子节点数据
            if (tmpdata) {
                //为输入框添加事件
                $aid.hover(function () {
                    if ($(this).val() === options.prompttxt) {
                        $(this).val("").css({ 'color': '' });
                    }
                }, function () {
                    //$(this).val(options.prompttxt);
                });
                $did.click(function (eb) {
                    //输入框单击事件，实现隐藏和弹出
                    if ($bid.is(":hidden")) {
                        $bid.show();
                    }
                    else {
                        $bid.hide();
                    }
                    try {
                        eb.stopPropagation();
                    }
                    catch (e) {
                        event.cancelBubble = true;
                    }
                });
                $bid.addClass('bm').hide();

                //整合数据
                var markup = changeDataToHtml(tmpdata, "", null, 0);
                if (markup.length <= 0) {
                    $bid.remove();
                    return false;
                }
                else {
                    var bid = $bid.attr("id");
                    //绑定父节点 
                    $bid.append(markup);
                    $('#' + bid + " div").addClass('cur');
                    $('#' + bid + " .cdiv").addClass('rowdiv');
                    //绑定子节点
                    $('#' + bid + " a").click(function () {
                        $("#" + currentid).val($(this).attr("v"));
                        $aid.val($(this).html().replace(/&nbsp;/g, ''));
                        $bid.hide();
                    }).hover(function () {
                        $(this).addClass('choose');
                    }, function () {
                        $(this).removeClass('choose');
                    }).addClass('first');
                }
                //获取高度
                var _count = $('#' + bid + " div").length;
                //设定下拉的高度
                if (_count > 10) {
                    $bid.css({ "height": "250px", "overflow": "auto" });
                }
                //点击下拉框外的地方则关闭下拉框
                $(document).click(function () {
                    $bid.hide();
                });
            }

            // 绑定初始值,待完善
            if (options.defaultval) {
                $aid.val('').val(options.defaultval);
                //$(this).val(options.defaultval);
            }
        });
    }

    //获取下拉菜单的子元素
    function changeDataToHtml(dtjson, pid, parr, nlevel) {
        var ahtml = [];
        $.each(dtjson, function (o, ov) {
            if (ov["pid"] == pid) {
                ahtml.push(changeDataToHtml(dtjson, ov["id"], ov, ++nlevel));
                nlevel--;
            }
        });

        //去掉最后父节点为空的节点
        if (parr == null) return ahtml.join("");
        //1,2行节点不可选择
        var str = "";
        if (nlevel == 1) {
            str = "<div >&nbsp;" + parr["name"] + "&nbsp;&nbsp;</div>";
            str += ahtml.join("");
        }
        else if (nlevel == 2) {
            str = "<div class='cdiv'>&nbsp;" + parr["name"] + "&nbsp;&nbsp;" + ahtml.join("") + "</div>";
        }
        else {
            //第三节点及其以下届点都属于可点击节点，并排在同一行
            str = "<a href='javascript:void(0)' v='" + parr["id"] + "' >&nbsp;&nbsp;" + parr["name"] + "&nbsp;&nbsp;</a>";
            str += ahtml.join("");
        }

        ahtml = [];
        ahtml.push(str);
        return ahtml.join("");
    }
})(jQuery);

//(function ($) {
//    $.fn.ccombox = function (options) {
//        var defaults = {
//            valfield: 'id',
//            textfield: 'name',
//            remote: function () { },
//            data: null,
//            prompttxt: '请选择',
//            cstyle: '',
//            defaultval: null
//        }
//        options = $.extend(defaults, options);

//        return this.each(function () {
//            $(this).hide();
//            var currentid = $(this).attr("id");
//            //模拟下拉框的id
//            var id = 'divSelect' + currentid;

//            //获取动态宽度
//            var getwidth = $(this).width();
//            var getheight = $(this).height();

//            //插入模拟的下拉框
//            var adddiv = '<div ><div id="' + id + '"><input type="text" id="a_' + id;
//            adddiv += '" value="' + options.prompttxt + '"/></div><div id="b_' + id + '"></div></div>';
//            $(this).after(adddiv);

//            var $did = $("#" + id);
//            var $aid = $('#a_' + id);
//            var $bid = $('#b_' + id);
//            //插入样式
//            if (options.cstyle != '') {
//                $did.parents("div").addClass(options.cstyle);
//            }
//            $did.addClass('am').css({ 'width': getwidth, 'height': getheight, 'line-height': getheight });
//            $aid.addClass('box').css({ 'color': "#999", 'height': '100%' });

//            //获取数据，以本地数据优先;
//            var tmpdata = options.data;
//            if (!tmpdata) {
//                if (options.remote) {
//                    tmpdata = options.remote();
//                }
//            }
//            //获取子节点数据
//            if (tmpdata) {
//                //为输入框添加事件
//                $aid.hover(function () {
//                    if ($(this).val() === options.prompttxt) {
//                        $(this).val("").css({ 'color': '' });
//                    }
//                }, function () {
//                    //$(this).val(options.prompttxt);
//                });
//                $did.click(function (eb) {
//                    //输入框单击事件，实现隐藏和弹出
//                    if ($bid.is(":hidden")) {
//                        $bid.show();
//                    }
//                    else {
//                        $bid.hide();
//                    }
//                    try {
//                        eb.stopPropagation();
//                    }
//                    catch (e) {
//                        event.cancelBubble = true;
//                    }
//                });
//                $bid.addClass('bm').hide();

//                //整合数据
//                var _count = 0; //获取高度
//                var markup = changeDataToHtml(tmpdata, "", null, 0); 
//                if (markup.length <= 0) {
//                    $bid.remove();
//                    return false;
//                }
//                else {
//                    var bid = $bid.attr("id");
//                    //绑定父节点 
//                    $bid.append(markup);
//                    _count = $('#' + bid + " div").length;
//                    $('#' + bid + " div").addClass('cur');
//                    $('#' + bid + " .cdiv").addClass('rowdiv');
//                    //绑定子节点
//                    $('#' + bid + " a").click(function () {
//                        $("#" + currentid).val($(this).attr("v"));
//                        $aid.val($(this).html().replace(/&nbsp;/g, ''));
//                        $bid.hide();
//                    }).hover(function () {
//                        $(this).addClass('choose');
//                    }, function () {
//                        $(this).removeClass('choose');
//                    }).addClass('first');
//                }

//                //设定下拉的高度
//                if (_count > 10) {
//                    $bid.css("height", "150px");
//                }
//                //点击下拉框外的地方则关闭下拉框
//                $(document).click(function () {
//                    $bid.hide();
//                });
//            }

//            // 绑定初始值,待完善
//            if (options.defaultval) {
//                $aid.val('').val(options.defaultval);
//                //$(this).val(options.defaultval);
//            }
//        });
//    }

//    //获取下拉菜单的子元素
//    function changeDataToHtml(dtjson, pid, parr, nlevel) {
//        var ahtml = [];
//        $.each(dtjson, function (o, ov) {
//            if (ov["pid"] == pid) {
//                ahtml.push(changeDataToHtml(dtjson, ov["id"], ov, ++nlevel));
//                nlevel--;
//            }
//        });

//        //去掉最后父节点为空的节点
//        if (parr == null) return ahtml.join("");
//        //1,2行节点不可选择
//        var str = "";
//        if (nlevel == 1) {
//            str = "<div >&nbsp;" + parr["name"] + "&nbsp;&nbsp;</div>";
//            str += ahtml.join("");
//        }
//        else if (nlevel == 2) {
//            str = "<div class='cdiv'>&nbsp;" + parr["name"] + "&nbsp;&nbsp;" + ahtml.join("") + "</div>";
//        }
//        else {
//            //第三节点及其以下届点都属于可点击节点，并排在同一行
//            str = "<a href='javascript:void(0)' v='" + parr["id"] + "' >&nbsp;&nbsp;" + parr["name"] + "&nbsp;&nbsp;</a>";
//            str += ahtml.join("");
//        }

//        ahtml = [];
//        ahtml.push(str);
//        return ahtml.join("");
//    }
//})(jQuery);
