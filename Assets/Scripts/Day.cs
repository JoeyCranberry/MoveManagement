using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Day
{
    public static double StartTimeTick = 0d;
    public static double EndTimeTick = 24d;

    public static double DaytimeStart = 8d;
    
    public static double NighttimeStart = 18d;

    public static double MorningEnd = 11d;
    public static double AfternoonEnd = 18d;

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
