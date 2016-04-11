using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Utils;

namespace Words.Account
{
    public static class CookieManage
    {
        public static HttpCookie Cookie;
        public static bool HasUserinfo() {
            return Cookie != null;
        }
        public static string GetUserinfo() {
            return CryptoUtils.DesDecrypt(Cookie.Value, CryptoUtils.KEY);
        }
        //private static User_t currentUser;
        //public static bool SetCurrentUserByUserName(string username) {
        // var   User = db.User_t.Where(p => p.User_Name == username).FirstOrDefault();
        //    if (User != null) {
        //        currentUser = User;
        //        return true;
        //    }
        //    return false;
        //}
        //public static User_t GetCurrentUser() {
        //    return currentUser;
        //}
        //public static bool IsLogin() {
        //    return GetCurrentUser() != null&&GetCurrentUser().User_Name!=null;
        //}
    }
}