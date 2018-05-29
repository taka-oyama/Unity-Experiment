using System;

public static class TimeSpanExtension
{
	public static DateTime Ago(this TimeSpan span, DateTime since)
	{
		return since.Subtract(span);
	}

	public static DateTime Ago(this TimeSpan span)
	{
		return DateTime.Now.Subtract(span);
	}

	public static DateTime Since(this TimeSpan span, DateTime time)
	{
		return time.Add(span);
	}

	public static DateTime FromNow(this TimeSpan span)
	{
		return DateTime.Now.Add(span);
	}
}
