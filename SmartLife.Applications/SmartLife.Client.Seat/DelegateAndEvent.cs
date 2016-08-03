using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartLife.Share.Models.PO.eComm;

namespace SmartLife.Client.Seat
{
    #region 平台准备完毕
    public delegate void dPlatformReady(object source, PlatformReadyEventArgs e);
    public class PlatformReadyEventArgs : EventArgs
    {
        public string PlatformAddress { get; set; }
    }
    #endregion

    #region 设置
    public delegate void dSettingsSave(object source, SettingsSaveEventArgs e);
    public class SettingsSaveEventArgs : EventArgs
    {
        public bool ServiceOnline { get; set; }
    }

    //
    public delegate void dPlayBackTone(object source, PlayBackToneEventArgs e);
    public class PlayBackToneEventArgs : EventArgs
    {
        public bool PlayTone { get; set; }
        public string StartPlayTime { get; set; }
        public string EndPlayTime { get; set; }
    }
    #endregion

    #region 发起呼叫
    public delegate void dMakeCall(object source, MakeCallEventArgs e);
    public class MakeCallEventArgs : EventArgs
    {
        public Guid? CallServiceId { get; set; }
        public string Uuid { get; set; }
        public int PhoneType { get; set; }
        public string PhoneNo { get; set; }
        public string Message { get; set; }
        public bool Cancel { get; set; }
    }
    #endregion

    #region 移除呼叫
    public delegate void dRemoveCall(object source, RemoveCallEventArgs e);
    public class RemoveCallEventArgs : EventArgs
    {
        public Guid? CallServiceId { get; set; }
        public int PhoneType { get; set; }
        public string PhoneNo { get; set; }
        public string Message { get; set; }
        public bool Cancel { get; set; }
    }
    #endregion

    #region 弹屏窗口关闭
    public delegate void dCTIWinClose(object source, EventArgs e);
    #endregion

    #region 呼叫保持
    public delegate void dInvokeHoldCall(object source, EventArgs e);
    #endregion

    #region 呼叫保持恢复
    public delegate void dInvokeRetreiveCall(object source, EventArgs e);
    #endregion

    #region 呼叫转接
    public delegate void dInvokeTransfer(object source, EventArgs e); 
    #endregion

    #region 双方通话消息处理
    public delegate void dOnCallRing(string extNo,string caller, string callee, string uuid);
    public delegate void dOnCallBusy(string extNo, string caller, string callee, string uuid);
    public delegate void dOnCallHold(string extNo, string caller, string callee, string uuid);
    public delegate void dOnCallIdle(string extNo, string caller, string callee, string uuid);
    #endregion

    #region 多方通话消息处理
    public delegate void dOnConferenceCreate(string conferenceName, string updateTime);
    public delegate void dOnConferenceDestroy(string conferenceName, string updateTime);
    public delegate void dOnConferenceMemberAdd(string conferenceName, string updateTime,string phoneNo,string memberId);
    public delegate void dOnConferenceMemberRemove(string conferenceName, string updateTime, string phoneNo);
    #endregion

    #region 微信
    public delegate void dOnGetWeiXinClientSuccess(dynamic openIds);
    #endregion
}
