using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using win.core.utils;
using e0571.web.core.Wcf;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using e0571.web.core.Utils;
using System.Web.Script.Serialization;
using System.Dynamic;
using System.Xml.Linq;

namespace SmartLife.Client.Gov
{
    public partial class formMain : Form
    {
        //public string AccessPoint { get; set; }
        const int CLOSE_SIZE = 8;
        List<ResidentBaseInfo> residentBaseInfos;
        public formMain()
        {
            InitializeComponent();
            this.tsslUserName.Text = "用户名：" + GovVar.UserName;
            this.WindowState = FormWindowState.Maximized;
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            //creatTreeData();
            //getData();


            //if (residentBaseInfos == null)
            //{
            //    residentBaseInfos = new List<ResidentBaseInfo>();
            //}
            //else
            //{
            //    residentBaseInfos.Clear();
            //}
            //StringObjectDictionary param = new { fuzzyFieldOP = "OR", order = "desc", sort = "OperatedOn", page = 1, rows = 18 }.ToStringObjectDictionary();

            //HttpAdapter.postSyncAsJSON(GovVar.DataPointOfManage + "/Bas/ResidentBaseInfoService/ResidentBaseInfo4/SmartLife1203", param,
            //    (ret, res) =>
            //    {
            //        if ((bool)ret.Success)
            //        {
            //            foreach (var row in ret.rows)
            //            {
            //                dynamic item = new ExpandoObject();
            //                ResidentBaseInfo dataItem = new ResidentBaseInfo();
            //                dataItem.ResidentId = row.ResidentId;
            //                dataItem.Id = row.Id;
            //                dataItem.CheckInTime = row.CheckInTime;
            //                dataItem.Status = row.Status;
            //                dataItem.OperatedBy = row.OperatedBy;
            //                dataItem.OperatedOn = row.OperatedOn;
            //                dataItem.DataSource = row.DataSource;
            //                dataItem.ResidentName = row.ResidentName;
            //                dataItem.ResidentStatus = row.ResidentStatus;
            //                dataItem.ResidentBizId = row.ResidentBizId;
            //                dataItem.IDNo = row.IDNo;
            //                dataItem.Gender = row.Gender;
            //                dataItem.NativePlace = row.NativePlace;
            //                dataItem.HouseholdRegister = row.HouseholdRegister;
            //                dataItem.EducationLevel = row.EducationLevel;
            //                dataItem.Marriage = row.Marriage;
            //                dataItem.LivingStatus = row.LivingStatus;
            //                dataItem.HousingStatus = row.HousingStatus;
            //                dataItem.IncomeStatus = row.IncomeStatus;
            //                dataItem.AreaId = row.AreaId;
            //                dataItem.AreaId2 = row.AreaId2;
            //                dataItem.AreaId3 = row.AreaId3;
            //                dataItem.ResidentialAddress = row.ResidentialAddress;
            //                dataItem.PlaceOfHouseholdRegister = row.PlaceOfHouseholdRegister;
            //                dataItem.PostCode = row.PostCode;
            //                dataItem.Tel = row.Tel;
            //                dataItem.Mobile = row.Mobile;
            //                dataItem.Remark = row.Remark;
            //                dataItem.InputCode1 = row.InputCode1;
            //                dataItem.InputCode2 = row.InputCode2;
            //                dataItem.ResidentialOfHometown = row.ResidentialOfHometown;
            //                dataItem.Nation = row.Nation;
            //                dataItem.AccountType = row.AccountType;
            //                dataItem.EventTime = row.EventTime;
            //                dataItem.StationId = row.StationId;

            //                //DynamicAdapter.Parse(item, XElement.Parse(row.ToString()));
            //                //ResidentBaseInfo dataItem = (item.StringObjectDictionary as IDictionary<string, object>).FromDynamic<ResidentBaseInfo>();
            //                residentBaseInfos.Add(dataItem);
            //            }
            //        }
            //    });


            //DataGridView dgv = new DataGridView();

            //TabPage thistPage = new TabPage();
            //thistPage = this.xTabs.SelectedTab;
            //Control c = thistPage.Controls.Find("dgvBaseinfo", true)[0];

            //dgv.Name = c.Name;
            //dataGridView1.DataSource = residentBaseInfos;
            //bdsResidentBaseInfo.DataSource = residentBaseInfos;
            //DataTable dt = new DataTable(); 

            //HttpAdapter.getSyncTo(GovVar.DataPointOfManage + "/Bas/ResidentBaseInfoService", null, new { ApplicationId = Common.APPLICATION_ID }.ToStringObjectDictionary(), (ret, res) =>
            //{
            //    if ((bool)ret.Success)
            //    {
            //    }
            //    else
            //    {
            //    }
            //});
            //HttpAdapter.getSyncTo(GovVar.DataPointOfManage + "/Oca/OldManBaseInfoService", null, new { ApplicationId = Common.APPLICATION_ID }.ToStringObjectDictionary(),
            //    (ret, res) =>
            //{
            //    bool succ = ret.Success;
            //    if (succ)
            //    {
            //        int a = 1; 
            //    } 
            //});
        } 
         
