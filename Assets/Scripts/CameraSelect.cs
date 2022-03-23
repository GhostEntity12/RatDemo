using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CameraSelect : MonoBehaviour
{
	Vector3 uiClickDown;
	Vector3 worldClickDown;
	Vector3 uiClickUp;
	Vector3 worldClickUp;
	[SerializeField] RectTransform marker;

	public Vector3[] vertices = new Vector3[4];
	public Vector3[] uiVertices = new Vector3[4];

	Camera mainCamera;

	[SerializeField] LayerMask groundMask;
	[SerializeField] LayerMask mouseMask;

	public GameObject[] testCorner;
	public GameObject[] testCornerUi;

	public TextMeshProUGUI nameplate;
	public InfoCard card;

	// Start is called before the first frame update
	void Start()
	{
		mainCamera = Camera.main;
		if (GameManager.Instance.Mice.Count == 0)
		{
			Debug.Log("No Mice");
		}
	}

	// Update is called once per frame
	void Update()
	{
		bool mouseCast = Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit mouseHit, 200f, mouseMask);
		if (mouseCast)
		{
			Mouse m = mouseHit.transform.GetComponent<Mouse>();
			nameplate.rectTransform.position = mainCamera.WorldToScreenPoint(m.gameObject.transform.position + Vector3.up);
			nameplate.text = m.Info.name;
			card.SetDisplayInfo(m.Info);
		}
		else
		{
			nameplate.text = string.Empty;
		}

		if (Input.GetMouseButtonDown(1))
		{
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 200f, groundMask))
			{
				//The corner position of the square
				worldClickDown = hit.point;
				uiClickDown = Input.mousePosition; //mainCamera.WorldToScreenPoint(hit.point);
				marker.gameObject.SetActive(true);
			}
		}
		if (Input.GetMouseButton(1))
		{
			foreach (Mouse mouse in GameManager.Instance.Mice)
			{
				mouse.Deselect();
			}
			GameManager.Instance.SelectedMice.Clear();

			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 200f, groundMask))
			{
				worldClickUp = hit.point;
				uiClickUp = Input.mousePosition; //mainCamera.WorldToScreenPoint(hit.point);

				DisplaySquare(uiClickDown, uiClickUp);

				float dist = Vector3.Distance(worldClickDown, worldClickUp);
				if (dist < 0.2f && mouseCast)
				{
					Mouse m = mouseHit.transform.GetComponent<Mouse>();
					m.Select();
					GameManager.Instance.SelectedMice.Add(m);
				}
				else
				{
					//Click dragging
					foreach (var mouse in GameManager.Instance.Mice)
					{
						if (IsInArea(mouse.transform.position))
						{
							mouse.Select();
							GameManager.Instance.SelectedMice.Add(mouse);
						}
					}
				}
			}
		}
		if (Input.GetMouseButtonUp(1))
		{
			marker.gameObject.SetActive(false);
		}

		if (Input.GetMouseButtonUp(0))
		{
			if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out RaycastHit rHit, 200f) && NavMesh.SamplePosition(rHit.point, out NavMeshHit nMHit, 200f, NavMesh.AllAreas))
			{
				GameManager.Instance.SelectedMice.ForEach(m => m.SetDestination(nMHit.position));
			}
		}
	}

	void DisplaySquare(Vector3 TL, Vector3 BR)
	{
		uiVertices[0] = TL;
		uiVertices[1] = new Vector3(BR.x, TL.y);
		uiVertices[2] = new Vector3(TL.x, BR.y);
		uiVertices[3] = BR;
		Vector3 center = (TL + BR) / 2;
		marker.sizeDelta = new Vector2(Mathf.Abs(TL.x - BR.x), Mathf.Abs(TL.y - BR.y)) / GameManager.Instance.c.scaleFactor;
		marker.transform.position = center;
		Debug.Log(Mathf.Abs(TL.x - BR.x));
		Debug.Log(BR);

		for (int i = 0; i < 4; i++)
		{
			if (Physics.Raycast(Camera.main.ScreenPointToRay(uiVertices[i]), out RaycastHit hit, 200f, groundMask))
			{
				vertices[i] = hit.point;
				//testCorner[i].transform.position = hit.point;
				//testCornerUi[i].transform.position = uiVertices[i];
			}
		}
	}

	//Is a unit within a polygon determined by 4 corners
	bool IsInArea(Vector3 unitPos)
	{
		//The polygon forms 2 triangles, so we need to check if a point is within any of the triangles
		//Triangle 1: TL - BL - TR
		if (IsWithinTriangle(unitPos, vertices[0], vertices[2], vertices[1]))
		{
			return true;
		}

		//Triangle 2: TR - BL - BR
		if (IsWithinTriangle(unitPos, vertices[1], vertices[2], vertices[3]))
		{
			return true;
		}

		return false;
	}

	//Is a point within a triangle
	//From http://totologic.blogspot.se/2014/01/accurate-point-in-triangle-test.html
	bool IsWithinTriangle(Vector3 p, Vector3 p1, Vector3 p2, Vector3 p3)
	{
		bool isWithinTriangle = false;

		//Need to set z -> y because of other coordinate system
		float denominator = ((p2.z - p3.z) * (p1.x - p3.x) + (p3.x - p2.x) * (p1.z - p3.z));

		float a = ((p2.z - p3.z) * (p.x - p3.x) + (p3.x - p2.x) * (p.z - p3.z)) / denominator;
		float b = ((p3.z - p1.z) * (p.x - p3.x) + (p1.x - p3.x) * (p.z - p3.z)) / denominator;
		float c = 1 - a - b;

		//The point is within the triangle if 0 <= a <= 1 and 0 <= b <= 1 and 0 <= c <= 1
		if (a >= 0f && a <= 1f && b >= 0f && b <= 1f && c >= 0f && c <= 1f)
		{
			isWithinTriangle = true;
		}

		return isWithinTriangle;
	}

}
