using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Utils;
using Words.Account;
using Words.Attribute;

namespace Words.Controllers
{
    public class UserController : BaseController
    {
        private string BytesToHexString(byte[] input)
        {
            StringBuilder hexString = new StringBuilder(64);

            for (int i = 0; i < input.Length; i++)
            {
                hexString.Append(String.Format("{0:X2}", input[i]));
            }
            return hexString.ToString();
        }

        public static byte[] HexStringToBytes(string hex)
        {
            if (hex.Length == 0)
            {
                return new byte[] { 0 };
            }

            if (hex.Length % 2 == 1)
            {
                hex = "0" + hex;
            }

            byte[] result = new byte[hex.Length / 2];

            for (int i = 0; i < hex.Length / 2; i++)
            {
                result[i] = byte.Parse(hex.Substring(2 * i, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            }

            return result;
        }






        [AccountAttribute(IsChecked =false)]
        // GET: User
        public ActionResult Login(string username,string encrypted_pwd, bool remember=false)
        {
          
       
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            RSAParameters parameter = rsa.ExportParameters(true);
            if (Request.HttpMethod == "GET") {
                
                Session["private_key"] = rsa.ToXmlString(true);
                 parameter = rsa.ExportParameters(true);
                ViewBag.strPublicKeyExponent = BytesToHexString(parameter.Exponent);
                ViewBag.strPublicKeyModulus = BytesToHexString(parameter.Modulus);
                return View();
            }
            if (username == null || encrypted_pwd == null)
            {
                Session["private_key"] = rsa.ToXmlString(true);
                 parameter = rsa.ExportParameters(true);
                ViewBag.strPublicKeyExponent = BytesToHexString(parameter.Exponent);
                ViewBag.strPublicKeyModulus = BytesToHexString(parameter.Modulus);
                return View();
            }
     
       
    
            string strPwdToDecrypt = encrypted_pwd;
            rsa.FromXmlString((string)Session["private_key"]);
            byte[] result = rsa.Decrypt(HexStringToBytes(strPwdToDecrypt), false);
            System.Text.ASCIIEncoding enc = new ASCIIEncoding();
            string strPwdMD5 = enc.GetString(result);
            var user = db.User_t.Where(p => p.User_Name == username).FirstOrDefault();
            if ((user != null && string.Compare(username, user.User_Name, true) == 0 && string.Compare(strPwdMD5, user.User_Password, true) == 0)){ 
          
                Response.Cookies["MYWORD"].Value = CryptoUtils.DesEncrypt(user.User_Name, CryptoUtils.KEY);
                if (remember)
                {
                    Response.Cookies["MYWORD"].Expires = DateTime.Now.AddDays(7);
                }
                return Redirect("/home/index");
            }
            Session["private_key"] = rsa.ToXmlString(true);
       
            ViewBag.strPublicKeyExponent = BytesToHexString(parameter.Exponent);
            ViewBag.strPublicKeyModulus = BytesToHexString(parameter.Modulus);
            return View();
        }
        [AccountAttribute(IsChecked = false)]
        public ActionResult Register(string username,string password) {
            if (Request.HttpMethod == "GET")
            {
                return View();
            }
            if (username == null || password == null)
            {
                return View();
            }
            if (db.User_t.Where(p => p.User_Name == username).Count() != 0)
            {
                return View();
            }
  
            db.User_t.Add(new User_t() {
                User_Name=username,
                User_Password= CryptoUtils.MD5(password)
            });
            db.SaveChanges();
            Response.Cookies["MYWORD"].Value = CryptoUtils.DesEncrypt(username, CryptoUtils.KEY);

            return Redirect("/home/index");
        }

        public ActionResult Logout() {
            if (Request.Cookies["MYWORD" ]!= null) {
                HttpCookie cookie = new HttpCookie("MYWORD");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
            return Redirect("/user/login");
        }
    }
}