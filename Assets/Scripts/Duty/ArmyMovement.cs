using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyMovement : MonoBehaviour
{
    public State armyState = State.Stop;
    [SerializeField] float speed;

    public List<Vector3> targetPoints;
    [SerializeField] float timer = 1f;

    public LayerMask layerMask;

    [SerializeField] Transform enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (armyState == State.Move) Movement();
    }

    void Movement()
    {
        if (targetPoints.Count > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPoints[0], speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPoints[0]) <= 0.01f) targetPoints.RemoveAt(0);
        }
        else
        {
            //Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, 3f, layerMask);
            if (enemy != null)
            {
                MoveEnemy(enemy.position);
            }
            else armyState = State.Stop;
        }
    }

    void SetTimer()
    {

    }

    void MoveEnemy(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) <= 0.01f) armyState = State.Stop;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) armyState = State.Stop;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) enemy = collision.transform;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) enemy = null;
    }
}

public enum State { Stop, Move}
