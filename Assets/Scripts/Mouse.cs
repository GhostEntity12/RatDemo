using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Mouse : MonoBehaviour
{
	[field: SerializeField]
	public NavMeshAgent NavAgent { get; private set; }
	float patience;
	[SerializeField] float wanderRadius = 5;
	float stoppingDistance = 1f;
	public bool occupied;
	[field: SerializeField]
	public bool Wandering { get; private set; }

	private void Start()
	{
		NavAgent = GetComponent<NavMeshAgent>();
		SetWander();
	}

	private void Update()
	{
		if (!occupied)
		{
			if (Vector3.Distance(transform.position, NavAgent.destination) < stoppingDistance)
			{
				//NavAgent.ResetPath();
				patience -= Time.deltaTime;
				if (patience <= 0)
				{
					SetWander();
				}
			}
		}
	}

	public void SetDestination(Vector3 position)
	{
		NavAgent.SetDestination(position);
		patience = 2f;
		NavAgent.speed = 10f;
		Wandering = false;
	}
	public void SetWander()
	{
		Wandering = true;
		NavAgent.speed = 1.5f;
		Vector2 rand = Random.insideUnitCircle * wanderRadius;
		Vector3 offsetted = new Vector3(transform.position.x + rand.x, transform.position.y, transform.position.z + rand.y);
		Debug.DrawLine(transform.position, NavAgent.destination, Color.red);
		if (NavMesh.SamplePosition(offsetted, out NavMeshHit nMHit, 200f, NavMesh.AllAreas))
		{
			NavAgent.SetDestination(nMHit.position);
		}
	}

	public void Select()
	{
		GetComponent<Renderer>().material.color = Color.red;
	}

	public void Deselect()
	{
		GetComponent<Renderer>().material.color = Color.gray;

	}

	public void Kill()
	{
		GameManager.Instance.Mice.Remove(this);
		GameManager.Instance.SelectedMice.Remove(this);
		Destroy(gameObject);
	}
}
