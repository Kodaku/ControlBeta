using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Attack
{
    public string attacksFileName;
    public AttackType attackType;
    [HideInInspector] public string characterName;
    [HideInInspector] public List<string> attacksTrigger;
}
