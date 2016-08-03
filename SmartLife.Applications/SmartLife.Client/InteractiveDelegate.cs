using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLife.Client
{
    public delegate void dTransferPhone(object source, TransferPhoneEventArgs e);
    public delegate void dRemovePhone(object source, RemovePhoneEventArgs e);
    public delegate void dCallServiceWinClose(object source, EventArgs e);
    public delegate void dCallServiceInfoWinClose(object source, EventArgs e);

    public delegate void dRemoteHostConnect(object source, RemoteHostConnectEventArgs e);

    public delegate void dCallCenterSettingsSave(object source,CallCenterSettingsSaveEventArgs e);

    public delegate void dOpenNewWinAsCallServiceFromClient(string caller,string callee,string queue);

    public delegate void dConfJoin(string telInfo);

    public delegate void dConfLeave(string telInfo);

    public delegate string dGetQueueNoAndCallee();

    public delegate void dAddServiceVoice(string strCallServiceId,int voiceType,string address,string logContent);

    public delegate void dFinishServiceVoice(string strCallServiceId, int voiceType, string ipAddress ,string logContent);

    public delegate void dChangeBusyFree(int status);

    public delegate void dHoldCall();

    public delegate void dRetreiveCall();

    public delegate void dInvokeHoldCall(object source, EventArgs e);

    public delegate void dInvokeRetreiveCall(object source, EventArgs e);

    public delegate void dInvokeFastTransfer(object source, FastTransferEventArgs e);

    public delegate void dCloseWinCallServiceToProcessing();

    public delegate void dCloseWinWorkOrder();

    public delegate void dSetCallback();

    public delegate void dLostConnect();

    public delegate void dAbandon(string queueNo,string callNo);


    public delegate void dAlerting();

    public delegate void dEstablished();

    public delegate void dCancelCallBySeat();

    public delegate void dDialBackNoWin(object source, DialBackNoWinEventArgs e);
}
