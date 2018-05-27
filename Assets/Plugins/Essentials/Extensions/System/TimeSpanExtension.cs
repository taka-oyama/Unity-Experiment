using System;

public static class TimeSpanExtension
{
	public static DateTimeOffset Ago(this TimeSpan span, DateTimeOffset since)
	{
		return since.Subtract(span);
	}

	public static DateTimeOffset Ago(this TimeSpan span)
	{
		return DateTimeOffset.Now.Subtract(span);
	}

	public static DateTimeOffset Since(this TimeSpan span, DateTimeOffset time)
	{
		return time.Add(span);
	}

	public static DateTimeOffset FromNow(this TimeSpan span)
	{
		return DateTimeOffset.Now.Add(span);
	}
}
