using AI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocatePath : MonoBehaviour
{
	private Dijkstra dijkstra;
	private MapReaderMono mapMono;
	Vector3 worldCoord;
	GameObject obj;
	List<Vector3> worldPOs;

[SerializeField] private GameObject prefabTest;

	private void Awake()
	{
		mapMono = GetComponent<MapReaderMono>();
	}

	private void Start()
	{
		GetPath();
	}


	public void GetPath()
	{
		dijkstra = new Dijkstra(mapMono.Test);

		List<Vector2Int> translateToVec3;
		translateToVec3 = dijkstra.FindPath(mapMono.StartPos, mapMono.EndPos).ToList();

		worldPOs = new List<Vector3>();
		foreach (var item in translateToVec3)
		{
			worldPOs.Add(new Vector3(item.x * 2, 0, item.y * 2));
		}

		foreach (var item in worldPOs)
		{
			GameObject.Instantiate(prefabTest, item, Quaternion.identity);
		}
	}


	/*
	 * 1.translate local position list to world position list
	 *		- transform.transformPoint, takes a vector3
	 *		- go from vector2int to vector 3 then back to vector2int when world coordinates are set
	 * 2.
	 * 
	 * 
	 */
}
