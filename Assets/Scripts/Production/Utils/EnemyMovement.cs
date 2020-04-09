using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	private LocatePath m_LocatePath;
	private int m_Current = 0;
	private GameObject m_EndObject;
	private Vector3 m_TargetPos;
	[SerializeField] [Range(1, 10)] private float m_moveSpeed = 5;


	private void Awake()
	{
		m_LocatePath = FindObjectOfType<LocatePath>();
		m_EndObject = m_LocatePath.MapReaderMono.CreateMap.EndObject;
	}

	private void Update()
	{
		FollowPath();
	}

	private void FollowPath()
	{
		m_TargetPos = m_LocatePath.GetWorldPos[m_Current];
		if (transform.position != m_TargetPos)
		{
			transform.position = Vector3.MoveTowards(transform.position, m_TargetPos, m_moveSpeed * Time.deltaTime);
			Vector3 direction = m_TargetPos - transform.position;
			Quaternion lookRotation = Quaternion.LookRotation(direction);
			Vector3 rotation = lookRotation.eulerAngles;
			transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
		}
		else
		{
			if (m_Current == m_LocatePath.GetWorldPos.Count - 1)
			{
				m_EndObject.GetComponent<PlayerBaseHealth>().Health--;
				Debug.Log(m_EndObject.GetComponent<PlayerBaseHealth>().Health);

				Destroy(gameObject);
			}
			m_Current++;
		}
	}
}
