using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	[SerializeField] [Range(1,15)] private int m_Health = 5;

	public int Health { get { return m_Health; } set { m_Health = value; } }

    // Update is called once per frame
    void Update()
    {
		if (Health <= 0)
		{
			Destroy(gameObject);
		}
    }
}
