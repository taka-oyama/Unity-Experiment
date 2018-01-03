using System;
using System.Security.Cryptography;
using System.Text;

namespace Game.Crypto
{
	public sealed class AlphaNumericRandom
	{
		/// Some useful info below
		/// http://stackoverflow.com/questions/32932679/using-rngcryptoserviceprovider-to-generate-random-string
		/// 
		/// Since RNGRandomNumberGenerator only returns byte arrays, you have to do it like this:
		/// Note however that this has a flaw, 62 valid characters is equal to
		/// 5.9541963103868752088061235991756 bits (log(62) / log(2)), 
		/// so it won't divide evenly on a 32 bit number (uint).
		/// 
		/// What consequences does this have? As a result, the random output won't be uniform.
		/// Characters which are lower in valid will occur more likely (just by a small fraction, 
		/// but still it happens).
		/// 
		/// To be more precise, the first 4 characters of the valid array are
		/// 0.00000144354999199840239435286% more likely to occur.
		/// 
		/// To avoid this, you should use array lengths which divide evenly like 64
		/// (Consider using Convert.ToBase64String on the output instead,
		/// since you can cleanly match 64 bits to 6 bytes.)

		const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

		public static string Generate(uint length = 32)
		{
			var sb = new StringBuilder();
			var rng = new RNGCryptoServiceProvider();
			byte[] buffer = new byte[sizeof(uint)];
			while(length-- > 0) {
				rng.GetBytes(buffer);
				uint num = BitConverter.ToUInt32(buffer, 0);
				sb.Append(valid[(int)(num % (uint)valid.Length)]);
			}
			return sb.ToString();
		}
	}
}
