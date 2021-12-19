using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CharacterSwitchManager : MonoBehaviour
{
    [SerializeField] private GameObject gary;
    [SerializeField] private GameObject erick;
    [SerializeField] private GameObject garyLife;
    [SerializeField] private GameObject garyMana;
    [SerializeField] private GameObject erickLife;
    [SerializeField] private GameObject erickMana;
    [SerializeField] private CinemachineFreeLook cinemachineFreeLook;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B) && GameManager.IsDoubleControlEnabled && !DialogueDirector.IsShowingDialogue)
        {
            GameManager.IsControllingErick = !GameManager.IsControllingErick;
            GameManager.IsControllingGary = !GameManager.IsControllingGary;
            if(GameManager.IsControllingErick)
            {
                cinemachineFreeLook.Follow = erick.transform;
                cinemachineFreeLook.LookAt = erick.transform;
                erick.gameObject.tag = "Player";
                gary.gameObject.tag = "Erick";
                if(GameManager.IsFightStarted)
                {
                    garyLife.SetActive(false);
                    garyMana.SetActive(false);
                    erickLife.SetActive(true);
                    erickMana.SetActive(true);
                }
            }
            else if(GameManager.IsControllingGary)
            {
                cinemachineFreeLook.Follow = gary.transform;
                cinemachineFreeLook.LookAt = gary.transform;
                gary.gameObject.tag = "Player";
                erick.gameObject.tag = "Erick";
                if(GameManager.IsFightStarted)
                {
                    garyLife.SetActive(true);
                    garyMana.SetActive(true);
                    erickLife.SetActive(false);
                    erickMana.SetActive(false);
                }
            }
            HumanPlayerMessage humanPlayerMessage = gary.GetComponent<HumanPlayerMessage>();
            foreach(string targetName in SpecialAttackTargetManager.targetNames)
            {
                humanPlayerMessage.PrepareAndSendMessage(MessageTypes.SWITCH_CHARACTER, new string[]{"Enemy", targetName});
            }
        }
    }

    public void SwitchCharacter()
    {
        GameManager.IsControllingErick = !GameManager.IsControllingErick;
        GameManager.IsControllingGary = !GameManager.IsControllingGary;
        if(GameManager.IsControllingErick)
        {
            cinemachineFreeLook.Follow = erick.transform;
            cinemachineFreeLook.LookAt = erick.transform;
            erick.gameObject.tag = "Player";
            gary.gameObject.tag = "Erick";
            if(GameManager.IsFightStarted)
            {
                garyLife.SetActive(false);
                garyMana.SetActive(false);
                erickLife.SetActive(true);
                erickMana.SetActive(true);
            }
        }
        else if(GameManager.IsControllingGary)
        {
            cinemachineFreeLook.Follow = gary.transform;
            cinemachineFreeLook.LookAt = gary.transform;
            gary.gameObject.tag = "Player";
            erick.gameObject.tag = "Erick";
            if(GameManager.IsFightStarted)
            {
                garyLife.SetActive(true);
                garyMana.SetActive(true);
                erickLife.SetActive(false);
                erickMana.SetActive(false);
            }
        }
    }
}
