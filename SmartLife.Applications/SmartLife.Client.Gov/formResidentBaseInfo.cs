using System;
using System.Windows.Forms;
using System.Collections;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Data;
using e0571.web.core.Wcf;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using e0571.web.core.Utils;
using win.core.utils;
using System.Dynamic;
using System.Xml.Linq;
using System.Collections.Generic;

namespace SmartLife.Client.Gov
{
    public partial class formResidentBaseInfo : Form
    {

        ArrayList arrListStreet = new ArrayList();
        ArrayList arrListCommunity = new ArrayList();

        public formResidentBaseInfo()
        {
            InitializeComponent();
        } 

        private void formResidentBaseInfo_Load(object sender, EventArgs e)
        { 
            banDingComboBoxData();//绑定下拉框的数据 
            StringObjectDictionary residentBaseInfo = userCtlResidentBaseInfo.dgvCurrentRow;
            if (residentBaseInfo == null)
            {
                formForAdd();
            }
            else {
                formForEidt(residentBaseInfo);
            }
        }

        private void formForAdd()
        {
            formResidentBaseInfo fm = new formResidentBaseInfo();
            fm.Text = "新增居民基本信息";
            lblResidentId.Text = Guid.NewGuid().ToString();
            txbResidentName.Text = null;
            txbIDNo.Text = null;
            txbTel.Text = null;
            txbMobile.Text = null;
            txbPlaceOfHouseholdRegister.Text = null;
            txbResidentialAddress.Text = null;
            txbPostCode.Text = null;
            txbRemark.Text = null;

            rbtGenderM.Checked = true;
            //cbxStreet.SelectedIndex = -1;
            //cbxCommunity.SelectedIndex = -1; 
            cbxResidentBizId.SelectedIndex = -1; 
            cbxAccountType.SelectedIndex = -1; 
            cbxEducationLevel.SelectedIndex = -1; 
            cbxMarriage.SelectedIndex = -1; 
            cbxLivingStatus.SelectedIndex = -1; 
            cbxHousingStatus.SelectedIndex = -1; 
            cbxIncomeStatus.SelectedIndex = -1; 
            cbxNation.SelectedIndex = -1; 
            cbxNativePlace.SelectedIndex = -1; 
            cbxHouseholdRegister.SelectedIndex = -1; 
            cbxResidentialOfHometown.SelectedIndex = -1;
        }

        private void formForEidt(StringObjectDictionary baseInfo )
        {
            formResidentBaseInfo fm = new formResidentBaseInfo();
            fm.Text = "编辑居民基本信息";
            lblResidentId.Text = baseInfo["ResidentId"].ToString();
            txbResidentName.Text = baseInfo["ResidentName"].ToString();
            txbIDNo.Text = baseInfo["IDNo"].ToString();
            txbTel.Text = baseInfo["Tel"].ToString();
            txbMobile.Text = baseInfo["Mobile"].ToString();
            txbPlaceOfHouseholdRegister.Text = baseInfo["PlaceOfHouseholdRegister"].ToString();
            txbResidentialAddress.Text = baseInfo["ResidentialAddress"].ToString();
            txbPostCode.Text = baseInfo["PostCode"].ToString();
            txbRemark.Text = baseInfo["Remark"].ToString();

            string gender = baseInfo["Gender"].ToString();
            if (gender == "M") { rbtGenderM.Checked = true; }
            else if (gender == "F") { rbtGenderF.Checked = true; }
            else { rbtGenderM.Checked = false; rbtGenderF.Checked = false; }

            //cbxStreet.SelectedIndex = -1;
            //cbxCommunity.SelectedIndex = -1; 
            //cbxResidentBizId.SelectedValue = Int32.Parse(baseInfo["ResidentBizId"].ToString());
            //cbxAccountType.SelectedValue = Int32.Parse(baseInfo["AccountType"].ToString());
            //cbxEducationLevel.SelectedValue = Int32.Parse(baseInfo["EducationLevel"].ToString());
            //cbxMarriage.SelectedValue = Int32.Parse(baseInfo["Marriage"].ToString());
            //cbxLivingStatus.SelectedValue = Int32.Parse(baseInfo["LivingStatus"].ToString());
            //cbxHousingStatus.SelectedValue = Int32.Parse(baseInfo["HousingStatus"].ToString());
            //cbxIncomeStatus.SelectedValue = Int32.Parse(baseInfo["IncomeStatus"].ToString());
            //cbxNation.SelectedValue = Int32.Parse(baseInfo["Nation"].ToString());
            //cbxNativePlace.SelectedValue = Int32.Parse(baseInfo["NativePlace"].ToString());
            //cbxHouseholdRegister.SelectedValue = Int32.Parse(baseInfo["HouseholdRegister"].ToString());
            //cbxResidentialOfHometown.SelectedValue = Int32.Parse(baseInfo["ResidentialOfHometown"].ToString());
        }

