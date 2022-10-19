using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorkerFocus : MonoBehaviour
{
    private WorkerManager manager;

    public void Initialize(WorkerManager manager)
    {
        this.manager = manager;
    }

    public WorkerManager FocusThis()
    {
        if(manager != null)
        {
            manager.WorkerFocused();
            return manager;
        }

        return null;
    }    
}
