using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private Transform target;
	[SerializeField] private float m_bulletSpeed = 70f;
	[SerializeField] private GameObject m_ImpactEffect;

	public void Seek(Transform _taget)
	{
		target = _taget;
	}

    // Update is called once per frame
    void Update()
    {
		if (target == null)
		{
			Destroy(gameObject);
			return;
		}

		Vector3 direction = target.position - transform.position;
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
		Destroy(target.gameObject);
		Destroy(gameObject);
	}
}
