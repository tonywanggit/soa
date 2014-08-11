using System;
using System.Text;
using System.Collections;
using System.DirectoryServices;

/// <summary>
/// LDAP验证类
/// </summary>

public class LdapAuthentication
{
    private string _path;
    private string _filterAttribute;

    public LdapAuthentication(string path)
    {
        _path = path;
    }

    public bool IsAuthenticated(string domain, string username, string pwd)
    {
        string domainAndUsername = domain + @"\" + username;
        DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);

        try
        {
            //Bind to the native AdsObject to force authentication.
            object obj = entry.NativeObject;

            DirectorySearcher search = new DirectorySearcher(entry);

            search.Filter = "(SAMAccountName=" + username + ")";
            search.PropertiesToLoad.Add("cn");
            SearchResult result = search.FindOne();

            if (null == result)
            {
                return false;
            }

            //Update the new path to the user in the directory.
            _path = result.Path;
            _filterAttribute = (string)result.Properties["cn"][0];
        }
        catch (System.Exception ex)
        {
            throw new System.Exception(" " + ex.Message);
        }

        return true;
    }

    public string GetGroups()
    {
        DirectorySearcher search = new DirectorySearcher(_path);
        search.Filter = "(cn=" + _filterAttribute + ")";
        search.PropertiesToLoad.Add("memberOf");
        StringBuilder groupNames = new StringBuilder();

        try
        {
            SearchResult result = search.FindOne();
            int propertyCount = result.Properties["memberOf"].Count;
            string dn;
            int equalsIndex, commaIndex;

            for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
            {
                dn = (string)result.Properties["memberOf"][propertyCounter];
                equalsIndex = dn.IndexOf("=", 1);
                commaIndex = dn.IndexOf(",", 1);
                if (-1 == equalsIndex)
                {
                    return null;
                }
                groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
                groupNames.Append("|");
            }
        }
        catch (System.Exception ex)
        {
            throw new System.Exception("Error obtaining group names. " + ex.Message);
        }
        return groupNames.ToString();
    }

}
