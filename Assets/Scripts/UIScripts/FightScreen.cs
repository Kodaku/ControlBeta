using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightScreen : MonoBehaviour
{
    public void EndFightPreparation()
    {
        GameManager.IsPreparingFight = false;
        GameManager.IsFightStarted = true;
        GameManager.IsFightEnded = false;
        this.gameObject.SetActive(false);
    }
}
