using System;
using System.Text;

namespace PRY.Common.Encript
{
    public static class Encript
    {
        public static string Encriptar(this string _cadenaAencriptar)
        {
            string result = null;
            if (!string.IsNullOrEmpty(_cadenaAencriptar))
            {
                byte[] encryted = Encoding.Unicode.GetBytes(_cadenaAencriptar);
                result = Convert.ToBase64String(encryted);
            }
            return result;
        }


        public static string DesEncriptar(this string _cadenaAdesencriptar)
        {
            string result = null;
            if (!string.IsNullOrEmpty(_cadenaAdesencriptar))
            {
                byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
                result = Encoding.Unicode.GetString(decryted);
            }
            return result;
        }
    }
}

