using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Appparalogin
{
    internal class Funcoes
    {
        public static string Criptografar(string Texto)
        {
            using (SHA256 hash = SHA256.Create())
            {
                byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(Texto));

                StringBuilder retorno = new StringBuilder();

                foreach (byte b in bytes)
                {
                    retorno.Append(b.ToString("x2"));
                }
                return retorno.ToString();
            }
        }
    }
}
