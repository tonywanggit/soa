using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace ESB.TestFramework
{
    class MBEmailTest
    {
        public static void DoTest()
        {
            String toEmail = "wangxudong@metersbonwe.com";
            String subject = "MBSOA-每日服务调用报告";
            String content = BuildReportEmail();

            MB.Util.ExtendMessageHelper helper = MB.Util.ExtendMessageHelper.Instance;
            //helper.SendEmail(toEmail, subject, content, true, null);

            MailMessage mailMessage = new MailMessage("erpnet@metersbonwe.com", toEmail, subject, content);
            mailMessage.IsBodyHtml = true;
            //mailMessage.From = new MailAddress("erpnet@metersbonwe.com");
            //MailAddressCollection toList = new MailAddressCollection();
            //toList.Add(new MailAddress(toEmail));


            helper.SendEmail(mailMessage);
        }

        private static String BuildReportEmail()
        {
            StringBuilder sbEmail = new StringBuilder();

            sbEmail.Append(@"<table width=""100%"" style=""border-bottom:5px solid black;font-size:30px;"">
	            <tr>
		            <td>MBSOA V1.0</td>
		            <td style=""text-align:right"">每日服务调用报告</td>
	            </tr>
            </table>
            <table width=""100%"" style=""margin-top:20px"" cellpadding=""0"" cellspacing=""0"">
                <tr>
                    <td width=""80px"" style=""height:30px"">服务名称:</td>
                    <td><b>WXSC_WeiXinServiceForApp</b></td>
                    <td width=""80px"">默认版本:</td>
                    <td width=""60px"">V1</td>
                    <td width=""60px"">管理员:</td>
                    <td width=""80px"">印冬俊</td>
                    <td width=""80px"">创建时间:</td>
                    <td>2014-09-04 11:30</td>
                    <td width=""80px"">发布时间:</td>
                    <td>2014-09-04 11:30</td>
                </tr>
                <tr>
                    <td width=""80px"" style=""height:30px"">统计信息:</td>
                    <td colspan=""9""><b>2014-09-04</b> 调用次数：<b>200</b>次，TPS峰值：<b>12</b>，流量 入：<b>1024KB</b>, 流量 出：<b>2048KB</b></td>
                </tr>
                <tr>
                    <td width=""80px"" style=""vertical-align:top"">调用次数:</td>
                    <td colspan=""9"" style=""padding-bottom:10px""><img src=""http://10.100.20.214/CallCenter/Images/1.jpg""></td>
                </tr>
                <tr>
                    <td width=""80px"" style=""vertical-align:top"">响应时间:</td>
                    <td colspan=""9""><img src=""http://10.100.20.214/CallCenter/Images/2.jpg""></td>
                </tr>
            </table>");


            return sbEmail.ToString();
        }
    }
}
