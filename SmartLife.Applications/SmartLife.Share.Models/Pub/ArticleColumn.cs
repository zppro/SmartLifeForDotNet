
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2014-07-09 $
 * 
 * iBATIS.NET For DotNet4.0 Models
 * Copyright (C) 2009 - CreateCode v0.5
 * 
 * $Remark: $
 * 
 * 
 * 
 ********************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using e0571.web.core.Model;

namespace SmartLife.Share.Models.Pub
{
    /// <summary>
    /// ArticleColumn模型
    /// </summary>
    public class ArticleColumn : BaseModel
    {
        #region 属性
        /// <summary>
        /// 栏目ID
        /// </summary>
        public Guid? ColumnId { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public byte? Status { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public Guid? OperatedBy { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? OperatedOn { get; set; }
        /// <summary>
        /// 栏目代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string COL_Name { get; set; }
        /// <summary>
        /// 栏目所属类型
        /// </summary>
        public string COL_Genre { get; set; }
        /// <summary>
        /// 父类栏目ID
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 地区ID
        /// </summary>
        public string AreaId { get; set; }
        /// <summary>
        /// 街道ID
        /// </summary>
        public string AreaId2 { get; set; }
        /// <summary>
        /// 社区ID
        /// </summary>
        public string AreaId3 { get; set; }
        /// <summary>
        /// 栏目描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 排序序号
        /// </summary>
        public int? OrderNo { get; set; }
        /// <summary>
        /// 是否需要登录
        /// </summary>
        public byte? LoginFlag { get; set; }
        /// <summary>
        /// 栏目展示图片
        /// </summary>
        public string COL_Logo { get; set; }
        /// <summary>
        /// 栏目打开方式
        /// </summary>
        public string OpenMode { get; set; }
        /// <summary>
        /// 展现方式
        /// </summary>
        public string ShowMode { get; set; }
        /// <summary>
        /// 描述显示判别
        /// </summary>
        public byte? DES_ShowFlag { get; set; }
        /// <summary>
        /// 拼音码
        /// </summary>
        public string InputCode1 { get; set; }
        /// <summary>
        /// 五笔码
        /// </summary>
        public string InputCode2 { get; set; }
        /// <summary>
        /// 角形码
        /// </summary>
        public string InputCode3 { get; set; }
        /// <summary>
        /// 自定码
        /// </summary>
        public string InputCode4 { get; set; }
        /// <summary>
        /// 栏目路径
        /// </summary>
        public string COL_Path { get; set; }
        /// <summary>
        /// 栏目链接
        /// </summary>
        public string COL_URL { get; set; }
        /// <summary>
        /// 菜单判别
        /// </summary>
        public byte? Menu_Flag { get; set; }
        /// <summary>
        /// 站点类型
        /// </summary>
        public byte? SiteType { get; set; }
        /// <summary>
        /// 统计类型
        /// </summary>
        public byte? StatistType { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "ArticleColumn";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_ArticleColumn";
        }
        #endregion
    }

    /// <summary>
    /// ArticleColumn主键
    /// </summary>
    public class ArticleColumnPK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 栏目ID
        /// </summary>
        public Guid? ColumnId { get; set; }
        #endregion
    }
}
