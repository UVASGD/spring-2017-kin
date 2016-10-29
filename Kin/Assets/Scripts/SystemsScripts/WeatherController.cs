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
    
    void Initialize ()
    {
        randomWeather = Random.Range(0, 2);
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
	    //reset random weather for different days
        
        if (this.gameObject.GetComponent<DayNightController>().worldTimeHour == 4.0f)
        {
            Initialize();
            test = 1.0f;
            
        } else
        {
               
        }
       
	}

    //deciding which weather 
    void Weather()
    {
        //switch layers
        if (randomWeather == 0)
        {
            //sun
            GUI.Button(new Rect(Screen.width - 100, 20, 100, 26), randomWeather.ToString() + timeOfDay.ToString());
        } else if (randomWeather == 1)
        {
            //cloudy
            GUI.Button(new Rect(Screen.width - 100, 20, 100, 26), randomWeather.ToString() + timeOfDay.ToString());
        } else if (randomWeather == 2)
        {
            //rain
            GUI.Button(new Rect(Screen.width - 100, 20, 100, 26), randomWeather.ToString() + timeOfDay.ToString());
        }

    }

    //deciding which time of day
    void Timing()
    {
        if (timeOfDay == this.gameObject.GetComponent<DayNightController>().worldTimeHour)
        {
            Weather();
        }
    }
}
