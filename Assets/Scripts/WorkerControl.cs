using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class WorkerControl : MonoBehaviour
{
    private WorkerManager manager;

    public Queue<Transform> waypoints = new();
    private NavMeshAgent navAgent;

    public float switchWaypointDistance = 1f;
    private float curDistanceToObjective;

    private bool pathfindingToObjective;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void Initialize(WorkerManager manager)
    {
        this.manager = manager;
    }

    public void Update()
    {
        if(waypoints != null && pathfindingToObjective)
        {
            if(waypoints.Count > 0)
            {
                // Pathfind to object
                Vector3 waypoint = waypoints.Peek().position;
                navAgent.destination = waypoint;
                curDistanceToObjective = Vector3.Distance(gameObject.transform.position, waypoint);

                // If close enough to the current waypoint, delete it and move to the next
                if (curDistanceToObjective <= switchWaypointDistance)
                {
                    waypoints.Dequeue();
                }
            }
            else
            {
                pathfindingToObjective = false;
                manager.ReachedObjective();
            }
        }
    }

    public void AddWaypoint(Transform newWaypoint)
    {
        waypoints.Enqueue(newWaypoint);
        pathfindingToObjective = true;
    }

    public void ClearWaypoints()
    {
        waypoints.Clear();
        pathfindingToObjective = false;
    }
}
