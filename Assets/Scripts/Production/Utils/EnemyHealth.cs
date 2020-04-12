using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : PoolObject
{
	private EnemyMovement enemyMovement;
	[SerializeField] [Range(1,15)] private int m_Health = 5;

	public int Health { get { return m_Health; } set { m_Health = value; } }

	private void OnEnable()
	{
		enemyMovement = GetComponent<EnemyMovement>();
	}

	// Update is called once per frame
	void Update()
    {
		if (Health <= 0)
		{
			Destroy();
		}
    }

	public override void OnObjectReuse()
	{
		enemyMovement.Current = 0;
	}
}
