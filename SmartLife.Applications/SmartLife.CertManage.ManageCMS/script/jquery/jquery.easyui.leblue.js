/**  
* 扩展  
*/
$.extend($.fn.datagrid.methods, {
    /**
    * 添加单元格选择  
    * @param {} jq  
    * @param {} params 
    * @return {}  
    */
    attachCellSelect: function (jq, params) {
        var _onmousedown = params.onmousedown || function (e) { };
        var _onmouseup = params.onmouseup || function (e) { };
        var _onmouseover = params.onmouseover || function (e) { };
        var _onmouseout = params.onmouseout || function (e) { };
        var _onkeydown = params.onkeydown || function (e) { };
        var _onbodyclick = params.onbodyclick || function (e) { };

        return jq.each(function () {
            var grid = $(this);
            var options = $(this).data('datagrid');
            var panel = grid.datagrid('getPanel').panel('panel');
            panel.find('.datagrid-body').each(function () {
                var delegateEle = $(this).find('> div.datagrid-body-inner').length
                            ? $(this).find('> div.datagrid-body-inner')[0]
                            : this;
                $(delegateEle).off({ 'mouseover': _onmouseover, 'mouseout': _onmouseout, 'mousedown': _onmousedown, 'mouseup': _onmouseup, 'keydown': _onkeydown }, 'td')
                            .on({ 'mouseover': _onmouseover, 'mouseout': _onmouseout, 'mousedown': _onmousedown, 'mouseup': _onmouseup, 'keydown': _onkeydown }, 'td');
                $(delegateEle).off({ 'click': _onbodyclick}).on({ 'click': _onbodyclick });
            });
        });
    },
    /**
    * 移除单元格选择  
    * @param {} jq  
    * @return {}  
    */
    detachCellSelect: function (jq) {
        return jq.each(function () {
            var data = $(this).data('datagrid');
            if (data.tooltip) {
                data.tooltip.remove();
                data.tooltip = null;
                var panel = $(this).datagrid('getPanel').panel('panel');
                panel.find('.datagrid-body').undelegate('td',
                                'mouseover').undelegate('td', 'mouseout')
                                .undelegate('td', 'mousemove')
            }
            if (data.tipDelayTime) {
                clearTimeout(data.tipDelayTime);
                data.tipDelayTime = null;
            }
        });
    }
});

$.extend($.fn.datagrid.defaults.view, {
    renderRow: function (target, fields, frozen, rowIndex, rowData) {
        var opts = $.data(target, 'datagrid').options;

        var cc = [];
        if (frozen && opts.rownumbers) {
            var rownumber = rowIndex + 1;
            if (opts.pagination) {
                rownumber += (opts.pageNumber - 1) * opts.pageSize;
            }
            cc.push('<td class="datagrid-td-rownumber"><div class="datagrid-cell-rownumber">' + rownumber + '</div></td>');
        }
        for (var i = 0; i < fields.length; i++) {
            var field = fields[i];
            var col = $(target).datagrid('getColumnOption', field);
            if (col) {
                var value = rowData[field]; // the field value
                var css = col.styler ? (col.styler(value, rowData, rowIndex) || '') : '';
                var classValue = '';
                var styleValue = '';
                if (typeof css == 'string') {
                    styleValue = css;
                } else if (cc) {
                    classValue = css['class'] || '';
                    styleValue = css['style'] || '';
                }
                var cls = classValue ? 'class="' + classValue + '"' : '';
                var style = col.hidden ? 'style="display:none;' + styleValue + '"' : (styleValue ? 'style="' + styleValue + '"' : '');

                cc.push('<td field="' + field + '" ' + cls + ' ' + style + '>');

                if (col.checkbox) {
                    var style = '';
                } else {
                    var style = styleValue;
                    if (col.align) { style += ';text-align:' + col.align + ';' }
                    if (!opts.nowrap) {
                        style += ';white-space:normal;height:auto;';
                    } else if (opts.autoRowHeight) {
                        style += ';height:auto;';
                    }
                }

                cc.push('<div style="' + style + '" ');
                cc.push(col.checkbox ? 'class="datagrid-cell-check"' : 'class="datagrid-cell ' + col.cellClass + '"');
                cc.push('>');

                if (col.checkbox) {
                    cc.push('<input type="checkbox" name="' + field + '" value="' + (value != undefined ? value : '') + '">');
                } else if (col.formatter) {
                    cc.push(col.formatter(value, rowData, rowIndex));
                } else {
                    cc.push(value);
                }

                cc.push('</div>');
                cc.push('</td>');
            }
        }
        return cc.join('');
    }
});
$.extend($.fn.datagrid.defaults.editors, {
    combogrid: {
        init: function (container, options) {
            var input = $('<input type="text" class="datagrid-editable-input">').appendTo(container);
            input.combogrid(options);
            return input;
        },
        destroy: function (target) {
            $(target).combogrid('destroy');
        },
        getValue: function (target) {
            return $(target).combogrid('getValue');
        },
        setValue: function (target, value) {
            $(target).combogrid('setValue', value);
        },
        resize: function (target, width) {
            $(target).combogrid('resize', width);
        }
    }
});

$.extend($.fn.tree.methods, {
    getLeafChildren: function (jq, params) {
        var nodes = [];
        $(params).next().children().children("div.tree-node").each(function () {
            nodes.push($(jq[0]).tree('getNode', this));
        });
        return nodes;
    }
});

var oldQuery = $.fn.combogrid.defaults.keyHandler.query;
$.fn.combogrid.defaults.keyHandler.query = function (q) {
    var target = this;
    var opts = $(target).combogrid('options');
    var grid = $(target).combogrid('grid');

    grid.datagrid('options').onBeforeLoad = function (param) {

        if (opts.onBeforeLoad.call(target, param) == false) return false;
        $.extend(param, opts.queryParams);

    }
    oldQuery.call(this, q);
}