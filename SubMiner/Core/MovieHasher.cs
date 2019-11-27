using System;
using System.Text;
using System.IO;

namespace SubMiner.Core
{
    public class MovieHasher
    {
        public static string ComputeMovieHash(string filename)
        {
            string result = "";
            using (Stream input = File.OpenRead(filename))
            {
                result = ComputeMovieHash(input);
            }
            return result;
        }

        public static string ComputeMovieHash(Stream input)
        {
            const int Max = 65536;

            long hash;
            long streamsize;
            streamsize = input.Length;
            hash = streamsize;

            long i = 0;
            byte[] buffer = new byte[sizeof(long)];
            while (i < Max / sizeof(long) && (input.Read(buffer, 0, sizeof(long)) > 0))
            {
                i++;
                hash += BitConverter.ToInt64(buffer, 0);
            }

            input.Position = Math.Max(0, streamsize - Max);
            i = 0;
            while (i < Max / sizeof(long) && (input.Read(buffer, 0, sizeof(long)) > 0))
            {
                i++;
                hash += BitConverter.ToInt64(buffer, 0);
            }
            input.Close();
            byte[] result = BitConverter.GetBytes(hash);
            Array.Reverse(result);
            return BytesToHexadecimal(result);
        }

        private static string BytesToHexadecimal(byte[] bytes)
        {
            StringBuilder hexBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                hexBuilder.Append(bytes[i].ToString("x2"));
            }
            return hexBuilder.ToString();
        }
    }
}
