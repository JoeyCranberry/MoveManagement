using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorkerManager : MonoBehaviour
{
    public List<WorkerProject> projects = new List<WorkerProject>();

    public List<WorkerTrait> workerTraits = new List<WorkerTrait>();

    private WorkerMaster wMaster;

    private WorkerProject curProject;
    private WorkerObjective curObjective;

    public Transform idleLocation;

    public GameObject workerBody;

    private bool capableOfWork = true;

    private WorkerControl wControl;
    public List<WorkerAbility> abilities;

    private WorkerWork work;

    // Start is called before the first frame update
    void Start()
    {
        wControl = gameObject.AddComponent<WorkerControl>();
        wControl.Initialize(this);

        workerBody.AddComponent<WorkerFocus>().Initialize(this);

        StartNextProject();
    }

    public void Initialize(WorkerMaster wMaster)
    {
        this.wMaster = wMaster;
    }

    public void AddProject(WorkerProject newProject)
    {
        projects.Add(newProject);
        projects.OrderBy(x => (int)x.priority).ThenBy(x => x.createdDt).ToList();
    }

    public void CompleteCurrentProject()
    {
        projects.Remove(curProject);
        StartNextProject();
    }

    public void StartNextProject()
    {
        if (projects.Count > 0)
        {
            curProject = projects[0];
            curObjective = curProject.objectives[0];

            // Move worker towards objective to start project process
            wControl.AddWaypoint(curObjective.location);
        }
        else
        {
            curProject = null;
            curObjective = null;

            wControl.AddWaypoint(idleLocation);
        }
    }

    private void GetNextObjective()
    {
        curProject.objectives.Remove(curObjective);
        if (curProject.objectives.Count > 0)
        {
            curObjective = curProject.objectives[0];
            wControl.AddWaypoint(curObjective.location);
        }
        else
        {
            // No more objectives in the current project to complete, move on to the next
            CompleteCurrentProject();
        }
    }

    // Once the work controller has reached the acceptable distance from the objective, start work on the objective
    public void ReachedObjective()
    {
        work = gameObject.AddComponent<WorkerWork>();
        work.Initialize(this, curObjective, abilities, workerTraits, wMaster.dayManager.curTime);
    }

    public void CompletedObjective()
    {
        Destroy(work);
        GetNextObjective();
    }

    public void WorkerFocused()
    {
        wMaster.SetProjectDisplay(curProject, curObjective);
    }
}
