using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using win.core.utils;
using System.Net;
using e0571.web.core.Security;
using e0571.web.core.TypeExtension;
using e0571.web.core.TypeInherited;
using System.IO;
using Newtonsoft.Json;


namespace SmartLife.Client.PensionAgency
{
    public partial class frmLogin : Form
    {
        string strUserNameSinceLast;
        string strObjectIdSinceLast;
        string strObjectNameSinceLast;
        string strRunMode;
        string authEndPoint;
        string authDataPoint;
        TreeNode selectedNode;

        System.Timers.Timer _tickTimer = null;

        public frmLogin()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            lblProductName.Text = Properties.Settings.Default.ProductName;
            lblProductVersionComment.Text = Properties.Settings.Default.ProductVersionComment;
            
            authEndPoint = INIAdapter.ReadValue(Common.CFG_SECTION_WEB, Common.CFG_KEY_AUTH_END_POINT, Common.CFG_FILE_PATH);
            authDataPoint = INIAdapter.ReadValue(Common.CFG_SECTION_WEB, Common.CFG_KEY_AUTH_DATA_POINT, Common.CFG_FILE_PATH);
            strUserNameSinceLast = INIAdapter.ReadValue(Common.INI_SECTION_LOCAL, Common.INI_KEY_USER_NAME_SINCE_LAST, Common.INI_FILE_PATH);
            strObjectIdSinceLast = INIAdapter.ReadValue(Common.INI_SECTION_LOCAL, Common.INI_KEY_OBJECT_ID_SINCE_LAST, Common.INI_FILE_PATH);
            strObjectNameSinceLast = INIAdapter.ReadValue(Common.INI_SECTION_LOCAL, Common.INI_KEY_OBJECT_NAME_SINCE_LAST, Common.INI_FILE_PATH);
            strRunMode = INIAdapter.ReadValue(Common.INI_SECTION_LOCAL, Common.INI_KEY_RUN_MODE, Common.INI_FILE_PATH);
            string strDeployNodeObjects = INIAdapter.ReadValue(Common.INI_SECTION_DATA, Common.INI_KEY_DEPLOY_NODE_OBJECTS, Common.INI_FILE_PATH);
            if (strDeployNodeObjects == "")
            {
                btnOk.Enabled = false;

                //远程获取deployNodeObjects
                ThreadAdapter.DoOnceTask(() =>
                {
                    HttpAdapter.getSyncTo(authDataPoint + "/GetDeployNodeObjects", (ret, res) =>
                    {
                        if ((bool)ret.Success)
                        {
                            strDeployNodeObjects = JsonConvert.SerializeObject(ret.rows);
                            INIAdapter.WriteValue(Common.INI_SECTION_DATA, Common.INI_KEY_DEPLOY_NODE_OBJECTS, strDeployNodeObjects, Common.INI_FILE_PATH);
                            this.UIInvoke(() =>
                            {
                                btnOk.Enabled = true;
                            });
                            BindDeployNodeObjects(strDeployNodeObjects);
                        }
                        else
                        {
                            this.UIInvoke(() =>
                            {
                                btnOk.Enabled = true;

                            });
                        }

                    });
                });
            }
            else
            {
                BindDeployNodeObjects(strDeployNodeObjects);
            }

            if (strRunMode == "")
            {
                strRunMode = "0";
            }
            if (!string.IsNullOrEmpty(strUserNameSinceLast))
            {
                txtUserName.Text = strUserNameSinceLast;
            }
        }

        private void frmLogin_Shown(object sender, EventArgs e)
        {
            if (txtUserName.Text != "")
            {
                txtPassword.Focus();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim() == "")
            {
                MessageBoxAdapter.ShowError("请输入用户名");
                return;
            }
            if (txtPassword.Text.Trim() == "")
            {
                MessageBoxAdapter.ShowError("请输入密码");
                return;
            }
            DoLogin(txtUserName.Text.Trim(), txtPassword.Text.Trim());
        }

        

