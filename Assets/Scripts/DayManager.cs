using UnityEngine;
using TMPro;

public class DayManager : MonoBehaviour
{
    public TMP_Text TimeDisplay;

    public double curTime = 0d;

    public float deltaTimeMultiplier = .1f;

    public Light sun;
    public Light moon;

    public bool isDaytime = true;

    public bool updateCelestialBodies = true;

    private double midDayTick;
    private double midNightTick;

    public float bodyMiddayRotation = 90f;
    private float rotateStep;

    public float intensityStart = 10000f;
    public float intensityEnd = 82500f;
    private float intensityStepAmount;

    private void Start()
    {
        // Calculate values needed for celestials bodies, just in case updateCelestialBodies is toggled during play
        rotateStep = bodyMiddayRotation / ((float)(Day.NighttimeStart - Day.DaytimeStart) / 2);

        intensityStepAmount = (intensityStart - intensityEnd) / (float)(Day.NighttimeStart - Day.DaytimeStart) / 2;

        midDayTick = Day.DaytimeStart + (Day.NighttimeStart - Day.DaytimeStart / 2);
        midNightTick = Day.NighttimeStart + (((Day.EndTimeTick - Day.NighttimeStart) + Day.DaytimeStart) / 2);

        curTime = Day.DaytimeStart;

        if (updateCelestialBodies)
        {
            sun.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            SetDaytime();
        }
    }

    private void Update()
    {
        curTime += Time.deltaTime * deltaTimeMultiplier;
        SetDisplayTime(curTime);

        // Reset time once it's reached end time
        if (curTime >= Day.EndTimeTick)
        {
            curTime = Day.StartTimeTick;
        }

        if (updateCelestialBodies)
        {
            UpdateCelestialBodies();
        }
    }

    /*
     * Rotate bodied (sun or moon) from 0 degrees to 90 degrees on the x axis
     * Start with reduced intensity, and gradually increase it
     * Rotation and intensity reach their peak at mid-day or mid-night
     */
    private void UpdateCelestialBodies()
    {
        if (isDaytime)
        {
            if (Day.IsNightime(curTime))
            {
                SetNighttime();
            }

            RotateBody(sun.transform);
            SetLightIntensity(sun, midDayTick);
        }
        else
        {
            if (Day.IsDaytime(curTime))
            {
                SetDaytime();
            }

            RotateBody(moon.transform);
            SetLightIntensity(moon, midNightTick);
        }
    }

    private void SetDaytime()
    {
        isDaytime = true;
        // No two directional lights can be on at the same time, so disable one before enabling the other
        moon.enabled = false;
        sun.enabled = true;
        sun.intensity = intensityStart;
    }

    private void SetNighttime()
    {
        isDaytime = false;
        // No two directional lights can be on at the same time, so disable one before enabling the other
        sun.enabled = false;
        moon.enabled = true;
        moon.intensity = intensityStart;
    }

    private void RotateBody(Transform target)
    {
        float rotateAmount = (rotateStep * Time.deltaTime * deltaTimeMultiplier);
        target.rotation = Quaternion.Euler(new Vector3(target.rotation.eulerAngles.x + rotateAmount, 0f, 0f));
    }

    private void SetLightIntensity(Light target, double midTick)
    {
        if (curTime <= midTick)
        {
            target.intensity += intensityStepAmount * Time.deltaTime * deltaTimeMultiplier;
        }
        else
        {
            target.intensity -= intensityStepAmount * Time.deltaTime * deltaTimeMultiplier;
        }
    }

    /*
     * Formats curtime as HH:MM
     */
    private void SetDisplayTime(double curTime)
    {
        TimeDisplay.text = ((int)curTime).ToString("00") + ":" + ((int)((curTime % 1)*60)).ToString("00");
    }

}
