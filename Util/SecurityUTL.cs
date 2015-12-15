using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Util
{
    public class SecurityUTL
    {
         #region VARIABLES
            public string sChav { get; set; }
            public string sUrl  { get; set; }
            public string sConn { get; set; }
            public string sProv { get; set; }
            public string sUser { get; set; }
            public string sPass { get; set; }

            private static DESCryptoServiceProvider des;
            private static MemoryStream ms;
            private static CryptoStream cs;
            private static StreamReader sr;
            private static FileInfo fileInfo;
            private static FileStream fileStream;
            private static byte[] input, bytExp;
            private static byte[] chave = { };
            private static byte[] iv = { 12, 34, 56, 78, 90, 102, 114, 126 };
            private static string sTxt;
            private static int iPos;
            private const string Password = "HBM@A!!!!Nois";
        #endregion

        #region METHODS
            /* CRIPTOGRAFAR */
            public string Encrypting(string sTxt)
            {
                if (sChav == null)
                { sChav = Password; }

                des     = new DESCryptoServiceProvider();
                ms      = new MemoryStream();
                input   = Encoding.UTF8.GetBytes(sTxt);
                chave   = Encoding.UTF8.GetBytes(sChav.Substring(0, 8));
                cs = new CryptoStream(ms, des.CreateEncryptor(chave, iv), CryptoStreamMode.Write);

                cs.Write(input, 0, input.Length);
                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
            }
            /* DESCRIPTOGRAFAR */
            public string Decrypting(string sTxt)
            {
                if (sChav == null)
                { sChav = Password; }

                des     = new DESCryptoServiceProvider();
                ms      = new MemoryStream();
                input   = new byte[sTxt.Length];
                input   = Convert.FromBase64String(sTxt.Replace(" ","+"));
                chave   = Encoding.UTF8.GetBytes(sChav.Substring(0, 8));
                cs      = new CryptoStream(ms, des.CreateDecryptor(chave, iv), CryptoStreamMode.Write);

                cs.Write(input, 0, input.Length);
                cs.FlushFinalBlock();

                return Encoding.UTF8.GetString(ms.ToArray());
            }
            /* EXPORTAR ARQUIVO .INI */
            public void FileExport()
            {
                fileInfo = new FileInfo(sUrl);
                fileInfo.Delete();

                fileStream = fileInfo.Create();
                bytExp = new UTF8Encoding(true).GetBytes(Encrypting(sProv) + "\n" + Encrypting(sConn) + "\n" + Encrypting(sUser) + "\n" + Encrypting(sPass));

                fileStream.Write(bytExp, 0, bytExp.Length);
                fileStream.Close();
            }
            /* IMPORTAR ARQUIVO .INI */
            public void FileImport()
            {
                iPos = 0;
                fileInfo = new FileInfo(sUrl);
                sr = fileInfo.OpenText();

                while ((sTxt = sr.ReadLine()) != null)
                {
                    iPos = iPos + 1;
                    switch (iPos)
                    {
                        case 1:
                            sProv = Decrypting(sTxt);
                            break;
                        case 2:
                            sConn = Decrypting(sTxt);
                            break;
                        case 3:
                            sUser = Decrypting(sTxt);
                            break;
                        case 4:
                            sPass = Decrypting(sTxt);
                            break;
                    }
                }
                sr.Close();
            }
        #endregion
    }
}
