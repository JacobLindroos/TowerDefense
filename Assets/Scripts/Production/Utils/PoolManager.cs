using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
	Dictionary<int, Queue<ObjectInstance>> m_PoolDictionary = new Dictionary<int, Queue<ObjectInstance>>();

	static PoolManager m_Instance;

	public static PoolManager Instance
	{
		get
		{
			if (m_Instance == null)
			{
				m_Instance = FindObjectOfType<PoolManager>();
			}
			return m_Instance;
		}
	}

	public void	CreatePool(GameObject prefab, int poolSize)
	{
		int poolKey = prefab.GetInstanceID();
		GameObject poolHolder = new GameObject(prefab.name + "pool");
		poolHolder.transform.parent = transform;

		if (!m_PoolDictionary.ContainsKey(poolKey))
		{
			m_PoolDictionary.Add(poolKey, new Queue<ObjectInstance>());
		}

		for (int i = 0; i < poolSize; i++)
		{
			ObjectInstance newObject = new ObjectInstance(Instantiate(prefab) as GameObject);
			m_PoolDictionary[poolKey].Enqueue(newObject);
			newObject.SetParent(poolHolder.transform);
		}
	}

	public void ReuseObject(GameObject prefab, Vector3 position, Quaternion rotation)
	{
		int poolKey = prefab.GetInstanceID();
		if (m_PoolDictionary.ContainsKey(poolKey))
		{
			ObjectInstance objectToReuse = m_PoolDictionary[poolKey].Dequeue();
			m_PoolDictionary[poolKey].Enqueue(objectToReuse);

			objectToReuse.Reuse(position, rotation);
		}
	}

	public class ObjectInstance
	{
		private GameObject gameObject;
		private Transform transform;

		private bool hasPoolObjectComponent;
		private PoolObject poolObjectScript;

		public ObjectInstance(GameObject objectInstance)
		{
			gameObject = objectInstance;
			transform = gameObject.transform;
			gameObject.SetActive(false);
			if (gameObject.GetComponent<PoolObject>())
			{
				hasPoolObjectComponent = true;
				poolObjectScript = gameObject.GetComponent<PoolObject>();
			}
		}

		public void Reuse(Vector3 position, Quaternion rotation)
		{
			if (hasPoolObjectComponent)
			{
				poolObjectScript.OnObjectReuse();
			}
			gameObject.SetActive(true);
			transform.position = position;
			transform.rotation = rotation;
		}

		public void SetParent(Transform parent)
		{
			transform.parent = parent;
		}
	}
}
