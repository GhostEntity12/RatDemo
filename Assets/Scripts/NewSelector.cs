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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Debug.Log(inputVector);

        transform.position += movementSpeed * Time.deltaTime * inputVector * circleSizeModifier;

        transform.localScale += Input.GetAxisRaw("CircleSize") * Vector3.one * Time.deltaTime * sizeChangeSpeed;
        circleSizeModifier += Input.GetAxisRaw("CircleSize") * Time.deltaTime * sizeChangeSpeed;

        if (Input.GetButtonDown("Select"))
		{
            GameManager.Instance.SelectedMice.ForEach(m => m.Deselect());
            GameManager.Instance.SelectedMice.Clear();
			foreach (Mouse mouse in GameManager.Instance.Mice)
			{
				if (Vector3.Distance(transform.position, mouse.transform.position) < defaultSize * circleSizeModifier)
				{
                    mouse.Select();
                    GameManager.Instance.SelectedMice.Add(mouse);
				}
			}
		}

		if (Input.GetButtonDown("Assign"))
		{
            if (NavMesh.SamplePosition(transform.position, out NavMeshHit nMHit, 200f, NavMesh.AllAreas))
            {
                GameManager.Instance.SelectedMice.ForEach(m => m.SetDestination(nMHit.position));
            }
        }
    }
}
