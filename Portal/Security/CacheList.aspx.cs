using ESB;
using ESB.Core;
using ESB.Core.Cache;
using ESB.Core.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Security_CacheList : BasePage
{
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

        MonitorStatService msService = new MonitorStatService();
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
}