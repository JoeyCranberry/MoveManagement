using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerObjective : MonoBehaviour
{
    public Transform location;
    
    public Vector2 completedableTime;
    public WorkerAbility abilityRequirement;
    
    public float workTime = 10f;

    public bool isComplete = false;

    [SerializeField]
    private float workTimeRemaining;

    private void Start()
    {
        workTimeRemaining = workTime;
    }

    public float ReduceWorkRemaining( float workDone )
    {
        if(!isComplete)
        {
            workTimeRemaining -= workDone;
        }
        
        if (workTimeRemaining <= 0f)
        {
            isComplete = true;
        }

        return workTimeRemaining;
    }

    public void Print()
    {
        Debug.Log("Location: " + location.position);
        Debug.Log("Completable Time: " + completedableTime);
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
