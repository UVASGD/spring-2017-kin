using UnityEngine;
using UnityEditor;
using System.Collections;

public class ChacAI : BaseGodAI
{
    public float maxInvincCd;
    protected float invincCurrCd;
    float meleeRange;
    public float maxMeleeCd;
    protected float meleeCurrCd;
    protected bool meleeCd;
    public float maxRangedCd;
    protected float rangedCurrCd;
    protected bool rangedCd;

    //Set of AI behavior states
    protected enum AIStates
    {
        InvincibleState, MeleeState, IdleState
    }
    protected AIStates curState; //Current AI behavior state

    protected new void Start()
    {
        base.Start();

        invincCurrCd = maxInvincCd;

        curState = AIStates.IdleState;
    }

    protected new void Update()
    {
        switch(curState)
        {
            case AIStates.IdleState:
                if ()//some condition
                    curState = AIStates.InvincibleState;
                break;
            case AIStates.InvincibleState:
                int health = gameObject.GetComponent<EnemyHealth>().getHp();
                int maxHealth = gameObject.GetComponent<EnemyHealth>().maxHealth;
                if(health < maxHealth/3) //final invincible phase
                {
                    //lightning and geysers
                }
                else if(health < (2/3) * maxHealth) //2nd invincible phase
                {
                    //lightning
                }
                else //first invincibility phase
                {
                    //geysers
                }

                if(invincCurrCd <= 0) //change to melee attacks after some time
                {
                    curState = AIStates.MeleeState;
                    invincCurrCd = maxInvincCd;
                    //come down and shockwave
                }
                else
                    invincCurrCd -= Time.deltaTime;
                break;
            case AIStates.MeleeState:
                float distance = Vector2.Distance((Vector2)targetObject.transform.position, (Vector2)gameObject.transform.position);
                if(distance > meleeRange) //if too far for melee, then ranged attack
                {
                    if (!rangedCd)
                    {
                        //attack ranged
                        rangedCd = true;
                    }
                    else //cooldown after shooting
                    {
                        if (rangedCurrCd <= 0)
                        {
                            rangedCurrCd = maxRangedCd;
                            rangedCd = false;
                        }
                        else
                            rangedCurrCd -= Time.deltaTime;
                    }
                }
                else
                {
                    if(!meleeCd)
                    {
                        //attack melee
                        meleeCd = true;
                    }
                    else //cooldown after melee
                    {
                        if (meleeCurrCd <= 0)
                        {
                            meleeCurrCd = maxMeleeCd;
                            meleeCd = false;
                        }
                        else
                            meleeCurrCd -= Time.deltaTime;
                    }
                }
                break;
            default:
                Debug.Log("Unknown state. Something be wrong mudda poop");
                break;
        }
    }
}
