using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerWork : MonoBehaviour
{
    private WorkerManager manager;

    private WorkerObjective objective;
    private List<WorkerAbility> abilities;

    private double traitMultiplier = 1d;

    public bool isWorking = false;

    public void Initialize(WorkerManager manager, WorkerObjective objective, List<WorkerAbility> abilities, List<WorkerTrait> traits, double curTick)
    {
        this.manager = manager;
        this.objective = objective;
        this.abilities = abilities;
        isWorking = true;

        // Get work multiplier for each trait
        Debug.Log("Intiailized work at time " + curTick);
        foreach (WorkerTrait trait in traits)
        {
            traitMultiplier *= trait.WorkerTraitObjectiveMultiplier(objective, curTick);
            Debug.Log("Since worker has trait " + trait.traitType + " multipler is " + traitMultiplier);
        }

        objective.StartObjective();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWorking)
        {
            // Objective returns remaining work
            if (objective.ReduceWorkRemaining(Time.deltaTime * (float)traitMultiplier) <= 0)
            {
                manager.CompletedObjective();
            }
        }
    }
}