        #region 检查所填数据是否符合要求
        private bool checkFormData()
        {
            bool result = false;
            if (checkIDNo())
            {
                string strResidentName = txbResidentName.Text.Trim();
                string strIDNo = txbIDNo.Text.Trim();
                var areaId2 = cbxStreet.SelectedValue;
                var areaId3 = cbxCommunity.SelectedValue;
                if (strResidentName != "" && strIDNo != "" && areaId2.ToString() != "" && areaId2 != null && areaId3.ToString() != "" && areaId3 != null)
                {
                    result = true;
                }
                else
                {
                    MessageBox.Show("必填项不能为空"); 
                }
            }
            return result;
        }
        #endregion

        #region 检查身份证号码是否符合要求
        private bool checkIDNo()
        {
            bool result = false;
            string strIDNo = txbIDNo.Text.Trim();

            //新增和修改之前查看身份证号位数是否正确
            if ((strIDNo.Length != 15) && (strIDNo.Length != 18))
            {
                //MessageBox.Show("输入的身份证号位数错误");
            }
            else {
                HttpAdapter.getSyncTo(GovVar.DataPointOfManage + "/Bas/ResidentBaseInfoService/IDNoDuplicatesForCS", null, new { ConnectId = GovVar.ConnectId, IDNo = strIDNo, Status = 0 }.ToStringObjectDictionary(), (ret, res) =>
                {
                    if ((bool)ret.Success.Value)
                    {
                        string strResidentId = lblResidentId.Text;
                        string strOldManName = "";
                        for (int i = 0; i < ret.rows.Count; i++)
                        {
                            dynamic item = new ExpandoObject();
                            DynamicAdapter.Parse(item, XElement.Parse(ret.rows[i].ToString()));
                            ResidentBaseInfo dtItem = (item.StringObjectDictionary as IDictionary<string, object>).FromDynamic<ResidentBaseInfo>();

                            if (dtItem.ResidentId.ToLower() != strResidentId.ToLower())
                            {
                                strOldManName += dtItem.ResidentName;
                            }
                        }
                        if (strOldManName == "")
                        {
                            result = true;
                        }
                        else
                        {
                            MessageBox.Show(strOldManName + "已使用此身份证号码");
                        }
                    }
                    else
                    {
                        MessageBox.Show("身份证号码验证失败");
                    }
                });
            }
            return result;
        }
        #endregion

