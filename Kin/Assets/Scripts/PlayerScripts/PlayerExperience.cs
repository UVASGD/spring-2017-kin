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
        currentExp += amount;
        // Play audio clip?
    }

    public bool decrementExp(long amount)
    {
        if(currentExp-amount >= 0)
        {
            currentExp -= amount;
            //Play audio clip?
            return true;
        }
        return false;
    }

	void OnGUI(){
		GUI.Button(new Rect (Screen.width - 120, 50, 100, 26), ("EXP: "+currentExp.ToString()));
	}

    public void setCurrentExp(long exp)
    {
        this.currentExp = exp;
		//Debug.Log (this.currentExp);
    }

    public long getCurrentExp()
    {
        return this.currentExp;
    }
}
