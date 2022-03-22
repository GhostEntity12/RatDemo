using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    Material mat;
    float timer;
    [SerializeField] Vector2 inactiveTimeRange;
    [SerializeField] float warningTime;
    bool isActive;
    Bounds bounds;
    [SerializeField] ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        bounds = GetComponent<Collider>().bounds;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
		if (timer <= 0)
        {
            if (isActive)
            {
                TriggerEffect();
                ps.Play();
                timer = Random.Range(inactiveTimeRange.x, inactiveTimeRange.y);
            }
			else
			{
                timer = warningTime;
			}
            isActive = !isActive;
            mat.SetFloat("_Active", isActive ? 1 : 0);
        }
    }

    void TriggerEffect()
	{
        Queue<Mouse> deadMice = new Queue<Mouse>();
		foreach (var mouse in GameManager.Instance.Mice)
		{
			if (bounds.Contains(mouse.transform.position))
			{
                deadMice.Enqueue(mouse);
			}
		}
		while (deadMice.Count > 0)
		{
            deadMice.Dequeue().Kill();
		}
	}
}
