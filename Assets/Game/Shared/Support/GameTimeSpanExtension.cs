using UnityEngine;
using System;

public static class GameTimeSpanExtension
{
	#region To TimeSpan

	public static TimeSpan GameDays(this int n)
	{
		return TimeSpan.FromDays(n * WorldData.DayInterval.TotalDays);
	}

	public static TimeSpan GameDays(this float n)
	{
		return TimeSpan.FromDays(n * WorldData.DayInterval.TotalDays);
	}

	public static TimeSpan GameHours(this int n)
	{
		return TimeSpan.FromHours(n * WorldData.DayInterval.TotalHours);
	}

	public static TimeSpan GameHours(this float n)
	{
		return TimeSpan.FromHours(n * WorldData.DayInterval.TotalHours);
	}

	public static TimeSpan GameMinutes(this int n)
	{
		return TimeSpan.FromMinutes(n * WorldData.DayInterval.TotalMinutes);
	}

	public static TimeSpan GameMinutes(this float n)
	{
		return TimeSpan.FromMinutes(n * WorldData.DayInterval.TotalMinutes);
	}

	public static TimeSpan GameSeconds(this int n)
	{
		return TimeSpan.FromSeconds(n * WorldData.DayInterval.TotalSeconds);
	}

	public static TimeSpan GameSeconds(this float n)
	{
		return TimeSpan.FromSeconds(n * WorldData.DayInterval.TotalSeconds);
	}

	public static TimeSpan GameMilliseconds(this int n)
	{
		return TimeSpan.FromMilliseconds(n * WorldData.DayInterval.TotalMilliseconds);
	}

	public static TimeSpan GameMilliseconds(this float n)
	{
		return TimeSpan.FromMilliseconds(n * WorldData.DayInterval.TotalMilliseconds);
	}

	#endregion
}
