using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Role
{
    Mouse mouse;
    public Vector3 rolePos;
	[SerializeField]
    public bool Filled => mouse;
	public Mouse OccupyingMouse => mouse;
    public bool MouseInPosition => mouse && Vector3.Distance(mouse.transform.position, rolePos) < 0.5f;

    public void SetMouse(Mouse _mouse)
	{
        mouse = _mouse;
		mouse.occupied = true;
		if (NavMesh.SamplePosition(rolePos, out NavMeshHit nMHit, 200f, NavMesh.AllAreas))
		{
			mouse.NavAgent.SetDestination(nMHit.position);
		}
	}

	public void FreeMouse()
	{
		mouse.occupied = false;
		mouse = null;
	}
}
