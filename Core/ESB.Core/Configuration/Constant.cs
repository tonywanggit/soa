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

    }
}
