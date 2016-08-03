using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms; 
using e0571.web.core.Wcf;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using e0571.web.core.Utils;
using win.core.utils;
using System.Dynamic;
using System.Xml.Linq;

namespace SmartLife.Client.Gov
{
    public partial class userCtlResidentBaseInfo : UserControl
    {  
        List<ResidentBaseInfo> residentBaseInfos;
        private int _pageSize = 30;  /// 每页显示记录数  
        private int _nMax = 0; /// 总记录数 
        private int _pageCount = 0;  /// 页数=总记录数/每页显示记录数   
        private int _pageCurrent = 1;  /// 当前页号   
        public static StringObjectDictionary dgvCurrentRow;

        #region 初始化页面
        public userCtlResidentBaseInfo()
        {
            InitializeComponent();
            cbxPageSize.SelectedIndex=1;//默认每页显示30条数据
            creatTreeData();//加载街道社区树
            getData("");//加载数据
        }
        #endregion

        #region 建立街道社区树
        private void creatTreeData()
        {
            String url = GovVar.DataPointOfManage + "/Share/TreeDataService/fetchTreeData";
            StringObjectDictionary _treeParam = new { TreeCode = "01$01$02", OrderByClause = "OrderNo asc", TreeParams = "{\"DictionaryId\":\"00005\" , \"ItemId\":\""+GovVar.Area1+"\"}" }.ToStringObjectDictionary();
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
                this.tvAreaInfo.Nodes.Add(treeNode);
                if (tvAreaInfo.Nodes.Count > 0) { tvAreaInfo.Nodes[0].Expand(); }
            });
        }
        #endregion

        #region 加载居民基本信息数据
        private void getData(string areaIdSub)
        {
            if (residentBaseInfos == null)
            {
                residentBaseInfos = new List<ResidentBaseInfo>();
            }
            else
            {
                residentBaseInfos.Clear();
            }

            StringObjectDictionary param = new { Order = " desc ", Sort = " OperatedOn ", AreaId = areaIdSub, KeyWord = txbKeyWord.Text.Trim(), PageNo = _pageCurrent, PageSize = _pageSize }.ToStringObjectDictionary();

            HttpAdapter.postSyncAsJSON(GovVar.DataPointOfManage + "/Bas/ResidentBaseInfoService/GetResidentBaseInfo", param, new { ConnectId = GovVar.ConnectId }.ToStringObjectDictionary(),
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
                        var total = ret.total.Value;
                        _nMax = (int)total; 
                        _pageCount = _nMax / _pageSize;
                        _pageCount = (_nMax % _pageSize) == 0 ? _pageCount : _pageCount + 1;
                    }
                    else
                    {
                        _nMax = 0;
                        _pageCount = 0;
                    }
                });
             

            dgvResidentBaseInfo.DataSource = null;
            dgvResidentBaseInfo.DataSource = residentBaseInfos;
            dgvResidentBaseInfo.ScrollBars = ScrollBars.Both;

            txbPageTo.Text = _pageCurrent.ToString() ;
            lblPageCount.Text = "共 "+_pageCount.ToString()+" 页";
            lblRecordCount.Text = "总共 " + _nMax + " 条；当前第 " + _pageCurrent + " 页；共 " + _pageCount + " 页，每页 " + _pageSize + " 条";
            //bdngBaseInfo.BindingSource = null;
            //bdngBaseInfo.BindingSource = residentBaseInfos;

        }
        #endregion
         
        #region 显示序号
        private void dgvResidentBaseInfo_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = sender as DataGridView;
            if (dgv != null)
            {
                Rectangle rect = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgv.RowHeadersWidth - 4, e.RowBounds.Height);
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dgv.RowHeadersDefaultCellStyle.Font, rect, dgv.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
                            }
        }
        #endregion

        #region 绑定数据到DataGridView
        private void dgvResidentBaseInfo_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgvResidentBaseInfo.Columns["ResidentName"].HeaderText = "姓名";
            dgvResidentBaseInfo.Columns["ResidentName"].Width = 80;
            dgvResidentBaseInfo.Columns["ResidentName"].Frozen = true;
            dgvResidentBaseInfo.Columns["ResidentName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvResidentBaseInfo.Columns["IDNo"].HeaderText = "身份证号码";
            dgvResidentBaseInfo.Columns["IDNo"].Width = 150;
            dgvResidentBaseInfo.Columns["IDNo"].Frozen = true;
            dgvResidentBaseInfo.Columns["IDNo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvResidentBaseInfo.Columns["Gender"].HeaderText = "性别";
            dgvResidentBaseInfo.Columns["Gender"].Width = 60;
            dgvResidentBaseInfo.Columns["Gender"].Frozen = true;
            dgvResidentBaseInfo.Columns["Gender"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvResidentBaseInfo.Columns["NationName"].HeaderText = "民族";
            dgvResidentBaseInfo.Columns["NationName"].Width = 80;
            dgvResidentBaseInfo.Columns["NationName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvResidentBaseInfo.Columns["Tel"].HeaderText = "座机";
            dgvResidentBaseInfo.Columns["Tel"].Width = 80;
            dgvResidentBaseInfo.Columns["Tel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvResidentBaseInfo.Columns["Mobile"].HeaderText = "电话";
            dgvResidentBaseInfo.Columns["Mobile"].Width = 80;
            dgvResidentBaseInfo.Columns["Mobile"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvResidentBaseInfo.Columns["NativePlaceName"].HeaderText = "籍贯";
            dgvResidentBaseInfo.Columns["NativePlaceName"].Width = 150;
            dgvResidentBaseInfo.Columns["NativePlaceName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvResidentBaseInfo.Columns["AccountTypeName"].HeaderText = "户口类型";
            dgvResidentBaseInfo.Columns["AccountTypeName"].Width = 80;
            dgvResidentBaseInfo.Columns["AccountTypeName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvResidentBaseInfo.Columns["HouseholdRegisterName"].HeaderText = "户籍";
            dgvResidentBaseInfo.Columns["HouseholdRegisterName"].Width = 150;
            dgvResidentBaseInfo.Columns["HouseholdRegisterName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvResidentBaseInfo.Columns["PlaceOfHouseholdRegister"].HeaderText = "户籍(详)";
            dgvResidentBaseInfo.Columns["PlaceOfHouseholdRegister"].Width = 100; 

            dgvResidentBaseInfo.Columns["ResidentialOfHometownName"].HeaderText = "现居地";
            dgvResidentBaseInfo.Columns["ResidentialOfHometownName"].Width = 150;
            dgvResidentBaseInfo.Columns["ResidentialOfHometownName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvResidentBaseInfo.Columns["ResidentialAddress"].HeaderText = "现居(详)";
            dgvResidentBaseInfo.Columns["ResidentialAddress"].Width = 250; 

            dgvResidentBaseInfo.Columns["EducationLevelName"].HeaderText = "文化程度";
            dgvResidentBaseInfo.Columns["EducationLevelName"].Width = 80;
            dgvResidentBaseInfo.Columns["EducationLevelName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvResidentBaseInfo.Columns["MarriageName"].HeaderText = "婚姻状况";
            dgvResidentBaseInfo.Columns["MarriageName"].Width = 80;
            dgvResidentBaseInfo.Columns["MarriageName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvResidentBaseInfo.Columns["ResidentBizIdName"].HeaderText = "身份情况";
            dgvResidentBaseInfo.Columns["ResidentBizIdName"].Width = 80;
            dgvResidentBaseInfo.Columns["ResidentBizIdName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvResidentBaseInfo.Columns["LivingStatusName"].HeaderText = "居住情况";
            dgvResidentBaseInfo.Columns["LivingStatusName"].Width = 80;
            dgvResidentBaseInfo.Columns["LivingStatusName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvResidentBaseInfo.Columns["IncomeStatusName"].HeaderText = "收入情况";
            dgvResidentBaseInfo.Columns["IncomeStatusName"].Width = 80;
            dgvResidentBaseInfo.Columns["IncomeStatusName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvResidentBaseInfo.Columns["HousingStatusName"].HeaderText = "住房情况";
            dgvResidentBaseInfo.Columns["HousingStatusName"].Width = 80;
            dgvResidentBaseInfo.Columns["HousingStatusName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvResidentBaseInfo.Columns["PostCode"].HeaderText = "邮编";
            dgvResidentBaseInfo.Columns["PostCode"].Width = 80;
            dgvResidentBaseInfo.Columns["PostCode"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvResidentBaseInfo.Columns["Remark"].HeaderText = "备注";
            dgvResidentBaseInfo.Columns["Remark"].Width = 250; 



            dgvResidentBaseInfo.Columns["ResidentId"].Visible = false;//隐藏某列：
            dgvResidentBaseInfo.Columns["Id"].Visible = false;//隐藏某列：
            dgvResidentBaseInfo.Columns["CheckInTime"].Visible = false;//隐藏某列：
            dgvResidentBaseInfo.Columns["Status"].Visible = false;//隐藏某列：
            dgvResidentBaseInfo.Columns["OperatedBy"].Visible = false;//隐藏某列：
            dgvResidentBaseInfo.Columns["OperatedOn"].Visible = false;//隐藏某列：
            dgvResidentBaseInfo.Columns["DataSource"].Visible = false;//隐藏某列： 
            dgvResidentBaseInfo.Columns["ResidentStatus"].Visible = false;//隐藏某列：
            dgvResidentBaseInfo.Columns["ResidentBizId"].Visible = false;//隐藏某列：
            dgvResidentBaseInfo.Columns["NativePlace"].Visible = false;//隐藏某列：
            dgvResidentBaseInfo.Columns["HouseholdRegister"].Visible = false;//隐藏某列：
            dgvResidentBaseInfo.Columns["EducationLevel"].Visible = false;//隐藏某列：
            dgvResidentBaseInfo.Columns["Marriage"].Visible = false;//隐藏某列：
            dgvResidentBaseInfo.Columns["LivingStatus"].Visible = false;//隐藏某列：
            dgvResidentBaseInfo.Columns["HousingStatus"].Visible = false;//隐藏某列：
            dgvResidentBaseInfo.Columns["IncomeStatus"].Visible = false;//隐藏某列：
            dgvResidentBaseInfo.Columns["AreaId"].Visible = false;//隐藏某列：
            dgvResidentBaseInfo.Columns["AreaId2"].Visible = false;//隐藏某列：
            dgvResidentBaseInfo.Columns["AreaId3"].Visible = false;//隐藏某列：
            dgvResidentBaseInfo.Columns["PlaceOfHouseholdRegister"].Visible = false;//隐藏某列：
            dgvResidentBaseInfo.Columns["InputCode1"].Visible = false;//隐藏某列：
            dgvResidentBaseInfo.Columns["InputCode2"].Visible = false;//隐藏某列：
            dgvResidentBaseInfo.Columns["ResidentialOfHometown"].Visible = false;//隐藏某列：
            dgvResidentBaseInfo.Columns["Nation"].Visible = false;//隐藏某列：
            dgvResidentBaseInfo.Columns["AccountType"].Visible = false;//隐藏某列：
            dgvResidentBaseInfo.Columns["EventTime"].Visible = false;//隐藏某列：
            dgvResidentBaseInfo.Columns["StationId"].Visible = false;//隐藏某列： 
                       

        }
        #endregion

        #region 查询事件
        private void btnExec_Click(object sender, EventArgs e)
        {
            Bind();
        }
        #endregion

        #region 执行查询操作
        private void Bind() {
            if (tvAreaInfo.SelectedNode != null)
            {
                string areaIdSub = "";
                string treeNodeId = tvAreaInfo.SelectedNode.Name; //当前节点的层次
                if (treeNodeId.Count() > 6)
                {
                    string sub_id = treeNodeId.Substring(14, 4);
                    if (sub_id == "0000")
                    {
                        areaIdSub = treeNodeId.Substring(0, 13); //选择的是街道节点 
                    }
                    else
                    {
                        areaIdSub = treeNodeId.Substring(0, 18); //选择的是社区节点  
                    }
                }
                getData(areaIdSub);
            }
            else {
                getData("");
            }
        }
        #endregion

        #region 首页操作
        private void llblFirst_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _pageCurrent = 1;
            Bind();
        }
        #endregion

        #region 上一页操作
        private void llblPrev_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_pageCurrent > 1)
            {
                _pageCurrent -= 1;
                Bind();
            }
            else
            {
                MessageBox.Show("当前已是第一页");
            }
        }
        #endregion

        #region 下一页操作
        private void llblNext_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_pageCurrent < _pageCount)
            {
                _pageCurrent += 1;
                Bind();
            }
            else
            {
                MessageBox.Show("当前已是最后一页");
            }
        }
        #endregion

        #region 最后一页操作
        private void llblLast_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _pageCurrent = _pageCount;
            Bind();
        }
        #endregion

        #region 输入某一页  回车键跳转
        private void txbPageTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                _pageCurrent = Int32.Parse(txbPageTo.Text);
                Bind();
            }
        }
        #endregion

        #region 输入页面时   控制只允许输入数字
        private void txbPageTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 0x20) e.KeyChar = (char)0;  //禁止空格键  
            if ((e.KeyChar == 0x2D) && (((TextBox)sender).Text.Length == 0)) return;   //处理负数  
            if (e.KeyChar > 0x20)
            {
                try
                {
                    double.Parse(((TextBox)sender).Text + e.KeyChar.ToString());
                }
                catch
                {
                    e.KeyChar = (char)0;   //处理非法字符  
                }
            }
        }
        #endregion

        #region 改变没有查询条数时  记录条数值
        private void cbxPageSize_SelectedValueChanged(object sender, EventArgs e)
        {
            string pSizeStr=cbxPageSize.Text;
            if (pSizeStr != "")
            {
                _pageSize = Int32.Parse(pSizeStr);
            }
        }
        #endregion

        #region 新增按钮
        private void btnAdd_Click(object sender, EventArgs e)
        {
            formResidentBaseInfo formBaseInfo = new formResidentBaseInfo();
            dgvCurrentRow = null;
            formBaseInfo.ShowDialog();
        }
        #endregion

        private void btnEidt_Click(object sender, EventArgs e)
        { 
            formResidentBaseInfo formBaseInfo = new formResidentBaseInfo();
            dgvCurrentRow = dgvResidentBaseInfo.CurrentRow.DataBoundItem.ToStringObjectDictionary();
            formBaseInfo.ShowDialog();
        }
         



    }
}
