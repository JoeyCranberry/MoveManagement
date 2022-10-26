using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorkerMaster : MonoBehaviour
{
    [SerializeField]
    private List<WorkerManager> workerManagers = new();

    public GameObject WorkerProjectDisplayObj;
    private TMP_Text workerProjectDisplay;
    public DayManager dayManager;

    public bool projectDisplayVisible = false;

    private void Start()
    {
        var workers = gameObject.GetComponentsInChildren<WorkerManager>();
        workerManagers.AddRange(workers);

        foreach (WorkerManager worker in workerManagers)
        {
            worker.Initialize(this);
        }

        workerProjectDisplay = WorkerProjectDisplayObj.GetComponent<TMP_Text>();
        workerProjectDisplay.text = "";
    }

    public void SetProjectDisplay(WorkerProject proj, WorkerObjective obj)
    {
        if(projectDisplayVisible)
        {
            string displayText;
            if (proj == null)
            {
                displayText = "No active projects";
            }
            else if(obj == null)
            {
                displayText = "No active objectives";
            }
            else
            {
                displayText = "*" + proj.Title + "*" + "\n";
                bool foundActive = false;
                foreach (WorkerObjective objectives in proj.objectives)
                {
                    displayText += "    " + objectives.Title;
                    if (!foundActive)
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
            }

            workerProjectDisplay.text = displayText;
        }
    }

    public void SetProjectDisplayVisibility(bool isVisible)
    {
        WorkerProjectDisplayObj.SetActive(isVisible);
        projectDisplayVisible = isVisible;
    }
}
