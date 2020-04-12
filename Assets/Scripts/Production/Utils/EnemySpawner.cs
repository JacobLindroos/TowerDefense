using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
	private LocatePath m_LocatePath;
	[SerializeField] private GameObject m_EnemyPrefab;
	[SerializeField][Range(0,10)] private float m_TimeBetweenWaves = 5f;
	[SerializeField][Range(0, 5)] private float m_Countdown = 2f;
	[SerializeField] private  int m_WaveIndex = 0;

	private void Start()
	{
		m_LocatePath = GetComponent<LocatePath>();
		PoolManager.Instance.CreatePool(m_EnemyPrefab, 35);
	}

	private void Update()
	{
		if (m_Countdown <= 0f)
		{
			StartCoroutine(SpawnWave());
			m_Countdown = m_TimeBetweenWaves;

		}

		m_Countdown -= Time.deltaTime;
	}

	IEnumerator SpawnWave()
	{
		m_WaveIndex++;
		for (int i = 0; i < m_WaveIndex; i++)
		{
			SpawnEnemy();
			yield return new WaitForSeconds(0.5f);
		}
	}

	private void SpawnEnemy()
	{
		PoolManager.Instance.ReuseObject(m_EnemyPrefab, m_LocatePath.GetWorldPos[0], Quaternion.identity);
		//Instantiate(m_EnemyPrefab, m_LocatePath.GetWorldPos[0], Quaternion.identity);
	}
}
