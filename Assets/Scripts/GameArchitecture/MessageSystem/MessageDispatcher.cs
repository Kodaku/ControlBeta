using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageDispatcher
{

    public void DispatchMessage(MessageTypes messageType, string info)
    {
        List<string> splittedString = new List<string>(info.Split(','));
        string destTag = splittedString[0];
        switch(messageType)
        {
            case MessageTypes.BEGIN_ATTACK:
            {
                //Extracting the destination tag and all the names the player wants to attack
                string canMove = splittedString[splittedString.Count - 1];
                splittedString.Remove(destTag); //now the splitted string contains just the target names
                splittedString.Remove(canMove);
                string data = PacketCreator.PrepareMessage(new string[] {canMove});
                string[] destNames = splittedString.ToArray();
                SendMessageToPlayer(messageType, destTag, data, destNames);
                break;
            }
            case MessageTypes.BEGIN_EXPLOSION_ATTACK:
            {
                //Extracting the destination tag and all the names the player wants to attack
                string canMove = splittedString[splittedString.Count - 1];
                splittedString.Remove(destTag); //now the splitted string contains just the target names
                splittedString.Remove(canMove);
                string data = PacketCreator.PrepareMessage(new string[] {canMove});
                string[] destNames = splittedString.ToArray();
                SendMessageToPlayer(messageType, destTag, data, destNames);
                break;
            }
            case MessageTypes.EXECUTE_ATTACK:
            {
                string damage = splittedString[splittedString.Count - 2];
                string range = splittedString[splittedString.Count - 1];
                splittedString.Remove(destTag);
                splittedString.Remove(damage);
                splittedString.Remove(range);
                string data = PacketCreator.PrepareMessage(new string[] {damage, range});
                string[] destNames = splittedString.ToArray();
                SendMessageToPlayer(messageType, destTag, data, destNames);
                break;
            }
            case MessageTypes.EXECUTE_EXPLOSION_ATTACK:
            {
                string damage = splittedString[splittedString.Count - 2];
                string range = splittedString[splittedString.Count - 1];
                splittedString.Remove(destTag);
                splittedString.Remove(damage);
                splittedString.Remove(range);
                string data = PacketCreator.PrepareMessage(new string[] {damage, range});
                string[] destNames = splittedString.ToArray();
                SendMessageToPlayer(messageType, destTag, data, destNames);
                break;
            }
            case MessageTypes.LAUNCH_PROJECTILE:
            {
                splittedString.Remove(destTag); //now the splitted string contains just the target names
                string[] destNames = splittedString.ToArray();
                SendMessageToPlayer(messageType, destTag, "", destNames);
                break;
            }
            case MessageTypes.UPDATE_MANA:
            {
                splittedString.Remove(destTag);
                string manaCount = splittedString[1];
                splittedString.Remove(manaCount);
                string updateMethod = splittedString[1];
                splittedString.Remove(updateMethod);
                string data = PacketCreator.PrepareMessage(new string[]{manaCount, updateMethod});
                string[] destNames = splittedString.ToArray();
                // print("Dest name: " + destNames[0]);
                SendMessageToPlayer(messageType, destTag, data, destNames);
                break;
            }
            case MessageTypes.BEGIN_SPECIAL_ATTACK: case MessageTypes.END_SPECIAL_ATTACK: case MessageTypes.EVADE: case MessageTypes.ACTION_TERMINATED:
            {
                splittedString.Remove(destTag);
                string[] destNames = splittedString.ToArray();
                SendMessageToPlayer(messageType, destTag, "", destNames);
                break;
            }
            case MessageTypes.APPLY_PUNCH_DAMAGE:
            {
                splittedString.Remove(destTag);
                string damage = splittedString[1];
                splittedString.Remove(damage);
                string data = PacketCreator.PrepareMessage(new string[] {damage});
                string[] destNames = splittedString.ToArray();
                SendMessageToPlayer(messageType, destTag, data, destNames);
                break;
            }
        }
    }

    private void SendMessageToPlayer(MessageTypes messageType, string destTag, string info, string[] destNames)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(destTag);
        foreach(GameObject target in targets)
        {
            string destName = Array.Find<string>(destNames, name => name == target.name);
            if(destName != "")
            {
                PlayerMessage playerMessage= target.GetComponent<PlayerMessage>();
                playerMessage.ReceiveMessage(messageType, info);
            }
        }
    }
}
