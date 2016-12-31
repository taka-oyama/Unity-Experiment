using System;

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
	public static string FormatWith(this string value, params object[] args)
	{
		return string.Format(value, args);
	}
}
