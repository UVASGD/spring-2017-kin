using UnityEngine;
using System.Collections;

public class PreLoader : MonoBehaviour {

    private static PreLoader s_instance;

    public bool resume = false;
    public string fileNumber = "";
    public bool autosave = false;

    void Awake()
    {
        if (s_instance == null)
        {
            DontDestroyOnLoad(gameObject); // save object on scene mvm
            s_instance = this;
        }
        else if (s_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static PreLoader Instance
    {
        get { return s_instance; }
    }

    public void preLoad(string fileNumber, bool autosave)
    {
        resume = true;
        this.fileNumber = fileNumber;
        this.autosave = autosave;
    }
}
