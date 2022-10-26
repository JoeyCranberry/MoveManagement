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

    // private bool capableOfWork = true;

    private WorkerControl wControl;
    public List<WorkerAbility> abilities;

    private WorkerWork work;

    private LineRenderer objectiveLine;
    private WorkerObjectiveLine objLineManager;

    // Start is called before the first frame update
    void Start()
    {
        wControl = gameObject.AddComponent<WorkerControl>();
        wControl.Initialize(this);

        workerBody.AddComponent<WorkerFocus>().Initialize(this);

        objectiveLine = gameObject.GetComponent<LineRenderer>();
        objLineManager = gameObject.AddComponent<WorkerObjectiveLine>();
        objLineManager.Initialize(objectiveLine, workerBody.transform);

        StartNextProject();
    }

    public void Initialize(WorkerMaster wMaster)
    {
        this.wMaster = wMaster;
    }


    #region Project State Management
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
            if (curProject.objectives.Count > 0)
            {
                curObjective = curProject.objectives[0];

                // Move worker towards objective to start project process
                wControl.AddWaypoint(curObjective.location);

                SetObjectiveLines(curProject);
            }
            else
            {
                Debug.LogWarning("Current project has no objectives");
                projects.Remove(curProject);
                StartNextProject();
            }
        }
        else
        {
            curProject = null;
            curObjective = null;

            wControl.AddWaypoint(idleLocation);
            objLineManager.AddLinePoints(new Vector3[2] { workerBody.transform.position, idleLocation.position });
        }
    }
    #endregion

    #region Objective State Management
    private void GetNextObjective()
    {
        curProject.objectives.Remove(curObjective);
        if (curProject.objectives.Count > 0)
        {
            curObjective = curProject.objectives[0];
            wControl.AddWaypoint(curObjective.location);
            wMaster.SetProjectDisplay(curProject, curObjective);

            SetObjectiveLines(curProject);
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
        if (curObjective != null)
        {
            work.Initialize(this, curObjective, abilities, workerTraits, wMaster.dayManager.curTime);
        }
    }

    public void CompletedObjective()
    {
        if (work != null)
        {
            Destroy(work);
        }

        GetNextObjective();
    }
    #endregion

    #region Focusing
    public void WorkerFocused()
    {
        wMaster.SetProjectDisplayVisibility(true);
        wMaster.SetProjectDisplay(curProject, curObjective);
    }

    /*
     * Recieved when a worker was focused and then an objective was focused
     */
    public void ObjectiveManuallyAssigned(WorkerProject proj)
    {
        // Add current project back to to-do list
        projects.Add(curProject);

        curProject = proj;
        curObjective = curProject.objectives[0];

        // Set waypoint for controller
        wControl.ClearWaypoints();
        wControl.AddWaypoint(curObjective.location);

        wMaster.SetProjectDisplay(curProject, curObjective);

        SetObjectiveLines(curProject);
    }
    #endregion

    private void SetObjectiveLines(WorkerProject project)
    {
        objLineManager.AddProjectObjectivePositions(project, workerBody.transform);
    }
}