        #region 添加选项卡的关闭功能
        private void xTabs_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                Rectangle myTabRect = this.xTabs.GetTabRect(e.Index);

                //先添加TabPage属性   
                e.Graphics.DrawString(this.xTabs.TabPages[e.Index].Text
                , this.Font, SystemBrushes.ControlText, myTabRect.X + 2, myTabRect.Y + 2);
                //再画一个矩形框
                using (Pen p = new Pen(Color.Black))//自动释放资源
                {
                    myTabRect.Offset(myTabRect.Width - (CLOSE_SIZE + 3), 2);
                    myTabRect.Width = CLOSE_SIZE;
                    myTabRect.Height = CLOSE_SIZE;
                    e.Graphics.DrawRectangle(p, myTabRect);
                }
                //画关闭符号
                using (Pen objpen = new Pen(Color.Black))
                {
                    //"/"线
                    Point p1 = new Point(myTabRect.X + 3, myTabRect.Y + 3);
                    Point p2 = new Point(myTabRect.X + myTabRect.Width - 3, myTabRect.Y + myTabRect.Height - 3);
                    e.Graphics.DrawLine(objpen, p1, p2);
                    //"/"线
                    Point p3 = new Point(myTabRect.X + 3, myTabRect.Y + myTabRect.Height - 3);
                    Point p4 = new Point(myTabRect.X + myTabRect.Width - 3, myTabRect.Y + 3);
                    e.Graphics.DrawLine(objpen, p3, p4);
                }
                e.Graphics.Dispose();
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region 关闭选项卡
        private void xTabs_MouseDown(object sender, MouseEventArgs e)
        {
             if (e.Button == MouseButtons.Left)
            {
                int x = e.X, y = e.Y; 
                //计算关闭区域   
                Rectangle myTabRect = this.xTabs.GetTabRect(this.xTabs.SelectedIndex);
                myTabRect.Offset(myTabRect.Width - (CLOSE_SIZE + 3), 2);
                myTabRect.Width = CLOSE_SIZE;
                myTabRect.Height = CLOSE_SIZE;
                //如果鼠标在区域内就关闭选项卡   
                bool isClose = x > myTabRect.X && x < myTabRect.Right
                 && y > myTabRect.Y && y < myTabRect.Bottom;
                if (isClose == true)
                {
                    int tabIndex = this.xTabs.SelectedIndex - 1;
                    this.xTabs.TabPages.Remove(this.xTabs.SelectedTab);
                    if (tabIndex > 0)
                    {
                        xTabs.SelectedIndex = tabIndex;
                    }

                }
            }
        }
        #endregion

        #region 新建选项卡
        private void CreatTabPage(String tabName,String tabText) { 
            bool isExist = false; 
             foreach (TabPage page in xTabs.TabPages)
            {
                 if(tabName==page.Name)
                 {
                     isExist = true; 
                     this.xTabs.SelectedTab = page;
                     break;
                 } 
            }
             if (!isExist)
             {
                 TabPage myTabPage = new TabPage();
                 myTabPage.Name = tabName;
                 myTabPage.Text = tabText;
                 xTabs.TabPages.Add(myTabPage);
                 xTabs.SelectedTab = myTabPage;
             } 
        }
        #endregion

        private void tsmiBaseinfo_Click(object sender, EventArgs e)
        {
            CreatTabPage("tpBaseinfo", "基本信息");
            //Fill_TabPage();
            //creatTreeData();


            userCtlResidentBaseInfo uCtlResidentBaseInfo = new userCtlResidentBaseInfo();
            uCtlResidentBaseInfo.Dock = DockStyle.Fill; 

            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill; 
            panel.Controls.Add(uCtlResidentBaseInfo);
            //formb.Parent = p;

            TabPage thistPage = new TabPage();
            thistPage = this.xTabs.SelectedTab;
            thistPage.Controls.Add(panel); 

            //HttpAdapter.getSyncTo(GovVar.DataPointOfManage + "/Oca/OldManBaseInfoService",
            //   (ret, res) =>
            //   {
            //       bool succ = true;
            //       if (succ)
            //       {
            //           int a = 1;
            //       }
            //   });
            //HttpAdapter.getSyncTo(GovVar.DataPointOfManage + "/Oca/OldManBaseInfoService/5DF32BCB-9E8A-4354-9139-001856265FE8",
            //   (ret, res) =>
            //   {
            //       bool succ = true;
            //       if (succ)
            //       {
            //           int a = 1;
            //       }
            //   }); 
        }

