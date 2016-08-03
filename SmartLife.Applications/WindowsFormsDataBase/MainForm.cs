using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsDataBase.App_Code;
using System.IO;

namespace WindowsFormsDataBase
{
    public partial class MainForm : Form
    {
        string dest_DBName_Top = "NotNull";

        #region 窗体初始化加载
        /// <summary>
        /// 绑定源数据库和目标数据库
        /// </summary>
        public void Load()
        {
            DataTable dt_Source = new DataTable();
            string cmdString_Source = "select [DatabaseName] from Cfg_Bridge where IsSourceDb=1";
            dt_Source = SQLServerDBHelper.GetDataBaseName(cmdString_Source);
            if (dt_Source != null)
            {
                cbxSourceDataBases.DataSource = dt_Source;
                cbxSourceDataBases.DisplayMember = "DatabaseName";
                txbResult.AppendText("源数据库绑定成功\r\n\r\n");
            }

            DataTable dt_Target = new DataTable();
            string cmdString_Target = "select [DatabaseName] from Cfg_Bridge where IsSourceDb=0";
            dt_Target = SQLServerDBHelper.GetDataBaseName(cmdString_Target);
            if (dt_Target != null)
            {
                cbxTargetDataBases.DataSource = dt_Target;
                cbxTargetDataBases.DisplayMember = "DatabaseName";
                txbResult.AppendText("目标数据库绑定成功\r\n\r\n");
            }
            DataTable dt_DBName_DML = new DataTable();
            string cmdString_DBName_DML = "select tablename,comment from Cfg_TableNameForLoadData ";
            dt_DBName_DML = SQLServerDBHelper.GetDataBaseName(cmdString_DBName_DML);
            if (dt_DBName_DML != null)
            {
                DataGridViewCheckBoxColumn checkBox = new DataGridViewCheckBoxColumn();
                checkBox.HeaderText = "选择";
                dgv_DataBaseName.Columns.Insert(0, checkBox);
                dgv_DataBaseName.DataSource = dt_DBName_DML;
                dgv_DataBaseName.Columns[0].Width = 40;
            }
        }
        #endregion
        public MainForm()
        {
            InitializeComponent();
            Load();
        }
        #region 源数据库测试连接
        /// <summary>
        /// 源数据库测试连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestSource_Click(object sender, EventArgs e)
        {
            string db_Source = cbxSourceDataBases.Text.Trim();
            if (SQLServerDBHelper.TestResult(db_Source) == true)
            {

                labSource.Text = "连接成功";
                labSource.ForeColor = System.Drawing.Color.Green;
                txbResult.AppendText("数据库“" + db_Source + "”连接测试成功\r\n\r\n");
            }
            else
            {
                labSource.Text = "连接失败";
                labSource.ForeColor = System.Drawing.Color.Red;
                txbResult.AppendText("数据库“" + db_Source + "”连接测试失败\r\n\r\n");
            }
        }
        #endregion
        #region 目标数据库测试连接
        /// <summary>
        /// 目标数据库测试连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestTarget_Click(object sender, EventArgs e)
        {
            string db_Target = cbxTargetDataBases.Text.Trim();
            if (SQLServerDBHelper.TestResult(db_Target) == true)
            {
                labTarget.Text = "连接成功";
                labTarget.ForeColor = System.Drawing.Color.Green;
                txbResult.AppendText("数据库“" + db_Target + "”连接测试成功\r\n\r\n");
            }
            else
            {
                labTarget.Text = "连接失败";
                labTarget.ForeColor = System.Drawing.Color.Red;
                txbResult.AppendText("数据库“" + db_Target + "”连接测试失败\r\n\r\n");
            }
        }
        #endregion
        #region 生成脚本DDL
        private void btnCreate_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string select_Result = "";
            int i;
            bool flog = false;
            string source_DBName_f = cbxSourceDataBases.Text.Trim();//下拉列表选择的是数据库
            string source_DBName = SQLServerDBHelper.IsLinkServer(source_DBName_f);
            //    source_DBName_f.Insert(0, "[");
            //source_DBName = source_DBName.Insert(source_DBName.Length, "]");//给数据库添加“[]”
            string dest_DBName_f;//下拉列表选择的是数据库
            string dest_DBName;
            if (dest_DBName_Top == "NotNull")
            {
                dest_DBName_f = cbxTargetDataBases.Text.Trim();//下拉列表选择的是数据库
                dest_DBName = SQLServerDBHelper.IsLinkServer(dest_DBName_f);
                //    dest_DBName_f.Insert(0, "[");
                //dest_DBName = dest_DBName.Insert(dest_DBName.Length, "]");//给数据库添加“[]”
            }
            else
            {
                dest_DBName_f = "空数据库";
                dest_DBName = "";
            }

