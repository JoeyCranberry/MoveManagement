using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerWork : MonoBehaviour
{
    private WorkerManager manager;

    public WorkerObjective objective;
    public List<WorkerAbility> abilities;

    public bool isWorking = false;

    public void Initialize(WorkerManager manager, WorkerObjective objective, List<WorkerAbility> abilities )
    {
        this.manager = manager;
        this.objective = objective;
        this.abilities = abilities;
        isWorking = true;

        objective.StartObjective();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWorking)
        {
            // Objective returns remaining work
            if (objective.ReduceWorkRemaining(Time.deltaTime) <= 0)
            {
                manager.CompletedObjective();
            }
        }
    }
}
