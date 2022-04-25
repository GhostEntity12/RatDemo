using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Clock : MonoBehaviour
{
	float time = 0;
	public float dayLength = 60;
	public Vector2Int activeHours;
	public RectTransform clockDisplay;
	public TextMeshProUGUI text;
	public TextMeshProUGUI eod;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		// Calculate the percentage of time that has passed in the day
		float timePercent = Mathf.Clamp01(time / dayLength);
		if (timePercent <= 1f)
		{
			time += Time.deltaTime;

			// Rotate the clock sprite
			clockDisplay.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(45, -230, timePercent));

			// Calculating the time in 24h time
			int time24 = Mathf.FloorToInt(Mathf.Lerp(activeHours.x, activeHours.y, timePercent));

			// Display the time in 12h time (has a bunch of handling for nighttime levels
			text.text = $"{((time24 + 23) % 12) + 1} {((time24 % 24) < 12 ? "AM" : "PM")}";
		}

		if (timePercent == 1)
		{
			// End of day
			eod.enabled = true;
			Time.timeScale = 0;
		}
	}
}
