using UnityEngine;
using System.Collections;

public class PlayerExperience : MonoBehaviour
{
    //Keeps track of Player Experience
    private long currentExp;

    void Start()
    {
        this.currentExp = 0;
    }

    public void incrementExp(long amount)
    {
        print("before: " + currentExp);
        currentExp += amount;
        print("after: " + currentExp);
        // Play audio clip?
    }

    public void decrementExp(long amount)
    {
        if(currentExp-amount >= 0)
        {
            currentExp -= amount;
            //Play audio clip?
        }
    }

    public void setCurrentExp(long exp)
    {
        this.currentExp = exp;
    }

    public long getCurrentExp()
    {
        return this.currentExp;
    }
}
