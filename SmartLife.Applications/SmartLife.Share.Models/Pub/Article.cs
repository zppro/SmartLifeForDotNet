
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
    /// Article模型
    /// </summary>
    public class Article : BaseModel
    {
        #region 属性
        /// <summary>
        /// 文章序号 
        /// </summary>
        public Guid? ArticleId { get; set; }
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
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 副标题
        /// </summary>
        public string SubTitle { get; set; }
        /// <summary>
        /// 所属栏目
        /// </summary>
        public Guid? ColumnId { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime? PublishDate { get; set; }
        /// <summary>
        /// 过期日期
        /// </summary>
        public DateTime? ExpiredDate { get; set; }
        /// <summary>
        /// 关键词
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public Guid? CreatorId { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        public string Abstract { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 附件数量
        /// </summary>
        public int? Attachments { get; set; }
        /// <summary>
        /// 附件预览方式
        /// </summary>
        public byte? PreviewMode { get; set; }
        /// <summary>
        /// 内容类别
        /// </summary>
        public byte? Category { get; set; }
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
        /// 显示方式
        /// </summary>
        public byte? ShowMode { get; set; }
        /// <summary>
        /// 首页显示判别
        /// </summary>
        public byte? HomeShowFlag { get; set; }
        /// <summary>
        /// 图片新闻判别
        /// </summary>
        public byte? PicFlag { get; set; }
        /// <summary>
        /// 视频新闻判别
        /// </summary>
        public byte? VideoFlag { get; set; }
        /// <summary>
        /// 新闻动态判别
        /// </summary>
        public byte? NewsFlag { get; set; }
        /// <summary>
        /// 匿名评论判别
        /// </summary>
        public byte? AnonCommentsFlag { get; set; }
        /// <summary>
        /// 投票判别
        /// </summary>
        public byte? VoteFlag { get; set; }
        /// <summary>
        /// 匿名投票判别
        /// </summary>
        public byte? Anonymous_Vote_FLAG { get; set; }
        /// <summary>
        /// 置顶标记
        /// </summary>
        public byte? TopFlag { get; set; }
        /// <summary>
        /// 置顶天数
        /// </summary>
        public int? TopDays { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "Article";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Pub_Article";
        }
        #endregion
    }

    /// <summary>
    /// Article主键
    /// </summary>
    public class ArticlePK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 文章序号 
        /// </summary>
        public Guid? ArticleId { get; set; }
        #endregion
    }
}
