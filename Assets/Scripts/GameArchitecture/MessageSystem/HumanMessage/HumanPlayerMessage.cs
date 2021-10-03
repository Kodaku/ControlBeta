using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayerMessage : PlayerMessage
{
    protected ButtonController buttonController;
    // Start is called before the first frame update
    public virtual void Start()
    {
        buttonController = GetComponent<ButtonController>();
    }

    public override void ReceiveMessage(MessageTypes messageType, string message)
    {
        base.ReceiveMessage(messageType, message);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
