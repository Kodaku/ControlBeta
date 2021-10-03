using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected PlayerStates currentState;
    private Updatable[] updatables = new Updatable[2];
    // protected bool[] activeStates = new bool[18];

    protected float[] optionalValues = null;
    // Start is called before the first frame update
    public virtual void Start()
    {
        //Both players starts in IDLE state
        currentState = PlayerStates.IDLE;
        //Initialize Player Health and Mana that are common to Human and AIPlayer
        updatables[(int)UpdatableIndices.HEALTH] = GetComponent<PlayerHealth>();
        updatables[(int)UpdatableIndices.MANA] = GetComponent<PlayerMana>();
    }

    public void SetCurrentState(PlayerStates newState, float[] values = null)
    {
        // activeStates[(int)currentState] = false;
        currentState = newState;
        // activeStates[(int)newState] = true;
        optionalValues = values;
    }

    public virtual void UpdateUpdatable(UpdatableIndices index, int amount)
    {
        updatables[(int)index].AddQuantity(amount);
    }

    public virtual void TranslateForEvade()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
}
