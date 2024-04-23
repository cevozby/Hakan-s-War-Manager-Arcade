using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetDuty : MonoBehaviour
{
    [SerializeField] ArmyMovement armyMovement;
    [SerializeField] GameObject circlePrefab;

    private void Update()
    {
        SetPatrolPoints();
    }

    void SetPatrolPoints()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit;
            if (EventSystem.current.IsPointerOverGameObject()) return;
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            bool isHit = Physics2D.Raycast(ray.origin, ray.direction);
            if (isHit) return;
            target.z = 0f;
            Instantiate(circlePrefab, target, Quaternion.identity);
            armyMovement.targetPoints.Add(target);
        }
    }
}
