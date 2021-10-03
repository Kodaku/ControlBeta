using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PacketCreator
{
    public static string PrepareMessage(string[] packet)
    {
        string info = "";
        foreach(string elem in packet)
        {
            info += elem + ",";
        }
        info = info.Remove(info.Length - 1); //remove the last comma
        return info;
    }
}
