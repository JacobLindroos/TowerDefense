using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
	LocatePath locatePath;
	[SerializeField] private GameObject enemyPrefab;
	[SerializeField][Range(0,10)] private float m_timeBetweenWaves = 5f;
	[SerializeField][Range(0, 5)] private float m_Countdown = 2f;
	[SerializeField] private  int m_waveIndex = 0;
	

	private void Start()
	{
		locatePath = GetComponent<LocatePath>();
	}

	private void Update()
	{
		if (m_Countdown <= 0f)
		{
			StartCoroutine(SpawnWave());
			m_Countdown = m_timeBetweenWaves;

		}

		m_Countdown -= Time.deltaTime;
	}

	IEnumerator SpawnWave()
	{
		m_waveIndex++;
		for (int i = 0; i < m_waveIndex; i++)
		{
			SpawnEnemy();
			yield return new WaitForSeconds(0.5f);
		}
	}

	private void SpawnEnemy()
	{
		Instantiate(enemyPrefab, locatePath.GetWorldPos[0], Quaternion.identity);
	}
}
