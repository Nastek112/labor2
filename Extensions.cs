using System;
using System.Linq;

namespace Lab2.Vectors
{
    public static class Extensions
    {
        public static string TruncateStart(this string s, int count)
        {
            if (s == null) return string.Empty;
            if (count <= 0) return s;
            if (count >= s.Length) return string.Empty;
            return s.Substring(count);
        }

        public static Vector RemovePositive(this Vector v)
        {
            if (v == null) throw new ArgumentNullException(nameof(v));
            var filtered = Enumerable.Range(0, v.Length)
                                     .Select(i => v[i])
                                     .Where(x => x <= 0.0);
            return new Vector(filtered);
        }
    }
}