        #region 建立街道社区树
        private void creatTreeData() {
            String url = GovVar.DataPointOfManage + "/Share/TreeDataService/fetchTreeData";
            StringObjectDictionary _treeParam = new { TreeCode = "01$01$02", OrderByClause = "OrderNo asc", TreeParams = "{\"DictionaryId\":\"00005\" , \"ItemId\":\"99999\"}" }.ToStringObjectDictionary();
            HttpAdapter.postSyncAsJSON(url, _treeParam, (ret, res) =>
            {
                TreeNode treeNode = new TreeNode();
                foreach (var row in ret)
                {
                    if (row.pId.Value == "_")
                    {
                        string areaId = row.id.Value.ToString();
                        //辖区绑定，作为一级层次
                        treeNode.Text = row.name.Value.ToString();
                        treeNode.Name = areaId;

                        //街道绑定 ,作为二级层次
                        foreach (var row_s in ret)
                            {
                                if (row_s.pId.Value.ToString() == areaId)
                                {
                                    string streetId = row_s.id.Value.ToString(); 
                                    TreeNode tn_s = new TreeNode();
                                    tn_s.Text = row_s.name.Value.ToString();
                                    tn_s.Name = streetId;
                                    treeNode.Nodes.Add(tn_s);

                                    //社区绑定 ,作为三级层次
                                    foreach (var row_c in ret)
                                    {
                                        if (row_c.pId.Value.ToString() == streetId)
                                        {
                                            TreeNode tn_c = new TreeNode();
                                            tn_c.Text = row_c.name.Value.ToString();
                                            tn_c.Name = row_c.id.Value.ToString();
                                            tn_s.Nodes.Add(tn_c);
                                        }
                                    }
                                }
                            }
                        break;
                    }
                }
                this.treeView1.Nodes.Add(treeNode);
                if (treeView1.Nodes.Count > 0) { treeView1.Nodes[0].Expand(); } 
            });
        }
        #endregion
         
        private void getData()
        {
            if (residentBaseInfos == null)
            {
                residentBaseInfos = new List<ResidentBaseInfo>();
            }
            else
            {
                residentBaseInfos.Clear();
            }
            StringObjectDictionary param = new { OrderByClause = " OperatedOn desc ", AreaIdSub = "", KeyWord = "", PageNo = 1, PageSize = 30 }.ToStringObjectDictionary();

            HttpAdapter.postSyncAsJSON(GovVar.DataPointOfManage + "/Bas/ResidentBaseInfoService/GetResidentBaseInfo", param,
                (ret, res) =>
                {
                    if ((bool)ret.Success)
                    {
                        foreach (var row in ret.rows)
                        {
                            dynamic item = new ExpandoObject(); 
                            DynamicAdapter.Parse(item, XElement.Parse(row.ToString()));
                            ResidentBaseInfo dtItem = (item.StringObjectDictionary as IDictionary<string, object>).FromDynamic<ResidentBaseInfo>();
                            residentBaseInfos.Add(dtItem);
                        }
                    }
                });

        }

        #region 初始化“所属街道社区”label
        private Label Creat_Label(String name, String text)
        {
            Label lbAreas = new Label();
            lbAreas.BackColor = System.Drawing.Color.WhiteSmoke;
            lbAreas.Dock = System.Windows.Forms.DockStyle.Fill;
            lbAreas.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            lbAreas.Location = new System.Drawing.Point(0, 0);
            lbAreas.Name = name;
            lbAreas.Size = new System.Drawing.Size(180, 30);
            lbAreas.TabIndex = 3;
            lbAreas.Text = text;
            lbAreas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            return lbAreas;
        }
        #endregion

        #region 初始化 DataGridView控件
        private DataGridView Creat_DataGridView(String name)
        {
            DataGridView dataGridView = new DataGridView();
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            dataGridView.Location = new System.Drawing.Point(0, 0);
            dataGridView.Name = name;
            dataGridView.ReadOnly = true;
            dataGridView.RowTemplate.Height = 23;
            dataGridView.Size = new System.Drawing.Size(810, 609);
            dataGridView.TabIndex = 0;
            return dataGridView;
        }
        #endregion

