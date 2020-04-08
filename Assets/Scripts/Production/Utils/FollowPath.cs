using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
//	private List<Vector3> m_targetPos;
//	private int current;
//	[SerializeField] private float m_moveSpeed;
//	private LocatePath locatePath;

//	private void Start()
//	{
//		locatePath = GetComponent<LocatePath>();

//		GetWorldPath();
//	}

//	private void GetWorldPath()
//	{
//		foreach (var item in locatePath.GetPath())
//		{
//			m_targetPos.Add(item);
//		}
//	}

//	private void Update()
//	{
//		if (transform.position != new Vector3(m_targetPos[current].x, m_targetPos[current].y, m_targetPos[current].z))
//		{
//			Vector3 pos = Vector3.MoveTowards(transform.position, new Vector3(m_targetPos[current].x, m_targetPos[current].y, m_targetPos[current].z), m_moveSpeed * Time.deltaTime);
//			GetComponent<Rigidbody>().MovePosition(pos);
//		}
//		else
//		{
//			current = (current + 1) % m_targetPos.Count;
//		}
//	}


}
