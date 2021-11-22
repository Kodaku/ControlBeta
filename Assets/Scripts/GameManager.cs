using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool HasGaryPower = false;
    public static bool HasErickPower = false;
    public static bool HasBrucePower = false;
    public static bool IsControllingGary = true;
    public static bool IsControllingErick = false;
    public static bool IsPreparingFight = false;
    public static bool IsFightStarted = false;
    public static bool IsFightEnded = true;
    public static bool IsPlayerDead = false;
    public static bool IsDoubleControlEnabled = false;
    public static bool CanTeleportPlayer = false;
    public static bool IsBossBattle = false;
    public static GameObject transitionScreen;
    public static WinLoseScreen winLoseScreen;
    public static EventsTriggerManager eventsTriggerManager;
    public static CharacterSwitchManager characterSwitchManager;
    public static WavesManagerController wavesController;

    void Start()
    {
        IsPlayerDead = false;
        IsFightEnded = true;
        IsFightStarted = false;
        HasGaryPower = false;
        HasErickPower = false;
        HasBrucePower = false;
        IsPreparingFight = false;
        IsControllingGary = true;
        IsControllingErick = false;
        IsDoubleControlEnabled = false;
        CanTeleportPlayer = false;
        IsBossBattle = false;
        transitionScreen = GameObject.FindGameObjectWithTag("TransitionScreen");
        winLoseScreen = GameObject.FindGameObjectWithTag("WinLoseScreen").GetComponent<WinLoseScreen>();
        eventsTriggerManager = GameObject.FindGameObjectWithTag("TriggerManager").GetComponent<EventsTriggerManager>();
        characterSwitchManager = GameObject.FindGameObjectWithTag("SwitchManager").GetComponent<CharacterSwitchManager>();
        wavesController = GameObject.FindGameObjectWithTag("WavesManager").GetComponent<WavesManagerController>();
        transitionScreen.gameObject.SetActive(false);
        winLoseScreen.gameObject.SetActive(false);
    }

    public static void ShowTransitionScreen()
    {
        transitionScreen.gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("TransitionScreen").GetComponent<Animator>().Play("FadeIn");
    }

    public static void ShowWinLoseScreen()
    {
        winLoseScreen.gameObject.SetActive(true);
    }

    public static void ShowWinLoseText(bool hasWon)
    {
        if(hasWon)
        {
            winLoseScreen.Win();
        }
        else
        {
            winLoseScreen.Lose();
        }
    }

    public static void LoadNextSceneTrigger()
    {
        eventsTriggerManager.LoadNextSceneTrigger();
    }

    public static void LoadAndDestroyNextSceneTrigger()
    {
        eventsTriggerManager.LoadAndDestroyNextSceneTrigger();
    }

    public static void SwitchCharacter()
    {
        characterSwitchManager.SwitchCharacter();
    }

    public static void SpawnFirstWave()
    {
        wavesController.SpawnWaveManager();
    }

    public static void SpawnWave()
    {
        wavesController.SpawnEnemies();
    }
}
