using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerObjectiveLine : MonoBehaviour
{
    private LineRenderer line;

    private Transform startPosition;

    public void Initialize(LineRenderer line, Transform workerTransform)
    {
        this.line = line;
        startPosition = workerTransform;
    }

    private void Update()
    {
        line.SetPosition(0, startPosition.position);
    }

    public void AddProjectObjectivePositions(WorkerProject project, Transform worker)
    {
        List<WorkerObjective> objectives = project.objectives;


        Vector3[] positions = new Vector3[objectives.Count + 1];
        positions[0] = worker.position;

        for (int i = 0; i < objectives.Count; i++)
        {
            positions[i + 1] = objectives[i].location.position;
        }

        AddLinePoints(positions);
    }

    public void AddLinePoints(Vector3[] newPoints)
    {
        line.positionCount = newPoints.Length;
        line.SetPositions(newPoints);
    }
}
