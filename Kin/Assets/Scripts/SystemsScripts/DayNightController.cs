using UnityEngine;
using System.Collections;

public class DayNightController : MonoBehaviour
{
    /// <summary>
	/// number of seconds in a day
    /// </summary>
	public float dayCycleLength;

    /// <summary>
	/// /// current time in game time (0 - dayCycleLength).  
    /// </summary>
    public float currentCycleTime = 0;

	/// <summary>
	/// number of hours per day. 
	/// </summary>
    public float hoursPerDay;

    /// <summary>
	/// The optional rotation pivot of Sun  
    /// </summary>
    public Transform rotation;

	/// <summary>
	/// current day phase, uses enum
	/// </summary>
    public DayPhase currentPhase;

    /// <summary>
	/// Dawn occurs at currentCycleTime = 0.0f, so this offsets the WorldHour 
	/// time to make dawn occur at a specified hour. 
	/// For example, a value of 3 results in a 5am dawn for a 24 hour world clock.  
    /// </summary>
    public float dawnTimeOffset;

	/// <summary>
	/// calculated hour of the day, based on the hoursPerDay setting.
	/// <remarks>See Only</remarks>
	/// </summary>
    public int worldTimeHour;

	/// <summary>
	/// calculated minutes of the day, based on the hoursPerDay setting.
	/// <remarks>See Only</remarks>
	/// </summary>
    public int minutes;

	/// <summary>
	/// The time per hour.
	/// </summary>
    private float timePerHour;

	/// <summary>
	/// The scene ambient color used for full daylight. 
	/// </summary>
	public Color fullLight;// = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);

	/// <summary>
	/// The scene ambient color used for full night.  
	/// </summary>
	public Color fullDark; //= new Color(80.0f / 255.0f, 80.0f / 255.0f, 90.0f / 255.0f);

	/// <summary>
	/// The scene fog color to use at dawn and dusk. 
	/// </summary>
	public Color dawnDuskFog;// = new Color(133.0f / 255.0f, 124.0f / 255.0f, 200.0f / 255.0f);

	/// <summary>
	/// The scene fog color to use during the day. 
	/// </summary>
	public Color dayFog;// = new Color(180.0f / 255.0f, 208.0f / 255.0f, 209.0f / 255.0f);

	/// <summary>
	/// The scene fog color to use at night.  
	/// </summary>
	public Color nightFog;// = new Color(12.0f / 255.0f, 15.0f / 255.0f, 91.0f / 255.0f);

	/// <summary>
	/// The calculated time at which dawn occurs based on 1/4 of dayCycleLength. 
	/// </summary>
    private float dawnTime;

	public float getDawnTime(){
		return dawnTime;
	}

	/// <summary>
	/// The calculated time at which day occurs based on 1/4 of dayCycleLength.
	/// </summary>
    private float dayTime;

	/// <summary>
	/// The calculated time at which dusk occurs based on 1/4 of dayCycleLength. 
	/// </summary>
    private float duskTime;

	public float getDuskTime(){
		return duskTime;
	}

	/// <summary>
	/// The calculated time at which night occurs based on 1/4 of dayCycleLength.  
	/// </summary>
    private float nightTime;

    // One quarter the value of dayCycleLength.  
    private float quarterDay;
    private float halfquarterDay;

	/// <summary>
	/// The specified intensity of the directional light, if one exists. This value will be  
	/// faded to 0 during dusk, and faded from 0 back to this value during dawn.  
	/// </summary>
    public float lightIntensity;

    /// <summary>
	/// Initializes working variables and performs starting calculations. 
    /// </summary>
    void Initialize()
    {
		dayCycleLength = this.gameObject.GetComponent<TimeController> ().getDayLength ();
        quarterDay = dayCycleLength * 0.25f;
        halfquarterDay = dayCycleLength * 0.125f;
        dawnTime = 0.0f;
        dayTime = dawnTime + halfquarterDay;
        duskTime = dayTime + quarterDay + halfquarterDay;
        nightTime = duskTime + halfquarterDay;
        timePerHour = dayCycleLength / hoursPerDay;
        if (this.gameObject.GetComponent<Light>() != null)
        { lightIntensity = this.gameObject.GetComponent<Light>().intensity; }

		currentCycleTime = dayCycleLength * (( dawnTimeOffset) / hoursPerDay);

		if (currentCycleTime < 0) {
			currentCycleTime = dayCycleLength + currentCycleTime;
		}
    }

