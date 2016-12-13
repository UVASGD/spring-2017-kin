using UnityEngine;
using System.Collections;

public class TimeController : MonoBehaviour {
	public float dayLength = 330.0f;

	private int hour;
	private int minute;

	private DayNightController DNC;

	/// <summary>
	/// The day.
	/// </summary>
	public int kin;
	/// <summary>
	/// 20 kin.
	/// </summary>
	private int uinal;
	/// <summary>
	/// 18 uinal.
	/// </summary>
	private int tun;
	/// <summary>
	/// 20 tun.
	/// </summary>
	private int katun;
	/// <summary>
	/// 20 katun; at 13 the game ends
	/// </summary>
	private int baktun;

	// Use this for initialization
	void Start () {
		kin = 0;

		CalculateCalendar ();
		DNC = this.gameObject.GetComponent<DayNightController> ();
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
	private void ProgressDay(int byNum) {
		kin += Mathf.Abs(byNum);
		CalculateCalendar ();
	}

	/// <summary>
	/// Calculates the calendar.
	/// </summary>
	private void CalculateCalendar(){
		uinal = kin % 20;
		tun = uinal % 18;
		katun = tun % 20;
		baktun = katun % 20;
	}
}
