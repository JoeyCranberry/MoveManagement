using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerProject : MonoBehaviour
{
    public string Title;

    public List<WorkerObjective> objectives = new List<WorkerObjective>();
    public ObjectivePriority priority;
    public DateTime createdDt;

    public void Initialize(ObjectivePriority priority)
    {
        this.priority = priority;
        createdDt = DateTime.Now;
    }
}

public enum ObjectivePriority
{
    MANUAL,
    CRITICAL,
    IMPORTANT,
    NORMAL,
    UNIMPORTANT,
    LOW
}
