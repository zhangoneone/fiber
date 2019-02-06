using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
namespace FiberopticServer
{
    public class RSAHelper
    {
        public string privateKey { get; set; }

        public string publicKey { get; set; }
        public RSAHelper()
        {
            //初始化时生成公钥和私钥
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            privateKey = provider.ToXmlString(true);//生成私钥
            publicKey = provider.ToXmlString(false);//生成公钥
        }

        /// <summary>
        /// 生成公钥、私钥
        /// </summary>
        /// <param name="PrivateKeyPath">私钥文件保存路径，包含文件名</param>
        /// <param name="PublicKeyPath">公钥文件保存路径，包含文件名</param>
        public void RSAKey()
        {
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.ToXmlString(true);//生成私钥文件
            provider.ToXmlString(false);//生成公钥文件
        }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="str">需签名的数据</param>
        /// <returns>签名后的值</returns>
        public string Sign(string str)
        {
            //根据需要加签时的哈希算法转化成对应的hash字符节
            byte[] bt = Encoding.GetEncoding("utf-8").GetBytes(str);
            var sha256 = new SHA256CryptoServiceProvider();
            byte[] rgbHash = sha256.ComputeHash(bt);

            RSACryptoServiceProvider key = new RSACryptoServiceProvider();
            key.FromXmlString(privateKey);
            RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(key);
            formatter.SetHashAlgorithm("SHA256");//此处是你需要加签的hash算法，需要和上边你计算的hash值的算法一致，不然会报错。
            byte[] inArray = formatter.CreateSignature(rgbHash);
            return Convert.ToBase64String(inArray);
        }

        /// <summary>
        /// 签名验证
        /// </summary>
        /// <param name="str">待验证的字符串</param>
        /// <param name="sign">加签之后的字符串</param>
        /// <returns>签名是否符合</returns>
        public bool SignCheck(string str, string sign)
        {
            try
            {
                byte[] bt = Encoding.GetEncoding("utf-8").GetBytes(str);
                var sha256 = new SHA256CryptoServiceProvider();
                byte[] rgbHash = sha256.ComputeHash(bt);

                RSACryptoServiceProvider key = new RSACryptoServiceProvider();
                key.FromXmlString(publicKey);
                RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(key);
                deformatter.SetHashAlgorithm("SHA256");
                byte[] rgbSignature = Convert.FromBase64String(sign);
                if (deformatter.VerifySignature(rgbHash, rgbSignature))
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
