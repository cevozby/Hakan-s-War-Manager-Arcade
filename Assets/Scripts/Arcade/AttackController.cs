using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] EnemyAI enemy;
    [SerializeField] PanelController panelController;

    public bool isAttackSuccess;
    public void SetFirstAttack()
    {
        float random = Random.value;
        if (random <= 0.5f) panelController.isPlayerAttack = true;
        else panelController.isPlayerAttack = false;
    }

    public void CheckThePlayerAttack()
    {
        Type enemyDefence = enemy.SelectDefence();
        int successRate = Random.Range(0, 101);
        if(enemyDefence == player.attackType)
        {
            if (successRate <= 50) isAttackSuccess = true;
            else isAttackSuccess = false;
        }
        else
        {
            if (successRate <= 25) isAttackSuccess = true;
            else isAttackSuccess = false;
        }
    }

    public void CheckThePlayerDefence()
    {
        int successRate = Random.Range(0, 101);
        if (enemy.attackType == player.defenceType)
        {
            if (successRate <= 50) isAttackSuccess = true;
            else isAttackSuccess = false;
        }
        else
        {
            if (successRate <= 25) isAttackSuccess = true;
            else isAttackSuccess = false;
        }
    }
}
