using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towers : MonoBehaviour
{
	[Header("Attributes")]
	[SerializeField] [Range(1,15)] private float m_Range = 15f;
	[SerializeField] [Range(1, 5)] private float m_FireRate = 1f;
	[SerializeField] private float m_FireCountdown = 0f;

	[Header("Unity Setup Fields")]
	[SerializeField] private GameObject m_Target;
	[SerializeField] private Transform m_PartToRotate;
	[SerializeField][Range(5,15)] private float m_TurnSpeed = 10f;
	[SerializeField] private GameObject m_BulletPrefab;
	[SerializeField] private Transform m_FirePoint;
	
	private string enemyTag = "Enemy";

    // Start is called before the first frame update
    void Start()
    {
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

	private void UpdateTarget()
	{
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDistance = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies)
		{
			float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDistance)
			{
				shortestDistance = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}

		if (nearestEnemy != null && shortestDistance <= m_Range)
		{
			m_Target = nearestEnemy;
		}
		else
		{
			m_Target = null;
		}
	}

    // Update is called once per frame
    void Update()
    {
		if (m_Target == null)
		{
			return;
		}

		//Target lock on
		Vector3 direction = m_Target.transform.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		Vector3 rotation = Quaternion.Lerp(m_PartToRotate.rotation, lookRotation, Time.deltaTime * m_TurnSpeed).eulerAngles;
		m_PartToRotate.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

		if (m_FireCountdown <= 0f)
		{
			Shoot();
			m_FireCountdown = 1f / m_FireRate;
		}

		m_FireCountdown -= Time.deltaTime;
    }

	private void Shoot()
	{
		GameObject bulletGO = (GameObject)Instantiate(m_BulletPrefab, m_FirePoint.position, m_FirePoint.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet>();

		if (bullet != null)
		{
			bullet.Seek(m_Target.transform);
		}
	}

	//Draws when gameobject is selected in the scene
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, m_Range);
	}
}
