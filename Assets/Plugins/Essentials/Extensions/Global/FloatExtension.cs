using UnityEngine;
using System;
using System.Collections.Generic;

public static class FloatExtension
{
	#region To TimeSpan
	public static TimeSpan Days(this float n)
	{
		return TimeSpan.FromDays(n);
	}

	public static TimeSpan Hours(this float n)
	{
		return TimeSpan.FromHours(n);
	}

	public static TimeSpan Minutes(this float n)
	{
		return TimeSpan.FromMinutes(n);
	}

	public static TimeSpan Seconds(this float n)
	{
		return TimeSpan.FromSeconds(n);
	}

	public static TimeSpan Milliseconds(this float n)
	{
		return TimeSpan.FromMilliseconds(n);
	}
	#endregion
}
