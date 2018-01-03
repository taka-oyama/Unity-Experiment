using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Game.Crypto
{
	public sealed class HMAC
	{
		#region MD5
		public static string MD5(string key, string message)
		{
			UTF8Encoding encoding = new UTF8Encoding();
			return MD5(encoding.GetBytes(key), encoding.GetBytes(message));
		}

		public static string MD5(string key, byte[] message)
		{
			return MD5(new UTF8Encoding().GetBytes(key), message);
		}

		public static string MD5(byte[] key, byte[] message)
		{
			return byteToHex(new HMACMD5(key).ComputeHash(message));
		}
		#endregion

		#region SHA1
		public static string SHA1(string key, string message)
		{
			UTF8Encoding encoding = new UTF8Encoding();
			return SHA1(encoding.GetBytes(key), encoding.GetBytes(message));
		}

		public static string SHA1(string key, byte[] message)
		{
			return SHA1(new UTF8Encoding().GetBytes(key), message);
		}

		public static string SHA1(byte[] key, byte[] message)
		{
			return byteToHex(new HMACSHA1(key).ComputeHash(message));
		}
		#endregion

		#region SHA256
		public static string SHA256(string key, string message)
		{
			UTF8Encoding encoding = new UTF8Encoding();
			return SHA256(encoding.GetBytes(key), encoding.GetBytes(message));
		}

		public static string SHA256(string key, byte[] message)
		{
			return SHA256(new UTF8Encoding().GetBytes(key), message);
		}

		public static string SHA256(byte[] key, byte[] message)
		{
			return byteToHex(new HMACSHA256(key).ComputeHash(message));
		}
		#endregion

		static string byteToHex(byte[] hash)
		{
			return string.Concat(hash.Select(h => h.ToString("x2")).ToArray());
		}
	}
}
