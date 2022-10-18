using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkerObjective : MonoBehaviour
{
    public string Title;
    public string Description;
    public Transform location;
    
    public Vector2 completedableDayTime;
    public WorkerAbility abilityRequirement;

    public Slider progressSlider;
    
    public float workTime = 10f;

    public bool isComplete = false;

    [SerializeField]
    private float workTimeRemaining;

    private void Start()
    {
        workTimeRemaining = workTime;
        if(progressSlider != null)
        {
            progressSlider.gameObject.SetActive(false);
        }
    }

    private void SetupSlider(Slider slider)
    {
        if(slider != null)
        {
            progressSlider.maxValue = workTime;
            progressSlider.value = 0;
        }
    }

    public float ReduceWorkRemaining( float workDone )
    {
        if(!isComplete)
        {
            workTimeRemaining -= workDone;

            if(progressSlider != null)
            {
                progressSlider.value = workTimeRemaining;
            }
        }
        
        if (workTimeRemaining <= 0f)
        {
            SetComplete();
        }

        return workTimeRemaining;
    }

    private void SetComplete()
    {
        isComplete = true;
        if (progressSlider != null)
        {
            progressSlider.gameObject.SetActive(value: false);
        }
    }

    public void StartObjective()
    {
        if(progressSlider != null)
        {
            progressSlider.gameObject.SetActive(true);
        }

        SetupSlider(progressSlider);
    }        

    public void Print()
    {
        Debug.Log("Location: " + location.position);
        Debug.Log("Ability Requirement: " + abilityRequirement);
    }
}

public enum WorkerAbility
{
    GENERAL,
    HAUL,
    CLEAN,
    CONSTRUCT
}
