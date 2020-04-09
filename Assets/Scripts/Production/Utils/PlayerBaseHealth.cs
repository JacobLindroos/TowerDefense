using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseHealth : MonoBehaviour
{
	[SerializeField] [Range(1, 25)] private int m_Health = 10;
	private int currentHealth;

	private void Start()
	{
		currentHealth = m_Health;
		Debug.Log("currentH: " + currentHealth);
	}

	public int Health { get { return currentHealth; } set { currentHealth = value; } }

	private void Update()
	{
		if (Health <= 0)
		{
			Destroy(gameObject);
			Time.timeScale = 0;
		}
	}
}
