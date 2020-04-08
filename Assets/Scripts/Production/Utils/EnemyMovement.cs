using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	LocatePath locatePath;
	EnemySpawner enemySpawner;
	private int current = 0;
	private Vector3 targetPos;
	[SerializeField] [Range(1, 10)] private float m_moveSpeed = 5;


	private void Start()
	{
		locatePath = FindObjectOfType<LocatePath>();
	}

	private void Update()
	{
		FollowPath();
	}

	private void FollowPath()
	{
		targetPos = locatePath.GetWorldPos[current];
		if (transform.position != targetPos)
		{
			transform.position = Vector3.MoveTowards(transform.position, targetPos, m_moveSpeed * Time.deltaTime);
		}
		else
		{
			if (locatePath.GetWorldPos[current] == locatePath.GetWorldPos[locatePath.GetWorldPos.Count- 1])
			{
				Destroy(this);
			}
			current++;
		}
	}
}
