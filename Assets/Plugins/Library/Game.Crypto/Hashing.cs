using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Game.Crypto
{
	public class Hashing
	{
		#region MD5
		public static string MD5(string source)
		{
			return MD5(new UTF8Encoding().GetBytes(source));
		}

		public static string MD5(byte[] source)
		{
			return byteToHex(new MD5CryptoServiceProvider().ComputeHash(source));
		}
		#endregion

		#region SHA1
		public static string SHA1(string source)
		{
			return SHA1(new UTF8Encoding().GetBytes(source));
		}

		public static string SHA1(byte[] source)
		{
			return byteToHex(new SHA1CryptoServiceProvider().ComputeHash(source));
		}
		#endregion

		#region SHA256
		public static string SHA256(string source)
		{
			return SHA256(new UTF8Encoding().GetBytes(source));
		}

		public static string SHA256(byte[] source)
		{
			return byteToHex(new SHA256Managed().ComputeHash(source));
		}
		#endregion

		#region CRC32
		public static string Crc32(string source)
		{
			return Crc32(Encoding.UTF8.GetBytes(source));
		}

		public static string Crc32(byte[] source)
		{
			UInt32 polynomial = 0xedb88320;
			UInt32[] table = new UInt32[256];
			for(int i = 0; i < 256; i++) {
				var entry = (UInt32)i;
				for (int j = 0; j < 8; j++) entry = (entry & 1) == 1 ? (entry >> 1) ^ polynomial : entry >> 1;
				table[i] = entry;
			}

			UInt32 crc = 0xffffffff;
			for(int i = 0; i < source.Length; i++) unchecked {
				crc = (crc >> 8) ^ table[source[i] ^ crc & 0xff];
			}
			crc = ~crc; // bitwise negation

			return byteToHex(new[] {
				(byte)((crc >> 24) & 0xff),
				(byte)((crc >> 16) & 0xff),
				(byte)((crc >> 8) & 0xff),
				(byte)(crc & 0xff)
			});
		}
		#endregion

		static string byteToHex(byte[] hash)
		{
			return string.Concat(hash.Select(h => h.ToString("x2")).ToArray());
		}
	}
}