    /// <summary>
    /// Sets the script control fields to reasonable default values for an acceptable day/night cycle effect.  
    /// </summary>
    void Reset()
    {
        dayCycleLength = 120.0f;
        hoursPerDay = 24.0f;
        dawnTimeOffset = 0.0f;
        fullLight = new Color(253.0f / 255.0f, 248.0f / 255.0f, 223.0f / 255.0f);
        fullDark = new Color(32.0f / 255.0f, 28.0f / 255.0f, 46.0f / 255.0f);
        dawnDuskFog = new Color(133.0f / 255.0f, 124.0f / 255.0f, 102.0f / 255.0f);
        dayFog = new Color(180.0f / 255.0f, 208.0f / 255.0f, 209.0f / 255.0f);
        nightFog = new Color(12.0f / 255.0f, 15.0f / 255.0f, 91.0f / 255.0f);
    }

    /// <summary>
    /// Start this instance.
    /// </summary>
    void Start()
    {
        Initialize();
    }

	/// <summary>
	/// Raises the GUI event.
	/// </summary>
    void OnGUI()
    {
        string jam = worldTimeHour.ToString();
        string menit = minutes.ToString();
        if (worldTimeHour < 10)
        {
            jam = "0" + worldTimeHour;
        }
        if (minutes < 10)
        {
            menit = "0" + minutes;
        }
        GUI.Button(new Rect(Screen.width - 120, 20, 100, 26), currentPhase.ToString() + " : " + jam + ":" + menit);
    }

    /// <summary>
    /// Update this instance.
    /// </summary>
    void Update()
    {
        // Rudementary phase-check algorithm:  
        if (currentCycleTime > nightTime && currentPhase == DayPhase.Dusk)
        {
            SetNight();
        }
        else if (currentCycleTime > duskTime && currentPhase == DayPhase.Day)
        {
			this.gameObject.GetComponent<MusicController> ().fading = true;
			this.gameObject.GetComponent<MusicController> ().up = false;

            SetDusk();
        }
        else if (currentCycleTime > dayTime && currentPhase == DayPhase.Dawn)
        {
			TimeController.ProgressDay (TimeController.kin + 1);
            SetDay();
        }
        else if (currentCycleTime > dawnTime && currentCycleTime < dayTime && currentPhase == DayPhase.Night)
        {
			this.gameObject.GetComponent<MusicController> ().fading = true;
			this.gameObject.GetComponent<MusicController> ().up = false;

            SetDawn();
        }

		// Update the current cycle time:  
		currentCycleTime += Time.deltaTime;
		currentCycleTime = currentCycleTime % dayCycleLength;

        // Perform standard updates:  
        UpdateWorldTime();
        UpdateDaylight();
        UpdateFog();

    }
		
	/// <summary>
	/// Sets the currentPhase to Dawn, turning on the directional light, if any. 
	/// </summary>
    public void SetDawn()
    {
        if (this.gameObject.GetComponent<Light>() != null){ 
			this.gameObject.GetComponent<Light>().enabled = true; 
		}
        currentPhase = DayPhase.Dawn;
    }
		
	/// <summary>
	/// Sets the currentPhase to Day, ensuring full day color ambient 
	/// light, and full directional light intensity, if any.  
	/// </summary>
    public void SetDay()
    {
        RenderSettings.ambientLight = fullLight;
        if (this.gameObject.GetComponent<Light>() != null){ 
			this.gameObject.GetComponent<Light>().intensity = lightIntensity; 
		}
        currentPhase = DayPhase.Day;
    }
		
	/// <summary>
	/// Sets the currentPhase to Dusk. 
	/// </summary>
    public void SetDusk()
    {
        currentPhase = DayPhase.Dusk;
    }
		
