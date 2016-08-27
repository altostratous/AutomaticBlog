using System.Runtime.InteropServices;

namespace FrontEndAutomation
{
    public class Common
    {
        public static int WAIT_INTERVAL = 100;
        public static int WAIT_TIMEOUT_COUNT = 200;
        public static void RemoveAll()
        {
            Gecko.Xpcom.GetService<nsICookieManager>("@mozilla.org/cookiemanager;1").removeAll();
        }
    }


    [Guid("AAAB6710-0F2C-11d5-A53B-0010A401EB10"),
        ComImport,
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    interface nsICookieManager
    {
        void removeAll();
        void remove(string aDomain, string aName, string aPath, bool aBlocked);
    }
}