        #region 初始化TreeView控件
        private TreeView Creat_TreeView(String name)
        {
            TreeView treeView = new TreeView();
            treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            treeView.Location = new System.Drawing.Point(0, 0);
            treeView.Name = name;
            treeView.Size = new System.Drawing.Size(180, 612);
            treeView.TabIndex = 0;
            return treeView;
        }
        #endregion

        #region 初始化SplitContainer控件
        private SplitContainer Creat_SplitContainer(String name, int sizeW, int sizeH, int splitterDistance, bool isHorizontal)
        {
            SplitContainer splitContainer = new SplitContainer();
            splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer.Location = new System.Drawing.Point(3, 3);
            splitContainer.Name = name;
            if (isHorizontal)
            {
                splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            }
            else
            {
                splitContainer.Orientation = System.Windows.Forms.Orientation.Vertical;
            }
            splitContainer.Size = new System.Drawing.Size(sizeW, sizeH);
            splitContainer.SplitterDistance = splitterDistance;
            splitContainer.TabIndex = 0;
            return splitContainer;
        }
        #endregion

        private void Fill_TabPage()
        {
            Label label = Creat_Label("lbAreaTitle", "所属街道社区");
            DataGridView dataGridView = Creat_DataGridView("dgvBaseinfo");
            TreeView treeView = Creat_TreeView("tvAreas");
            SplitContainer splitContainer_ = Creat_SplitContainer("splitContainer_All",994,660,40,true);
            SplitContainer splitContainer_Panel1 = Creat_SplitContainer("splitContainer_Lfet",200,660,30,false);
            SplitContainer splitContainer_Panel2 = Creat_SplitContainer("splitContainer_Right", 200, 660, 30, false);

            splitContainer_Panel1.Panel1.Controls.Add(label);
            splitContainer_Panel1.Panel2.Controls.Add(treeView);
            splitContainer_Panel2.Panel2.Controls.Add(dataGridView); 

            splitContainer_.Panel1.Controls.Add(splitContainer_Panel1);
            splitContainer_.Panel2.Controls.Add(splitContainer_Panel2);
            TabPage thistPage = new TabPage();
            thistPage = this.xTabs.SelectedTab;
            thistPage.Controls.Add(splitContainer_); 

        }

        private void tsmiDestroy_Click(object sender, EventArgs e)
        {
            CreatTabPage("tpDestroy", "死亡处理");


            //formResidentBaseInfo formb = new formResidentBaseInfo();
            //formb.TopLevel = false;
            //formb.FormBorderStyle = FormBorderStyle.None;
            ////panel1.Controls.Add(formb);


            userCtlResidentBaseInfo mForm = new userCtlResidentBaseInfo();
            mForm.Dock = DockStyle.Fill;
            //this.panel.Controls.Add(mForm);

            Panel p = new Panel();
            p.Dock = DockStyle.Fill;
            p.BackColor = Color.Blue;
            p.Controls.Add(mForm);
            //formb.Parent = p;

            TabPage thistPage = new TabPage();
            thistPage = this.xTabs.SelectedTab;
            thistPage.Controls.Add(p); 
        }

        private void tsmiIDNoChange_Click(object sender, EventArgs e)
        {
            CreatTabPage("tpIDNoChange", "变更身份证号");
        }

        private void tsmiEvaRequisition_Click(object sender, EventArgs e)
        {
            CreatTabPage("tpEvaRequisition", "申请评估");
        }

        private void tsmiEvaInfo_Click(object sender, EventArgs e)
        {
            CreatTabPage("tpEvaInfo", "指标评估");
        }

        private void tsmiEvaCommunity_Click(object sender, EventArgs e)
        {
            CreatTabPage("tpEvaCommunity", "社区意见");
        }

        private void tsmiEvaStreet_Click(object sender, EventArgs e)
        {
            CreatTabPage("tpEvaStreet", "街道审核");
        }

        private void tsmiEvaNotice_Click(object sender, EventArgs e)
        {
            CreatTabPage("tpEvaNotice", "告知书");
        }

        private void tsmiEvaReport_Click(object sender, EventArgs e)
        {
            CreatTabPage("tpEvaReport", "评估结果");
        }

        private void tsmiStation_Click(object sender, EventArgs e)
        {
            CreatTabPage("tpStation", "养老服务站");
        }

        private void tsmiInstitution_Click(object sender, EventArgs e)
        {
            CreatTabPage("tpInstitution", "养老机构");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.tsslDateTime.Text = "系统时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

    }
}
