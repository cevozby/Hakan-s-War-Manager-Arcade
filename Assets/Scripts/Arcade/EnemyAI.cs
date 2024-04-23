using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Type attackType;
    public Type defenceType;
    public bool isAttack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Type SelectAttack()
    {
        int random = Random.Range(0, 4);
        attackType = (Type)random;
        return (Type)random;
    }

    public Type SelectDefence()
    {
        int random = Random.Range(0, 4);
        defenceType = (Type)random;
        return (Type)random;
    }
}
