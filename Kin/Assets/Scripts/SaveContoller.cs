using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class SaveContoller : MonoBehaviour {
    //on enable on disable for autosave

    public static SaveContoller s_instance;

    public float health;
    public float stamina;

    public int healthLvlP;
    public int healthLvlO;
    public int stamLvlP;    //stamina
    public int stamLvlO;
    public int strLvlP;     //strength
    public int strLvlO;
    public int wisLvlP;     //wisdom
    public int wisLvlO;

    public long day;
    public float time;

    // Use this for initialization
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

    void Update() { }
	
    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Health: " + health);
        if (GUI.Button(new Rect(10, 40, 100, 30), "Health Up"))
        {
            health++;
        }
        if (GUI.Button(new Rect(10, 70, 100, 30), "Health down"))
        {
            health--;
        }
        GUI.Label(new Rect(10, 100, 100, 30), "Stamina: " + stamina);
        if(GUI.Button(new Rect(10, 130, 100, 30), "Save"))
        {
            Save();
        }
        if(GUI.Button(new Rect(10, 160, 100, 30), "Load"))
        {
            Load();
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/saveInfo.dat");
        SaveData data = WriteToData();
        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/saveInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saveInfo.dat",
                FileMode.Open);
            SaveData data = (SaveData) bf.Deserialize(file);
            file.Close();

            health = data.health;
            stamina = data.stamina;
            healthLvlP = data.healthLvlP;
            healthLvlO = data.healthLvlO;
            stamLvlP = data.stamLvlP;
            stamLvlO = data.stamLvlO;
            strLvlP = data.strLvlP;
            strLvlO = data.strLvlO;
            wisLvlP = data.wisLvlP;
            wisLvlO = data.wisLvlO;
            day = data.day;
            time = data.time;
        }
    }

    private SaveData WriteToData ()
    {
        SaveData data = new SaveData();
        data.health = health;
        data.stamina = stamina;
        data.healthLvlP = healthLvlP;
        data.healthLvlO = healthLvlO;
        data.stamLvlP = stamLvlP;
        data.stamLvlO = stamLvlO;
        data.strLvlP = strLvlP;
        data.strLvlO = strLvlO;
        data.wisLvlP = wisLvlP;
        data.wisLvlO = wisLvlO;
        data.day = day;
        data.time = time;

        return data;
    }
}

[Serializable]
class SaveData 
{
    public float health;
    public float stamina;

    public int healthLvlP;
    public int healthLvlO;
    public int stamLvlP;    //stamina
    public int stamLvlO;
    public int strLvlP;     //strength
    public int strLvlO;
    public int wisLvlP;     //wisdom
    public int wisLvlO;

    public long day;
    public float time;
}
