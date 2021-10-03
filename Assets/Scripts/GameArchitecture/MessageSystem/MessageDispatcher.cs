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
                // string destNames = PacketCreator.PrepareMessage(splittedString.ToArray());
                string data = PacketCreator.PrepareMessage(new string[] {canMove});
                // SendStartAttackMessage(destTag, canMove, splittedString.ToArray());
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
                // SendStartExplosionAttackMessage(destTag, canMove, splittedString.ToArray());
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
                // SendExecuteAttackMessage(destTag, damage, range, splittedString.ToArray());
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
                // SendExecuteExplosionAttackMessage(destTag, damage, range, splittedString.ToArray());
                break;
            }
            case MessageTypes.LAUNCH_PROJECTILE:
            {
                splittedString.Remove(destTag); //now the splitted string contains just the target names
                // string data = PacketCreator.PrepareMessage(new string[] {damage, range});
                string[] destNames = splittedString.ToArray();
                SendMessageToPlayer(messageType, destTag, "", destNames);
                // SendProjectileAttackMessage(destTag, splittedString.ToArray());
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
            case MessageTypes.BEGIN_SPECIAL_ATTACK:
            {
                splittedString.Remove(destTag);
                string[] destNames = splittedString.ToArray();
                SendMessageToPlayer(messageType, destTag, "", destNames);
                break;
            }
            case MessageTypes.END_SPECIAL_ATTACK:
            {
                splittedString.Remove(destTag);
                string[] destNames = splittedString.ToArray();
                SendMessageToPlayer(messageType, destTag, "", destNames);
                break;
            }
            case MessageTypes.EVADE:
            {
                splittedString.Remove(destTag);
                string[] destNames = splittedString.ToArray();
                SendMessageToPlayer(messageType, destTag, "", destNames);
                break;
            }
            case MessageTypes.REACHED_FLEE_POINT:
            {
                splittedString.Remove(destTag);
                string[] destNames = splittedString.ToArray();
                SendMessageToPlayer(messageType, destTag, "", destNames);
                break;
            }
        }
    }

    // private void SendStartAttackMessage(string destTag, string canMove, string[] destNames)
    // {
    //     GameObject[] targets = GameObject.FindGameObjectsWithTag(destTag);
    //     foreach(GameObject target in targets)
    //     {
    //         string destName = Array.Find<string>(destNames, name => name == target.name);
    //         if(destName != "")
    //         {
    //             PlayerAttack playerAttack = target.GetComponent<PlayerAttack>();
    //             playerAttack.ReceiveMessage(MessageTypes.BEGIN_ATTACK, canMove);
    //         }
    //     }
    // }
    // private void SendStartExplosionAttackMessage(string destTag, string canMove, string[] destNames)
    // {
    //     GameObject[] targets = GameObject.FindGameObjectsWithTag(destTag);
    //     foreach(GameObject target in targets)
    //     {
    //         string destName = Array.Find<string>(destNames, name => name == target.name);
    //         if(destName != "")
    //         {
    //             PlayerAttack playerAttack = target.GetComponent<PlayerAttack>();
    //             playerAttack.ReceiveMessage(MessageTypes.BEGIN_EXPLOSION_ATTACK, canMove);
    //         }
    //     }
    // }

    // private void SendExecuteAttackMessage(string destTag, string damage, string range, string[] destNames)
    // {
    //     GameObject[] targets = GameObject.FindGameObjectsWithTag(destTag);
    //     foreach(GameObject target in targets)
    //     {
    //         string destName = Array.Find<string>(destNames, name => name == target.name);
    //         if(destName != "")
    //         {
    //             PlayerAttack playerAttack = target.GetComponent<PlayerAttack>();
    //             playerAttack.ReceiveMessage(MessageTypes.EXECUTE_ATTACK, damage + "," + range);
    //         }
    //     }
    // }
    // private void SendExecuteExplosionAttackMessage(string destTag, string damage, string range, string[] destNames)
    // {
    //     GameObject[] targets = GameObject.FindGameObjectsWithTag(destTag);
    //     foreach(GameObject target in targets)
    //     {
    //         string destName = Array.Find<string>(destNames, name => name == target.name);
    //         if(destName != "")
    //         {
    //             PlayerAttack playerAttack = target.GetComponent<PlayerAttack>();
    //             playerAttack.ReceiveMessage(MessageTypes.EXECUTE_EXPLOSION_ATTACK, damage + "," + range);
    //         }
    //     }
    // }

    // private void SendProjectileAttackMessage(string destTag, string[] destNames)
    // {
    //     GameObject[] targets = GameObject.FindGameObjectsWithTag(destTag);
    //     foreach(GameObject target in targets)
    //     {
    //         string destName = Array.Find<string>(destNames, name => name == target.name);
    //         if(destName != "")
    //         {
    //             PlayerAttack playerAttack = target.GetComponent<PlayerAttack>();
    //             playerAttack.ReceiveMessage(MessageTypes.LAUNCH_PROJECTILE, "");
    //         }
    //     }
    // }

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
