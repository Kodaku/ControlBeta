using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScreen : MonoBehaviour
{
    public GameObject playerLife;
    public GameObject playerMana;
    public GameObject erickLife;
    public GameObject erickMana;
    public GameObject fightScreen;
    public GameObject enemyHealth;
    public GameObject bossHealth;
    public GameObject bossMana;

    public void PreparingFight()
    {
        GameManager.IsPreparingFight = true;
    }
    public void SimpleDeactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void FightDeactivate()
    {
        if(GameManager.IsControllingGary)
        {
            playerLife.SetActive(true);
            playerMana.SetActive(true);
        }
        else if(GameManager.IsControllingErick)
        {
            erickLife.SetActive(true);
            erickMana.SetActive(true);
        }
        if(GameManager.IsBossBattle)
        {
            bossHealth.SetActive(true);
            bossMana.SetActive(true);
        }
        else
            enemyHealth.SetActive(true);
        fightScreen.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
