using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESB.Core.Rpc
{
    /// <summary>
    /// 通用工具类
    /// </summary>
    public class CommonUtil
    {
        /// <summary>
        /// 针对特殊的XML字符进行编码
        /// </summary>
        /// <param name="rawString"></param>
        /// <returns></returns>
        public static String XmlEncoding(String rawString)
        {
            if (String.IsNullOrEmpty(rawString))
                return rawString;

            return rawString.Replace("&", " &amp;").Replace("'", "&apos;")
                .Replace("\"", "&quot;").Replace(">", "&gt;").Replace("<", "&lt;");
        }
    }
}