using ESB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_Index : System.Web.UI.Page
{
    MonitorStatService msService = new MonitorStatService();

    protected Int32 m_CallHitCacheNum = 0;
    protected Int32 m_InvokeSuccessNum = 0;
    protected Int32 m_InvokeFailureNum = 0;
    protected Int32 m_InvokeQueueSuccessNum = 0;
    protected Int32 m_InvokeQueueFailureNum = 0;
    protected Int32 m_ServiceCallNum = 0;
    protected Int32 m_ServiceNum = 0;
    protected Int32 m_ContractNum = 0;
    protected Int32 m_ContractNoDocNum = 0;

    
    protected Int32 m_ServicePercent = 0;   //--服务调用百分比
    protected Int32 m_ContractPercent = 0;  //--契约未文档化百分比
    protected Int32 m_InvokePercent = 0;    //--调用（非队列）成功百分比
    protected Int32 m_InvokeQueuePercent = 0;    //--调用（队列）成功百分比
    protected Int32 m_CachePercent = 0;     //--缓存命中百分比
    protected Int32 m_ExceptionPercent = 0; //--异常百分比

    protected String m_BusinessID;

    protected void Page_Load(object sender, EventArgs e)
    {
        m_BusinessID = "All";
        if (!String.IsNullOrEmpty(Request["BusinessID"]))
        {
            m_BusinessID = Request["BusinessID"];
        }

        if (Request["Action"] == "GetChartData")
        {
            Response.ContentEncoding = Encoding.UTF8;
            Response.ContentType = "application/json";
            Response.Write(GetChartData());
            Response.End();
        }
        else
        {
            InitOverviewData();
            InitControl();
        }
    }

    /// <summary>
    /// 初始化控件
    /// </summary>
    private void InitControl()
    {
        this.osService.Title = "Service";
        this.osService.Value = m_ServiceNum;
        this.osService.Percent = m_ServicePercent;
        this.osService.Chart = Dashboard_ctl_OverviewStat.ChartType.ok;
        this.osService.ChartData = "5, 6, 7, 11, 14, 10, 15, 19, 15, 2 ";
        this.osService.ChartLOB = Dashboard_ctl_OverviewStat.ChartLineOrBar.line;
        this.osService.ProgressBar = Dashboard_ctl_OverviewStat.ProgressBarType.info;

        this.osContract.Title = "Contract";
        this.osContract.Value = m_ContractNum;
        this.osContract.Percent = m_ContractPercent;
        this.osContract.Chart = Dashboard_ctl_OverviewStat.ChartType.good;
        this.osContract.ChartData = "2, 6, 8, 12, 11, 15, 16, 11, 16, 11, 10, 3, 7, 8, 12, 19";
        this.osContract.ChartLOB = Dashboard_ctl_OverviewStat.ChartLineOrBar.line;
        this.osContract.ProgressBar = Dashboard_ctl_OverviewStat.ProgressBarType.danger;

        this.osInvoke.Title = "Invoke";
        this.osInvoke.Value = m_InvokeSuccessNum;
        this.osInvoke.Percent = m_InvokePercent;
        this.osInvoke.Chart = Dashboard_ctl_OverviewStat.ChartType.bad;
        this.osInvoke.ChartLOB = Dashboard_ctl_OverviewStat.ChartLineOrBar.line;
        this.osInvoke.ChartData = "2, 6, 8, 11, 14, 11, 12, 13, 15, 12, 9, 5, 11, 12, 15, 9, 3";
        this.osInvoke.ProgressBar = Dashboard_ctl_OverviewStat.ProgressBarType.success;

        this.osInvokeQueue.Title = "InvokeQueue";
        this.osInvokeQueue.TitleFontSize = 13;
        this.osInvokeQueue.Value = m_InvokeQueueSuccessNum;
        this.osInvokeQueue.Percent = m_InvokeQueuePercent;
        this.osInvokeQueue.Chart = Dashboard_ctl_OverviewStat.ChartType.good;
        this.osInvokeQueue.ChartLOB = Dashboard_ctl_OverviewStat.ChartLineOrBar.bar;
        this.osInvokeQueue.ChartData = "1, 4, 9, 12, 10, 11, 12, 15, 12, 11, 9, 12, 15, 19, 14, 13, 15";
        this.osInvokeQueue.ProgressBar = Dashboard_ctl_OverviewStat.ProgressBarType.warning;

        this.osCache.Title = "CacheHit";
        this.osCache.Value = m_CallHitCacheNum;
        this.osCache.Percent = m_CachePercent;
        this.osCache.Chart = Dashboard_ctl_OverviewStat.ChartType.ok;
        this.osCache.ChartLOB = Dashboard_ctl_OverviewStat.ChartLineOrBar.line;
        this.osCache.ChartData = "2, 6, 8, 12, 11, 15, 16, 17, 14, 12, 10, 8, 10, 2, 4, 12, 19";
        this.osCache.ProgressBar = Dashboard_ctl_OverviewStat.ProgressBarType.success;

        this.osException.Title = "Exception";
        this.osException.Value = m_InvokeFailureNum + m_InvokeQueueFailureNum;
        this.osException.Percent = m_ExceptionPercent;
        this.osException.Chart = Dashboard_ctl_OverviewStat.ChartType.bad;
        this.osException.ChartLOB = Dashboard_ctl_OverviewStat.ChartLineOrBar.line;
        this.osException.ChartData = "1,7,9,11, 14, 12, 6, 7, 4, 2, 9, 8, 11, 12, 14, 12, 10 ";
        this.osException.ProgressBar = Dashboard_ctl_OverviewStat.ProgressBarType.danger;

    }

    /// <summary>
    /// 初始化统计数据
    /// </summary>
    private void InitOverviewData()
    {
        DataSet dsOverView = msService.GetDashboardOverview(m_BusinessID);

        //--获取到非队列的调用数量
        Int32 rowsCount1 = dsOverView.Tables[0].Rows.Count;
        if (rowsCount1 > 0)
        {
            for (int i = 0; i < rowsCount1; i++)
            {
                if (dsOverView.Tables[0].Rows[i][0].ToString() == "1")
                {
                    m_InvokeSuccessNum += Int32.Parse(dsOverView.Tables[0].Rows[i][1].ToString());
                }
                else
                {
                    m_InvokeFailureNum += Int32.Parse(dsOverView.Tables[0].Rows[i][1].ToString());
                }
            }
        }

        //--获取到队列的调用数量
        Int32 rowsCount2 = dsOverView.Tables[1].Rows.Count;
        if (rowsCount2 > 0)
        {
            for (int i = 0; i < rowsCount2; i++)
            {
                if (dsOverView.Tables[1].Rows[i][0].ToString() == "1")
                {
                    m_InvokeQueueSuccessNum += Int32.Parse(dsOverView.Tables[1].Rows[i][1].ToString());
                }
                else
                {
                    m_InvokeQueueFailureNum += Int32.Parse(dsOverView.Tables[1].Rows[i][1].ToString());
                }
            }
        }

        //--获取到缓存命中的调用数量
        Int32 rowsCount3 = dsOverView.Tables[2].Rows.Count;
        if (rowsCount3 > 0)
        {
            m_CallHitCacheNum += Int32.Parse(dsOverView.Tables[2].Rows[0][0].ToString());
        }

        //--获取到当天被调用的服务数量
        Int32 rowsCount4 = dsOverView.Tables[3].Rows.Count;
        if (rowsCount4 > 0)
        {
            m_ServiceCallNum += rowsCount4;
        }

        //--获取到服务总数量
        Int32 rowsCount5 = dsOverView.Tables[4].Rows.Count;
        if (rowsCount5 > 0)
        {
            m_ServiceNum += Int32.Parse(dsOverView.Tables[4].Rows[0][0].ToString());
        }

        //--获取到契约总数量
        Int32 rowsCount6 = dsOverView.Tables[5].Rows.Count;
        if (rowsCount6 > 0)
        {
            m_ContractNum += Int32.Parse(dsOverView.Tables[5].Rows[0][0].ToString());
        }

        //--获取没有被契约化的方法数量
        Int32 rowsCount7 = dsOverView.Tables[6].Rows.Count;
        if (rowsCount7 > 0)
        {
            m_ContractNoDocNum += Int32.Parse(dsOverView.Tables[6].Rows[0][0].ToString());
        }

        //--服务调用率
        if (m_ServiceNum == 0)
            m_ServicePercent = 0;
        else
            m_ServicePercent = (Int32)Math.Floor((m_ServiceCallNum + 0d) / m_ServiceNum * 100);

        //--契约未文档化百分比
        if (m_ContractNum == 0)
        {
            if (m_ContractNoDocNum > 0)
                m_ContractPercent = 100;
            else
                m_ContractPercent = 0;
        }
        else
        {
            if (m_ContractNum < m_ContractNoDocNum)
                m_ContractPercent = 100;
            else
                m_ContractPercent = (Int32)Math.Floor((m_ContractNoDocNum + 0d) / m_ContractNum * 100);
        }

        //--调用（非队列）成功百分比
        if ((m_InvokeSuccessNum + m_InvokeFailureNum) == 0)
            m_InvokePercent = 0;
        else
            m_InvokePercent = (Int32)Math.Floor((m_InvokeSuccessNum + 0d) / (m_InvokeSuccessNum + m_InvokeFailureNum) * 100);

        //--调用（非队列）成功百分比
        if ((m_InvokeQueueSuccessNum + m_InvokeQueueFailureNum) == 0)
            m_InvokeQueuePercent = 0;
        else
            m_InvokeQueuePercent = (Int32)Math.Floor((m_InvokeQueueSuccessNum + 0d) / (m_InvokeQueueSuccessNum + m_InvokeQueueFailureNum) * 100);

        //--缓存命中百分比
        if (m_InvokeSuccessNum == 0)
            m_CachePercent = 0;
        else
            m_CachePercent = (Int32)Math.Floor((m_CallHitCacheNum + 0d) / m_InvokeSuccessNum * 100);

        //--缓存命中百分比
        if (m_InvokeSuccessNum + m_InvokeQueueSuccessNum == 0)
            m_ExceptionPercent = 0;
        else
            m_ExceptionPercent = (Int32)Math.Floor((m_InvokeQueueFailureNum + m_InvokeFailureNum + 0d) / (m_InvokeSuccessNum + m_InvokeQueueSuccessNum) * 100);

    }

    /// <summary>
    /// 获取到图表的数据
    /// </summary>
    /// <returns></returns>
    private String GetChartData()
    {
        StringBuilder sbServiceData = new StringBuilder();
        DateTime now = DateTime.Now;

        ESB.ServiceMonitor[] lstSM = msService.GetInvokeAnalyse(m_BusinessID);

        var expTopSM = from sm in lstSM
                       group sm by sm.ServiceName into g
                       orderby g.Max(x => x.MaxInovkeTimeSpan) descending
                       select new { ServiceName = g.Key, MaxInovkeTimeSpan = g.Max(x => x.MaxInovkeTimeSpan) };

        foreach (var item in expTopSM)
        {
            sbServiceData.AppendFormat(@"{{
                    ""data"": {0},
                    ""label"": ""{1}"",
                    ""lines"": {{
                        ""lineWidth"": 1
                    }},
                    ""shadowSize"": 0
                }},", GetServiceData(item.ServiceName, lstSM, now), item.ServiceName);
        }

        String chartData = String.Format(@"{{
            ""nowTime"": ""{0}"",
            ""data"":[
                {1}
            ]
        }}", now.ToString("hh:mm"), sbServiceData.ToString().TrimEnd(','));

        return chartData;
    }

    /// <summary>
    /// 获取到服务的图表数据
    /// </summary>
    /// <param name="serviceName"></param>
    /// <returns></returns>
    private String GetServiceData(String serviceName, ESB.ServiceMonitor[] lstSM, DateTime now)
    {
        StringBuilder sbServiceData = new StringBuilder();
        ///Random random = new Random(serviceName.Length * DateTime.Now.Millisecond);
        sbServiceData.Append("[");

        List<ESB.ServiceMonitor> lstSMService = lstSM.Where(x => x.ServiceName == serviceName).ToList();

        for (int i = 0; i < 30; i++)
        {
            Int32 invokeTimeSpan = 0;
            Int32 callSuccessNum = 0;
            Int32 callFailureNum = 0;
            Int32 callLevel1Num = 0;
            Int32 callLevel2Num = 0;
            Int32 callLevel3Num = 0;

            if (lstSMService != null || lstSMService.Count > 0)
            {
                DateTime dtStat = now.AddMinutes(i - 30 + 1);
                ESB.ServiceMonitor sm = lstSMService.FirstOrDefault(x => x.Hour == dtStat.Hour && x.Minute == dtStat.Minute);
                if (sm != null)
                {
                    invokeTimeSpan = (Int32)sm.MaxInovkeTimeSpan;
                    callSuccessNum = sm.CallSuccessNum;
                    callFailureNum = sm.CallFailureNum;
                    callLevel1Num = sm.CallLevel1Num;
                    callLevel2Num = sm.CallLevel2Num;
                    callLevel3Num = sm.CallLevel3Num;
                }
            }

            if(i == 29)
                sbServiceData.AppendFormat(@"[{0}, {1}, {2}]", i + 1, invokeTimeSpan, callLevel2Num + callLevel3Num);
            else
                sbServiceData.AppendFormat(@"[{0}, {1}, {2}],", i + 1, invokeTimeSpan, callLevel2Num + callLevel3Num);
        }

        sbServiceData.Append("]");

        return sbServiceData.ToString();
    }
}