            if (labSource.Text == "连接结果")//判断源数据库是否进行测试连通
            {
                txbResult.AppendText("数据库“" + cbxSourceDataBases.Text.Trim() + "”未进行连通测试\r\n\r\n");
                MessageBox.Show("请对“" + cbxSourceDataBases.Text.Trim() + "”进行连通测试");
            }
            else if (labTarget.Text == "连接结果" && dest_DBName_Top == "NotNull")//判断目标数据库是否进行测试连通
            {
                txbResult.AppendText("数据库“" + cbxTargetDataBases.Text.Trim() + "”未进行连通测试\r\n\r\n");
                MessageBox.Show("请对“" + cbxTargetDataBases.Text.Trim() + "”进行连通测试");
            }
            else if (labSource.Text == "连接失败")//源数据库连接失败，提示重新选择数据库
            {
                txbResult.AppendText("源数据库无法连通，请选择正确的源数据库，并进行测试\r\n\r\n");
                MessageBox.Show("源数据库无法连通，请选择正确的源数据库，并进行测试");
            }
            else if (labTarget.Text == "连接失败" && dest_DBName_Top == "NotNull")//目标数据库连接失败，提示重新选择数据库
            {
                txbResult.AppendText("目标数据库无法连通，请选择正确的目标数据库，并进行测试\r\n\r\n");
                MessageBox.Show("目标数据库无法连通，请选择正确的目标数据库，并进行测试");
            }
            else
            {
                if (SQLServerDBHelper.TestResult(cbxSourceDataBases.Text.Trim()) == false)//源数据库选择之后，没有进行测试，并且有不能连通，提示进行连接测试
                {
                    txbResult.AppendText("请点击“测试连接”按钮，确认源数据库" + cbxSourceDataBases.Text.Trim() + "是否能连通\r\n\r\n");
                    MessageBox.Show("请点击“测试连接”按钮，确认源数据库" + cbxSourceDataBases.Text.Trim() + "是否能连通");
                }
                else if (SQLServerDBHelper.TestResult(cbxTargetDataBases.Text.Trim()) == false && dest_DBName_Top == "NotNull")//目标数据库选择之后，没有进行测试，并且有不能连通，提示进行连接测试
                {
                    txbResult.AppendText("请点击“测试连接”按钮，确认源数据库" + cbxTargetDataBases.Text.Trim() + "是否能连通\r\n\r\n");
                    MessageBox.Show("请点击“测试连接”按钮，确认源数据库" + cbxTargetDataBases.Text.Trim() + "是否能连通");
                }
                else
                {
                    flog = true;
                }
            }
            if (flog == true)
            {
                //判断选择的复选框
                //string str = "";
                //string str_Type = "";
                //string str_FileName = "";
                if (cbx_DDL_Table.Checked)
                {
                    //str = "表";
                    i = DDL_Operation_Table(ref dt, ref select_Result, source_DBName, dest_DBName, dest_DBName_f);
                }
                if (cbx_DDL_View.Checked)
                {
                    //str = "视图"; 
                    i = DDL_Operation_View(ref dt, ref select_Result, source_DBName_f, source_DBName, dest_DBName_f, dest_DBName);
                }
                if (cbx_DDL_Procedure.Checked)
                {
                    //str = "存储过程";
                    i = DDL_Operation_Procedure(ref dt, ref select_Result, source_DBName_f, source_DBName, dest_DBName_f, dest_DBName);
                }
                if (cbx_DDL_Function.Checked)
                {
                    //str = "函数";
                    i = DDL_Operation_Function(ref dt, ref select_Result, source_DBName_f, source_DBName, dest_DBName_f, dest_DBName);
                }
                if (cbx_DDL_Type.Checked)
                {
                    //str = "类型"; 
                    i = DDL_Operation_Type(ref dt, ref select_Result, source_DBName_f, source_DBName, dest_DBName, dest_DBName_f);
                }
                if (cbx_DDL_Job.Checked)
                {
                    string getDBInfoToStore_Job = "[SP_Cfg_GetJobMain]";
                    string str_Type = "J";
                    string str_FileName = "Job";
                    string operation_Type = "作业";
                    if (source_DBName.Length > (source_DBName_f.Length + 2))
                    {
                        source_DBName = source_DBName.Substring(0, source_DBName.IndexOf("."));
                    }
                    else source_DBName = "";
                    if (dest_DBName != "")
                    {
                        dest_DBName = dest_DBName.Substring(0,dest_DBName.IndexOf("."));
                    }
                    SQLServerDBHelper.GetDBInfoToStore(source_DBName, dest_DBName, getDBInfoToStore_Job);//获得定义的信息，存入配置库 
                    txbResult.AppendText("作业：取“" + source_DBName_f + "”中存储过程的定义信息，并存入“" + dest_DBName_f + "”的操作成功\r\n\r\n");
                    i = ScriptToStore(ref dt, ref select_Result, str_Type, str_FileName, operation_Type, dest_DBName_f);
                }
            }
        }


