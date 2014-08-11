using System;
using System.Configuration;

public class DemoSettings {
    private static object isSiteMode;
    public const string ReadOnlyErrorMessage =
        "<b>Data modifications are not allowed in the online demo.</b><br>" +
        "If you need to test data editing functionality, please install the " +
        "ASPxGridView on your machine and run the demo locally.";

    public static bool IsSiteMode {
        get {
            if(isSiteMode != null)
                return (bool)isSiteMode;
            string val = ConfigurationManager.AppSettings["SiteMode"];
            isSiteMode = val == "true";
            return (bool)isSiteMode;
        }
    }

    public static void AssertNotReadOnly() {
        if(IsSiteMode)
            throw new InvalidOperationException(ReadOnlyErrorMessage);
    }
}
