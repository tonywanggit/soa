using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;
using JN.Esb.Portal.ServiceMgt.服务目录服务;
using JN.Esb.Portal.ServiceMgt.审计服务;
using JN.Esb.Portal.ServiceMgt.调度服务;
using System.Xml;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.Data;
using System.Collections.Generic;

public partial class Schedule_ScheduleList : BasePage
{
    #region 属性
    protected ASPxComboBox cbService
    {
        get
        {
            return grid.FindEditFormTemplateControl("pageControl").FindControl("cbService") as ASPxComboBox;
        }
    }

    protected ASPxComboBox cbEntity
    {
        get
        {
            return grid.FindEditFormTemplateControl("pageControl").FindControl("cbEntity") as ASPxComboBox;
        }
    }

    protected ASPxTextBox txtMethodName
    {
        get
        {
            return grid.FindEditFormTemplateControl("pageControl").FindControl("txtMethodName") as ASPxTextBox;
        }
    }

    protected ASPxTextBox txtPassWord
    {
        get
        {
            return grid.FindEditFormTemplateControl("pageControl").FindControl("txtPassWord") as ASPxTextBox;
        }
    }

    protected ASPxMemo txtParam
    {
        get
        {
            return grid.FindEditFormTemplateControl("pageControl").FindControl("txtParam") as ASPxMemo;
        }
    }
    #endregion

    #region 初始化函数
    protected void Page_Load(object sender, EventArgs e)
    {
        HideSourceCodeTable();
        InitRight();

        if (!IsCallback && !IsPostBack)
        {
            InitPage();
        }
    }

    protected void InitPage()
    {
    }

    protected void InitRight()
    {
        this.grid.Columns[0].Visible = AuthUser.IsSystemAdmin;
    }
    #endregion

    #region 自定义函数
    /// <summary>
    /// 增加验证失败信息
    /// </summary>
    /// <param name="errors"></param>
    /// <param name="column"></param>
    /// <param name="errorText"></param>
    void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
    {
        if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
    }

    /// <summary>
    /// 判断是否为日期值yyyy-MM-dd HH:mm:ss
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    bool IsDateTime(object value)
    {
        if (value == null)
            return false;

        string strDateTime = value.ToString();
        if (string.IsNullOrEmpty(strDateTime))
            return false;
        
        strDateTime = strDateTime.Trim();
        if (strDateTime.Length != 19)
            return false;

        try
        {
            DateTime.Parse(strDateTime);
        }
        catch
        {
            return false;
        }

        return true;
    }

   #endregion

    #region 数据源接口函数
    protected void OdsHostScheduler_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        Guid host = Guid.Empty;

        if (cbProvider.Value != null)
        {
            host = new Guid(cbProvider.Value.ToString());
        }