        #endregion
        #region DDL视图、存储过程、函数、类型、表的操作
        private int DDL_Operation_Type(ref DataTable dt, ref string select_Result, string source_DBName_f, string source_DBName, string dest_DBName, string dest_DBName_f)
        {
            int i;
            //string getDBInfoToStore_Type = "[]";
            string generateScriptToStore_Type = "[SP_Dey_CreateTypeScriptProc]";
            string str_Type = "Y";
            string str_FileName = "Type";
            string operation_Type = "类型";
            //SQLServerDBHelper.GetDBInfoToStore(source_DBName, dest_DBName, getDBInfoToStore_Type); //获得定义的信息，存入配置库 
            //txbResult.AppendText("类型：取“" + source_DBName_f + "”中类型的定义信息，并存入“" + dest_DBName_f + "”的操作成功\r\n\r\n");
            SQLServerDBHelper.GetDBInfoToStore(source_DBName, dest_DBName, generateScriptToStore_Type);  //生成创建元数据的脚本  
            txbResult.AppendText("类型：生成创建“" + source_DBName_f + "”中类型的元数据的脚本操作成功\r\n\r\n");
            i = ScriptToStore(ref dt, ref select_Result, str_Type, str_FileName, operation_Type, dest_DBName_f);
            return i;
        }
        private int DDL_Operation_Function(ref DataTable dt, ref string select_Result, string source_DBName_f, string source_DBName, string dest_DBName_f, string dest_DBName)
        {
            int i;
            string getDBInfoToStore_Function = "[SP_Cfg_GetFunctionDefinition]";
            string generateScriptToStore_Function = "[SP_Dey_CreateFunctionMetaScript]";
            string str_Type = "F";
            string str_FileName = "Function";
            string operation_Type = "函数";
            SQLServerDBHelper.GetDBInfoToStore(source_DBName, dest_DBName, getDBInfoToStore_Function); //获得定义的信息，存入配置库 
            txbResult.AppendText("函数：取“" + source_DBName_f + "”中函数的定义信息，并存入“" + dest_DBName_f + "”的操作成功\r\n\r\n");
            SQLServerDBHelper.GetDBInfoToStore(source_DBName, dest_DBName, generateScriptToStore_Function); //生成创建元数据的脚本 
            txbResult.AppendText("函数：生成创建“" + source_DBName_f + "”中函数的元数据的脚本操作成功\r\n\r\n");
            i = ScriptToStore(ref dt, ref select_Result, str_Type, str_FileName, operation_Type, dest_DBName_f);
            return i;
        }
        private int DDL_Operation_Procedure(ref DataTable dt, ref string select_Result, string source_DBName_f, string source_DBName, string dest_DBName_f, string dest_DBName)
        {
            int i;
            //存储过程的操作存入数据库
            string getDBInfoToStore_Procedure = "[SP_Cfg_GetProcedureDefinition]";
            string generateScriptToStore_Procedure = "[SP_Dey_CreateProcedureMetaScript]";
            string str_Type = "P";
            string str_FileName = "Procedure";
            string operation_Type = "存储过程";
            SQLServerDBHelper.GetDBInfoToStore(source_DBName, dest_DBName, getDBInfoToStore_Procedure);//获得定义的信息，存入配置库 
            txbResult.AppendText("存储过程：取“" + source_DBName_f + "”中存储过程的定义信息，并存入“" + dest_DBName_f + "”的操作成功\r\n\r\n");
            SQLServerDBHelper.GetDBInfoToStore(source_DBName, dest_DBName, generateScriptToStore_Procedure);  //生成创建元数据的脚本  
            txbResult.AppendText("存储过程：生成创建“" + source_DBName_f + "”中存储过程的元数据的脚本操作成功\r\n\r\n");
            i = ScriptToStore(ref dt, ref select_Result, str_Type, str_FileName, operation_Type, dest_DBName_f);
            return i;
        }
        private int DDL_Operation_View(ref DataTable dt, ref string select_Result, string source_DBName_f, string source_DBName, string dest_DBName_f, string dest_DBName)
        {
            int i;
            string getDBInfoToStore_View = "[SP_Cfg_GetViewDefinition]";
            string generateScriptToStore_View = "[SP_Dey_CreateViewScriptProc]";
            string str_Type = "V";
            string str_FileName = "View";
            string operation_Type = "视图";
            SQLServerDBHelper.GetDBInfoToStore(source_DBName, dest_DBName, getDBInfoToStore_View); //获得定义的信息，存入配置库 
            txbResult.AppendText("视图：取“" + source_DBName_f + "”中视图的定义信息，并存入“" + dest_DBName_f + "”的操作成功\r\n\r\n");
            SQLServerDBHelper.GetDBInfoToStore(generateScriptToStore_View); //生成创建元数据的脚本  
            txbResult.AppendText("视图：生成创建“" + source_DBName_f + "”中视图的元数据的脚本操作成功\r\n\r\n");
            i = ScriptToStore(ref dt, ref select_Result, str_Type, str_FileName, operation_Type, dest_DBName_f);
            return i;
        }
        private int DDL_Operation_Table(ref DataTable dt, ref string select_Result, string source_DBName, string dest_DBName, string dest_DBName_f)
        {
            int i = -1;
            //string getDBInfoToStore_Table = "[]";
            string generateScriptToStore_Table = "[SP_Dey_CreateTableScriptMain]";
            //SQLServerDBHelper.GenerateScriptToStore(source_DBName, dest_DBName, getDBInfoToStore_Table);  //获得定义的信息，存入配置库 
            //txbResult.AppendText("表：取“" + source_DBName_f + "”中表的定义信息，并存入“" + dest_DBName_f + "”的操作成功\r\n\r\n");
            txbResult.AppendText("操作比较多！请稍等，让它跑一会！\r\n\r\n");
            string log_Information = SQLServerDBHelper.GetTableInfoToStore(source_DBName, dest_DBName, generateScriptToStore_Table);//生成创建元数据的脚本  
            string[] log_Information_new = log_Information.Split(new char[] { '@' });
            for (int j = 0; j < log_Information_new.Length - 1; j++)
            {
                txbResult.AppendText("表：" + log_Information_new[j] + "\r\n\r\n");
            }

            //创建SQL文件夹
            string fp = "d:\\SQL\\" + DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月";
            Directory.CreateDirectory(fp);
            DirectoryInfo dir = new DirectoryInfo(fp);
            if (!File.Exists(fp))
            {
                dir.Create();
            }
            string str_Type = "T,M,N,A,S,R,L";
            string str_FileName = "Table";
            select_Result = "";
            string[] str_type_new = str_Type.Split(new char[] { ',' });
            string dateTime = DateTime.Now.Year.ToString()  + DateTime.Now.Month.ToString()  + DateTime.Now.Day.ToString()  + DateTime.Now.Hour.ToString()   + DateTime.Now.Minute.ToString()  + DateTime.Now.Second.ToString();
            string filePath = fp + @"\" + str_FileName + "_" + dest_DBName_f + "_" + dateTime + ".sql";//定义文件名
            for (int k = 0; k < str_type_new.Length; k++)
            {
                //把数据库中的数据存入文件中
                dt.Clear();
                dt = SQLServerDBHelper.GetInfoStoreFile(str_type_new[k]);
                txbResult.AppendText("表：获取数据库中" + str_type_new[k] + "类型的数据成功\r\n\r\n");
                for (i = 0; i < dt.Rows.Count; i++)//取出的数据连成字符串
                {
                    select_Result += dt.Rows[i][0].ToString();
                    txbResult.AppendText("表：获取的" + str_type_new[k] + "类型数据转换成功\r\n\r\n");

                    if (!File.Exists(filePath))
                    {
                        FileStream fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.Read);
                        StreamWriter sw = new StreamWriter(fs);//写入文件中
                        fs.SetLength(0);
                        sw.WriteLine(select_Result);
                        sw.Close();
                    }
                    else
                    {
                        FileStream fst = new FileStream(filePath, FileMode.Open, FileAccess.Write, FileShare.Read);
                        StreamWriter swt = new StreamWriter(fst);
                        fst.SetLength(0);
                        swt.WriteLine(select_Result);
                        swt.Close();
                    }
                } txbResult.AppendText("表：" + str_type_new[k] + "类型的数据存储完成\r\n\r\n");
            }
            txbResult.AppendText("表：文件存储的完整路径：" + filePath + "\r\n\r\n");
            txbResult.AppendText("表：所以数据存储完成\r\n\r\n");
            return i;
        }
        #endregion


        #region 视图、存储过程、函数、类型、导入系统数据操作  把数据存在文件中
        /// <summary>
        /// 取数据库中的数据，写到文件中
        /// </summary>
        /// <param name="dt">查询的数据</param>
        /// <param name="select_Result">转换后的数据</param>
        /// <param name="str_Type">查询的类型</param>
        /// <param name="str_FileName">文件名</param>
        /// <returns></returns>
        private int ScriptToStore(ref DataTable dt, ref string select_Result, string str_Type, string str_FileName, string op_Type, string source_DBName_f)
        {
            int i;
            //创建SQL文件夹
            string fp = "d:\\SQL\\" + DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月";
            select_Result = "";
            Directory.CreateDirectory(fp);
            DirectoryInfo dir = new DirectoryInfo(fp);
            if (!File.Exists(fp))
            {
                dir.Create();
            }

            //把数据库中的数据存入文件中
            dt.Clear();
            dt = SQLServerDBHelper.GetInfoStoreFile(str_Type);
            txbResult.AppendText("" + op_Type + "：获取数据库中的数据成功\r\n\r\n");
            for (i = 0; i < dt.Rows.Count; i++)//取出的数据连成字符串
            {
                select_Result += dt.Rows[i][0].ToString();
            }
            txbResult.AppendText("" + op_Type + "：获取的数据转换成功\r\n\r\n");
            string dateTime = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString()  + DateTime.Now.Minute.ToString()  + DateTime.Now.Second.ToString();
            string filePath = fp + @"\" + str_FileName + "_" + source_DBName_f + "_" + dateTime + ".sql";//定义文件名
            txbResult.AppendText("" + op_Type + "：文件存储的完整路径：" + filePath + "\r\n\r\n");
            FileStream fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.Read);
            StreamWriter sw = new StreamWriter(fs,Encoding.UTF8);//写入文件中
            fs.SetLength(0);
            sw.WriteLine(select_Result);
            sw.Close();
            txbResult.AppendText("" + op_Type + "：数据存储完成\r\n\r\n");
            return i;
        }
        #endregion
        #region 退出系统
        /// <summary>
        /// 关闭程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
        #region 复选框中全选操作
        private void cbx_DDL_All_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_DDL_All.Checked)
            {
                cbx_DDL_Table.Checked = true;
                cbx_DDL_Function.Checked = true;
                cbx_DDL_Procedure.Checked = true;
                cbx_DDL_Type.Checked = true;
                cbx_DDL_View.Checked = true;
                cbx_DDL_Job.Checked = true;
            }
            else
            {
                cbx_DDL_Table.Checked = false;
                cbx_DDL_Function.Checked = false;
                cbx_DDL_Procedure.Checked = false;
                cbx_DDL_Type.Checked = false;
                cbx_DDL_View.Checked = false;
                cbx_DDL_Job.Checked = false;
            }
        }
        #endregion
        #region DML 导入系统数据的操作
        /// <summary>
        /// DML的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreateDML_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string source_DBName_f = cbxSourceDataBases.Text.Trim();//下拉列表选择的是数据库
            string source_DBName = SQLServerDBHelper.IsLinkServer(source_DBName_f);

            string dest_DBName_f;
            string dest_DBName;
            if (dest_DBName_Top == "NotNull")
            {
                dest_DBName_f = cbxTargetDataBases.Text.Trim();//下拉列表选择的是数据库
                dest_DBName = SQLServerDBHelper.IsLinkServer(dest_DBName_f);
            }
            else
            {
                dest_DBName_f = "空数据库";
                dest_DBName = "";
            }
            string generateScriptToStore_InsertData = "[SP_Dey_InsertDataScriptForTable]";
            string operation_Type = "导入系统数据";
            string fp = "d:\\SQL\\" + DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月";
            string str_Type = "D";
            string str_FileName = "InsertData";
            string select_Result = "";
            bool flog = false;
            int select_Count = 0;
            Directory.CreateDirectory(fp);
            DirectoryInfo dir = new DirectoryInfo(fp);
            if (!File.Exists(fp))
            {
                dir.Create();
            }
            if (labSource.Text == "连接结果")//判断源数据库是否进行测试连通
            {
                txbResult.AppendText("数据库“" + cbxSourceDataBases.Text.Trim() + "”未进行连通测试\r\n\r\n");
                MessageBox.Show("请对“" + cbxSourceDataBases.Text.Trim() + "”进行连通测试");
            }
            else if (labTarget.Text == "连接结果" && dest_DBName_Top == "NotNull")//判断目标数据库是否进行测试连通
            {
                txbResult.AppendText("数据库“" + cbxTargetDataBases.Text.Trim() + "”未进行连通测试\r\n\r\n");
                MessageBox.Show("请对“" + cbxTargetDataBases.Text.Trim() + "”进行连通测试");
            }
            else if (labSource.Text == "连接失败")//源数据库连接失败，提示重新选择数据库
            {
                txbResult.AppendText("源数据库无法连通，请选择正确的源数据库，并进行测试\r\n\r\n");
                MessageBox.Show("源数据库无法连通，请选择正确的源数据库，并进行测试");
            }
            else if (labTarget.Text == "连接失败" && dest_DBName_Top == "NotNull")//目标数据库连接失败，提示重新选择数据库
            {
                txbResult.AppendText("目标数据库无法连通，请选择正确的目标数据库，并进行测试\r\n\r\n");
                MessageBox.Show("目标数据库无法连通，请选择正确的目标数据库，并进行测试");
            }
            else
            {
                if (SQLServerDBHelper.TestResult(cbxSourceDataBases.Text.Trim()) == false)//源数据库选择之后，没有进行测试，并且有不能连通，提示进行连接测试
                {
                    txbResult.AppendText("请点击“测试连接”按钮，确认源数据库" + cbxSourceDataBases.Text.Trim() + "是否能连通\r\n\r\n");
                    MessageBox.Show("请点击“测试连接”按钮，确认源数据库" + cbxSourceDataBases.Text.Trim() + "是否能连通");
                }
                else if (SQLServerDBHelper.TestResult(cbxTargetDataBases.Text.Trim()) == false && dest_DBName_Top == "NotNull")//目标数据库选择之后，没有进行测试，并且有不能连通，提示进行连接测试
                {
                    txbResult.AppendText("请点击“测试连接”按钮，确认源数据库" + cbxTargetDataBases.Text.Trim() + "是否能连通\r\n\r\n");
                    MessageBox.Show("请点击“测试连接”按钮，确认源数据库" + cbxTargetDataBases.Text.Trim() + "是否能连通");
                }
                else
                {
                    flog = true;
                }
            }

            if (flog == true)
            {
                string tableName = "";
                for (int t = 0; t < dgv_DataBaseName.Rows.Count; t++)
                {
                    //int count = 0;
                    //if (dgv_DataBaseName.Rows[t].Cells[0].EditedFormattedValue.ToString() == "true")
                    //{ count++; }
                    //if (count == 0)
                    //{
                    //    MessageBox.Show("请至少选择一条数据！", "提示");
                    //    return;
                    //}
                    //else
                    //{
                    //    string tableName = dgv_DataBaseName.Rows[t].Cells["TableName"].Value.ToString().Trim();
                    //    SQLServerDBHelper.GetDBInfoToStore(source_DBName, dest_DBName, tableName, generateScriptToStore_InsertData);  //生成创建元数据的脚本  
                    //    txbResult.AppendText("导入系统数据：“" + source_DBName_f + "”中" + tableName + "表的脚本操作成功\r\n\r\n");
                    //}
                    DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgv_DataBaseName.Rows[t].Cells[0];
                    bool flag = Convert.ToBoolean(checkCell.Value);
                    if (flag == true)
                    {
                        tableName += "'" + dgv_DataBaseName.Rows[t].Cells["TableName"].Value.ToString().Trim() + "',";
                        txbResult.AppendText("导入系统数据：“" + source_DBName_f + "”中" + tableName + "表的脚本获取成功\r\n\r\n");
                        select_Count++;
                    }
                }
                tableName = tableName.Remove(tableName.Length - 1, 1);
                SQLServerDBHelper.GetDBInfoToStore(source_DBName, dest_DBName, tableName, generateScriptToStore_InsertData);  //生成创建元数据的脚本 
                txbResult.AppendText("导入系统数据：“" + source_DBName_f + "”中" + tableName + "表的脚本操作成功\r\n\r\n");
                if (select_Count != 0)
                {
                    dt.Clear();
                    dt = SQLServerDBHelper.GetInfoStoreFile(str_Type);
                    txbResult.AppendText("" + operation_Type + "：获取数据库中的数据成功\r\n\r\n");
                    for (int i = 0; i < dt.Rows.Count; i++)//取出的数据连成字符串
                    {
                        select_Result += dt.Rows[i][0].ToString();
                    }
                    txbResult.AppendText("" + operation_Type + "：获取的数据转换成功\r\n\r\n");
                    string dateTime = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString()  + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString()  + DateTime.Now.Minute.ToString()  + DateTime.Now.Second.ToString();
                    string filePath = fp + @"\" + str_FileName + "_" + source_DBName_f + "_" + dateTime + ".sql";//定义文件名
                    LinkLabel link=new LinkLabel();
                    link.Text=filePath;
                    link.Cursor = Cursors.Hand;
                    link.LinkBehavior = LinkBehavior.HoverUnderline;
                    link.Parent = this.txbResult;
                    this.txbResult.Controls.Add(link);
                    txbResult.AppendText("" + operation_Type + "：文件存储的完整路径：" + filePath + "\r\n\r\n");
                    
                    FileStream fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write, FileShare.Read);
                    StreamWriter sw = new StreamWriter(fs);//写入文件中
                    fs.SetLength(0);
                    sw.WriteLine(select_Result);
                    sw.Close();
                    txbResult.AppendText("" + operation_Type + "：数据存储完成\r\n\r\n");
                }
                else { MessageBox.Show("至少选择一个表"); }
            }
        }
        #endregion

        private void cbxIsNull_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxIsNull.Checked == true)
            {
                dest_DBName_Top = "Null";
                labTarget.Text = "不用测试连接";
                labTarget.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                dest_DBName_Top = "NotNull";
                labTarget.Text = "连接结果";
                labTarget.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe",@"d:\SQL\" + DateTime.Now.Year.ToString() + "年" + DateTime.Now.Month.ToString() + "月");
        }
       

    }
}
