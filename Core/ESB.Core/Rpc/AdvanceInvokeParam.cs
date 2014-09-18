﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESB.Core.Rpc
{
    /// <summary>
    /// 内部的调用参数
    /// </summary>
    internal class AdvanceInvokeParam
    {
        /// <summary>
        /// 客户端应用名称
        /// </summary>
        internal String ConsumerAppName { get; set; }
        /// <summary>
        /// 客户端IP:队列服务调用时需要传递
        /// </summary>
        internal String ConsumerIP { get; set; }
    }
}