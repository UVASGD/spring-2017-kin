using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class SaveController : MonoBehaviour {
    //on enable on disable for autosave

    public static SaveController s_instance;

    public float stamina;

	public PlayerHealth playerHealthController;
	public float health;

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
		GUI.Label(new Rect(10, 10, 100, 20), "Health: " + PlayerHealth.s_instance.currentHealth);
        if (GUI.Button(new Rect(10, 40, 100, 30), "Health Up"))
        {
			PlayerHealth.s_instance.currentHealth++;
			health = PlayerHealth.s_instance.currentHealth;
        }
        if (GUI.Button(new Rect(10, 70, 100, 30), "Health down"))
        {
			PlayerHealth.s_instance.currentHealth--;
			health = PlayerHealth.s_instance.currentHealth;
        }
        GUI.Label(new Rect(10, 100, 100, 30), "Stamina: " + stamina);
        if(GUI.Button(new Rect(10, 130, 100, 30), "Save"))
        {
            Save("", false);
        }
        if(GUI.Button(new Rect(10, 160, 100, 30), "Load"))
        {
            Load("", false);
        }
    }

	public void Save(String fileNumber, bool autosave)
    {
		if (autosave) {
			fileNumber = "autosave" + fileNumber;
		}
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/saveInfo" + fileNumber + ".dat");
        SaveData data = WriteToData();
        bf.Serialize(file, data);
        file.Close();
    }

	public void Load(String fileNumber, bool autosave)
    {
		if (autosave) {
			fileNumber = "autosave" + fileNumber;
		}
		if(File.Exists(Application.persistentDataPath + "/saveInfo" + fileNumber + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/saveInfo" + fileNumber + ".dat",
                FileMode.Open);
            SaveData data = (SaveData) bf.Deserialize(file);
            file.Close();

			WriteFromData (data);
        }
    }

	private void WriteFromData(SaveData data)
	{
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

    private SaveData WriteToData ()
    {
        SaveData data = new SaveData();
		data.health = PlayerHealth.s_instance.currentHealth;
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
