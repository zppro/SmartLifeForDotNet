using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

using e0571.web.core.Utils;
using e0571.web.core.Model;
using e0571.web.core.Wcf;
using e0571.web.core.TypeDefine;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using e0571.web.core.DataAccess;

using SmartLife.Share.Behaviors;
using SmartLife.Share.Behaviors.Services;
using SmartLife.Share.Behaviors.Operations;
using SmartLife.Share.Models.Sys;
using SmartLife.Share.Models.Pub;
using SmartLife.Share.Models.Oca;

namespace SmartLife.CertManage.ManageServices.Pub
{
    [ServiceLogging]
    [ServiceValidateSession]
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class SubsetHelperService : BaseWcfService
    {
        [WebInvoke(UriTemplate = "GetHtml_Sets", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        public string GetHtml_Sets(SetSolution param)
        {
            string str_html = string.Empty;
            if (!string.IsNullOrEmpty(param.SolutionId))
            {
                str_html = CreateHtml_Tab_Set(param.SolutionId, UIType.EasyUI);//暂时只有EasyUI,有其他则添加其处理方法并扩展枚举
            }
               
            return str_html;
        }

        #region--私有--
        /*备注
         * 集的容器为div(id="div_sets"),
         * 集自身为div(id="tab_"+SetId),
         */

        #region 集合页签(Tab)
        //生成EasyUI格式
        private string CreateHtml_Tab_EasyUI_Set(string solutionId) 
        {
            StringBuilder str_html = new StringBuilder();
            SetSolution obj_setSoltion = BuilderFactory.DefaultBulder().Load<SetSolution, SetSolutionPK>(new SetSolutionPK { SolutionId = solutionId });
            //排序后的集合
            IList<Set> list_set = BuilderFactory.DefaultBulder().List<Set>(new { SolutionId = solutionId, OrderByClause = "OrderNo" }.ToStringObjectDictionary());

            str_html.Append("<div id=\"div_sets\" class=\"easyui-tabs\" >");
            for(int i=0;i< list_set.Count;i++ )
            {
                string str_widgetOption = string.Empty;
                string str_val= string.Empty;
                string str_select=string.Empty;

                str_select = list_set[i].SetId == obj_setSoltion.DefaultSetId ? "selected=true" : "";//默认选中Tab
                //if (HaveKey("href", item.WidgetOption, out str_val))
                //{
                //    str_widgetOption += " href=\"" + str_val + "\" ";
                //}
                //取键值条件可在此增加
                if (HaveKey("style", list_set[i].WidgetOption, out str_val))
                {
                    str_widgetOption += " style=\"" + str_val + "\" ";
                }
                str_html.AppendFormat("<div id=\"tab_{0}\" title=\"{1}\" {2} {3}>", list_set[i].SetId, list_set[i].SetName, str_select,str_widgetOption);
                str_html.Append(CreateHtml_EasyUI_Subset(list_set[i].SetId));
                str_html.Append("</div>");
            }
            str_html.Append("</div>");
            return str_html.ToString();
        }
        #endregion

        #region 子集
        //生成EasyUI格式
        private string CreateHtml_EasyUI_Subset(string setId)
        {
            StringBuilder str_html = new StringBuilder();
            IList<SubSet> list_set = BuilderFactory.DefaultBulder().List<SubSet>(new { SetId = setId, OrderByClause = "OrderNo" }.ToStringObjectDictionary());
            foreach (var item in list_set)
            {
                string html_tag = CreateHtmlWidget_EasyUI(item.WidgetId);//小部件类型(方法暂未实现)
                str_html.AppendFormat("<{0} id=\"{1}\">", html_tag, setId+"_"+item.SubSetId);
                //列
                str_html.Append(CreateHtml_EasyUI_SubsetColumn(item.SubSetId));
                str_html.AppendFormat("</{0}>", html_tag);
            }
            return str_html.ToString();
        }
        #endregion

        #region 列
        //生成EasyUI格式
        private string CreateHtml_EasyUI_SubsetColumn(string subsetId)
        {
            StringBuilder str_html = new StringBuilder();
            //排序后的自己列
            IList<Column> list_set = BuilderFactory.DefaultBulder().List<Column>(new { SubsetId = subsetId, OrderByClause = "OrderNo" }.ToStringObjectDictionary()); 
            //
          
            return str_html.ToString();
        }
        #endregion

        #region
        #endregion

        #region 封装后的方法
        //暂时只有EasyUI,有其他则添加其处理方法并扩展枚举
        private string CreateHtml_Tab_Set(string solutionId, UIType type)
        {
            if(type==UIType.EasyUI)
                return CreateHtml_Tab_EasyUI_Set(solutionId);
            else
                return CreateHtml_Tab_EasyUI_Set(solutionId);
        }
        #endregion
        /// <summary>
        /// 检查是否存键名,并返回该键值
        /// </summary>
        /// <param name="keyName">键名</param>
        /// <param name="content">json格式的字符串</param>
        /// <param name="val">键值</param>
        /// <returns>存在true,不存在fasle</returns>
        private bool HaveKey(string keyName, string content,out string val)
        {
            bool result = true;
            try 
            {
                var SODic = new StringObjectDictionary().MixInJson(content);
                if (!SODic.ContainsKey(keyName))
                {
                    result = false;
                    val = string.Empty;
                }
                else
                    val = Convert.ToString(SODic[keyName]);
            }
            catch
            {
                result = false;
                val = string.Empty;
            }
            return result;
        }

        /// <summary>
        /// 生成EasyUI控件标签名(不带"<"，在列方法里添加)
        /// </summary>
        /// <param name="dictionaryItemId">字典表对应的控件id</param>
        /// <returns></returns>
        private string CreateHtmlWidget_EasyUI(string dictionaryItemId)
        {
            StringBuilder str_Widget = new StringBuilder();


            return str_Widget.ToString();
        }

        /// <summary>
        /// 查询语句
        /// </summary>
        /// <param name="list_subsetColumn">子集列</param>
        /// <param name="mainTable">主表名</param>
        /// <returns></returns>
        private string GetSubSQL(IList<Column> list_subsetColumn,string mainTable) 
        {
            string str_from = string.Empty;
            StringBuilder sb_sql = new StringBuilder(); 
            IList<TableJoin> list_tableJoin=new List<TableJoin>();
            //取到相关表
            var tables = list_subsetColumn.Select(item=>item.TableName.ToString()).Distinct().ToList();
            if (!tables.Contains(mainTable))
            {
                throw new Exception("主表名异常!");
            }
            if (list_subsetColumn.Count == 1)
            {
                sb_sql.AppendFormat("select {0}.{1} from {0}", list_subsetColumn[0].TableName, list_subsetColumn[0].ColumnName);
            }
            else if (list_subsetColumn.Count > 1)
            {
                //取到表关系
                sb_sql.Append("select distinct TableName1,TableAliasName1,ColumnName1," +
                                "TableName2,TableAliasName2,ColumnName2,JoinType,Condition from Pub_TableJoin where ");
                string[] whereClause = new string[tables.Count()-1]; 
                for (int i = 0; i < tables.Count()-1; i++)
                {
                    for (int y = i+1; y < tables.Count(); y++)
                    {
                        whereClause[i]=string.Format("((TableName1='{0}' and TableName2='{1}') or (TableName1='{1}' and TableName2='{0}'))", tables[i], tables[y]);
                    }
                }
                string str_whereClause = string.Join(" or ", whereClause);
                sb_sql.Append(str_whereClause);
                IList<StringObjectDictionary> list_dic_tablejoin = BuilderFactory.DefaultBulder().ExecuteNativeSqlForQuery(sb_sql.ToString());
                sb_sql.Clear();
                //拼选择的字段SQL语句
                sb_sql.Append("select ");
                IList<string> fields = new List<string>();
                foreach (var item in list_subsetColumn)
                {
                    if (tables.Contains(item.TableName))
                    {
                        fields.Add(string.Format("{0}.{1} ", item.TableName, item.ColumnName));
                    }
                }
                string str_fields = string.Join(",", fields);
                sb_sql.Append(str_fields);
                //拼from表SQL语句
                sb_sql.Append("from ");
                //让主表排第一
                //list_dic_tablejoin
                foreach(var item in list_dic_tablejoin)
                {
                    //主表
                    if (item["TableName1"].ToString() == mainTable)
                    {
                        sb_sql.AppendFormat("{0} {1} {2} on {0}.{3}={2}.{4} {5}", item["TableName1"], item["JoinType"],
                        item["TableName2"], item["ColumnName1"], item["ColumnName2"], item["Condition"]);
                    }
                    else 
                    {
                        sb_sql.AppendFormat("{0} {1} {2} on {0}.{3}={2}.{4} {5}", item["TableName1"], item["JoinType"],
                        item["TableName2"], item["ColumnName1"], item["ColumnName2"], item["Condition"]);
                    }
                }
            }
            return str_from;
        }

        //枚举:控件类型
        private enum UIType 
        { 
            Normal=0,
            EasyUI=1,
        }
        #endregion

    }
}