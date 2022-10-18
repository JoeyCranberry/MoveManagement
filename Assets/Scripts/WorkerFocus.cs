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

    public void FocusThis()
    {
        if(manager != null)
        {
            manager.WorkerFocused();
        }
    }    
}
