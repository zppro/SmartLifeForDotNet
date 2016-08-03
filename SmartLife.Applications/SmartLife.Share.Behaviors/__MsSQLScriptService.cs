using System;
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

namespace SmartLife.Share.Behaviors
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class __MsSQLScriptService : AppBaseWcfService
    {
        [WebGet(UriTemplate = "getTableScript/{strTableName},{strPkColumnName},{strPKs}", ResponseFormat = WebMessageFormat.Json)]
        public IList<string> getTableScript(string strTableName, string strPkColumnName, string strPKs)
        {
            IEnumerable<string> pks = null;
            if (strPKs != "none")
            {
                pks = strPKs.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            }
            return _getScript(strTableName, null, strPkColumnName, pks);
        }

        [WebGet(UriTemplate = "getTableScriptWithItem/{strTableName},{strTableItemName},{strPkColumnName},{strPKs}", ResponseFormat = WebMessageFormat.Json)]
        public IList<string> getTableScriptWithItem(string strTableName, string strTableItemName, string strPkColumnName, string strPKs)
        {
            IEnumerable<string> pks = null;
            IEnumerable<string> tableItemNames = null;
            if (strPKs != "none")
            {
                pks = strPKs.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            }
            tableItemNames = strTableItemName.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            return _getScript(strTableName, tableItemNames, strPkColumnName, pks);
        }

        private IList<string> _getScript(string tableName, IEnumerable<string> tableItemNames,string pkColumnName, IEnumerable<string> pks)
        {
            List<string> result = new List<string>();
            string whereClause = "";
            if (pks != null && pks.Count() > 0)
            {
                whereClause = "where " + pkColumnName + " in ('" + string.Join("','", pks.ToArray()) + "')";
            } 
            result.AddRange(BuilderFactory.DefaultBulder().ExecuteSPForQuery<StringObjectDictionary>("SP_Tol_UspOutputDataEx", new { TableName = tableName, WhereClause = whereClause }).Select(item => item["targetscript"].ToString()).ToList());
            if (tableItemNames != null && tableItemNames.Count() > 0)
            {
                foreach (string tableItemName in tableItemNames)
                {
                    result.AddRange(BuilderFactory.DefaultBulder().ExecuteSPForQuery<StringObjectDictionary>("SP_Tol_UspOutputDataEx", new { TableName = tableItemName, WhereClause = whereClause }).Select(item => item["targetscript"].ToString()).ToList());
                }
            }
            return result;

        }
    }
}
