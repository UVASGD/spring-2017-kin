using UnityEngine;
using System.Collections;

public class TimeController : MonoBehaviour {
	public float dayLength = 420.0f;
	private float timeLeft;
	public int dayMod = 0;

	/// <summary>
	/// The day.
	/// </summary>
	private int kin;
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
		timeLeft = dayLength;
		kin = 1 + Mathf.Abs(dayMod);

		CalculateCalendar ();
	}

	void Update(){
		timeLeft -= Time.deltaTime;
		if(timeLeft <= 0)
		{
			ProgressDay (1);
			timeLeft = dayLength;
		}
		if (kin >= 1872000 || baktun >= 13) {
			//game ends
		}
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
