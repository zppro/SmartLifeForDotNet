using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;

namespace SmartLife.Client.Sms
{
    public enum SmsStatusEnum
    {
        [Description("发送短信成功")]
        smsCode0 = 0,
        [Description("提交参数不能为空")]
        smsCode1 = 1,
        [Description("账号无效或未开户")]
        smsCode2 = 2,
        [Description("账号密码错误")]
        smsCode3 = 3,
        [Description("预约发送时间格式不正确")]
        smsCode4 = 4,
        [Description("IP不合法")]
        smsCode5 = 5,
        [Description("号码中含有无效号码或不在规定的号段")]
        smsCode6 = 6,
        [Description("非法关键字")]
        smsCode7 = 7,
        [Description("内容长度超过上限，最大402字符")]
        smsCode8 = 8,
        [Description("接受号码过多，最大1000")]
        smsCode9 = 9,
        [Description("未知错误10")]
        smsCode10 = 10,
        [Description("提交速度太快")]
        smsCode11 = 11,
        [Description("您尚未订购[普通短信业务]，暂不能发送该类信息")]
        smsCode12 = 12,
        [Description("您的[普通短信业务]剩余数量发送不足，暂不能发送该类信息")]
        smsCode13 = 13,
        [Description("流水号格式不正确")]
        smsCode14 = 14,
        [Description("流水号重复")]
        smsCode15 = 15,
        [Description("超出发送上限（操作员帐户当日发送上限）")]
        smsCode16 = 16,
        [Description("余额不足")]
        smsCode17 = 17,
        [Description("扣费不成功")]
        smsCode18 = 18,
        [Description("未知错误19")]
        smsCode19 = 19,
        [Description("发送失败")]
        smsCode20 = 20,
        [Description("未知错误21")]
        smsCode21 = 21,
        [Description("未知错误22")]
        smsCode22 = 22,
        [Description("未知错误23")]
        smsCode23 = 23,
        [Description("账户状态不正常")]
        smsCode24 = 24,
        [Description("账户权限不足")]
        smsCode25 = 25,
        [Description("需要人工审核")]
        smsCode26 = 26,
        [Description("未知错误27")]
        smsCode27 = 27,
        [Description("发送内容与模板不符")]
        smsCode28 = 28,
        [Description("扩展号太长或不是数字&accnum=")]
        smsCode29 = 29,
        [Description("未知错误30")]
        smsCode30 = 30,
        [Description("未知错误31")]
        smsCode31 = 31,
        [Description("同一号码相同内容发送次数太多")]
        smsCode32 = 32
    }
}
