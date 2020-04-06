using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthListner : MonoBehaviour
{
	[SerializeField] private Text m_TextField;

	private Player m_Player;

	private void Awake() { }

	private void OnEnable()
	{
		if (m_Player != null)
		{
			m_Player.OnPlayerHealthChanged += UpdateTextField;
		}
	}

	void Start()
    {
		m_Player = FindObjectOfType<Player>();
		m_Player.OnPlayerHealthChanged += UpdateTextField;
    }

	private void OnDisable()
	{
		m_Player.OnPlayerHealthChanged -= UpdateTextField;
	}

	private void UpdateTextField(int playerHealth)
	{
		m_TextField.text = playerHealth.ToString();
	}
}
