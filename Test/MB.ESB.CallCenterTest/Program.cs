using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MB.ESB.CallCenterTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //--获取到调用中心客户端代理
            CallCenterProxy ccProxy = CallCenterProxy.GetInstance();

            Console.WriteLine("默认的超时：{0}, 你可以手工进行设置客户端超时时间, 但是不要超过100*1000。", ccProxy.Timeout);
            //--配置超时为60s
            ccProxy.Timeout = 60 * 1000;

            String servieName = "ESB_ASHX";
            String methodName = "HelloAction";

            //--CallCenter目前最大数据支持4096KB, 同时还取决于服务提供者的配置
            String messageBigData = new String('T', 1024 * 1024 * 4 - 1000);
            String messageNoEncode = "TEST1=1&CallTime=2&Chinese=中国#!*%\\";

            //--大数据测试
            String resBigData = ccProxy.Invoke(servieName, methodName, messageBigData, 1);
            if (resBigData == String.Format("收到参数：HelloAction={0}。", messageBigData))
            {
                Console.WriteLine("大数据测试成功，数量为：1024 * 1024 * 4 - 1000字节。");
            }

            //--特殊字符测试
            String resNoEncode = ccProxy.Invoke(servieName, methodName, messageNoEncode, 1);
            if (resNoEncode == String.Format("收到参数：HelloAction={0}。", messageNoEncode))
            {
                Console.WriteLine("特殊字符测试成功，特殊字符：{0}", messageNoEncode);
            }

            //--传参不对造成的异常：版本号不对
            try
            {
                ccProxy.Invoke(servieName, methodName, messageNoEncode, 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine("异常测试：{0}", ex.Message);
            }

            //--强制弃用缓存用例
            ccProxy.Invoke(servieName, methodName, messageNoEncode, 0, true);

            //--重新使用缓存用例
            ccProxy.Invoke(servieName, methodName, messageNoEncode, 0, false);


            //--队列调用测试:失败用例
            try
            {
                ccProxy.InvokeQueue(servieName, methodName, messageNoEncode, 1);
                Console.WriteLine("队列调用测试成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine("队列调用测试失败：{0}", ex.Message);
            }


            //--队列调用测试:成功用例
            try
            {
                ccProxy.InvokeQueue("ESB_QUEUE_10", methodName, messageNoEncode, 1);
                Console.WriteLine("队列调用测试成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine("队列调用测试失败：{0}", ex.Message);
            }

            //--微信商城测试用例GET用例
            servieName = "WXSC_WeiXinServiceForApp";
            methodName = "GET:JSON:CollocationFilter";
            String messageWX = "id=227";

            String responseWX = ccProxy.Invoke(servieName, methodName, messageWX);
            Console.WriteLine("微信商城GET服务返回：{0}", responseWX);

            //--微信商城测试用例POST用例
            servieName = "WXSC_WeiXinServiceForApp";
            methodName = "POST:JSON:WxSellerChangeCashPws";
            messageWX = @"{""accountId"":325,""oldPws"":""123456"",""newPws"":""123456""}";

            responseWX = ccProxy.Invoke(servieName, methodName, messageWX);
            Console.WriteLine("微信商城POST服务返回：{0}", responseWX);


            //--WXSC_Product测试用例POST用例
            servieName = "WXSC_Product";
            methodName = "GET:JSON:ProductCategoryClsFilter";
            messageWX = @"{""Route"":""ProductCategoryClsFilter"",""CategoryId"":10,""PageIndex"":0,""PageSize"":50}";

            responseWX = ccProxy.Invoke(servieName, methodName, messageWX);
            Console.WriteLine("微信商城POST服务返回：{0}", responseWX);



            Console.ReadKey();
        }
    }
}
