using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using ESB;

/// <summary>
/// 授权用户信息
/// </summary>
public class AuthenUser
{
	public AuthenUser()
	{
	}

    public string UserName;
    public bool IsSystemAdmin;
    public string UserID;
    public string LoginName;

    public static Hashtable OnlineAuthenUserList = new Hashtable();

    /// <summary>
    /// 实现ESB用户到授权用户的装换
    /// </summary>
    /// <param name="person"></param>
    /// <returns></returns>
    public static AuthenUser GetAuthenUser(Personal person)
    {
        AuthenUser authenUser = new AuthenUser();

        authenUser.IsSystemAdmin = (person.permission == 0);
        authenUser.UserID = person.PersonalID;
        authenUser.UserName = person.PersonalName;
        authenUser.LoginName = person.PersonalAccount;

        return authenUser;
    }

    /// <summary>
    /// 根据登陆帐号实例化授权用户
    /// </summary>
    /// <param name="loginName"></param>
    /// <returns></returns>
    public static AuthenUser GetAuthenUserByLoginName(string loginName)
    {
        if(AuthenUser.OnlineAuthenUserList.ContainsKey(loginName))
            return AuthenUser.OnlineAuthenUserList[loginName] as AuthenUser;

        ESB.UddiService uddiService = new UddiService();
        Personal person = uddiService.GetPersonByLoginName(loginName);

        if (person == null)
            return null;

        return PushAuthenUserOnline(GetAuthenUser(person));
    }

    /// <summary>
    /// 将授权用户加入在线列表
    /// </summary>
    /// <param name="authenUser"></param>
    /// <returns></returns>
    public static AuthenUser PushAuthenUserOnline(AuthenUser authenUser)
    {
        if (AuthenUser.OnlineAuthenUserList.ContainsKey(authenUser.LoginName))
            return AuthenUser.OnlineAuthenUserList[authenUser.LoginName] as AuthenUser;

        AuthenUser.OnlineAuthenUserList.Add(authenUser.LoginName, authenUser);
        return authenUser;
    }

    /// <summary>
    /// 将授权用户加入在线列表
    /// </summary>
    /// <param name="authenUser"></param>
    /// <returns></returns>
    public static void RemoveAuthenUserOnline(string loginName)
    {
        if (AuthenUser.OnlineAuthenUserList.ContainsKey(loginName))
            AuthenUser.OnlineAuthenUserList.Remove(loginName);
    }
}
