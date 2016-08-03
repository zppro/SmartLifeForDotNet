using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace CallCenterAPI.TC_IPCC
{
    #region 事件枚举
    public enum MgrEvent
    {
        Event_OnOriginated = 1,//去电呼叫事件
        Event_OnAlerting = 2,//来电去电回铃事件
        Event_OnFailed = 3,
        Event_OnEstablished = 4,//通话建立事件
        Event_OnConnectionCleared = 5,//联结断开事件
        Event_OnCallCleared = 6,//挂机
        Event_OnQueued = 7,//进入队列事件
        Event_OnDiverted = 8,//离开队列事件
        Event_OnLinkDown = 9,
        Event_OnHeld = 10,//呼叫保持事件
        Event_OnPaused = 11,//坐席示闲示忙事件
        Event_OnChanSpy = 12,//坐席监听
        Event_OnConfLeave = 13,//坐席离开会议
        Event_OnConfJoin = 14,//坐席加入会议
        Event_OnConfList = 15,//会议中成员列表
        Event_OnMemberAdded = 16,//队列登入
        Event_OnMemberRemoved = 17,//队列登出
        Event_OnOriginateSucc = 18,//呼叫成功
        Event_OnOriginateFail = 19,//呼叫失败
        Event_OnSendSMS = 20,//发送短信
        Event_OnLeave = 21,//座席离线
        Event_OnPickup = 22,//座席代接通话中
        Event_OnUserAuth = 23,//座席身份验证
        Event_OnHoldCall = 24,// 呼叫保持
        Event_OnRetreiveCall = 25,// 呼叫保持取消
        Event_OnQueueStatus = 26,// 队列状态20110330
        Event_OnQueueMbrStatus = 27,// 队列成员状态20120228
        Event_OnBroadCast = 28,// 消息广播20120411
        Resp_OnPong = 29,// 心跳20120727
        Event_OnConfRecord = 30,//会议录音
        Event_OnAbandon = 31,//来电放弃
        Event_OnKickoff = 32//重复登录
    }

    
    #endregion

    #region 返回结果枚举
    public enum MgrResult
    {
        not_init = -13,
        interface_not_found = -12,
        not_logged_in = -11,
        no_such_queue = -10,
        interface_not_specified = -9,
        queue_not_specified = -8,
        already_logged_in = -7,
        error_not_implemented = -6,
        fails_already_connected = -5,
        invalid_callid = -4,
        socket_error = -3,
        time_out = -2,
        generic_failure = -1,
        ok = 0,
        ok_feature_dialed = 1
    }
    #endregion

    #region 委托
    public delegate void EventCallbackDelegate(int eventType, int watchId, int callId, string caller, string callee); //声明委托 
    #endregion

    public class MgrAPIWrapper
    {
       
        #region 函数

        [DllImport("mgrapi.dll")]
        public static extern int SetEventCallback(EventCallbackDelegate callback);

        /// <summary>
        /// 通讯初始化
        /// </summary>
        /// <returns></returns>
        [DllImport("mgrapi.dll")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool Manager_Init();

        /// <summary>
        /// 通讯连接TC-IPCC服务器
        /// </summary>
        /// <param name="ip">服务器IP</param>
        /// <param name="login">工号</param>
        /// <param name="pwd">密码</param>
        /// <param name="ext">登录分机</param>
        /// <param name="queue">登录队列</param>
        /// <returns></returns>
        [DllImport("mgrapi.dll")]
        public static extern MgrResult Manager_Connect(string ip, string login, string pwd, string ext, string queue);

        /// <summary>
        /// 通讯断开连接TC-IPCC服务器
        /// </summary>
        /// <returns></returns>
        [DllImport("mgrapi.dll")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool Manager_Disconnect();

        /// <summary>
        /// 监听分机事件
        /// </summary>
        /// <param name="dn">座席当前使用通道号</param>
        /// <param name="ext">座席当前使用分机号</param>
        /// <returns>小于零表示监听失败，大于零为当前监听ID </returns>
        [DllImport("mgrapi.dll")]
        public static extern int Manager_OpenMonitor(string dn, string ext);

        /// <summary>
        /// 停止监听分机事件
        /// </summary>
        /// <param name="xref">关闭当前ID的监听</param>
        /// <returns></returns>
        [DllImport("mgrapi.dll")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool Manager_CloseMonitor(int xref);

        /// <summary>
        /// 发起呼叫
        /// </summary>
        /// <param name="from">主叫号码</param>
        /// <param name="to">被叫号码</param>
        /// <param name="context">主叫号码通道号(默认为空)</param>
        /// <param name="calledIdName">被叫名</param>
        /// <returns></returns>
        [DllImport("mgrapi.dll")]
        public static extern MgrResult Manager_MakeCall(string from, string to, string context, string calledIdName);

        /// <summary>
        /// 发起外呼流程
        /// </summary>
        /// <param name="from">外拨号码</param>
        /// <param name="to"></param>
        /// <param name="context">进入流程</param>
        /// <param name="calledIdName">被叫名</param>
        /// <param name="actionid">外拨编号</param>
        /// <returns></returns>
        [DllImport("mgrapi.dll")]
        public static extern MgrResult Manager_MakeOutCall(string from, string to, string context, string calledIdName, string actionid);

        /// <summary>
        /// 通话监听
        /// </summary>
        /// <param name="from">监听分机</param>
        /// <param name="to">被监听分机</param>
        /// <returns></returns>
        [DllImport("mgrapi.dll")]
        public static extern MgrResult Manager_ChanSpy(string from, string to);

        /// <summary>
        /// 通话代接
        /// </summary>
        /// <param name="from">接听分机</param>
        /// <param name="to">振铃分机</param>
        /// <returns></returns>
        [DllImport("mgrapi.dll")]
        public static extern MgrResult Manager_Pickup(string from, string to);

        /// <summary>
        /// 清除呼叫
        /// </summary>
        /// <param name="callId">当前呼叫ID</param>
        /// <returns></returns>
        [DllImport("mgrapi.dll")]
        public static extern MgrResult Manager_ClearCall(int callId);

        /// <summary>
        /// 清除呼叫
        /// </summary>
        /// <returns></returns>
        [DllImport("mgrapi.dll")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool Manager_ClearCurCall();

        /// <summary>
        /// 呼叫转接
        /// </summary>
        /// <param name="callId">当前呼叫ID</param>
        /// <param name="from">当前分机</param>
        /// <param name="to">转移分机</param>
        /// <param name="context"></param>
        /// <returns></returns>
        [DllImport("mgrapi.dll")]
        public static extern MgrResult Manager_FastTransfer(int callId, string from, string to, string context);

        /// <summary>
        /// 呼叫转接
        /// </summary>
        /// <param name="from">当前分机</param>
        /// <param name="to">转移分机</param>
        /// <param name="context"></param>
        /// <returns></returns>
        [DllImport("mgrapi.dll")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool Manager_CurFastTransfer(string from, string to, string context);

        /// <summary>
        /// 呼叫保持
        /// </summary>
        /// <param name="callId">当前呼叫ID</param>
        /// <param name="holdingDevice">当前分机</param>
        /// <param name="holdContext">转移分机</param>
        /// <returns></returns>
        [DllImport("mgrapi.dll")]
        public static extern MgrResult Manager_HoldCall(int callId, string holdingDevice, string holdContext);

        /// <summary>
        /// 呼叫保持
        /// </summary>
        /// <param name="holdingDevice">当前分机</param>
        /// <param name="holdContext">转移分机</param>
        /// <returns></returns>
        [DllImport("mgrapi.dll")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool Manager_CurHoldCall(string holdingDevice, string holdContext);

        /// <summary>
        /// 呼叫恢复
        /// </summary>
        /// <param name="callId">当前呼叫ID</param>
        /// <param name="retreiveDevice">当前分机</param>
        /// <param name="retreiveContext">转移分机</param>
        /// <returns></returns>
        [DllImport("mgrapi.dll")]
        public static extern MgrResult Manager_RetreiveCall(int callId, string retreiveDevice, string retreiveContext);

        /// <summary>
        /// 呼叫恢复
        /// </summary>
        /// <param name="retreiveDevice">当前分机</param>
        /// <param name="retreiveContext">转移分机</param>
        /// <returns></returns>
        [DllImport("mgrapi.dll")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool Manager_CurRetreiveCall(string retreiveDevice, string retreiveContext);


        /// <summary>
        /// 登入队列
        /// </summary>
        /// <param name="extension">分机号</param>
        /// <param name="queue">队列号</param>
        /// <param name="id">登入编号</param>
        /// <param name="agentid">坐席工号</param>
        /// <param name="penalty">登入队列技能等级1-9</param>
        /// <returns></returns>
        [DllImport("mgrapi.dll")]
        public static extern MgrResult Manager_QueueAdd(string extension, string queue, string id, string agentid, int penalty);

        /// <summary>
        /// 退出队列
        /// </summary>
        /// <param name="extension">分机号</param>
        /// <param name="queue">队列号</param>
        /// <param name="id">登出编号和登入编号一致</param>
        /// <param name="agentid">坐席工号</param>
        /// <returns></returns>
        [DllImport("mgrapi.dll")]
        public static extern MgrResult Manager_QueueRemove(string extension, string queue, string id, string agentid);

        /// <summary>
        /// 示闲、示忙
        /// </summary>
        /// <param name="extension">分机号</param>
        /// <param name="queue">队列号</param>
        /// <param name="id">登出编号和登入编号一致</param>
        /// <param name="agentid">坐席工号</param>
        /// <param name="pause">true示忙，false示闲</param>
        /// <returns></returns>
        [DllImport("mgrapi.dll")]
        public static extern MgrResult Manager_QueuePause(string extension, string queue, string id, string agentid, byte pause);

        /// <summary>
        /// 创建会议 备注：如果是被邀请者是非SIP类型分机，参数6中需要携带完整通道号
        /// </summary>
        /// <param name="callId">当前分机通话callid</param>
        /// <param name="inviter">被邀请分机</param>
        /// <param name="caller">通话主叫</param>
        /// <param name="callee">通话被叫</param>
        /// <param name="confnum">会议号</param>
        /// <param name="dial_inviter">被邀请者通道号(默认为空)</param>
        /// <returns></returns>
        [DllImport("mgrapi.dll")]
        public static extern MgrResult Manager_CreatConf(int callId, string inviter, string caller, string callee, string confnum, string dial_inviter);

        /// <summary>
        /// 解散会议
        /// </summary>
        /// <param name="confnum">会议号</param>
        /// <returns></returns>
        [DllImport("mgrapi.dll")]
        public static extern MgrResult Manager_ConfKickAll(string confnum);

        /// <summary>
        /// 邀请会议 备注：如果是被邀请者是非SIP类型分机，参数3中需要携带完整通道号
        /// </summary>
        /// <param name="inviter">被邀请分机号</param>
        /// <param name="confnum">会议号</param>
        /// <param name="dial_inviter">被邀请者通道号(默认为空)</param>
        /// <returns></returns>
        [DllImport("mgrapi.dll")]
        public static extern MgrResult Manager_ConfInvite(string inviter, string confnum, string dial_inviter);

        /// <summary>
        /// 查看会议成员列表
        /// </summary>
        /// <param name="confnum">会议号</param>
        /// <returns></returns>
        [DllImport("mgrapi.dll")]
        public static extern MgrResult Manager_ConfList(string confnum);

        /// <summary>
        /// 踢出成员
        /// </summary>
        /// <param name="confnum">会议号</param>
        /// <param name="user">成员号码</param>
        /// <returns></returns>
        [DllImport("mgrapi.dll")]
        public static extern MgrResult Manager_ConfKickUser(string confnum, string user);

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="dstnum">短信接收号码（如8613588136062）</param>
        /// <param name="msg">短信内容</param>
        /// <param name="id">短信发送编号</param>
        /// <returns></returns>
        [DllImport("mgrapi.dll")]
        public static extern MgrResult Manager_SendSMS(string dstnum, string msg, string id);

        /// <summary>
        /// 发起心跳
        /// </summary>
        /// <param name="agentid">坐席编号</param>
        /// <returns></returns>
        [DllImport("mgrapi.dll")]
        public static extern MgrResult Manager_Ping(string agentid);
        #endregion

    }
}
