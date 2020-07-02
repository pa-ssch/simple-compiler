using System.IO;
using System.Text;

namespace CompilerExtensions
{
    public static class StringExtensions
    {
        public static Stream ToStream(this string str) => new MemoryStream(Encoding.UTF8.GetBytes(str));
    }
}
