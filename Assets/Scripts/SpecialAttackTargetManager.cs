using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackTargetManager : MonoBehaviour
{
    public static List<string> targetNames = new List<string>();

    public static void ClearTargetNames()
    {
        targetNames.Clear();
    }

    public static void UpdateTargetNames(string targetName)
    {
        targetNames.Add(targetName);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
