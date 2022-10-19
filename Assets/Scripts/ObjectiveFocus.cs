using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveFocus : MonoBehaviour
{
    public WorkerObjective objective;

    public void Initialize(WorkerObjective objective)
    {
        this.objective = objective;
    }

    /*
     * When a worker has been focused previously, set this current objective to be active for that worker
     */
    public void FocusThisWorker(WorkerManager worker)
    {
        // Since this objective is focused, it needs a project to contain it
        // So create a new project with only this as the objective
        var project = objective.gameObject.AddComponent<WorkerProject>();
        project.Initialize(ObjectivePriority.MANUAL);
        project.objectives.Add(objective);
        project.Title = objective.Title;

        worker.ObjectiveManuallyAssigned(project);
    }

    public void FocusThisNoWorker()
    {
        Debug.Log("Objective " + objective.Title + " focussed");
    }
}
