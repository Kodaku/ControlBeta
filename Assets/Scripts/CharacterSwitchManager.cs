using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CharacterSwitchManager : MonoBehaviour
{
    [SerializeField] private GameObject gary;
    [SerializeField] private GameObject erick;
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
            }
            else if(GameManager.IsControllingGary)
            {
                cinemachineFreeLook.Follow = gary.transform;
                cinemachineFreeLook.LookAt = gary.transform;
                gary.gameObject.tag = "Player";
                erick.gameObject.tag = "Erick";
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
        }
        else if(GameManager.IsControllingGary)
        {
            cinemachineFreeLook.Follow = gary.transform;
            cinemachineFreeLook.LookAt = gary.transform;
            gary.gameObject.tag = "Player";
            erick.gameObject.tag = "Erick";
        }
    }
}
