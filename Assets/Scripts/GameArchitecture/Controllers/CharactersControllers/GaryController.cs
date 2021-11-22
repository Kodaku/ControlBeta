using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaryController : GeneralController
{
    public override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        if(!GameManager.HasGaryPower)
        {
            aura.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(GameManager.IsControllingGary)
        {
            buttonController.ProcessInput();
            dialogueTrigger.ProcessInput();
        }
        if(GameManager.HasGaryPower)
        {
            // print("Power");
            buttonController.ActivatePower();
            aura.gameObject.SetActive(true);
        }
        if(GameManager.IsControllingErick)
        {
            // follow.FollowTarget();
        }
    }
}
