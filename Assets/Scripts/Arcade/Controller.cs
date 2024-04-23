using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public bool isEngage;
    [SerializeField] GameObject optionPanel;

    [SerializeField] PanelController panelController;
    [SerializeField] AttackController attackController;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Controller>().isEngage = true;
        if (!isEngage)
        {
            isEngage = true;
            attackController.SetFirstAttack();
            panelController.OpenOptionPanel();
        }
    }
}