using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsDataBase.App_Code
{
    class SQLServerDBHelper
    {
        #region 创建连接字符串
        public static string connStr = "Data Source = 115.236.175.109,10016;Network Library = DBMSSOCN;Initial Catalog = Leblue-Configuration;User ID = dbo;Password = 109,dbo@2015;";
        public static SqlConnection conn = new SqlConnection(connStr);
        //public static string connStr1 = "Data Source = 192.168.1.109,1433;Network Library = DBMSSOCN;Initial Catalog = Leblue-Configuration;User ID = dbo;Password = leblue@2014;";
        //public static SqlConnection conn1 = new SqlConnection(connStr1);
        //public static string UserName;
        //public static string PassWord;
        //public static SqlConnection conn_User_PWD = new SqlConnection(connStr_User_PWD);
        #endregion

        #region 查询数据库名称
        /// <summary>
        /// 查询数据库名称
        /// </summary>
        /// <param name="sqlStr">连接字符串</param>
        /// <returns>返回的是DataTable</returns>
        public static DataTable GetDataBaseName(string sqlStr)
        {
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataAdapter adpt = new SqlDataAdapter(sqlStr, conn);
            dt.Clear();
            adpt.Fill(dt);
            conn.Close();
            if (dt.Rows.Count != 0)
            { return dt; }
            else return null;
        }
        #endregion
        #region 测试连接数据库
        /// <summary>
        ///  测试连接数据库
        /// </summary>
        /// <param name="connStr_test">数据库名称</param>
        /// <returns>返回值是bool型</returns>
        public static bool TestResult(string connStr_test)
        {
            DataTable dt = new DataTable();
            //从数据库中获取选中数据库对应的IP和端口            
            string cmdIP = "select * from Cfg_Bridge where [DatabaseName]= '" + connStr_test + "'";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            SqlDataAdapter adpt_IpPort = new SqlDataAdapter(cmdIP, connStr);
            dt.Clear();
            adpt_IpPort.Fill(dt);
            conn.Close();
            DataRow dr = dt.Rows[0];
            string ip = dr["NodeIp"].ToString();
            string port = dr["Port"].ToString();
            string user = dr["UserName"].ToString();
            string Pass = dr["Pass"].ToString();

            string cmd_User_PWD = "select dbo.Func_Tol_PassUn('" + user + "','" + Pass + "')";
            SqlConnection conn_User_PWD = new SqlConnection(connStr);
            conn_User_PWD.Open();
            //SqlDataAdapter adpt_User_PWD = new SqlDataAdapter(cmd_User_PWD, connStr);
            SqlCommand cmd = new SqlCommand(cmd_User_PWD, conn_User_PWD);
            string Pass_User_PWD=cmd.ExecuteScalar().ToString();
            //dt.Clear();
            //adpt_User_PWD.Fill(dt);
            conn_User_PWD.Close();
            //DataRow dr_User_PWD = dt.Rows[0];
            //string Pass_User_PWD = dr_User_PWD["Pass"].ToString();
            //string Pass_User_PWD = dt.Rows[0][0].ToString();


            //通过IP、端口和数据库名，查询数据库中的系统表，查到结果返回true
            string connStrTest = "Data Source = " + ip + "," + port + ";Network Library = DBMSSOCN;Initial Catalog = '" + connStr_test + "';User ID = " + user + ";Password = " + Pass_User_PWD + ";";
            string cmdText = "select * from sys.tables";
            SqlConnection conn_test = new SqlConnection(connStrTest);
            try
            {
                conn_test.Open();
                SqlDataAdapter adpt = new SqlDataAdapter(cmdText, conn_test);
                dt.Clear();
                adpt.Fill(dt);
                conn_test.Close();
                if (dt != null) return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        #region 判断是否是远程连接
        /// <summary>
        /// 判断是否是远程连接
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <returns>处理后的数据库名称</returns>
        public static string IsLinkServer(string dbName)
        {
            DataTable dt = new DataTable();
            //从数据库中获取选中数据库对应的IP和端口
            string connStrIP = "Data Source = 115.236.175.109,10016;Network Library = DBMSSOCN;Initial Catalog = Leblue-Configuration;User ID = dbo;Password = 109,dbo@2015;";
            string cmdIP = "select * from Cfg_Bridge where [DatabaseName]= '" + dbName + "'";
            SqlConnection conn_Ip = new SqlConnection(connStrIP);
            conn_Ip.Open();
            SqlDataAdapter adpt_IpPortLink = new SqlDataAdapter(cmdIP, connStrIP);
            dt.Clear();
            adpt_IpPortLink.Fill(dt);
            conn_Ip.Close();
            DataRow dr = dt.Rows[0];
            string ip = dr["NodeIp"].ToString().Trim();
            string linkServer = dr["LinkServerName"].ToString().Trim();
            if (ip != "115.236.175.109 ")
            {
                return linkServer + "." + "[" + dbName + "]";
            }
            return "[" + dbName + "]";
        }
        #endregion
        #region 生成脚本：视图、存储过程、函数、类型、作业
        /// <summary>
        /// 生成脚本：视图、存储过程、函数、类型
        /// </summary>
        /// <param name="source_dbName">源数据库名</param>
        /// <param name="dest_dbName">目标数据库名</param>
        /// <param name="getDBInfoToStore">存储过程名</param>
        /// <returns>返回bool</returns>
        public static string GetDBInfoToStore(string source_dbName, string dest_dbName, string getDBInfoToStore)
        {
            conn.Open();
            SqlCommand cmd_StoredProcedure = new SqlCommand(getDBInfoToStore, conn);
            cmd_StoredProcedure.CommandType = CommandType.StoredProcedure;
            cmd_StoredProcedure.Parameters.Add(new SqlParameter("@source_dbName", SqlDbType.NVarChar, 100));
            cmd_StoredProcedure.Parameters["@source_dbName"].Value = source_dbName;
            cmd_StoredProcedure.Parameters.Add(new SqlParameter("@dest_dbName", SqlDbType.NVarChar, 100));
            cmd_StoredProcedure.Parameters["@dest_dbName"].Value = dest_dbName;
            cmd_StoredProcedure.ExecuteNonQuery();
            conn.Close();
            return "操作成功";

        }
        #endregion
        #region 生成脚本：视图
        /// <summary>
        /// 生成脚本：视图
        /// </summary>
        /// <param name="getDBInfoToStore">存储过程名</param>
        /// <returns>返回bool</returns>
        public static string GetDBInfoToStore(string generateScriptToStore)
        {
            conn.Open();
            SqlCommand cmd_StoredScript = new SqlCommand(generateScriptToStore, conn);
            cmd_StoredScript.CommandType = CommandType.StoredProcedure;
            cmd_StoredScript.ExecuteNonQuery();
            conn.Close();
            return "操作成功";
        }
        #endregion
        #region 生成脚本：表
        /// <summary>
        /// 生成脚本：表
        /// </summary>
        /// <param name="source_dbName">源数据库名</param>
        /// <param name="dest_dbName">目标数据库名</param>
        /// <param name="getDBInfoToStore">存储过程名</param>
        /// <returns>返回bool</returns>
        public static string GetTableInfoToStore(string source_dbName, string dest_dbName, string generateScriptToStore)
        {
            try
            {
                conn.Open();
                SqlCommand cmd_StoredScript = new SqlCommand(generateScriptToStore, conn);
                cmd_StoredScript.CommandTimeout = 180;
                cmd_StoredScript.CommandType = CommandType.StoredProcedure;
                cmd_StoredScript.Parameters.Add(new SqlParameter("@source_dbName", SqlDbType.NVarChar, 100));
                cmd_StoredScript.Parameters["@source_dbName"].Value = source_dbName;
                cmd_StoredScript.Parameters.Add(new SqlParameter("@dest_dbName", SqlDbType.NVarChar, 100));
                cmd_StoredScript.Parameters["@dest_dbName"].Value = dest_dbName;
                cmd_StoredScript.Parameters.Add(new SqlParameter("@log_Information", SqlDbType.NVarChar, 1000));
                cmd_StoredScript.Parameters["@log_Information"].Direction = ParameterDirection.Output;
                cmd_StoredScript.ExecuteNonQuery();
                return cmd_StoredScript.Parameters["@log_Information"].Value.ToString();
            }
            catch { return "超时，请重新尝试@"; }
            finally { conn.Close(); }
        }
        #endregion
        #region 生成脚本：有表名参数的
        /// <summary>
        /// 生成脚本：有表名参数的
        /// </summary>
        /// <param name="source_dbName">源数据库名</param>
        /// <param name="dest_dbName">目标数据库名</param>
        /// <param name="tableName">表名</param>
        /// <returns>返回bool</returns>
        public static string GetDBInfoToStore(string source_dbName, string dest_dbName, string tableName, string getDBInfoToStore)
        {
            conn.Open();
            SqlCommand cmd_StoredProcedure = new SqlCommand(getDBInfoToStore, conn);
            cmd_StoredProcedure.CommandType = CommandType.StoredProcedure;
            cmd_StoredProcedure.Parameters.Add(new SqlParameter("@source_dbName", SqlDbType.NVarChar, 100));
            cmd_StoredProcedure.Parameters["@source_dbName"].Value = source_dbName;
            cmd_StoredProcedure.Parameters.Add(new SqlParameter("@dest_dbName", SqlDbType.NVarChar, 100));
            cmd_StoredProcedure.Parameters["@dest_dbName"].Value = dest_dbName;
            cmd_StoredProcedure.Parameters.Add(new SqlParameter("@TableName", SqlDbType.NVarChar, 4000));
            cmd_StoredProcedure.Parameters["@TableName"].Value = tableName;
            cmd_StoredProcedure.ExecuteNonQuery();
            conn.Close();
            return "操作成功";

        }
        #endregion
        #region 从表中取出生成的数据
        public static DataTable GetInfoStoreFile(string selectType)
        {
            DataTable dt = new DataTable();
            string cmdtext = "select UpdateScript from Dey_Script where Type='" + selectType + "'";
            conn.Open();
            SqlDataAdapter adpt = new SqlDataAdapter(cmdtext, conn);
            dt.Clear();
            adpt.Fill(dt);
            conn.Close();
            if (dt != null)
            { return dt; }
            else return null;

        }
        #endregion

    }
}
