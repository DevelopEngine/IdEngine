using System;

namespace IdEngine
{
    public static class CoreExtensions
    {
        public static Guid ToGuid(this string src)
        {
            byte[] stringbytes = System.Text.Encoding.UTF8.GetBytes(src);
            byte[] hashedBytes = new System.Security.Cryptography
                .SHA1CryptoServiceProvider()
                .ComputeHash(stringbytes);
            Array.Resize(ref hashedBytes, 16);
            return new Guid(hashedBytes);
        }
    }
}