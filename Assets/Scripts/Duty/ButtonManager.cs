using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] ArmyMovement armyMovement;
    

    public void StartMission()
    {
        armyMovement.armyState = State.Move;
    }
}
