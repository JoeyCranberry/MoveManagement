using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerObjective : MonoBehaviour
{
    public Transform location;
    public ObjectivePriority priority;
    public Vector2 completedableTime;
    public WorkerAbility abilityRequirement;
    public DateTime createdDt;
    public float workTime = 10f;

    public bool isComplete = false;

    [SerializeField]
    private float workTimeRemaining;

    private void Start()
    {
        workTimeRemaining = workTime;
        createdDt = DateTime.Now;
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
        Debug.Log("Priority: " + priority);
        Debug.Log("Completable Time: " + completedableTime);
        Debug.Log("Ability Requirement: " + abilityRequirement);
        Debug.Log("Created Time: " + createdDt);
    }
}

public enum ObjectivePriority
{
    CRITICAL,
    IMPORTANT,
    NORMAL,
    UNIMPORTANT,
    LOW
}

public enum WorkerAbility
{
    GENERAL,
    HAUL,
    CLEAN,
    CONSTRUCT
}