        #region 绑定对象数据 
        private void BindDeployNodeObjects(string strDeployNodeObjects)
        {
            dynamic rows = JsonConvert.DeserializeObject<dynamic>(strDeployNodeObjects);
            List<dynamic> objects = new List<dynamic>();
            List<dynamic> rootObjects = new List<dynamic>();
            foreach (dynamic row in rows)
            {
                if (row.ObjectParentId == null)
                {
                    rootObjects.Add(row);
                }
                else
                {
                    objects.Add(row);
                }
            }
            
            TreeNode defaultNode = new TreeNode { Text = "自动", Name = "default" };
            
            ctvObjectNodes.TreeView.Nodes.Add(defaultNode);
            
            foreach (dynamic ro in rootObjects)
            {
                TreeNode rootNode = new TreeNode();
                rootNode.Text = ro.ObjectItemName.ToString();
                rootNode.Name = ro.ObjectId.ToString();
                rootNode.Tag = ro;
                ctvObjectNodes.TreeViewWidth = 202;
                ctvObjectNodes.TreeViewHeight = 303;
                //增加树的根节点   
                ctvObjectNodes.TreeView.Nodes.Add(rootNode);
                ctvObjectNodes.TreeView.BeforeSelect += new TreeViewCancelEventHandler((o, te) =>
                {
                    dynamic deployNodeObject = te.Node.Tag as dynamic;
                    if (deployNodeObject != null)
                    {
                        te.Cancel = deployNodeObject.ObjectLevels <= 2;
                    }
                });
                if (rootNode.Name == strObjectIdSinceLast)
                {
                    selectedNode = rootNode;
                }
                addDeployNodeObject(objects, rootNode, ro.ObjectItemId.ToString());


                rootNode.ExpandAll();
            }

            if (strObjectIdSinceLast == "default" || strObjectIdSinceLast == "")
            {
                ctvObjectNodes.Text = defaultNode.Text;
                selectedNode = defaultNode;
            }
            else
            {
                ctvObjectNodes.Text = strObjectNameSinceLast;
            }
            ctvObjectNodes.TreeView.SelectedNode = selectedNode;

        }
        public void addDeployNodeObject(IList<dynamic> objects, TreeNode node, string parentId)
        {
            IList<dynamic> currentObjects = objects.Where(item => item.ObjectParentId.ToString() == parentId).ToList();

            foreach (dynamic o in currentObjects)
            {
                TreeNode subNode = new TreeNode();
                subNode.Text = o.ObjectItemName.ToString();
                subNode.Name = o.ObjectId.ToString();
                subNode.Tag = o;
                node.Nodes.Add(subNode);
                addDeployNodeObject(objects, subNode, o.ObjectItemId.ToString());
                if (subNode.Name == strObjectIdSinceLast)
                {
                    selectedNode = subNode;
                }
            }
        }
        #endregion

        #region 登录
        private void DoLogin(string userCode, string password)
        {
            //
            btnOk.Enabled = false;

            lblMsg.Text = "登录中";
            _tickTimer = new System.Timers.Timer(1 * 200);
            _tickTimer.Elapsed += new System.Timers.ElapsedEventHandler(delegate(object source, System.Timers.ElapsedEventArgs ee)
            {
                BeginInvoke(new Action(() =>
                {
                    if (lblMsg.Text.IndexOf(".") == -1)
                    {
                        lblMsg.Text += ".";
                    }
                    else if (lblMsg.Text.IndexOf(".") + 5 == lblMsg.Text.Length)
                    {
                        lblMsg.Text = lblMsg.Text.Substring(0, lblMsg.Text.Length - 5);
                    }
                    else
                    {
                        lblMsg.Text += ".";
                    }
                }));

            });//到达时间的时候执行事件； 
            _tickTimer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)； 
            _tickTimer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件； 

            byte runMode = byte.Parse(strRunMode);//测试1 正式0 
            string objectId = null;
            string objectName = null;
            TreeNode theSelectedNode = ctvObjectNodes.TreeView.SelectedNode;
            if (theSelectedNode != null && theSelectedNode.Name != "default")
            {
                objectId = theSelectedNode.Name;
                objectName = theSelectedNode.Text;
            }
            else
            {
                objectName = "自动";
            }



            HttpAdapter.postAsyncAsJSON(authEndPoint, new { RunMode = runMode, ObjectId = objectId, UserCode = userCode, PasswordHash = MD5Provider.Generate(password) }.ToStringObjectDictionary(), new { ApplicationId = Common.APPLICATION_ID }.ToStringObjectDictionary(), (ret, res) =>
            {
                if ((bool)ret.Success)
                {
                    PensionAgencyVar.UserCode = userCode;
                    PensionAgencyVar.Password = password;
                    PensionAgencyVar.Load(ret.ret);
                    INIAdapter.WriteValue(Common.INI_SECTION_LOCAL, Common.INI_KEY_USER_NAME_SINCE_LAST, userCode, Common.INI_FILE_PATH);
                    INIAdapter.WriteValue(Common.INI_SECTION_LOCAL, Common.INI_KEY_OBJECT_ID_SINCE_LAST, (objectId ?? "default"), Common.INI_FILE_PATH);
                    INIAdapter.WriteValue(Common.INI_SECTION_LOCAL, Common.INI_KEY_OBJECT_NAME_SINCE_LAST, objectName, Common.INI_FILE_PATH);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    lblMsg.Text = ret.ErrorMessage;

                    this.UIInvoke(() =>
                    {
                        btnOk.Enabled = true;

                    });
                }
                _tickTimer.Enabled = false;
                _tickTimer = null;

            });
        }

        #endregion

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtUserName.Text.Trim() == "")
                {
                    MessageBoxAdapter.ShowError("请输入用户名");
                    return;
                }
                if (txtPassword.Text.Trim() == "")
                {
                    MessageBoxAdapter.ShowError("请输入密码");
                    return;
                }
                DoLogin(txtUserName.Text.Trim(), txtPassword.Text.Trim());
            }
        }

       

        
    }
}
