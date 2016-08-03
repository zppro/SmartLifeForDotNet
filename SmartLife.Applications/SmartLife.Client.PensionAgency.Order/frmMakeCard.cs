using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Hweny.TabSlider.TabSlideControl;
using Hweny.TabSlider;
using win.core.utils;
using win.tools.ic;
using SmartLife.Client.PensionAgency.Models;

namespace SmartLife.Client.PensionAgency.Order
{
    public partial class frmMakeCard : Form
    {
        public frmMakeCard()
        {
            InitializeComponent(); 
        }

        int pageSize = 20;
        SlideableTabControl xSlideTab;
        List<OldManInfo> dataSource = new List<OldManInfo>();
        private void frmMakeCard_Load(object sender, EventArgs e)
        {
            BuildControls();
        } 
         
        private void BuildControls()
        {

            this.UIDoStepTasks(new List<Func<bool>>(){
                ()=>{
                    xTab.TabPages.Clear();
                    return true;
                },
                ()=>{
                    #region 数据源
                    dataSource.Clear();
                    dataSource.AddRange(Data.OldMans);
                    return true;
                    #endregion 
                    
                },
                ()=>{
                    #region 构建UI
                    //计算页数
                    int needCreateTabPageCount = 1 + dataSource.Count / 20;
                    int padding = 15;
                    int indexPage = 0;

                    int indexOfOldMan = 0;
                    int x, y = 0;
                    do
                    {
                        int currentPageSize = pageSize;
                        xTab.TabPages.Add(indexPage.ToString());
                        needCreateTabPageCount--;
                        if (needCreateTabPageCount == 0)
                        {
                            currentPageSize = dataSource.Count % pageSize;
                        }

                        TabPage tb = xTab.TabPages[indexPage];
                        tb.BackColor = Color.FromArgb(129, 61, 222);
                        int i = 0;
                        for (indexOfOldMan = indexPage * pageSize + i; i < currentPageSize; i++)
                        {
                            OldManInfo oldman = dataSource[indexOfOldMan];


                            x = i % 4;
                            y = i / 4;

                            ucOldManCard card = new ucOldManCard();

                            card.BackColor = string.IsNullOrEmpty(oldman.ICNo) ? Color.Silver : Color.FromArgb(255, 192, 192);
                            card.Location = new Point(padding * (x + 1) + x * card.Size.Width, padding * (y + 1) + y * card.Size.Height);
                            
                            card.ClickLabel.Text = oldman.OldManName;
                            card.ClickLabel.Tag = oldman.OldManId;

                            tb.Controls.Add(card);

                             
                            indexOfOldMan++;

                        }

                        indexPage++;

                    } while (needCreateTabPageCount > 0);
                    #endregion
                    return true;
                },
                ()=>{
                     
                    lblPageNo.Text = "1";
                    lblPageAll.Text = "/ "+(dataSource.Count / pageSize + 1).ToString();
                    xSlideTab = new SlideableTabControl(xTab); 
                    xSlideTab.SlidePageEamless = true;
                     
                    return true;
                },
                ()=>{
                    #region 绑定卡片点击事件
                    ThreadAdapter.DoOnceTask(() =>
                    {
                        foreach (TabPage page in xTab.TabPages)
                        {
                            Common.DebugLog(page.Text + ":" + page.Controls.Count);
                            foreach (ucOldManCard card in page.Controls)
                            {
                                card.ClickLabel.Click += new EventHandler((o, de) =>
                                {
                                    API icAPI = new API();
                                    icAPI.InitIC();
                                    if (icAPI.IcDev < 0)
                                    {
                                        MessageBoxAdapter.ShowError("初始化IC读卡设备失败，请确认是否已连接IC读卡设备");
                                        return;
                                    }
                                    frmCardView frm = new frmCardView();
                                    frm.LoadInfo(icAPI, (string)(o as Control).Tag);
                                    DialogResult result = frm.ShowDialog();
                                    if(result == System.Windows.Forms.DialogResult.OK){
                                        (o as Control).Parent.BackColor = Color.FromArgb(255, 192, 192);
                                    }
                                    icAPI.ExitIC();
                                });
                            }
                        }
                    });
                    return true;
                    #endregion
                }
            }, 0); 

            
        }

        private void btnLeft_Click(object sender, EventArgs e)
        { 
            if (xTab.SelectedIndex > 0)
            {
                lblPageNo.Text = xTab.SelectedIndex.ToString();
                xTab.SelectedIndex--;
                
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        { 
            if (xTab.SelectedIndex < xTab.TabPages.Count - 1)
            {
                lblPageNo.Text = xTab.SelectedIndex.ToString();
                xTab.SelectedIndex++;
            }
        }
         
    }
}
