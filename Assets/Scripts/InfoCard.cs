using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoCard : MonoBehaviour
{
	[SerializeField] StatInfo speed, strength;
	TextMeshProUGUI mouseName;
	// Start is called before the first frame update
	void Start()
	{
		mouseName = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
	}

	public void SetDisplayInfo(MouseInfo mouse)
	{
		mouseName.text = mouse.name;
		speed.SetPips(mouse.SpeedValue);
		strength.SetPips(mouse.Strength);
	}
}

[System.Serializable]
public class StatInfo
{
	[SerializeField] Image[] pips = new Image[10];
	[SerializeField] TextMeshProUGUI number;

	public void SetPips(int active)
	{
		for (int i = 0; i < pips.Length; i++)
		{
			pips[i].color = i + 1 <= active ? new Color(0.66f, 0.44f, 0.83f) : Color.gray;
		}
		number.text = active.ToString();
	}
}
