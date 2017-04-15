using UnityEngine;
using System.Collections;

public class TimeController : MonoBehaviour {
	public float dayLength = 330.0f;

	private static int hour;
	private static int minute;

	private static DayNightController DNC;

	private static CalendarController calendar;

	private static bool calendarActive = false;

	/// <summary>
	/// The day.
	/// </summary>
	public static int kin;
	/// <summary>
	/// 20 kin.
	/// </summary>
	private static int uinal;
	/// <summary>
	/// 18 uinal.
	/// </summary>
	private static int tun;
	/// <summary>
	/// 20 tun.
	/// </summary>
	private static int katun;
	/// <summary>
	/// 20 katun; at 13 the game ends
	/// </summary>
	private static int baktun;

	// Use this for initialization
	void Start () {
		kin = 0;

		CalculateCalendar ();
		DNC = this.gameObject.GetComponent<DayNightController> ();
		DontDestroyOnLoad (this.gameObject);
		if (GameObject.Find("Calendar")) calendar = GameObject.Find("Calendar").GetComponent<CalendarController>();
		calendarActive = calendar != null;
	}

	void Update(){
		hour = DNC.worldTimeHour;
		minute = DNC.minutes;

		if (kin >= 1872000 || baktun >= 13) {
			//game ends
		}
	}

	public float getDayLength() {
		return dayLength;
	}

	/// <summary>
	/// Progresses the day.
	/// </summary>
	/// <param name="byNum">By number.</param>
	public static void ProgressDay(int byNum) {
		kin += Mathf.Abs(byNum);
		CalculateCalendar ();
		Debug.Log(kin);
	}

	/// <summary>
	/// Calculates the calendar.
	/// </summary>
	private static void CalculateCalendar(){
		if (calendarActive) {
			calendar.CalendarSet(kin);
		}
		uinal = kin % 20;
		tun = uinal % 18;
		katun = tun % 20;
		baktun = katun % 20;
	}
}