	/// <summary>
	/// Sets the currentPhase to Night, ensuring full night color ambient 
	/// light, and turning off the directional light, if any.  
	/// </summary>
    public void SetNight()
    {
        RenderSettings.ambientLight = fullDark;
        if (this.gameObject.GetComponent<Light>() != null){ 
			this.gameObject.GetComponent<Light>().enabled = false; 
		}
        currentPhase = DayPhase.Night;
    }
		
	/// <summary>
	/// If the currentPhase is dawn or dusk, this method adjusts the ambient 
	/// light color and direcitonal light intensity (if any) to a percentage 
	/// of full dark or full light as appropriate. Regardless  of currentPhase, 
	/// the method also rotates the transform of this component, thereby rotating 
	/// the directional light, if any. 
	/// </summary>
    private void UpdateDaylight()
    {
        if (currentPhase == DayPhase.Dawn)
        {
            float relativeTime = currentCycleTime - dawnTime;
            RenderSettings.ambientLight = Color.Lerp(fullDark, fullLight, relativeTime / halfquarterDay);
            if (this.gameObject.GetComponent<Light>() != null)
            { this.gameObject.GetComponent<Light>().intensity = lightIntensity * (relativeTime / halfquarterDay); }
        }
        else if (currentPhase == DayPhase.Dusk)
        {
            float relativeTime = currentCycleTime - duskTime;
            RenderSettings.ambientLight = Color.Lerp(fullLight, fullDark, relativeTime / halfquarterDay);
            if (this.gameObject.GetComponent<Light>() != null)
            { this.gameObject.GetComponent<Light>().intensity = lightIntensity * ((halfquarterDay - relativeTime) / halfquarterDay); }
        }

        //transform.Rotate(Vector3.up * ((Time.deltaTime / dayCycleLength) * 360.0f), Space.Self);  
        transform.RotateAround(rotation.position, Vector3.forward, ((Time.deltaTime / dayCycleLength) * 360.0f));
    }
		
	/// <summary>
	/// Interpolates the fog color with Color.Lerp between the specified phase colors during 
	/// each phase's transition.  
	/// For example, from DawnDusk to Day, Day to DawnDusk, DawnDusk to Night, and Night to DawnDusk 
	/// </summary>
    private void UpdateFog()
    {
        if (currentPhase == DayPhase.Dawn){
            float relativeTime = currentCycleTime - dawnTime;
            RenderSettings.fogColor = Color.Lerp(dawnDuskFog, dayFog, relativeTime / halfquarterDay);
        } else if (currentPhase == DayPhase.Day){
            float relativeTime = currentCycleTime - dayTime;
            RenderSettings.fogColor = Color.Lerp(dayFog, dawnDuskFog, relativeTime / (quarterDay + halfquarterDay));
        } else if (currentPhase == DayPhase.Dusk){
            float relativeTime = currentCycleTime - duskTime;
            RenderSettings.fogColor = Color.Lerp(dawnDuskFog, nightFog, relativeTime / halfquarterDay);
        } else if (currentPhase == DayPhase.Night){
            float relativeTime = currentCycleTime - nightTime;
            RenderSettings.fogColor = Color.Lerp(nightFog, dawnDuskFog, relativeTime / (quarterDay + halfquarterDay));
        }
    }
		
	/// <summary>
	/// Updates the World-time hour based on the current time of day.
	/// </summary>
    private void UpdateWorldTime()
    {
        worldTimeHour = (int)((Mathf.Ceil((currentCycleTime / dayCycleLength) * hoursPerDay) + dawnTimeOffset) % hoursPerDay) + 1;
        minutes = (int)(Mathf.Ceil((currentCycleTime * (60 / timePerHour)) % 60));

		if (worldTimeHour >= hoursPerDay) {
			worldTimeHour = worldTimeHour - ((int)hoursPerDay);
		}
	
    }

	/// <summary>
	/// Day phase enumerator.
	/// </summary>
    public enum DayPhase
    {
        Night = 0,
        Dawn = 1,
        Day = 2,
        Dusk = 3
    }
}