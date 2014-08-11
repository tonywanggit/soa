using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace JN.ESB.Core.Service.Common
{

    public enum 异常类型枚举
    {
        请求消息格式错误,
        返回消息格式错误,
        目标系统无法连接,
        目标名称无法解析,        
        目标系统无此方法,
        正文部分为空,
        无法找到目标地址,
        找不到服务或该服务已停用,
        权限不足,
        操作超时,
        其他异常
    }

    public enum 异常级别枚举
    {
        一般,
        重要,
        严重,
    }
    public class 异常类型
    {
        public 异常类型()
        {
        }
        public int 类型编码;
        public string 类型描述;
        public int 异常级别;
        //public string 级别描述;
    }

    public static class 异常类型处理
    {
        
        public static 异常类型 获取异常类型(string 异常消息)
        {
            
            try
            {
                异常类型 异常类型实体 = new 异常类型();
                if (Regex.IsMatch(异常消息, "无法解析此远程名称", RegexOptions.IgnoreCase))
                {
                    异常类型实体.类型编码 = (int)异常类型枚举.目标名称无法解析;
                    异常类型实体.异常级别 = (int)异常级别枚举.严重;
                    异常类型实体.类型描述 = 异常类型枚举.目标名称无法解析.ToString();
                    
                    return 异常类型实体;
                }

                if (Regex.IsMatch(异常消息, "无法连接到远程服务器", RegexOptions.IgnoreCase))
                {
                    异常类型实体.类型编码 = (int)异常类型枚举.目标系统无法连接;
                    异常类型实体.异常级别 = (int)异常级别枚举.严重;
                    异常类型实体.类型描述 = 异常类型枚举.目标系统无法连接.ToString();
                    
                    return 异常类型实体;
                }

                if (Regex.IsMatch(异常消息, "Unauthorized", RegexOptions.IgnoreCase))
                {
                    异常类型实体.类型编码 = (int)异常类型枚举.权限不足;
                    异常类型实体.异常级别 = (int)异常级别枚举.严重;
                    异常类型实体.类型描述 = 异常类型枚举.权限不足.ToString();
                    
                    return 异常类型实体;
                }

                if (Regex.IsMatch(异常消息, "UDDI", RegexOptions.IgnoreCase))
                {
                    异常类型实体.类型编码 = (int)异常类型枚举.找不到服务或该服务已停用;
                    异常类型实体.异常级别 = (int)异常级别枚举.严重;
                    异常类型实体.类型描述 = 异常类型枚举.找不到服务或该服务已停用.ToString();

                    return 异常类型实体;
                }

                //if (Regex.IsMatch(异常消息, "停用", RegexOptions.IgnoreCase))
                //{
                //    异常类型实体.类型编码 = (int)异常类型枚举.目标系统已停用;
                //    异常类型实体.异常级别 = (int)异常级别枚举.严重;
                //    异常类型实体.类型描述 = 异常类型枚举.目标系统已停用.ToString();

                //    return 异常类型实体;
                //}

                if (Regex.IsMatch(异常消息, "正文部分为空", RegexOptions.IgnoreCase))
                {
                    异常类型实体.类型编码 = (int)异常类型枚举.正文部分为空;
                    异常类型实体.异常级别 = (int)异常级别枚举.严重;
                    异常类型实体.类型描述 = 异常类型枚举.正文部分为空.ToString();

                    return 异常类型实体;
                }

                if (Regex.IsMatch(异常消息, "Not found", RegexOptions.IgnoreCase))
                {
                    异常类型实体.类型编码 = (int)异常类型枚举.无法找到目标地址;
                    异常类型实体.异常级别 = (int)异常级别枚举.严重;
                    异常类型实体.类型描述 = 异常类型枚举.无法找到目标地址.ToString();

                    return 异常类型实体;
                }

                if (Regex.IsMatch(异常消息, "超时", RegexOptions.IgnoreCase))
                {
                    异常类型实体.类型编码 = (int)异常类型枚举.操作超时;
                    异常类型实体.异常级别 = (int)异常级别枚举.严重;
                    异常类型实体.类型描述 = 异常类型枚举.操作超时.ToString();

                    return 异常类型实体;
                }

                if (Regex.IsMatch(异常消息, "服务器未能识别 HTTP 头", RegexOptions.IgnoreCase))
                {
                    异常类型实体.类型编码 = (int)异常类型枚举.目标系统无此方法;
                    异常类型实体.异常级别 = (int)异常级别枚举.严重;
                    异常类型实体.类型描述 = 异常类型枚举.目标系统无此方法.ToString();

                    return 异常类型实体;
                }
                
                异常类型实体.类型编码 = (int)异常类型枚举.其他异常;
                异常类型实体.异常级别 = (int)异常级别枚举.严重;
                异常类型实体.类型描述 = 异常类型枚举.其他异常.ToString();
                
                return 异常类型实体;
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public static int 获取类型编码(string 异常消息)
        {
            try
            {
                return 获取异常类型(异常消息).类型编码;
            }
            catch 
            {
                return (int)异常类型枚举.其他异常;
            }
        }

        public static int 获取异常级别(string 异常消息)
        {
            try
            {
                return 获取异常类型(异常消息).异常级别;
            }
            catch
            {
                return (int)异常级别枚举.重要;
            }
        }

        public static string 获取类型描述(string 异常消息)
        {
            try
            {
                return 获取异常类型(异常消息).类型描述;
            }
            catch
            {
                return null;
            }
        }

        
    }
}
