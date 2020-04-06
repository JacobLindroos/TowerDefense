using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
	private int m_Health;

	public event Action<int> OnPlayerHealthChanged;

	public int Health
	{
		get => m_Health;
		set
		{
			if (m_Health != value)
			{
				m_Health = value;
				OnPlayerHealthChanged?.Invoke(m_Health);
			}
		}
	}

	[ContextMenu("Increase Health")]
	public void Increase()
	{
		Health += 1;
	}
}