        #region 获取所以表单信息
        private StringObjectDictionary getFormData()
        {
            StringObjectDictionary baseInfo = new StringObjectDictionary();
            baseInfo["ResidentId"] = lblResidentId.Text;
            baseInfo["Status"] = 1;
            baseInfo["OperatedBy"] =  GovVar.UserId.ToGuid();
            baseInfo["DataSource"] = "00005";
            baseInfo["ResidentName"] = txbResidentName.Text;
            baseInfo["ResidentStatus"] = 1;
            baseInfo["ResidentBizId"] = isNull(cbxResidentBizId.SelectedValue);
            baseInfo["IDNo"] = txbIDNo.Text ;
            if (rbtGenderM.Checked) { baseInfo["Gender"] = "M"; }
            else if(rbtGenderF.Checked){ baseInfo["Gender"] = "F"; }
            baseInfo["Nation"] = isNull(cbxNation.SelectedValue);
            baseInfo["NativePlace"] = isNull(cbxNativePlace.SelectedValue );
            baseInfo["HouseholdRegister"] = isNull(cbxHouseholdRegister.SelectedValue );
            baseInfo["EducationLevel"] = isNull(cbxEducationLevel.SelectedValue);
            baseInfo["Marriage"] = isNull(cbxMarriage.SelectedValue );
            baseInfo["LivingStatus"] = isNull(cbxLivingStatus.SelectedValue );
            baseInfo["HousingStatus"] = isNull(cbxHousingStatus.SelectedValue );
            baseInfo["IncomeStatus"] = isNull(cbxIncomeStatus.SelectedValue );
            baseInfo["AccountType"] = isNull(cbxAccountType.SelectedValue );
            baseInfo["AreaId"] = GovVar.Area1;
            baseInfo["AreaId2"] = isNull(cbxStreet.SelectedValue);
            baseInfo["AreaId3"] = isNull(cbxCommunity.SelectedValue );
            baseInfo["ResidentialOfHometown"] = isNull(cbxResidentialOfHometown.SelectedValue);
            baseInfo["ResidentialAddress"] = txbResidentialAddress.Text;
            baseInfo["PlaceOfHouseholdRegister"] = txbPlaceOfHouseholdRegister.Text;
            baseInfo["PostCode"] = txbPostCode.Text;
            baseInfo["Tel"] = txbTel.Text;
            baseInfo["Mobile"] = txbMobile.Text;
            baseInfo["Remark"] = txbIDNo.Text;

            return baseInfo;
        }
        #endregion

        #region 判断是否为NULL
        private string isNull(object value) {
            string ret = null;
            if (value != null) {
                ret = value.ToString();
            }
            return ret;
        }
        #endregion

