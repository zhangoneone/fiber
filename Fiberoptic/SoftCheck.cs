using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
namespace FiberopticServer
{
    class SoftCheck
    {
        //RSA加密
        public string RSAEncrypt(string normaltxt)
        {
            var bytes = Encoding.Default.GetBytes(normaltxt);
            var encryptBytes = new RSACryptoServiceProvider(new CspParameters()).Encrypt(bytes, false);
            return Convert.ToBase64String(encryptBytes);
        }  
        //RSA解密
        public string RSADecrypt(string securityTxt)
        {
            try//必须使用Try catch,不然输入的字符串不是净荷明文程序就Gameover了
            {
                var bytes = Convert.FromBase64String(securityTxt);
                var DecryptBytes = new RSACryptoServiceProvider(new CspParameters()).Decrypt(bytes, false);
                return Encoding.Default.GetString(DecryptBytes);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public void AddCheck()
        {

        }
        public void CheckSoft()
        {

        }
        public void ResetCheck()
        {

        }
    }
}
