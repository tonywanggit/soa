using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard_ctl_OverviewStat : System.Web.UI.UserControl
{
    public String Title { get; set; }
    public ProgressBarType ProgressBar { get; set; }
    public ChartType Chart { get; set; }
    public ChartLineOrBar ChartLOB { get; set; }
    public String ChartData { get; set; }
    public Int32 Value { get; set; }
    public Int32 Percent { get; set; }
    public Int32 TitleFontSize { get; set; }

    protected String TitleStyle;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (TitleFontSize > 0)
        {
            TitleStyle = String.Format("font-size:{0}px", TitleFontSize);
        }
    }

    public enum ProgressBarType
    {
        info,
        success,
        warning,
        danger
    }

    public enum ChartType
    {
        ok,
        good,
        bad
    }

    public enum ChartLineOrBar
    {
        line,
        bar
    }
}

