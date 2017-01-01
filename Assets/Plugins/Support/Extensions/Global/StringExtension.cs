using System;
using System.Text;

public static class StringExtension
{
	/// <summary>
	/// Converts string to Enum.
	/// </summary>
	/// <typeparam name="T">Type of enum</typeparam>
	public static T ToEnum<T>(this string source, bool ignoreCase = true) where T : struct
	{
		Type type = typeof(T);
		if(!type.IsEnum) {
			string message = string.Format("<{0}> is not an Enum Type", type.Name);
			throw new ArgumentException(message);
		}
		return (T)Enum.Parse(type, source, ignoreCase);
	}

	/// <summary>
	/// Extension method for applying string.Format on an instance.
	/// </summary>
	public static string FormatWith(this string source, params object[] args)
	{
		return string.Format(source, args);
	}

	/// <summary>
	/// Repeat the specified string n times.
	/// </summary>
	public static string Repeat(this string source, int times)
	{
		if(times < 0) {
			throw new Exception("Repeating number cannot be less than zero.");
		}
		StringBuilder sb = new StringBuilder();
		for(int i = 0; i < times; i++) {
			sb.Append(source);
		}
		return sb.ToString();
	}
}