        #region 绑定控件的数据
        private void banDingComboBoxData()
        {
            dynamic streetAndCommunityInfo = DictionaryItem.getStreetAndCommunity(GovVar.Area1);
            foreach (var row in streetAndCommunityInfo)
            {
                if (row.Levels.Value == 4)
                {
                    arrListStreet.Add(row);
                }
                else if (row.Levels.Value == 5)
                {
                    arrListCommunity.Add(row);               
                }
            }
            cbxStreet.DataSource = arrListStreet;
            cbxStreet.ValueMember = "ItemId";
            cbxStreet.DisplayMember = "ItemName";
            //cbxStreet.SelectedIndex = -1;

            //cbxCommunity.DataSource = arrListCommunity;
            //cbxCommunity.ValueMember = "ItemId";
            //cbxCommunity.DisplayMember = "ItemName";
            //cbxCommunity.SelectedIndex = -1;

            cbxResidentBizId.DataSource = DictionaryItem.getDictionaryItemList("01005");
            cbxResidentBizId.ValueMember = "ItemId";
            cbxResidentBizId.DisplayMember = "ItemName";
            //cbxResidentBizId.SelectedIndex = -1;

            cbxAccountType.DataSource = DictionaryItem.getDictionaryItemList("00016");
            cbxAccountType.ValueMember = "ItemId";
            cbxAccountType.DisplayMember = "ItemName";
            //cbxAccountType.SelectedIndex = -1;

            cbxEducationLevel.DataSource = DictionaryItem.getDictionaryItemList("00017");
            cbxEducationLevel.ValueMember = "ItemId";
            cbxEducationLevel.DisplayMember = "ItemName";
            //cbxEducationLevel.SelectedIndex = -1;

            cbxMarriage.DataSource = DictionaryItem.getDictionaryItemList("00018");
            cbxMarriage.ValueMember = "ItemId";
            cbxMarriage.DisplayMember = "ItemName";
            //cbxMarriage.SelectedIndex = -1;

            cbxLivingStatus.DataSource = DictionaryItem.getDictionaryItemList("00019");
            cbxLivingStatus.ValueMember = "ItemId";
            cbxLivingStatus.DisplayMember = "ItemName";
            //cbxLivingStatus.SelectedIndex = -1;

            cbxHousingStatus.DataSource = DictionaryItem.getDictionaryItemList("00020");
            cbxHousingStatus.ValueMember = "ItemId";
            cbxHousingStatus.DisplayMember = "ItemName";
            //cbxHousingStatus.SelectedIndex = -1;

            cbxIncomeStatus.DataSource = DictionaryItem.getDictionaryItemList("00021");
            cbxIncomeStatus.ValueMember = "ItemId";
            cbxIncomeStatus.DisplayMember = "ItemName";
            //cbxIncomeStatus.SelectedIndex = -1;

            cbxNation.DataSource = DictionaryItem.getDictionaryItemList("00022");
            cbxNation.ValueMember = "ItemId";
            cbxNation.DisplayMember = "ItemName";
            //cbxNation.SelectedIndex = -1;

            dynamic dyCitys = DictionaryItem.getDictionaryItemList("00005");
            ArrayList arrCitys = new ArrayList();
            ArrayList arrCounty = new ArrayList();
            foreach (var row in dyCitys)
            {
                int len = row.ItemCode.Value.Length;
                if (len == 4)
                {
                    arrCitys.Add(row);
                }
                else if (len == 6)
                {
                    arrCounty.Add(row);
                }
            }

            cbxNativePlace.DataSource = arrCitys;
            cbxNativePlace.ValueMember = "ItemId";
            cbxNativePlace.DisplayMember = "ItemName";
            //cbxNativePlace.SelectedIndex = -1;
            
            cbxHouseholdRegister.DataSource = arrCitys;
            cbxHouseholdRegister.ValueMember = "ItemId";
            cbxHouseholdRegister.DisplayMember = "ItemName";
            //cbxHouseholdRegister.SelectedIndex = -1;

            cbxResidentialOfHometown.DataSource = arrCounty;
            cbxResidentialOfHometown.ValueMember = "ItemId";
            cbxResidentialOfHometown.DisplayMember = "ItemName";
            //cbxResidentialOfHometown.SelectedIndex = -1;

        }
        #endregion

        #region  取消关闭按钮
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 选择街道  获取街道下的社区信息
        private void cbxStreet_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ArrayList arrList = new ArrayList();
            string areaId = cbxStreet.SelectedValue.ToString().ToLower();
            if (areaId != null)
            { 
                JavaScriptSerializer jss = new JavaScriptSerializer(); 
                foreach (var row in arrListCommunity)
                {
                    string str = row.ToString();
                    str = str.Replace("\n", string.Empty).Replace("\r", string.Empty);
                    DictionaryItem js = jss.Deserialize<DictionaryItem>(str);
                    if (areaId == js.ParentId.ToString().ToLower())
                    {
                        arrList.Add(row);
                    }
                }
                cbxCommunity.DataSource = arrList;
                cbxCommunity.ValueMember = "ItemId";
                cbxCommunity.DisplayMember = "ItemName";
                cbxCommunity.SelectedIndex = 0;

            }
        }
        #endregion

        #region 保存确定按钮
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (checkFormData())
            {
                HttpAdapter.postSyncAsJSON(GovVar.DataPointOfManage + "/Bas/ResidentBaseInfoService/CreateInfo", getFormData(), new { ConnectId = GovVar.ConnectId }.ToStringObjectDictionary(),
                    (ret, res) =>
                    {
                        if ((bool)ret.Success.Value)
                        {
                            if (MessageBox.Show("新增成功，是否继续新增？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                            { 
                                ///
                            }
                            else
                            {
                                this.Close();
                            }
                        } 
                    });             
            }
        }
        #endregion
    }
}
