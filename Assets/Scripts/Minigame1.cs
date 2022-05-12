using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class Minigame1 : MonoBehaviour
{
	[SerializeField] Canvas minigameCanvas;

	[SerializeField] TextMeshProUGUI counterText;

	[SerializeField] bool active = false;
	bool completed = false;

	int counter = 0;
	[SerializeField] int threshold = 10;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (active)
		{
			if (Input.GetKeyDown(KeyCode.JoystickButton0))
			{
				counter++;
				counterText.text = counter.ToString();
				if (counter >= threshold)
				{
					active = false;
					completed = true;
					minigameCanvas.gameObject.SetActive(false);
				}
			}
		}
	}

	[ContextMenu("Enable")]
	void OnMinigameStart()
	{
		minigameCanvas.gameObject.SetActive(true);
		active = true;
	}
}
