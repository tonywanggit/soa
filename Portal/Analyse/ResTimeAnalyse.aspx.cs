using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraCharts;
using JN.Esb.Portal.ServiceMgt.日志分析;
using DevExpress.Web.ASPxEditors;
using DevExpress.XtraCharts.Web;
using System.Data;

public partial class Analyse_ResTimeAnalyse : BasePage
{
    AuditAnalyseService aas = new AuditAnalyseService();

    ASPxComboBox cbType
    {
        get
        {
            return this.mnuToolbar.Items[5].FindControl("cbType") as ASPxComboBox;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HideSourceCodeTable();

        if (!IsCallback && !IsPostBack)
            InitPage();

        if (!IsCallback)
            BindingData();
    }

    protected void InitPage()
    {
        mnuToolbar.AutoSeparators = DevExpress.Web.ASPxMenu.AutoSeparatorMode.None;
        cbType.Visible = false;
    }

    /// <summary>
    /// 绑定到按服务细分的数据
    /// </summary>
    /// <returns></returns>
    private void BindingDataByService()
    {
        chart.DataSource = aas.GetResTimeAnalyseData();

        ChartTitle ct = new ChartTitle();
        ct.Text = "按服务细分的调用次数统计";
        chart.Titles.Clear();
        chart.Titles.Add(ct);

        chart.Series.Clear();
        chart.SeriesDataMember = "MethodName";

        chart.SeriesTemplate.View = new StackedBarSeriesView();
        chart.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "ResTimeAvg" });
        chart.SeriesTemplate.ArgumentDataMember = "ServiceName";
        chart.SeriesTemplate.PointOptions.PointView = PointView.Values;
        chart.SeriesTemplate.PointOptions.ValueNumericOptions.Format = NumericFormat.General;

        chart.SeriesNameTemplate.BeginText = "服务方法:";
        ((XYDiagram)chart.Diagram).AxisX.Label.Angle = 30;
        ((XYDiagram)chart.Diagram).Rotated = true;

        chart.DataBind();
    }

    /// <summary>
    /// 获取到所有的数据
    /// </summary>
    /// <returns></returns>
    private void BindingAllData()
    {
        chart.DataSource = aas.GetResTimeAnalyseData();

        ChartTitle ct = new ChartTitle();
        ct.Text = "按服务细分的响应时间分析";
        chart.Titles.Clear();
        chart.Titles.Add(ct);

        chart.Legend.Direction = LegendDirection.LeftToRight;
        chart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;
        chart.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside;

        chart.Series.Clear();
        chart.SeriesDataMember = "MethodName";

        chart.SeriesTemplate.View = new SideBySideBarSeriesView();
        chart.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "ResTimeAvg" });
        chart.SeriesTemplate.ArgumentDataMember = "ServiceName";
        chart.SeriesTemplate.PointOptions.PointView = PointView.Values;
        chart.SeriesTemplate.PointOptions.ValueNumericOptions.Format = NumericFormat.General;
        ((SideBySideBarSeriesLabel)chart.SeriesTemplate.Label).Position = BarSeriesLabelPosition.Top;
        
        ((XYDiagram)chart.Diagram).Rotated = true;
        ((XYDiagram)chart.Diagram).AxisY.Label.Font = new System.Drawing.Font("Tahoma", 8, System.Drawing.FontStyle.Bold);
        ((XYDiagram)chart.Diagram).AxisY.Label.Antialiasing = true;
        ((XYDiagram)chart.Diagram).AxisY.Label.Angle = 30;

        //((XYDiagram)chart.Diagram).AxisY.Label.Font = new System.Drawing.Font("宋体", 10, System.Drawing.FontStyle.Bold);

        chart.DataBind();
    }

    protected void chart_CustomCallback(object sender, CustomCallbackEventArgs e)
    {
        if (e.Parameter == "Type")
            BindingData();
    }

    private void BindingData()
    {
        if (cbType == null)
            BindingAllData();
        else
            if (cbType.Value == "all")
                BindingAllData();
            else
                BindingDataByService();
    }

}
