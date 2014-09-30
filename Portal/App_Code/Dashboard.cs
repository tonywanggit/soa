using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// 看板演示通用类
/// </summary>
public class Dashboard
{
    ESB.UddiService uddiService = new ESB.UddiService();

	public Dashboard()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 获取到服务提供者导航
    /// </summary>
    /// <returns></returns>
    public String GetProviderNav(String serivceID, String pageName)
    {
        StringBuilder sbNav = new StringBuilder();

        if (serivceID == "All")
            sbNav.AppendFormat(@"<li class=""active""><a href=""{0}?ServiceID={1}"">概览</a></li>", pageName, serivceID);
        else
            sbNav.Append(@"<li><a href=""#"">概览</a></li>");


        String nav = @"<li class=""active""><a href=""#"">概览</a></li>
            <li><a href=""Page?asp=1"">微信商城</a></li>
            <li><a href=""#"">企业服务总线</a></li>
            <li><a href=""#"">邦购网</a></li>";


        ESB.BusinessEntity[] lstBE = uddiService.GetAllBusinessEntity();

        return nav;
    }
}