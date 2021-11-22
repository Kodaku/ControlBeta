using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralController : MonoBehaviour
{
    protected ButtonController buttonController;
    protected DialogueTrigger dialogueTrigger;
    // protected Follow follow;
    [SerializeField] protected GameObject aura;
    private Queue<string> visitedTriggers = new Queue<string>();
    public virtual void Awake()
    {
        buttonController = GetComponent<ButtonController>();
        dialogueTrigger = GetComponentInParent<DialogueTrigger>();
        // follow = GetComponent<Follow>();
    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
}
