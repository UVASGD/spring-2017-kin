using UnityEngine;
using System.Collections;

public class WeatherController : MonoBehaviour {
    public float randomWeather;
    public float timeOfDay;
    public float weatherDuration;
    public float sunny = 0.0f;
    public float rain = 1.0f;
    public float cloudy = 2.0f;
    public float test = 0.0f;
    public float layer;
    public int weatherPeriod = 3;

    public GameObject puddle;
    public GameObject cloud;
    public GameObject sun;

    bool generate = true;
    bool set = false;

    void Initialize ()
    {
        randomWeather = Random.Range(0, 3);
        timeOfDay = Random.Range(1, 24);
        weatherDuration = Random.Range(2, 8);
        
        Timing();
    }

	// Use this for initialization
	void Start () {
        Initialize();
        Update();
	}
	
	// Update is called once per frame
	void Update () {
        //reset random weather every weatherPeriod hours
        if (this.gameObject.GetComponent<DayNightController>().worldTimeHour % weatherPeriod == 0 && !set)
        {
            Initialize();
            test = 1.0f;
            set = true;
            
        } else if (this.gameObject.GetComponent<DayNightController>().worldTimeHour % weatherPeriod != 0 && set)
        {
            set = false;
        }
       
	}

    //deciding which weather 
    void Weather()
    {
        //switch layers
        if (randomWeather == 0)
        {
            //sun
            //GUI.Button(new Rect(Screen.width - 100, 20, 100, 26), randomWeather.ToString() + timeOfDay.ToString());
            if (generate)
            {
                for (int i = 0; i < 30; i++)
                {
                    Instantiate(sun, new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
                }
                generate = false;
            }
        } else if (randomWeather == 1)
        {
            //cloudy
            //GUI.Button(new Rect(Screen.width - 100, 20, 100, 26), randomWeather.ToString() + timeOfDay.ToString());
            if (generate)
            {
                for (int i = 0; i < 30; i++)
                {
                    Instantiate(cloud, new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
                }
                generate = false;
            }
        }
        else if (randomWeather == 2)
        {
            //rain
            //GUI.Button(new Rect(Screen.width - 100, 20, 100, 26), randomWeather.ToString() + timeOfDay.ToString());
            if (generate)
            {
                for (int i = 0; i < 30; i++)
                {
                    Instantiate(puddle, new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
                }
                generate = false;
            }

        }

    }

    void OnGUI()
    {
        GUI.Button(new Rect(Screen.width - 100, 20, 100, 26), randomWeather.ToString() + timeOfDay.ToString());
    }

    //deciding which time of day
    void Timing()
    {
        //if (timeOfDay == this.gameObject.GetComponent<DayNightController>().worldTimeHour)
        // Destroy all previous weather objects before making new ones
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("weather"))
        {
            Destroy(g);
        }
        Weather();
        generate = true;
    }
}
