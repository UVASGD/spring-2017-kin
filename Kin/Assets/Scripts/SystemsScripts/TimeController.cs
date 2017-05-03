using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
	public static int kin = 0;
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
	void Awake () {
		if (GameObject.Find("Calendar")) calendar = GameObject.Find("Calendar").GetComponent<CalendarController>();
		calendarActive = calendar != null;
		CalculateCalendar ();
		DNC = this.gameObject.GetComponent<DayNightController> ();
		DontDestroyOnLoad (this.gameObject);
	}

	void Update(){
		hour = DNC.worldTimeHour;
		minute = DNC.minutes;

		//CalculateCalendar();
		if (calendar == null) {
			calendar = GameObject.FindObjectOfType<CalendarController>();
		}

		if (kin >= 1872000 || baktun >= 13) {
			//game ends
			SceneManager.LoadScene("END");
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
	}

	/// <summary>
	/// Calculates the calendar.
	/// </summary>
	public static void CalculateCalendar(){
		if (calendarActive) {
			calendar.CalendarSet(kin);
		}
		uinal = kin % 20;
		tun = uinal % 18;
		katun = tun % 20;
		baktun = katun % 20;
	}
}
