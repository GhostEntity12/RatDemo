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
		float timePercent = Mathf.Clamp01(time / dayLength);
		if (timePercent <= 1f)
		{
			time += Time.deltaTime;
			clockDisplay.localRotation = Quaternion.Euler(0, 0, Mathf.Lerp(45, -230, timePercent));
			int time24 = Mathf.FloorToInt(Mathf.Lerp(activeHours.x, activeHours.y, timePercent));
			text.text = $"{((time24 - 1) % 12) + 1} {(time24 < 12 ? "AM" : "PM")}";
		}

		if (timePercent == 1)
		{
			eod.enabled = true;
		}
	}
}
