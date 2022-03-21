using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace LibManagementSys
{
    internal class HashEncryption
    {

        static public string HashText(string str)
        {
            byte[] txt = Encoding.Default.GetBytes(str);
            HashAlgorithm hash = HashAlgorithm.Create("SHA256");
            HashAlgorithm hash2 = HashAlgorithm.Create("MD5");
            byte[] res = hash2.ComputeHash(hash.ComputeHash(txt));

            return BitConverter.ToString(res);
        }

    }
}
