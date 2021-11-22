using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErickController : GeneralController
{
    public override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        if(!GameManager.HasErickPower)
        {
            aura.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(GameManager.IsControllingErick)
        {
            buttonController.ProcessInput();
            dialogueTrigger.ProcessInput();
        }
        if(GameManager.HasErickPower)
        {
            buttonController.ActivatePower();
            aura.gameObject.SetActive(true);
        }
        if(GameManager.IsControllingGary && GameManager.CanTeleportPlayer)
        {
            
        }
    }
}
