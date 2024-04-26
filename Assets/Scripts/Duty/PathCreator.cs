using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    LineRenderer lineRenderer;

    List<Vector3> points = new List<Vector3>();

    public Action<IEnumerable<Vector3>> OnNewPathCreated = delegate { };

    [SerializeField] float distanceOfPoints;
    [SerializeField] Transform player;

    bool isDrawPath;

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
        //if (Input.GetMouseButtonDown(0))
        //{
        //    //points.Clear();
        //    points.Add(player.position);
            
        //}

        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.up);
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
            {
                isDrawPath = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDrawPath = false;
            EdgeCollider2D edge = GetComponent<EdgeCollider2D>();
            List<Vector2> points2D = new List<Vector2>();
            foreach (var item in points)
            {
                points2D.Add(item);
            }
            edge.SetPoints(points2D);
            OnNewPathCreated(points);
        }
        if (isDrawPath) DrawPath();
    }

    void DrawPath()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        point.z = 0;
        if (DistanceToLastPoint(point) > distanceOfPoints)
        {
            points.Add(point);

            lineRenderer.positionCount = points.Count;
            lineRenderer.SetPositions(points.ToArray());
        }
    }

    private float DistanceToLastPoint(Vector3 point)
    {
        if (!points.Any())
            return Mathf.Infinity;
        return Vector3.Distance(points.Last(), point);
    }
}
