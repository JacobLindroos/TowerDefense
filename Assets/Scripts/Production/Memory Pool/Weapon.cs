using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[SerializeField] private GameObject m_BulletPrefab;
	private GameObjectPool m_BulletPool;

	private void Awake()
	{
		m_BulletPool = new GameObjectPool(1, m_BulletPrefab, 1, new GameObject("Bullet Parent").transform);
	}

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			GameObject bullet = m_BulletPool.Rent(false);
			BulletExampel bulletComponent = bullet.GetComponent<BulletExampel>();
			bullet.transform.position = transform.position;
			bulletComponent.Reset();
			bullet.SetActive(true);
			bulletComponent.Push();
		}
    }
}
