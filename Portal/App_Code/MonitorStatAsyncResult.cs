using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

/// <summary>
/// 监控统计异步结果类
/// </summary>
public class MonitorStatAsyncResult : IAsyncResult
{
    /// <summary>
    /// 保存所有客户端的异步操作结果
    /// </summary>
    public static List<MonitorStatAsyncResult> Queue = new List<MonitorStatAsyncResult>();

    /// <summary>
    /// 设置所有的客户端结果已经完成
    /// </summary>
    /// <param name="message"></param>
    public static void SetAllResultComplete(String message)
    {
        foreach (var item in Queue)
        {
            item.Message = message;
            item.SetCompleted(true);
        }
    }

    /// <summary>
    /// 是否结束请求
    /// true:完成
    /// false:阻塞
    /// </summary>
    public bool IsCompleted
    {
        get;
        private set;
    }

    public WaitHandle AsyncWaitHandle
    {
        get;
        private set;
    }

    public object AsyncState
    {
        get;
        private set;
    }

    public bool CompletedSynchronously
    {
        get { return false; }
    }

    public HttpContext Context { get; set; }
    public AsyncCallback CallBack { get; set; }

    /// <summary>
    /// 自定义标识
    /// </summary>
    public string SessionId { get; set; }

    /// <summary>
    /// 自定义消息
    /// </summary>
    public string Message { get; set; }

    public MonitorStatAsyncResult(HttpContext context, AsyncCallback cb, string sessionId)
    {
        this.SessionId = sessionId;
        this.Context = context;
        this.CallBack = cb;
        IsCompleted = true;
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="iscompleted">确认下发信息</param>
    public void SetCompleted(bool iscompleted)
    {
        if (iscompleted && this.CallBack != null)
        {
            CallBack(this);
        }
    }
}