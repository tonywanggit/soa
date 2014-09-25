using DevExpress.Web.ASPxGridView;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Web;
using ESB;
using ESB.Core;
using ESB.Core.Cache;
using ESB.Core.Registry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Security_CacheList : BasePage
{
    MonitorStatService msService = new MonitorStatService();

    protected void Page_Load(object sender, EventArgs e)
    {
        HideSourceCodeTable();
    }

    protected void InitRight()
    {
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        grid.DataBind();
    }

    protected void OdsServiceConfig_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        ESB.EsbView_ServiceConfig[] lstServiceConfig = e.ReturnValue as ESB.EsbView_ServiceConfig[];
        //esbProxy.RegistryConsumerClient.
        CacheManager cacheManager = CacheManager.GetInstance();
        List<CacheInfo> lstCacheInfo = cacheManager.GetCacheStatic();

        ServiceMonitor[] lsServiceMonitor = msService.GetMonitorServiceStatic();

        foreach (ESB.EsbView_ServiceConfig item in lstServiceConfig)
        {
            //--统计Key数量
            if (lstCacheInfo != null && lstCacheInfo.Count > 0)
            {
                if (item.MethodName == "*")
                    item.CacheKeyNum = (from s in lstCacheInfo
                                        where s.ServiceName == item.ServiceName
                                        select s.CacheKeyNum
                                         ).Sum();
                else
                    item.CacheKeyNum = (from s in lstCacheInfo
                                        where s.ServiceName == item.ServiceName && s.MethodName == item.MethodName
                                        select s.CacheKeyNum
                                         ).Sum();
            }

            //--统计缓存命中率
            if(lsServiceMonitor != null && lsServiceMonitor.Length > 0)
            {
                Int32 callSuccessNum;
                Int32 callHitCacheNum;

                if (item.MethodName == "*")
                {
                    callSuccessNum = (from s in lsServiceMonitor
                                            where s.ServiceName == item.ServiceName
                                            select s.CallSuccessNum
                                            ).Sum();
                    callHitCacheNum = (from s in lsServiceMonitor
                                             where s.ServiceName == item.ServiceName
                                             select s.CallHitCacheNum
                                            ).Sum();

                }
                else
                {
                    callSuccessNum = (from s in lsServiceMonitor
                                            where s.ServiceName == item.ServiceName && s.MethodName == item.MethodName
                                            select s.CallSuccessNum
                                            ).Sum();
                    callHitCacheNum = (from s in lsServiceMonitor
                                             where s.ServiceName == item.ServiceName && s.MethodName == item.MethodName
                                             select s.CallHitCacheNum
                                            ).Sum();
                }

                if (callSuccessNum == 0)
                    item.CacheHitRate = 0;
                else
                    item.CacheHitRate = (callHitCacheNum / (callSuccessNum + 0F)) * 100;
            }
        }
    }

    protected void grid_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        String serviceName = grid.GetRowValues(e.VisibleIndex, "ServiceName").ToString();
        String methodName = grid.GetRowValues(e.VisibleIndex, "MethodName").ToString();


        CacheManager cacheManager = CacheManager.GetInstance();
        cacheManager.RemoveCache(serviceName, methodName);

        grid.DataBind();
    }
    protected void OdsServiceConfig_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        e.InputParameters["businessID"] = this.cbProvider.Value;
    }
    protected void cbProvider_SelectedIndexChanged(object sender, EventArgs e)
    {
        grid.DataBind();
    }

    protected void chart_Init(object sender, EventArgs e)
    {
        WebChartControl chart = (WebChartControl)sender;
        GridViewDetailRowTemplateContainer container =chart.NamingContainer as GridViewDetailRowTemplateContainer;

        String oid = container.KeyValue.ToString();
        String serviceName = grid.GetRowValuesByKeyValue(oid, new String[] { "ServiceName" }).ToString();
        String methodName = grid.GetRowValuesByKeyValue(oid, new String[] { "MethodName" }).ToString();

        // Specify data members to bind the chart's series template.
        chart.SeriesDataMember = "Type";
        chart.SeriesTemplate.Label.Visible = false;
        chart.SeriesTemplate.ArgumentDataMember = "DateTime";
        chart.SeriesTemplate.ArgumentScaleType = ScaleType.DateTime;
        chart.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "Value" });

        XYDiagram diagram = chart.Diagram as XYDiagram;

        diagram.AxisX.DateTimeGridAlignment = DateTimeMeasurementUnit.Second;
        diagram.AxisX.DateTimeMeasureUnit = DateTimeMeasurementUnit.Second;
        diagram.AxisX.GridSpacing = 1;

        diagram.AxisX.DateTimeOptions.Format = DateTimeFormat.Custom;
        diagram.AxisX.DateTimeOptions.FormatString = "HH:mm";

        //diagram.AxisX.


        // Specify the template's series view.
        chart.SeriesTemplate.View = new SideBySideBarSeriesView();
        chart.SeriesTemplate.Label.Visible = true;

        // Specify the template's name prefix.
        chart.SeriesNameTemplate.BeginText = "";

        // Generate a data table and bind the chart to it.
        DataView dv = CreateChartData(serviceName, methodName).DefaultView;
        dv.Sort = "DateTime asc";

        chart.DataSource = dv;
        chart.DataBind();
    }

    /// <summary>
    /// 创建图形数据
    /// </summary>
    /// <param name="serviceName"></param>
    /// <param name="methodName"></param>
    /// <returns></returns>
    private DataTable CreateChartData(String serviceName, String methodName)
    {
        // Create an empty table.
        DataTable table = new DataTable("Table1");

        // Add three columns to the table.
        table.Columns.Add("Type", typeof(String));
        table.Columns.Add("DateTime", typeof(DateTime));
        table.Columns.Add("Value", typeof(Double));

        // Add data rows to the table.

        String seriesName = "缓存命中率";
        ServiceMonitor[] lsServiceMonitor = msService.GetAllByServiceAndMethodToday(serviceName, methodName);

        if (lsServiceMonitor != null && lsServiceMonitor.Length > 0)
        {
            table.Rows.Add(new object[] { seriesName, DateTime.Now.AddHours(-4), 0F });
            foreach (ServiceMonitor item in lsServiceMonitor)
            {
                table.Rows.Add(new object[] { seriesName, item.MonitorStamp, Math.Round(item.CallHitCacheNum / (item.CallSuccessNum + 0F) * 100, 2) });
            }
            table.Rows.Add(new object[] { seriesName, DateTime.Now, 0F });
        }
        else
        {
            table.Rows.Add(new object[] { seriesName, DateTime.Now.AddHours(-4), 0F });
            table.Rows.Add(new object[] { seriesName, DateTime.Now, 0F });
        }

        return table;
    }

}