using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraCharts;
using System.Data;
using System.Drawing.Imaging;

namespace ESB.TestFramework
{
    public class MonitorData
    {
        public String Type;
        public DateTime Hour;
        public Int32 Value;
    }

    class MonitorDataChartTest
    {
        private static List<MonitorData> CreateData()
        {
            List<MonitorData> lstMonitorData = new List<MonitorData>();

            lstMonitorData.Add(new MonitorData() { Type = "成功", Hour = DateTime.Now.AddHours(-10), Value = 100 });
            lstMonitorData.Add(new MonitorData() { Type = "失败", Hour = DateTime.Now.AddHours(-10), Value = 40 });
            lstMonitorData.Add(new MonitorData() { Type = "成功", Hour = DateTime.Now.AddHours(-5), Value = 200 });
            lstMonitorData.Add(new MonitorData() { Type = "失败", Hour = DateTime.Now.AddHours(-5), Value = 10 });

            return lstMonitorData;
        }

        private static DataTable CreateChartData()
        {
            // Create an empty table.
            DataTable table = new DataTable("Table1");

            // Add three columns to the table.
            table.Columns.Add("Type", typeof(String));
            table.Columns.Add("Hour", typeof(DateTime));
            table.Columns.Add("Value", typeof(Int32));

            List<MonitorData> lstMonitorData = new List<MonitorData>();

            lstMonitorData.Add(new MonitorData() { Type = "成功", Hour = DateTime.Now.AddHours(-10), Value = 100 });
            lstMonitorData.Add(new MonitorData() { Type = "失败", Hour = DateTime.Now.AddHours(-10), Value = 100 });

            // Add data rows to the table.
            table.Rows.Add(new object[] { "成功", DateTime.Now.AddHours(-24), 100 });
            table.Rows.Add(new object[] { "失败", DateTime.Now.AddHours(-24), 50 });
            table.Rows.Add(new object[] { "成功", DateTime.Now.AddHours(-22), 100 });
            table.Rows.Add(new object[] { "失败", DateTime.Now.AddHours(-22), 50 });
            table.Rows.Add(new object[] { "成功", DateTime.Now.AddHours(-15), 100 });
            table.Rows.Add(new object[] { "失败", DateTime.Now.AddHours(-15), 50 });
            table.Rows.Add(new object[] { "成功", DateTime.Now.AddHours(-10), 100 });
            table.Rows.Add(new object[] { "失败", DateTime.Now.AddHours(-10), 50 });
            table.Rows.Add(new object[] { "成功", DateTime.Now.AddHours(-2), 1 });
            table.Rows.Add(new object[] { "失败", DateTime.Now.AddHours(-2), 2 });
            table.Rows.Add(new object[] { "成功", DateTime.Now.AddHours(-1), 10 });
            table.Rows.Add(new object[] { "失败", DateTime.Now.AddHours(-1), 20 });
            table.Rows.Add(new object[] { "成功", DateTime.Now, 20 });
            table.Rows.Add(new object[] { "失败", DateTime.Now, 30 });

            return table;
        }

        private static DataTable CreateChartData1()
        {
            // Create an empty table.
            DataTable table = new DataTable("Table1");

            // Add three columns to the table.
            table.Columns.Add("Type", typeof(String));
            table.Columns.Add("Hour", typeof(DateTime));
            table.Columns.Add("Value", typeof(Int32));


            // Add data rows to the table.
            table.Rows.Add(new object[] { "[20~100ms]", DateTime.Now.AddHours(-24), 3 });
            table.Rows.Add(new object[] { "[100~200ms]", DateTime.Now.AddHours(-24), 2 });
            table.Rows.Add(new object[] { "[>200ms]", DateTime.Now.AddHours(-24), 50 });
            table.Rows.Add(new object[] { "[20~100ms]", DateTime.Now.AddHours(-10), 20 });
            table.Rows.Add(new object[] { "[100~200ms]", DateTime.Now.AddHours(-10), 2 });
            table.Rows.Add(new object[] { "[>200ms]", DateTime.Now.AddHours(-10), 100 });
            table.Rows.Add(new object[] { "[20~100ms]", DateTime.Now, 100 });
            table.Rows.Add(new object[] { "[100~200ms]", DateTime.Now, 20 });
            table.Rows.Add(new object[] { "[>200ms]", DateTime.Now, 3 });

            return table;
        }

        public static void DoTest()
        {
            ChartControl chart = new ChartControl();
            chart.Width = 900;

            // Specify data members to bind the chart's series template.
            chart.SeriesDataMember = "Type";
            chart.SeriesTemplate.Label.Visible = false;
            chart.SeriesTemplate.ArgumentDataMember = "Hour";
            chart.SeriesTemplate.ArgumentScaleType = ScaleType.DateTime;
            chart.SeriesTemplate.ValueDataMembers.AddRange(new string[] { "Value" });

            XYDiagram diagram = chart.Diagram as XYDiagram;

            diagram.AxisX.DateTimeGridAlignment = DateTimeMeasurementUnit.Hour;
            diagram.AxisX.DateTimeMeasureUnit = DateTimeMeasurementUnit.Second;
            diagram.AxisX.GridSpacing = 1;

            diagram.AxisX.DateTimeOptions.Format = DateTimeFormat.Custom;
            diagram.AxisX.DateTimeOptions.FormatString = "HH:mm";


            // Specify the template's series view.
            chart.SeriesTemplate.View = new SideBySideBarSeriesView();

            // Specify the template's name prefix.
            chart.SeriesNameTemplate.BeginText = "";

            // Generate a data table and bind the chart to it.
            DataView dv = CreateChartData().DefaultView;
            dv.Sort = "Hour asc";

            chart.DataSource = dv;
            
            chart.ExportToImage("f:\\1.jpg", ImageFormat.Jpeg);
        }
    }
}
