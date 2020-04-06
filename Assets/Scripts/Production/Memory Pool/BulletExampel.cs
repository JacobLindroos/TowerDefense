using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletExampel : MonoBehaviour
{
	[SerializeField][Range(1,3)] float m_MinSpeed;
	[SerializeField] [Range(10, 15)] float m_MaxSpeed;
	[SerializeField] private Rigidbody m_Rigidbody;

	public void Push()
	{
		m_Rigidbody.AddForce(Vector3.up * Random.Range(m_MinSpeed, m_MaxSpeed));
		Invoke(nameof(Sleep), 2.25f);
	}

	public void Reset()
	{
		m_Rigidbody.velocity = Vector3.zero;
	}

	private void Sleep()
	{
		gameObject.SetActive(false);
	}

	private void OnDisable()
	{
		CancelInvoke(nameof(Sleep));
	}
}
