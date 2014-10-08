using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Collections.Generic;
using JN.Esb.Portal.ServiceMgt.服务目录服务;
using System.DirectoryServices;
using System.Threading;

public partial class LoginPage : System.Web.UI.Page
{

    /// <summary>
    /// ESB记录登陆用户的Cookie名称
    /// </summary>
    public const String ESB_COOKIE_LOGINNAME = "Esb-Cookie-LoginName";

	protected void Page_Load(object sender, EventArgs e) {
        if (!this.IsPostBack && this.Request.Cookies[ESB_COOKIE_LOGINNAME] != null)
        {
            this.txtUsername.Text = this.Request.Cookies[ESB_COOKIE_LOGINNAME].Value;
        }
	}

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        //string adPath = "LDAP://DC=mb,DC=com"; //Path to your LDAP directory server
        LdapAuthentication adAuth = new LdapAuthentication(txtDomain.Text);
        try
        {
            if (true == adAuth.IsAuthenticated(txtDomain.Text, txtUsername.Text, txtPassword.Text))
            //if(true)
            {
                // string groups = adAuth.GetGroups();
                string groups = "";

                //Create the ticket, and add the groups.
                bool isCookiePersistent = chkPersist.Checked;
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1,
                          txtUsername.Text, DateTime.Now, DateTime.Now.AddMinutes(60), isCookiePersistent, groups);

                //Encrypt the ticket.
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                //Create a cookie, and then add the encrypted ticket to the cookie as data.
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                HttpCookie loginNameCookie = new HttpCookie(ESB_COOKIE_LOGINNAME, txtUsername.Text);

                if (true == isCookiePersistent)
                {
                    authCookie.Expires = authTicket.Expiration;
                    loginNameCookie.Expires = DateTime.Now.AddDays(30);
                }

                //Add the cookie to the outgoing cookies collection.
                Response.Cookies.Add(authCookie);
                Response.Cookies.Add(loginNameCookie);


                //Esb授权校验
                EsbAuthen(txtUsername.Text);

                //Server.Transfer("Default.aspx");
                //You can redirect now.
                //Response.Redirect(FormsAuthentication.GetRedirectUrl(txtUsername.Text, false));
                Response.Redirect("Default.aspx", false);
            }
            else
            {
                errorLabel.Text = "登录失败，请检查用户名和密码！";
            }
        }
        catch (System.Exception ex)
        {
            errorLabel.Text = "登录失败，请检查用户名和密码！";
        }
    }

    /// <summary>
    /// Esb授权校验
    /// </summary>
    /// <param name="userName"></param>
    private void EsbAuthen(String loginName)
    {
        AuthenUser authenUser = AuthenUser.GetAuthenUserByLoginName(loginName);

        if (null == authenUser){
            //string strValue = "您是未授权的用户！";
            //string strAll = "<SCRIPT lanquage='JScript'>window.alert('" + strValue + "');window.location.href='Logout.aspx'<" + "/SCRIPT>";
            //Response.Write(strAll);
            //Response.End();

            authenUser = new AuthenUser();

            authenUser.IsSystemAdmin = false;
            authenUser.IsVisitor = true;    //--访客模式
            authenUser.LoginName = loginName;
            authenUser.UserName = loginName;
            authenUser.UserID = loginName;

            AuthenUser.PushAuthenUserOnline(authenUser);
        }

        Session["ESB_MENU"] = authenUser.Menu;
    }
}
