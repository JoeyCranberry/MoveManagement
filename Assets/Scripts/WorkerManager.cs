using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorkerManager : MonoBehaviour
{
    public List<WorkerObjective> objectives = new();
    private WorkerObjective curObjective;

    private bool capableOfWork = true;

    private WorkerControl wControl;
    public List<WorkerAbility> abilities;

    private WorkerWork work;

    // Start is called before the first frame update
    void Start()
    {
        wControl = gameObject.AddComponent<WorkerControl>();
        wControl.Initialize(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(capableOfWork)
        {
            // Doesn't have an objective, but there are objectives availible
            if(curObjective == null && objectives.Count > 0)
            {
                curObjective = objectives[0];

                wControl.AddWaypoint(curObjective.location);
            }
        }
    }

    private void AddObjecttive(WorkerObjective obj)
    {
        if(!abilities.Contains(obj.abilityRequirement))
        {
            Debug.Log("Worker cannot complete objective: does not have ability");
        }
        else
        {
            objectives.Add(obj);

            // Order the objectives by priority, then whichever was created first
            objectives = objectives.OrderBy(x => (int)x.priority).ThenBy(x => x.createdDt).ToList();
        }
    }

    private void GetNextObjective()
    {
        objectives.Remove(curObjective);
        if (objectives.Count > 0)
        {
            curObjective = objectives[0];
            wControl.AddWaypoint(curObjective.location);
        }
    }

    public void ReachedObjective()
    {
        work = gameObject.AddComponent<WorkerWork>();
        work.Initialize(this, curObjective, abilities);
    }

    public void CompletedObjective()
    {
        Destroy(work);
        GetNextObjective();
    }
}
