using System;
using System.IO;

namespace IdEngine
{
    public class Id
    {

        private Id(Guid g)
        {
            Code = Encode(g);
        }

        public static Id Create()
        {
            return new Id(Guid.NewGuid());
        }

        private string Code { get; set; }
        public override string ToString()
        {
            return Code;
        }

        public static implicit operator string(Id c)
        {
            return c.Code;
        }

        public static implicit operator Id(FileInfo fi)
        {
            return new Id(fi.FullName.ToGuid());
        }

        public static bool TryParse(string s, out Id c)
        {
            if (s != null && s.Length == 22)
            {
                var g = Decode(s);
                c = g.HasValue
                    ? new Id(g.Value)
                    : null;
                return g.HasValue;
                // c = new ShortCode(Decode(s).Value);
                // return true;
            }
            c = null;
            return false;
        }

        public static implicit operator Guid(Id c)
        {
            var d = Decode(c.Code);
            return d.HasValue
                ? d.Value
                : throw new InvalidCastException("Not a valid ShortCode!");
        }

        private static string Encode(Guid guid)
        {
            try
            {
                string enc = Convert.ToBase64String(guid.ToByteArray());
                enc = enc.TrimEnd('=');
                enc = enc.Replace("/", "_");
                enc = enc.Replace("+", "-");
                return enc.Substring(0, 22);
            }
            catch
            {
                return null;
            }
        }

        private static Guid? Decode(string encoded)
        {
            try
            {
                encoded = encoded.Replace("_", "/");
                encoded = encoded.Replace("-", "+");
                byte[] buffer = Convert.FromBase64String(encoded + "==");
                return new Guid(buffer);
            }
            catch
            {
                return null;
            }
        }
    }
}
