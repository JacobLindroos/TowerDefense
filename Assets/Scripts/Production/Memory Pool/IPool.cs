using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public interface IPool<T>
    {
        T Rent(bool returnActive);
    }

	public class GameObjectPool : IPool<GameObject>, IDisposable
	{
		private bool m_IsDisposed;
		//when u need to create more gameObjects 
		private readonly uint m_ExpandBy;
		//Game Object to use in pool
		private readonly GameObject m_Prefab;
		private readonly Transform m_Parent;

		readonly Stack<GameObject> m_Objects = new Stack<GameObject>();
		readonly List<GameObject> m_Created = new List<GameObject>();

		public GameObjectPool(uint initSize, GameObject prefab, uint expandBy = 1, Transform parent = null)
		{
			m_ExpandBy = (uint)Mathf.Max(1, expandBy);
			m_Prefab = prefab;
			m_Parent = parent;
			//deactivate it
			m_Prefab.SetActive(false);
			Expand((uint)Mathf.Max(1, initSize));
		}

		private void Expand(uint amount)
		{
			for (int i = 0; i < amount; i++)
			{
				GameObject instance = GameObject.Instantiate(m_Prefab, m_Parent);
				EmitOnDisable emitOnDisable = instance.AddComponent<EmitOnDisable>();
				emitOnDisable.OnDisableGameObject += UnRent;
				m_Objects.Push(instance);
				m_Created.Add(instance);
			}
		}

		public void Clear()
		{
			foreach (GameObject gameObject in m_Created)
			{
				gameObject.GetComponent<EmitOnDisable>().OnDisableGameObject -= UnRent;
				UnityEngine.Object.Destroy(gameObject);
			}
		}

		private void UnRent(GameObject gameObject)
		{
			if (m_IsDisposed == false)
			{
				m_Objects.Push(gameObject);
			}
		}

		public GameObject Rent(bool returnActive)
		{
			if (m_Objects.Count == 0)
			{
				Expand(m_ExpandBy);
			}
			GameObject instance = m_Objects.Pop();
			instance.SetActive(returnActive);
			return instance;
		}

		public void Dispose()
		{
			m_IsDisposed = true;
			Clear();
			m_Objects.Clear();
			m_Created.Clear();
		}
	}
}