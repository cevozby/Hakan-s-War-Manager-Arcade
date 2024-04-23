using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    LineRenderer lineRenderer;

    List<Vector3> points = new List<Vector3>();

    public Action<IEnumerable<Vector3>> OnNewPathCreated = delegate { };

    [SerializeField] float distanceOfPoints;
    [SerializeField] Transform player;

    void Awake ()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        points.Add(player.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            points.Clear();
            points.Add(player.position);
        }

        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.up);
            if (hit.collider != null)
            {
                if (DistanceToLastPoint(hit.point) > distanceOfPoints)
                {
                    points.Add(hit.point);

                    lineRenderer.positionCount = points.Count;
                    lineRenderer.SetPositions(points.ToArray());
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
            OnNewPathCreated(points);
    }

    private float DistanceToLastPoint(Vector3 point)
    {
        if (!points.Any())
            return Mathf.Infinity;
        return Vector3.Distance(points.Last(), point);
    }
}
