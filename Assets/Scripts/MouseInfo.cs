using UnityEngine;

[System.Serializable]
public class MouseInfo
{
	public string name;
	int speed = 1;
	public float SpeedModifier => 1 + speed / 10f;
	public int SpeedValue => speed;
	
	int strength = 1;
	public int Strength => strength;

	public void IncreaseRandomStat()
	{
		int i = Random.Range(0, 2);
		switch (i)
		{
			case 0:
				IncreaseStrength();
				break;
			case 1:
				IncreaseSpeed();
				break;
			default:
				break;
		}
	}

	public void IncreaseStrength(int amount = 1) => strength += amount;

	public void IncreaseSpeed(int amount = 1) => speed += amount;
}