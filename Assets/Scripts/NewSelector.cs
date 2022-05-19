using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewSelector : MonoBehaviour
{
	[SerializeField] float movementSpeed = 2f;
	float defaultSize = 3f;
	float circleSizeModifier = 1f;

	float sizeChangeSpeed = 0.5f;

	// Update is called once per frame
	void FixedUpdate()
	{
		Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		Debug.Log(inputVector);

		// Move the circle, multiply by circleSizeModifier to spped up large circle
		transform.position += circleSizeModifier * movementSpeed * Time.deltaTime * inputVector;

		// Resizing circle
		transform.localScale += Input.GetAxisRaw("CircleSize") * sizeChangeSpeed * Time.deltaTime * Vector3.one;
		circleSizeModifier += Input.GetAxisRaw("CircleSize") * Time.deltaTime * sizeChangeSpeed;

		// Selecting characters
		if (Input.GetButtonDown("Select"))
		{
			// Clear selected mice
			GameManager.Instance.SelectedMice.ForEach(m => m.Deselect());
			GameManager.Instance.SelectedMice.Clear();
			
			// iterate over mice, select if they are close enough to the center of the circle
			foreach (Mouse mouse in GameManager.Instance.Mice)
			{
				if (Vector3.Distance(transform.position, mouse.transform.position) < defaultSize * circleSizeModifier)
				{
					mouse.Select();
					GameManager.Instance.SelectedMice.Add(mouse);
				}
			}
		}

		// Moving characters to position
		if (Input.GetButtonDown("Assign"))
		{
			if (NavMesh.SamplePosition(transform.position, out NavMeshHit nMHit, 200f, NavMesh.AllAreas))
			{
				GameManager.Instance.SelectedMice.ForEach(m => m.SetDestination(nMHit.position));
			}
		}
	}
}
