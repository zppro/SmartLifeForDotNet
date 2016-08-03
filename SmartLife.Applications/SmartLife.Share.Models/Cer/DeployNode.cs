
#region 模型代码文件说明
/*****************************************************************************
 * $Author: $
 * $Revision: $
 * $Date: 2013-05-09 $
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

namespace SmartLife.Share.Models.Cer
{
    /// <summary>
    /// DeployNode模型
    /// </summary>
    public class DeployNode : BaseModel
    {
        #region 属性
        /// <summary>
        /// 序号
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// 对象编号
        /// </summary>
        public string ObjectId { get; set; }
        /// <summary>
        /// 应用编号从
        /// </summary>
        public string ApplicationIdFrom { get; set; }
        /// <summary>
        /// 应用编号到
        /// </summary>
        public string ApplicationIdTo { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }
        /// <summary>
        /// 数据库连接
        /// </summary>
        public string ConnectString { get; set; }
        /// <summary>
        /// 访问地址
        /// </summary>
        public string AccessPoint { get; set; }
        /// <summary>
        /// 运行模式
        /// </summary>
        public byte? RunMode { get; set; }
        #endregion

        #region 重写方法
        /// <summary>
        /// 获取模型名称
        /// </summary>
        public override string GetMappingModelName()
        {
            return "DeployNode";
        }
        /// <summary>
        /// 获取表名称
        /// </summary>
        public override string GetMappingTableName()
        {
            return "Cer_DeployNode";
        }
        #endregion
    }

    /// <summary>
    /// DeployNode主键
    /// </summary>
    public class DeployNodePK : IPrimaryKeys
    {
        #region 属性
        /// <summary>
        /// 序号
        /// </summary>
        public int? Id { get; set; }
        #endregion
    }
}