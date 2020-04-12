using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private Transform m_Target;
	[SerializeField] private float m_bulletSpeed = 70f;
	[SerializeField] private GameObject m_ImpactEffect;

	public void Seek(Transform target)
	{
		m_Target = target;
	}

	// Update is called once per frame
	void Update()
	{
		if (m_Target == null)
		{
			Destroy(gameObject);
			return;
		}

		Vector3 direction = m_Target.position - transform.position;
		float distanceThisFrame = m_bulletSpeed * Time.deltaTime;

		if (direction.magnitude <= distanceThisFrame)
		{
			HitTarget();
			return;
		}

		transform.Translate(direction.normalized * distanceThisFrame, Space.World);
	}

	private void HitTarget()
	{
		GameObject effectIns = (GameObject)Instantiate(m_ImpactEffect, transform.position, transform.rotation);
		Destroy(effectIns, 2f);
		m_Target.GetComponent<EnemyHealth>().Health--;
		Destroy(gameObject);

	}
}
