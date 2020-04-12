using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	public static ObjectPooler SharedInstance;
	public List<GameObject> m_PooledObjects;
	[SerializeField] private GameObject m_ObjectToPool;
	[SerializeField] private int m_AmountToPool;

	private void Awake()
	{
		SharedInstance = this;
	}

	private void Start()
	{
		m_PooledObjects = new List<GameObject>();
		for (int i = 0; i < m_AmountToPool; i++)
		{
			GameObject obj = (GameObject)Instantiate(m_ObjectToPool);
			//setting to an inactive state
			obj.SetActive(false);
			m_PooledObjects.Add(obj);
		}
	}

	public GameObject GetPooledObject()
	{
		for (int i = 0; i < m_PooledObjects.Count; i++)
		{
			if (!m_PooledObjects[i].activeInHierarchy)
			{
				return m_PooledObjects[i];
			}
		}
		return null;
	}
}
