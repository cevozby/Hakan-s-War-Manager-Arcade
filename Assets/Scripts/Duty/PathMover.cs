using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMover : MonoBehaviour
{
    private Queue<Vector3> pathPoints = new Queue<Vector3>();
    [SerializeField] float speed;

    int currentIndex;
    Vector3 currentPoint;

    private void Awake()
    {
        currentPoint = transform.position;
        FindObjectOfType<PathCreator>().OnNewPathCreated += SetPoints;
    }

    private void SetPoints(IEnumerable<Vector3> points)
    {
        pathPoints = new Queue<Vector3>(points);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePathing();
    }

    private void UpdatePathing()
    {
        if (ShouldDestination())
            currentPoint = pathPoints.Dequeue();
        else if (!ShouldDestination())
            Movement();
    }

    private void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPoint, speed * Time.deltaTime);
    }

    private bool ShouldDestination()
    {
        if (pathPoints.Count == 0)
            return false;
        if (currentPoint == null || Vector2.Distance(transform.position, currentPoint) <= 0.01f)
            return true;

        return false;
    }
}
