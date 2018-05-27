using System;

public static class DateTimeExtension
{
	public static TimeSpan SpanFromNow(this DateTime target)
	{
		return DateTime.Now - target;
	}
}
