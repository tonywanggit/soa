using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class SSO : System.Web.UI.Page
{
    private HttpCookie authCookie;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string adPath = "LDAP://DC=jn,DC=com"; //Path to your LDAP directory server
        LdapAuthentication adAuth = new LdapAuthentication(adPath);
        try
        {
            if (true == adAuth.IsAuthenticated("jn.com", txtUsername.Text, txtPassword.Text))
            {
                // string groups = adAuth.GetGroups();
                string groups = "";

                //Create the ticket, and add the groups.
                bool isCookiePersistent = true;
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1,
                          txtUsername.Text, DateTime.Now, DateTime.Now.AddMinutes(60), isCookiePersistent, groups);

                //Encrypt the ticket.
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                //Create a cookie, and then add the encrypted ticket to the cookie as data.
                authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

                if (true == isCookiePersistent)
                {
                    authCookie.Expires = authTicket.Expiration;
                }

                //Add the cookie to the outgoing cookies collection.
                Response.Cookies.Add(authCookie);


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
    private void EsbAuthen(string userName)
    {
        AuthenUser authenUser = AuthenUser.GetAuthenUserByLoginName(userName);

        if (null == authenUser)
        {
            string strValue = "您是未授权的用户！";
            string strAll = "<SCRIPT lanquage='JScript'>window.alert('" + strValue + "');window.location.href='Logout.aspx'<" + "/SCRIPT>";
            Response.Write(strAll);
            Response.End();
        }
    }
}
