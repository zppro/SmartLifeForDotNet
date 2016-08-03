using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SmartLife.Client.Merchant
{
    public partial class frmWorkOrderNotify : Form
    {

        #region 事件
        public event dNotifyWorkOrderReminderStatInfo ClickReminderStatInfoWin;
        public event dNotifyWorkOrderReminderInfo ClickReminderInfoWin;
        public event dIgnoreWorkOrderReminderStatInfo IgnoreReminderStatInfo;
        public event dIgnoreWorkOrderReminderInfo IgnoreReminderInfo;
        public event Action CloseWin;
        #endregion

        #region Timers
        private System.Timers.Timer displayTimer;
        private System.Timers.Timer iconTimer;
        #endregion

        #region 声明的变量
        
        public static int SW_SHOWNOACTIVATE = 4;//该变量决定窗体的显示方式
        public static int CurrentState;//该变量标识当前窗口状态
        public static bool MainFormFlag = true;
        private System.Drawing.Rectangle Rect;//定义一个存储矩形框的区域
        
        public static bool MouseState; //该变量标识当前鼠标状态
        bool IconFlag = true;//用来标识图标闪动
        public static bool IconFlickerFlag;//运用本标识避免单击“关闭”按钮时弹出信息框

        protected FormState FormNowState { get; set; }
        private WorkOrderReminderInfo _ReminderInfo;
        private WorkOrderReminderStatInfo _ReminderStatInfo;
        #endregion

        #region 声明API函数
        [DllImportAttribute("user32.dll")]
        private static extern Boolean ShowWindow(IntPtr hwnd, Int32 cmdShow);  //该方法用来显示窗体
        #endregion

        #region 定义标识窗体移动状态的枚举值
        protected enum FormState
        {
            //隐藏窗体
            Hide = 0,
            //显示窗体
            Display = 1,
            //隐藏窗体中
            Hiding = 3,
            //显示窗体中
            Displaying = 4,
        }
        #endregion

        #region 鼠标控制图片的变化
        private void pbClose_MouseEnter(object sender, EventArgs e)
        {
            pbClose.Image = xImage.Images[1];  //设定当鼠标进入PictureBox控件时PictureBox控件的图片
        }

        private void pbClose_MouseLeave(object sender, EventArgs e)
        {
            pbClose.Image = xImage.Images[0];  //设定当鼠标离开PictureBox控件时PictureBox控件的图片
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            CloseNewWindow();//调用关闭窗体方法
        }
        #endregion

        public frmWorkOrderNotify()
        {
            InitializeComponent();

            FormNowState = FormState.Hide;
            System.Drawing.Rectangle rect = System.Windows.Forms.Screen.GetWorkingArea(this);//实例化一个当前窗口的对象
            this.Rect = new System.Drawing.Rectangle(rect.Right - this.Width - 1, rect.Bottom - this.Height - 1, this.Width, this.Height); //为实例化的对象创建工作区域
            //setTimeout,setInternal


            displayTimer = new System.Timers.Timer(1000);
            displayTimer.Elapsed += new System.Timers.ElapsedEventHandler(delegate(object source, System.Timers.ElapsedEventArgs ee)
            {
                switch (this.FormNowState)  //判断当前窗体的状态
                {
                    case FormState.Display:  //当窗体处于显示状态时
                        BeginInvoke(new Action(() =>
                        {
                            this.displayTimer.Start();//启动计时器displayCounter
                            this.displayTimer.Interval = 100;//设定计时器的时间事件间隔
                            if (!MouseState)     //当鼠标不在窗体上时
                            {
                                this.FormNowState = FormState.Hiding;//隐藏当前窗体
                            }
                            this.displayTimer.Start();            //启动计时器displayCounter
                        }));
                        
                        break;
                    case FormState.Displaying:                  //当窗体处于显示中状态时
                        BeginInvoke(new Action(() =>
                        {
                            if (this.Height <= this.Rect.Height - 12) //如果窗体没有完全显示
                            {
                                this.SetBounds(Rect.X, this.Top - 12, Rect.Width, this.Height + 12);//设定窗体的边界
                            }
                            else                                     //当窗体完全显示时
                            {
                                displayTimer.Stop();                //停止计时器displayCounter

                                this.SetBounds(Rect.X, Rect.Y, Rect.Width, Rect.Height);//设定当前窗体的边界

                                this.FormNowState = FormState.Display;  //修改当前窗体所处的状态值
                                this.displayTimer.Interval = 5000;    //设定计时器的时间事件间隔
                                this.displayTimer.Start();           //启动计时器低displayCounter
                            }
                        }));
                        
                        break;
                    case FormState.Hiding:                       //当窗体处于隐藏中时
                        BeginInvoke(new Action(() =>
                        {

                            if (MouseState)                             //当鼠标在窗体上边时
                            {
                                this.FormNowState = FormState.Displaying; //修改窗体的状态为显示中
                            }
                            else                                         //当鼠标离开窗体时
                            {

                                if (this.Top <= this.Rect.Bottom - 12)        //当窗体没有完全隐藏时
                                {
                                    this.SetBounds(Rect.X, this.Top + 12, Rect.Width, this.Height - 12);//设定控件的边界
                                }
                                else                                    //当窗体完全隐藏时
                                {
                                    this.FormNowState = FormState.Hide;     //设定当前的窗体状态
                                    this.CloseNewWindow();
                                    /*
                                    this.Hide();                         //隐藏当前窗体
                                   

                                    if (HideWin != null)
                                    {
                                        HideWin();
                                    }
                                    */
                                }
                            }
                        }));
                       
                        break;
                }
            });
            displayTimer.Start();

             


        }

        #region 显示窗体
        public void ShowNewWindow()
        {
            switch (FormNowState) //判断当前窗体处于那种状态
            {
                case FormState.Hide://当提示窗体的状态为隐藏时
                    this.FormNowState = FormState.Displaying;//设置提示窗体的状态为显示中
                    this.SetBounds(Rect.X, Rect.Y + Rect.Height, Rect.Width, 0);//显示提示窗体，并把它放在屏幕底端
                    ShowWindow(this.Handle, 4);      //显示窗体
                    displayTimer.Interval = 100;   //设定时间事件的频率为100ms一次
                    displayTimer.Start();         //启动计时器displayCounter          
                    break;
                case FormState.Display://当提示窗体的状态为显示时
                    displayTimer.Stop();          //停止计时器displayCounter
                    displayTimer.Interval = 5000;  //设定时间事件的频率为50000ms一次
                    displayTimer.Start();         //启动计时器displayCounter
                    break;
            } 
        }
        #endregion

        #region 关闭窗体
        public void CloseNewWindow()
        {
            base.Hide();//隐藏该窗体
            iconTimer.Enabled = false;//设定计时器iconCounter不可用
            xIcon.Icon = Properties.Resources.empty;//设定托盘图标
            frmWorkOrderNotify.IconFlickerFlag = false;     //更改静态变量IconFlickerFlag的值

            if (CloseWin != null)
            {
                CloseWin();
            }
        }
        #endregion

        public void ReceiveReminderInfo(WorkOrderReminderInfo reminderInfo)//自定义方法用来使托盘图标闪烁
        {
            _ReminderInfo = reminderInfo;
            _ReminderStatInfo = null;
            if (_ReminderInfo != null && frmWorkOrderNotify.IconFlickerFlag != false)     //当托盘闪动图标为真时
            {
                //xIcon.Icon = Properties.Resources.leblue;//托盘图标显示为图像
                //iconTimer.Enabled = true;//启动托盘图标的Timer 
                BeginInvoke(new Action(() =>
                {
                    lblMessage.Text = _ReminderInfo.RemindContent;//在cententInform中显示通知内容

                    ShowNewWindow();//调用显示窗体方法
                })); 
            }
        }
        public void ReceiveReminderStatInfo(WorkOrderReminderStatInfo reminderStatInfo)//自定义方法用来使托盘图标闪烁
        {
            _ReminderInfo = null;
            _ReminderStatInfo = reminderStatInfo;
            if (_ReminderStatInfo != null && frmWorkOrderNotify.IconFlickerFlag != false)     //当托盘闪动图标为真时
            {
                //xIcon.Icon = Properties.Resources.leblue;//托盘图标显示为图像
                //iconTimer.Enabled = true;//启动托盘图标的Timer 
                BeginInvoke(new Action(() =>
                {
                    var typeItem = Common.sourceTypes.FirstOrDefault(item => item.Value.ToString() == _ReminderStatInfo.SourceType);

                    lblMessage.Text = "您有" + _ReminderStatInfo.ReminderNum + "条" + typeItem.Text;

                    ShowNewWindow();//调用显示窗体方法
                }));
            }
        }

        private void frmWorkOrderNotify_Load(object sender, EventArgs e)
        {
            
        }

        private void xIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            iconTimer.Enabled = false;//停止闪烁托盘图标计时器
            xIcon.Icon = Properties.Resources.empty;//清空托盘中原有的图像
            ShowNewWindow();//调用显示窗体方法
        }

        private void frmWorkOrderNotify_MouseEnter(object sender, EventArgs e)
        {
            MouseState = true;     //设定bool型变量MouseState的值为真
        }

        private void frmWorkOrderNotify_MouseLeave(object sender, EventArgs e)
        {
            MouseState = false;   //设定bool型变量MouseState的值为假
        }

        private void frmWorkOrderNotify_Click(object sender, EventArgs e)
        {
            base.Hide();//隐藏该窗体
            iconTimer.Enabled = false;//设定计时器iconCounter不可用
            xIcon.Icon = Properties.Resources.empty;//设定托盘图标
            frmWorkOrderNotify.IconFlickerFlag = false;     //更改静态变量IconFlickerFlag的值

            if (SettingsVar.CurrentRemindType == RemindType.详细信息)
            {
                if (ClickReminderInfoWin != null)
                {
                    ClickReminderInfoWin(_ReminderInfo);
                }
            }
            else if (SettingsVar.CurrentRemindType == RemindType.统计信息)
            {
                if (ClickReminderStatInfoWin != null)
                {
                    ClickReminderStatInfoWin(_ReminderStatInfo);
                } 
            }
            
        }

        private void lblMessage_Click(object sender, EventArgs e)
        {
            this.OnClick(new EventArgs());
        }

        private void lbtnIgore_Click(object sender, EventArgs e)
        {
            //忽略提醒
            if (SettingsVar.CurrentRemindType == RemindType.详细信息)
            {
                if (IgnoreReminderInfo != null)
                {
                    IgnoreReminderInfo(_ReminderInfo);
                    this.CloseNewWindow();
                }
            }
            else if (SettingsVar.CurrentRemindType == RemindType.统计信息)
            {
                if (IgnoreReminderStatInfo != null)
                {
                    IgnoreReminderStatInfo(_ReminderStatInfo);
                    this.CloseNewWindow();
                }
            }
            
        }

        
    }
}
