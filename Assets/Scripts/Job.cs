using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Job : MonoBehaviour
{
	public List<Role> roles;
	public float duration;
	float progress;
	ProgressBar pb;

	private void Start()
	{
		pb = Instantiate(GameManager.Instance.progressBarPrefab, GameManager.Instance.c.transform);
		pb.number.text = roles.Count.ToString();
	}

	// Rewrite so player has to interact with jobsite instead of trigger
	private void OnTriggerEnter(Collider other)
	{
		Mouse m = other.GetComponent<Mouse>();
		if (m && !m.Wandering)
		{
			Debug.Log(m);
			Role unoccupied = roles.Where(r => !r.Filled).FirstOrDefault();
			if (unoccupied != null)
			{
				unoccupied.SetMouse(m);
			}
		}
	}
	private void OnTriggerExit(Collider other)
	{
		Mouse m = other.GetComponent<Mouse>();
		if (m)
		{
			Debug.Log(m);
			Role unoccupied = roles.Where(r => r.OccupyingMouse == m).FirstOrDefault();
			if (unoccupied != null)
			{
				unoccupied.FreeMouse();
			}
		}
	}

	private void Update()
	{
		pb.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);

		if (roles.All(r => r.Filled && r.MouseInPosition))
		{
			progress += Time.deltaTime;
			pb.AddProgress(Time.deltaTime / duration);
			if (progress >= duration)
			{
				Debug.Log("finished");
				roles.ForEach(r => r.FreeMouse());
				pb.gameObject.SetActive(false);
				gameObject.SetActive(false);
			}
		}
	}
}
