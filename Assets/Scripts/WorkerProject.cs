using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerProject : MonoBehaviour
{
    public List<WorkerObjective> objectives = new List<WorkerObjective>();
    public ObjectivePriority priority;
    public DateTime createdDt;
}
public enum ObjectivePriority
{
    CRITICAL,
    IMPORTANT,
    NORMAL,
    UNIMPORTANT,
    LOW
}
