using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelController : MonoBehaviour
{
    [SerializeField] GameObject optionPanel;
    [SerializeField] GameObject resultPanel;

    [SerializeField] TextMeshProUGUI panelTitle;
    [SerializeField] TextMeshProUGUI resultTitle;
    [SerializeField] TextMeshProUGUI battleText;

    [SerializeField] List<Button> types;
    [SerializeField] Player player;
    Type type;

    [SerializeField] AttackController attackController;
    [SerializeField] EnemyAI enemyAI;

    public bool isAttack; 
    public bool isPlayerAttack;

    [SerializeField] float timer;
    // Start is called before the first frame update
    void OnEnable()
    {
        
    }

    public void OpenOptionPanel()
    {
        resultPanel.SetActive(false);
        for (int i = 0; i < types.Count; i++)
        {
            types[i].onClick.RemoveAllListeners();
            type = (Type)i;
            types[i].GetComponentInChildren<TextMeshProUGUI>().text = type.ToString();
            if (isPlayerAttack)
            {
                panelTitle.text = "Player Attack";
                types[i].onClick.AddListener(() => SetPlayerAttackType(i));
            }
            else
            {
                panelTitle.text = "Player Defence";
                types[i].onClick.AddListener(() => SetPlayerDefenceType(i));
                enemyAI.SelectAttack();
            }
        }
        optionPanel.SetActive(true);
    }

    void SetPlayerAttackType(int i)
    {
        player.attackType = (Type)i;
        attackController.CheckThePlayerAttack();
        OpenSecondPanel();
    }

    void SetPlayerDefenceType(int i)
    {
        player.defenceType = (Type)i;
        attackController.CheckThePlayerDefence();
        OpenSecondPanel();
    }

    void OpenSecondPanel()
    {
        optionPanel.SetActive(false);
        resultPanel.SetActive(true);
        if (isPlayerAttack && attackController.isAttackSuccess)
        {
            resultTitle.text = "Attack Success";
            battleText.text = "Player attack: " + player.attackType.ToString() + "\nEnemy defence: " + enemyAI.defenceType.ToString();

        }
        else if (isPlayerAttack && !attackController.isAttackSuccess)
        {
            resultTitle.text = "Attack Unsuccess";
            battleText.text = "Player attack: " + player.attackType.ToString() + "\nEnemy defence: " + enemyAI.defenceType.ToString();
        }
        else if (!isPlayerAttack && attackController.isAttackSuccess)
        {
            resultTitle.text = "Defence Unsuccess";
            battleText.text = "Enemy attack: " + enemyAI.attackType.ToString() + "\nPlayer defence: " + player.defenceType.ToString();
        }
        else if(!isPlayerAttack && !attackController.isAttackSuccess)
        {
            resultTitle.text = "Defence Success";
            battleText.text = "Enemy attack: " + enemyAI.attackType.ToString() + "\nPlayer defence: " + player.defenceType.ToString();
        }
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        yield return new WaitForSeconds(timer);
        isPlayerAttack = !isPlayerAttack;
        OpenOptionPanel();
    }
}
public enum Type { Sword, Spear, Arrow, Cavalry }
