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

    public void incrementExperience(int amount)
    {
            currentExp += amount;
            // Play audio clip?
    }

    public void decrementExperience(long amount)
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
