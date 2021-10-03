using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMessage : MonoBehaviour
{
    private MessageDispatcher messageDispatcher;
    void Awake()
    {
        messageDispatcher = new MessageDispatcher();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrepareAndSendMessage(MessageTypes messageType, string[] info)
    {
        string data = PacketCreator.PrepareMessage(info);
        SendMessage(messageType, data);
    }

    private void SendMessage(MessageTypes messageType, string data)
    {
        messageDispatcher.DispatchMessage(messageType, data);
    }

    public virtual void ReceiveMessage(MessageTypes messageType, string message)
    {
        
    }
}
