using UnityEngine;
using System;
using System.Collections.Generic;

public static class IntExtension
{
	public static IEnumerable<int> Times(this int n)
	{
		for(int i = 0; i < n; i++) {
			yield return i;
		}
	}

	#region File Size
	public static int Kilobytes(this int n)
	{
		return n * 1024;
	}

	public static int Megabytes(this int n)
	{
		return n * 1024 * 1024;
	}
	#endregion

	#region To TimeSpan
	public static TimeSpan Days(this int n)
	{
		return TimeSpan.FromDays(n);
	}

	public static TimeSpan Hours(this int n)
	{
		return TimeSpan.FromHours(n);
	}

	public static TimeSpan Minutes(this int n)
	{
		return TimeSpan.FromMinutes(n);
	}

	public static TimeSpan Seconds(this int n)
	{
		return TimeSpan.FromSeconds(n);
	}

	public static TimeSpan Milliseconds(this int n)
	{
		return TimeSpan.FromMilliseconds(n);
	}
	#endregion
}
