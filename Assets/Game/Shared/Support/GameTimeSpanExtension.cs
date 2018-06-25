using UnityEngine;
using System;

public static class GameTimeSpanExtension
{
	#region To TimeSpan

	public static TimeSpan GameYears(this int n)
	{
		return TimeSpan.FromDays(n * (City.YearInterval.TotalDays));
	}

	public static TimeSpan GameYears(this float n)
	{
		return TimeSpan.FromDays(n * (City.YearInterval.TotalDays));
	}

	public static TimeSpan GameDays(this int n)
	{
		return TimeSpan.FromDays(n * City.YearInterval.TotalDays / 365f);
	}

	public static TimeSpan GameDays(this float n)
	{
		return TimeSpan.FromDays(n * City.YearInterval.TotalDays / 365f);
	}

	public static TimeSpan GameHours(this int n)
	{
		return TimeSpan.FromHours(n * City.YearInterval.TotalHours / 365f);
	}

	public static TimeSpan GameHours(this float n)
	{
		return TimeSpan.FromHours(n * City.YearInterval.TotalHours / 365f);
	}

	public static TimeSpan GameMinutes(this int n)
	{
		return TimeSpan.FromMinutes(n * City.YearInterval.TotalMinutes / 365f);
	}

	public static TimeSpan GameMinutes(this float n)
	{
		return TimeSpan.FromMinutes(n * City.YearInterval.TotalMinutes / 365f);
	}

	public static TimeSpan GameSeconds(this int n)
	{
		return TimeSpan.FromSeconds(n * City.YearInterval.TotalSeconds / 365f);
	}

	public static TimeSpan GameSeconds(this float n)
	{
		return TimeSpan.FromSeconds(n * City.YearInterval.TotalSeconds / 365f);
	}

	public static TimeSpan GameMilliseconds(this int n)
	{
		return TimeSpan.FromMilliseconds(n * City.YearInterval.TotalMilliseconds / 365f);
	}

	public static TimeSpan GameMilliseconds(this float n)
	{
		return TimeSpan.FromMilliseconds(n * City.YearInterval.TotalMilliseconds / 365f);
	}

	#endregion
}