        e.InputParameters.Clear();
        e.InputParameters["host"] = host;
    }

    protected void OdsHostScheduler_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        if (cbEntity.Value == null || cbService.Value == null || string.IsNullOrEmpty(txtMethodName.Text)
                || string.IsNullOrEmpty(txtPassWord.Text) || string.IsNullOrEmpty(txtParam.Text)
            )
        {
            throw new Exception("请详细填写任务信息！");
        }


        ESB_SCHD sched = new ESB_SCHD();
        ESB_SCHD_EsbWS esbWS = new ESB_SCHD_EsbWS();
        SchedulerService schedulerService = new SchedulerService();

        string guid = Guid.NewGuid().ToString().ToUpper();

        sched.START_TIME = DateTime.Parse(e.InputParameters["START_TIME"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
        sched.END_TIME = DateTime.Parse(e.InputParameters["END_TIME"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
        sched.SCHD_CRON = e.InputParameters["SCHD_CRON"].ToString();
        sched.SCHD_DESC = e.InputParameters["SCHD_DESC"].ToString();
        sched.SCHD_FREQ = e.InputParameters["SCHD_FREQ"].ToString();
        sched.SCHD_HOST = new Guid(e.InputParameters["SCHD_HOST"].ToString());
        sched.SCHD_NAME = e.InputParameters["SCHD_NAME"].ToString();
        sched.SCHD_TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        sched.SCHD_USER = AuthUser.UserName;
        sched.TRIG_GROUP = e.InputParameters["TRIG_GROUP"].ToString();
        sched.TRIG_NAME = "TRIG_" + sched.TRIG_GROUP + guid;
        sched.JOB_NAME = "JOB_" + sched.TRIG_GROUP + guid;

        注册服务目录服务 目录服务 = new 注册服务目录服务();
        业务实体 entity = 目录服务.获得服务提供者(sched.SCHD_HOST);
        sched.HOST_NAME = entity.业务名称;

        esbWS.EntityID = new Guid(cbEntity.SelectedItem.Value.ToString());
        esbWS.EntityName = cbEntity.SelectedItem.GetValue("业务名称").ToString(); 
        esbWS.ServiceID = new Guid(cbService.SelectedItem.Value.ToString());
        esbWS.ServiceName = cbService.SelectedItem.GetValue("服务名称").ToString(); 
        esbWS.MethodName = txtMethodName.Text;
        esbWS.PassWord = txtPassWord.Text;
        esbWS.ParamString = txtParam.Text;

        e.InputParameters.Clear();
        e.InputParameters["scheduler"] = sched;
        e.InputParameters["esbWS"] = esbWS;
    }

    protected void OdsHostScheduler_OnUpdating(object sender, ObjectDataSourceMethodEventArgs e)
    {
        if (cbEntity.Value == null || cbService.Value == null || string.IsNullOrEmpty(txtMethodName.Text)
                || string.IsNullOrEmpty(txtPassWord.Text) || string.IsNullOrEmpty(txtParam.Text)
            )
        {
            throw new Exception("请详细填写任务信息！");
        }


        ESB_SCHD sched = new ESB_SCHD();
        ESB_SCHD_EsbWS esbWS = new ESB_SCHD_EsbWS();
        SchedulerService schedulerService = new SchedulerService();

        sched.SCHD_ID = e.InputParameters["SCHD_ID"].ToString();
        sched.START_TIME = DateTime.Parse(e.InputParameters["START_TIME"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
        sched.END_TIME = DateTime.Parse(e.InputParameters["END_TIME"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
        sched.SCHD_CRON = e.InputParameters["SCHD_CRON"].ToString();
        sched.SCHD_DESC = e.InputParameters["SCHD_DESC"].ToString();
        sched.SCHD_FREQ = e.InputParameters["SCHD_FREQ"].ToString();
        sched.SCHD_HOST = new Guid(e.InputParameters["SCHD_HOST"].ToString());
        sched.SCHD_NAME = e.InputParameters["SCHD_NAME"].ToString();
        sched.SCHD_TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        sched.SCHD_USER = AuthUser.UserName;

        注册服务目录服务 目录服务 = new 注册服务目录服务();
        业务实体 entity = 目录服务.获得服务提供者(sched.SCHD_HOST);
        sched.HOST_NAME = entity.业务名称;

        esbWS.EntityID = new Guid(cbEntity.SelectedItem.Value.ToString());
        esbWS.EntityName = cbEntity.SelectedItem.GetValue("业务名称").ToString();
        esbWS.ServiceID = new Guid(cbService.SelectedItem.Value.ToString());
        esbWS.ServiceName = cbService.SelectedItem.GetValue("服务名称").ToString();
        esbWS.MethodName = txtMethodName.Text;
        esbWS.PassWord = txtPassWord.Text;
        esbWS.ParamString = txtParam.Text;

        e.InputParameters.Clear();
        e.InputParameters["scheduler"] = sched;
        e.InputParameters["esbWS"] = esbWS;
    }

    protected void OdsHostScheduler_OnUpdated(object sender, ObjectDataSourceStatusEventArgs e)
    {
        Exception ex = e.Exception;
    }

    protected void OdsHostScheduler_OnDeleting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        if (!AuthUser.IsSystemAdmin)
        {
            throw new Exception("您不是系统管理员，不能进行删除操作！");
        }
        else
        {
            string schedID = e.InputParameters["SCHD_ID"].ToString();

            e.InputParameters.Clear();
            e.InputParameters["schedID"] = schedID;
        }
    }

    protected void OdsService_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
    {
        object entity = Session["Schedule_ScheduleList_Entity"];

        业务实体 svrEntity = new 业务实体();

        if (entity != null)
        {
            svrEntity.业务编码 = new Guid(entity.ToString());
        }
        else
        {
            svrEntity.业务编码 = Guid.Empty;
        }

        e.InputParameters["服务提供者"] = svrEntity;
    }



    #endregion

    #region 控件接口函数

    protected void cbProvider_SelectedIndexChanged(object sender, EventArgs e)
    {
        grid.DataBind();
    }

    protected void cbService_Callback(object source, CallbackEventArgsBase e)
    {
        Session["Schedule_ScheduleList_Entity"] = e.Parameter;
        OdsService.DataBind();
        cbService.DataBind();
    }

    protected void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["SCHD_FREQ"] = "天";
        e.NewValues["TRIG_GROUP"] = "EsbWS";
        e.NewValues["START_TIME"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        e.NewValues["END_TIME"] = DateTime.MaxValue;

        if(cbProvider.SelectedItem != null)
            e.NewValues["SCHD_HOST"] = cbProvider.SelectedItem.Value;
    }

    protected void grid_RowValidating(object sender, ASPxDataValidationEventArgs e)
    {

        if (e.NewValues["SCHD_NAME"] == null)
        {
            AddError(e.Errors, grid.Columns["SCHD_NAME"], "该字段不能为空。");
        }

        if (e.NewValues["SCHD_DESC"] == null)
        {
            AddError(e.Errors, grid.Columns["SCHD_DESC"], "该字段不能为空。");
        }

        if (e.NewValues["SCHD_HOST"] == null)
        {
            AddError(e.Errors, grid.Columns["SCHD_HOST"], "该字段不能为空。");
        }

        if (e.NewValues["SCHD_FREQ"] == null)
        {
            AddError(e.Errors, grid.Columns["SCHD_FREQ"], "该字段不能为空。");
        }

        if (e.NewValues["SCHD_CRON"] == null)
        {
            AddError(e.Errors, grid.Columns["SCHD_CRON"], "该字段不能为空。");
        }

        if (e.NewValues["TRIG_GROUP"] == null)
        {
            AddError(e.Errors, grid.Columns["TRIG_GROUP"], "该字段不能为空。");
        }

        if (e.NewValues["START_TIME"] == null)
        {
            AddError(e.Errors, grid.Columns["START_TIME"], "该字段不能为空。");
        }
        else
        {
            if (!IsDateTime(e.NewValues["START_TIME"]))
            {
                AddError(e.Errors, grid.Columns["START_TIME"], "该字段必须按照 yyyy-MM-dd HH:mm:ss 格式填写。");
            }
        }

        if (e.NewValues["END_TIME"] == null)
        {
            AddError(e.Errors, grid.Columns["END_TIME"], "该字段不能为空。");
        }
        else
        {
            if (!IsDateTime(e.NewValues["END_TIME"]))
            {
                AddError(e.Errors, grid.Columns["END_TIME"], "该字段必须按照 yyyy-MM-dd HH:mm:ss 格式填写。");
            }
            else if (IsDateTime(e.NewValues["START_TIME"]) && IsDateTime(e.NewValues["END_TIME"]))
            {
                if (DateTime.Parse(e.NewValues["START_TIME"].ToString()) >= DateTime.Parse(e.NewValues["END_TIME"].ToString()))
                {
                    AddError(e.Errors, grid.Columns["END_TIME"], "结束时间必须大于开始时间。");
                }
            }
        }

        if (e.Errors.Count > 0) e.RowError = "请务必修正所有问题。";
    }

    protected void grid_OnCustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs e)
    {
        if (e.Column.FieldName == "PREV_FIRE_TIME")
        {
            long prevTime = long.Parse(e.GetFieldValue("PREV_FIRE_TIME").ToString());
            if (prevTime > 0)
                e.DisplayText = new DateTime(prevTime).AddHours(8).ToString("yyyy-MM-dd HH:mm:ss");
            else
                e.DisplayText = "-----------------";
        }

        if (e.Column.FieldName == "NEXT_FIRE_TIME")
        {
            e.DisplayText = new DateTime(long.Parse(e.GetFieldValue("NEXT_FIRE_TIME").ToString())).AddHours(8).ToString("yyyy-MM-dd HH:mm:ss");

            long nextTime = long.Parse(e.GetFieldValue("NEXT_FIRE_TIME").ToString());
            if (nextTime > 0)
                e.DisplayText = new DateTime(nextTime).AddHours(8).ToString("yyyy-MM-dd HH:mm:ss");
            else
                e.DisplayText = "-----------------";
        
        }
    }


    protected void grid_OnCustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
    {
        if (e.ButtonID != "btnPauseOrStart") return;

        string operation = grid.GetRowValues(e.VisibleIndex, "TRIGGER_STATE") as string == "PAUSED" ? "启用" : "停用";

        if (!AuthUser.IsSystemAdmin)
        {
            throw new Exception("您不是系统管理员，不能进行" + operation + "操作！");
        }
        else
        {
            try
            {
                SchedulerService schedulerService = new SchedulerService();

                if (operation == "停用")
                    schedulerService.PauseQuartzWebServiceScheduler(grid.GetRowValues(e.VisibleIndex, "SCHD_ID") as string);
                else
                    schedulerService.ResumeQuartzWebServiceScheduler(grid.GetRowValues(e.VisibleIndex, "SCHD_ID") as string);
            }
            catch (Exception ex)
            {
                throw new Exception(operation + "失败！异常信息：" + ex.Message); ;
            }

            grid.DataBind();
        }
    }

    protected void grid_OnCustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
    {
        if (grid.GetRowValues(e.VisibleIndex, "TRIGGER_STATE") as string == "PAUSED")
            e.Button.Text = "启用";
    }

    protected void grid_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs e)
    {
        if (grid.IsNewRowEditing) return;

        string schedID = grid.GetRowValues(e.VisibleIndex, "SCHD_ID") as string;
        SchedulerService schedulerService = new SchedulerService();
        ESB_SCHD_EsbWS esbWS = schedulerService.GetEsbWebServiceBySchedID(schedID);

        cbEntity.Value = esbWS.EntityID;
        Session["Schedule_ScheduleList_Entity"] = esbWS.EntityID;
        cbService.DataBind();
        cbService.Value = esbWS.ServiceID;
        txtMethodName.Text = esbWS.MethodName;
        txtParam.Text = esbWS.ParamString;
        txtPassWord.Text = esbWS.PassWord;
    }
    #endregion
}
