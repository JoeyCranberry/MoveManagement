using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Day
{
    public static double StartTimeTick = 0d;
    public static double EndTimeTick = 23d;

    public static double DaytimeStart = 7d;
    public static double MorningEnd = 12d;
    public static double AfternoonEnd = 18d;
    public static double NighttimeStart = 18d;

    public static bool IsDaytime(double tick)
    {
        return (tick >= DaytimeStart && tick <= NighttimeStart);
    }

    public static bool IsNightime(double tick)
    {
        return !(tick >= DaytimeStart && tick <= NighttimeStart);
    }

    public static bool IsMorning(double tick)
    {
        return tick >= DaytimeStart && tick <= MorningEnd;
    }

    public static bool IsAfternoon(double tick)
    {
        return tick >= MorningEnd && tick <= AfternoonEnd;
    }
}
