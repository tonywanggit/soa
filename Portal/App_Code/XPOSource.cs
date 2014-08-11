using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Xpo;

public class ServerSideGridTest : XPObject {
    public ServerSideGridTest(Session session) : base(session) { }
    [Indexed]
    public string Subject;
    [Indexed]
    public string From;
    [Indexed]
    public DateTime Sent;
    [Indexed]
    public Int64 Size;
    [Indexed]
    public bool HasAttachment;
}

