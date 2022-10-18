using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerMaster : MonoBehaviour
{
    [SerializeField]
    private List<WorkerManager> workerManagers = new();

    public TMPro.TMP_Text WorkerProjectDisplay;
    public DayManager dayManager;

    private void Start()
    {
        var workers = gameObject.GetComponentsInChildren<WorkerManager>();
        workerManagers.AddRange(workers);

        foreach (WorkerManager worker in workerManagers)
        {
            worker.Initialize(this);
        }

        WorkerProjectDisplay.text = "";
    }

    public void SetProjectDisplay(WorkerProject proj, WorkerObjective obj)
    {
        string displayText = "*"+ proj.Title + "*" + "\n";
        bool foundActive = false;
        foreach(WorkerObjective objectives in proj.objectives)
        {
            displayText += "    " + objectives.Title;
            if(!foundActive)
            {
                if (objectives == obj)
                {
                    displayText += " (Active)";
                    foundActive = true;
                }
                else
                {
                    displayText += " (Completed)";
                }
            }
            displayText += "\n";
        }

        WorkerProjectDisplay.text = displayText;
    }
}
