using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESB.Core.Configuration
{
    /// <summary>
    /// 保存所有的常量
    /// </summary>
    public class Constant
    {
        /// <summary>
        /// ESB审计消息队列
        /// </summary>
        public const String ESB_AUDIT_QUEUE = "esb.audit";
        /// <summary>
        /// ESB异常消息队列
        /// </summary>
        public const String ESB_EXCEPTION_QUEUE = "esb.exception";
        /// <summary>
        /// ESB队列专用交换机
        /// </summary>
        public const String ESB_INVOKE_QUEUE = "esb.invokeQueue";
        /// <summary>
        /// ESB队列自定义队列：消息需要自行处理
        /// </summary>
        public const String ESB_CUST_INVOKE_QUEUE = "esb.cust.invokeQueue";


        /// <summary>
        /// ContentType: XML
        /// </summary>
        public const String CONTENT_TYPE_XML = "application/xml; charset=utf-8";
        /// <summary>
        /// ContentType: JSON
        /// </summary>
        public const String CONTENT_TYPE_JSON = "application/json; charset=utf-8";
        /// <summary>
        /// ContentType: FORM
        /// </summary>
        public const String CONTENT_TYPE_FROM = "application/x-www-form-urlencoded; charset=utf-8";


        /// <summary>
        /// 公司网址,用于SOAP协议中的基地址
        /// </summary>
        public const String COMPANY_URL = "http://mb.com";

        /// <summary>
        /// ESB返回消息头信息-服务考核开始
        /// </summary>
        public const String  ESB_HEAD_SERVICE_BEGIN = "Esb-ServiceBegin";
        /// <summary>
        /// ESB返回消息头信息-服务考核结束
        /// </summary>
        public const String ESB_HEAD_SERVICE_END = "Esb-ServiceEnd";
        /// <summary>
        /// ESB请求消息头信息-跟踪上下文
        /// </summary>
        public const String ESB_HEAD_TRACE_CONTEXT = "Esb-TraceContext";
        /// <summary>
        /// ESB请求消息头信息-调用方法
        /// </summary>
        public const String ESB_HEAD_ANVOKE_ACTION = "Esb-InvokeAction";

    }
}
