using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// NavBase 的摘要说明
/// </summary>
public class NavBaseControl : System.Web.UI.UserControl
{
    /// <summary>
    /// 获取到Business的服务
    /// </summary>
    protected ESB.UddiService uddiService = new ESB.UddiService();

    /// <summary>
    /// 获取到当前页面的名称
    /// </summary>
    protected String CurrentPageName
    {
        get
        {
            string currentFilePath = HttpContext.Current.Request.FilePath;
            return currentFilePath.Substring(currentFilePath.LastIndexOf("/") + 1); ;
        }
    }

    public NavBaseControl()
	{
    }

    /// <summary>
    /// 导航条列表
    /// </summary>
    protected List<NavItem> Nav { set; get; }

    /// <summary>
    /// 选中值
    /// </summary>
    public String SelectValue
    {
        get
        {
            if (String.IsNullOrEmpty(Request["BusinessID"]))
            {
                return "All";
            }
            else
            {
                return Request["BusinessID"];
            }
        }
    }

    /// <summary>
    /// 导航条数据模型
    /// </summary>
    public class NavItem
    {
        public String ID { get; set; }
        public String Name { get; set; }
        public String Class { get; set; }
    }
}