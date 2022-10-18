using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public double curTime = 0d;

    public float deltaTimeMultiplier = .1f;

    public Light sun;
    public Light moon;

    public float midDayRotation = 90f;

    public bool isDaytime = true;

    private void Start()
    {
        moon.enabled = false;
        sun.enabled = true;

        sun.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
    }

    private void Update()
    {
        curTime += Time.deltaTime * deltaTimeMultiplier;

        // Reset time once it's reached end time
        if(curTime >= Day.EndTimeTick)
        {
            curTime = Day.StartTimeTick;
        }

        if(isDaytime)
        {
            if(Day.IsNightime(curTime))
            {
                isDaytime = false;
                sun.enabled = false;
                moon.enabled = true;
            }
        }
        else
        {
            if (Day.IsDaytime(curTime))
            {
                isDaytime = true;
                moon.enabled = false;
                sun.enabled = true;
            }
        }
    }
}
